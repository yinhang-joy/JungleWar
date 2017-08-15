using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using Common;
using MySql.Data.MySqlClient;
using GameServer.Tool;
using GameServer.Model;

namespace GameServer.Servers
{
    public class Client
    {
        private User user;
        private Result result;
        Socket ClientSocket;
        private Server server;
        Message _message = new Message();
        private MySqlConnection mySqlConnection;
        public Room room;
        public MySqlConnection MySQLConn
        {
            get { return mySqlConnection; }
        }

        internal User User
        {
            get{return user; }
            set{ user = value; }
        }

        internal Result Result
        {
            get {  return result;}
            set{  result = value; }
        }

        public Client() { }
        public Client(Socket socket,Server server)
        {
            ClientSocket = socket;
            this.server = server;
            mySqlConnection = ConnHelper.Connect();
        }
        public void Start()//开始监听接受消息
        {
            if (ClientSocket == null || ClientSocket.Connected == false)
            {
                Close();
                return;
            }
            ClientSocket.BeginReceive(_message.Data, _message.StartIndex, _message.RemainSize, SocketFlags.None, ReveiveCallBack, ClientSocket);
        }
        private void OnProcessMessage(RequestCode resqusetCode, ActionCode actionCode, string data)
        {
            server.HandleRequest(resqusetCode, actionCode,data,this);
        }
        private void ReveiveCallBack(IAsyncResult ar)
        {
            try
            {
                if (ClientSocket == null || ClientSocket.Connected == false) {Close(); return; }
                Console.WriteLine("监听接受消息");
                //if (ClientSocket == null || ClientSocket.Connected == false) return;
                int count = ClientSocket.EndReceive(ar);//一次接受了多少字节
                Console.WriteLine(count);
                if (count == 0)
                {
                    Close();
                }
                _message.ReadMessage(count,OnProcessMessage);//解析数据
                Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Close();
            }
        }
        private void Close()
        {
            if (ClientSocket != null)
            {
                ClientSocket.Close();
                if (room != null)
                {
                    room.ClientQuitRoom(this);
                }
                server.RemoveClient(this);
                ConnHelper.CloseConnect(mySqlConnection);
                Console.WriteLine("客户端关闭");
            }
        }
        public void Send(ActionCode actionCode,string data)
        {
            byte[] bytes = Message.PackData(actionCode, data);
            ClientSocket.Send(bytes);
        }
        public string GetUserData()
        {
            return User.ID + "," + User.Username + "," + Result.TotalCount + "," + Result.WinCount;
        }
        public bool IsRoomOwner()
        {
            return room.IsRoomOwner(this);
        }
    }
}
