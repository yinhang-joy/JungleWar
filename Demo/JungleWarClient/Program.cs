using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Sockets;
using System.Net;
using System.Collections;

namespace JungleWarClient
{
    class Program
    {
        static void Main(string[] args)
        {
            Socket clientSocket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            clientSocket.Connect(new IPEndPoint(IPAddress.Parse("127.0.0.1"), 666));

            //接收从服务器发送过来的消息
            byte[] data = new byte[1024];
            int count = clientSocket.Receive(data);
            string strReceive = Encoding.UTF8.GetString(data, 0, count);
            Console.WriteLine(strReceive);
            //向服务器发送消息 
            //while (true)
            //{
            //    string strSend = Console.ReadLine();
            //    if (strSend=="c")
            //    {
            //        clientSocket.Close();
            //        return;
            //    }
            //    byte[] dataSend = Encoding.UTF8.GetBytes(strSend);
            //    clientSocket.Send(dataSend);
            //}
            for (int i = 0; i < 100; i++)
            {
                clientSocket.Send(Message.GetBytes(i.ToString()));
            }
            Console.ReadKey();
        }

    }
}

