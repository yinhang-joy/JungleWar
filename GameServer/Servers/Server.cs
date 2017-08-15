using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using GameServer.Controller;
using Common;

namespace GameServer.Servers
{
    public class Server
    {
        private IPEndPoint ipEndPoint;
        private Socket ServerSocket;
        List<Client> ClientList = new List<Client>(); //客户端链接集合
        public List<Room> RoomList = new List<Room>();
        private ControllerManager controllerManager;
        public Server() { }
        public Server(string ip,int port)
        {
            controllerManager = new ControllerManager(this);
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }
        public void SetIPAndPort(string ip,int port)
        {
            ipEndPoint = new IPEndPoint(IPAddress.Parse(ip), port);
        }
        public void Start()
        {
            ServerSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            ServerSocket.Bind(ipEndPoint);
            ServerSocket.Listen(0);
            ServerSocket.BeginAccept(StartClientServerAsync, ServerSocket);
        }

        private void StartClientServerAsync(IAsyncResult ar)
        {
            Socket clientServer = ServerSocket.EndAccept(ar);
            Client client = new Client(clientServer,this);//创建客户端的链接
            client.Start();
            Console.WriteLine("客户端已连接");
            ServerSocket.BeginAccept(StartClientServerAsync, ServerSocket);
            ClientList.Add(client);
        }
        public void RemoveClient(Client client)
        {
            lock (ClientList)
            {
                ClientList.Remove(client);
            }
        }
        /// <summary>
        /// 给客户端发起响应
        /// </summary>
        public void SendResponse(Client client,ActionCode actionCode,string data)
        {
            Console.WriteLine("给客户端响应"+actionCode+","+data);
            //TODO给客户端响应
            client.Send(actionCode, data);
        }
        /// <summary>
        /// 解析客户端消息后，调用请求内容
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="actionCode"></param>
        /// <param name="data"></param>
        /// <param name="clien"></param>
        public void HandleRequest(RequestCode requestCode, ActionCode actionCode, string data,Client client)
        {
            controllerManager.HandleRequest(requestCode,actionCode,data,client);
        }
        public void CreateRoom(Client client)
        {
            Room room = new Room(this);
            room.AddClient(client);
            RoomList.Add(room);
        }
        public void RemoveRoom(Room room)
        {
            if (RoomList!=null &&room!=null)
            {
                RoomList.Remove(room);
                Console.WriteLine("移除房间");
            }

        }
        public Room GetRoomByID(int id)
        {
            foreach (var item in RoomList)
            {
                if (item.GetRoomID() == id)
                {
                    return item;
                }
            }
            return null;
        }
    }
}
