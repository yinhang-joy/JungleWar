using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;

namespace JungleWarServer
{
    class Program
    {
        static void Main(string[] args)
        {
            StartServerAsync();
            //StartServerSync();
            Console.ReadKey();
        }
        static void StartServerAsync()
        {
            //1.创建服务端套接字
            Socket serverSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            //2.创建服务端ip地址与端口号
            IPAddress ipAddress = IPAddress.Parse("127.0.0.1");
            IPEndPoint ipEndPoint = new IPEndPoint(ipAddress, 666);
            //3.服务端与ip地址端口号绑定
            serverSocket.Bind(ipEndPoint);
            serverSocket.Listen(50);//开始监听端口号，处理链接的队列
            serverSocket.BeginAccept(StartClientServerAsync, serverSocket);
        }
        static Message _message =new Message();

        private static void StartClientServerAsync(IAsyncResult ar)
        { 
            Socket serverSocket = ar.AsyncState as Socket;
            Socket clientSocket = serverSocket.EndAccept(ar);
            //向客户端发送消息
            string str = "Hello ！客户端。。。";
            byte[] data = System.Text.Encoding.UTF8.GetBytes(str);
            clientSocket.Send(data);
            //异步接收客户端发送的消息，不阻塞主线程
            //开始监听客户端数据的传递，当接收到客户端数据时才调用传入的回调函数
            clientSocket.BeginReceive(_message.Data, _message.StartIndex, _message.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);
            serverSocket.BeginAccept(StartClientServerAsync, serverSocket);
        }

        static byte[] dataBuffer = new byte[1024];

        private static void ReceiveCallBack(IAsyncResult ar)
        {
            Socket clientSocket =null;
            try
            {
                clientSocket = ar.AsyncState as Socket;
                int count = clientSocket.EndReceive(ar);//一次接受了多少字节
                if (count==0)
                {
                    //clientSocket.Close();
                    return;
                }
                _message.AddCount(count);//修改存储数据的大小
                //string strReceive = Encoding.UTF8.GetString(dataBuffer, 0, count);
                //Console.WriteLine(strReceive);
                //clientSocket.BeginReceive(dataBuffer, 0, 1024, SocketFlags.None, ReceiveCallBack, clientSocket);
                _message.ReadMessage();//解析数据
                clientSocket.BeginReceive(_message.Data, _message.StartIndex, _message.RemainSize, SocketFlags.None, ReceiveCallBack, clientSocket);
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                if (clientSocket!=null)
                {
                    clientSocket.Close();
                }
            }
        }

    }
}
