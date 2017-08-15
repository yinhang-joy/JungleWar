using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;

namespace GameServer.Servers
{
    class Message
    {
        private byte[] data = new byte[1024];
        private int startIndex = 0;//存取了多少个字节的数据在数组里

        public byte[] Data
        {
            get { return data; }
        }

        public int StartIndex
        {
            get { return startIndex; }
        }
        public int RemainSize
        {
            get { return data.Length - startIndex; }
        }
        /// <summary>
        /// 解析数据
        /// </summary>
        public void ReadMessage(int newDataAmount,Action<RequestCode,ActionCode,string> processDataCallBack)
        {
            startIndex += newDataAmount;
            while (true)
            {
                if (startIndex <= 4)//当存储的数据小于四时说明里面的数据不足，等待下一次补充存储
                {
                    return;
                }
                int count = BitConverter.ToInt32(data, 0);//将byte数组中的指定位置的四个字节转为int 转出的数字表示收到的消息字节长度
                if ((startIndex - 4) >= count)//字节数组中剩余字节大于一次消息传送的字节。也就是字节数组中的数据必须要大于要解析的数据长度
                {
                    //string s = Encoding.UTF8.GetString(data, 4, count);//从第四位字节以后开始解析，前四位代表数据长度
                    //Console.WriteLine("解析出一条数据" + s);
                    RequestCode requestCode =(RequestCode)BitConverter.ToInt32(data, 4);
                    Console.WriteLine(requestCode.ToString());
                    ActionCode actionCode = (ActionCode)BitConverter.ToInt32(data, 8);
                    Console.WriteLine(actionCode.ToString());
                    string s = Encoding.UTF8.GetString(data, 12, count-8);
                    Console.WriteLine(s);
                    processDataCallBack(requestCode, actionCode, s);//Message只负责解析消息，解析后的消息发送给服务端处理，由于Message中么有Server的引用，并且为了降低耦合度，发送消息交给客户端链接去做，所以这里回调调用方法。
                    Array.Copy(data, count + 4, data, 0, startIndex - 4 - count);//将已经解析过的位置之后的数据移动到首位，移动的数据长度是减去已经解析过的数据。相当于覆盖已经解析过的数据，直到当前读取客户端消息的字节少于等于4也就是字节数组中没有有效数据了。
                    startIndex -= (count + 4);
                }
                else
                {
                    break;
                }
            }
        }
        /// <summary>
        /// 拼接数据
        /// </summary>
        /// <param name="requestCode"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static byte[] PackData(ActionCode actionCode,string data)
        {
            byte[] requestCodeBytes = BitConverter.GetBytes((int)actionCode);
            byte[] dataBytes = Encoding.UTF8.GetBytes(data);
            int dataAmount = requestCodeBytes.Length + dataBytes.Length;
            byte[] dataAmountBytes = BitConverter.GetBytes(dataAmount);
            return dataAmountBytes.Concat(requestCodeBytes).ToArray<byte>().Concat(dataBytes).ToArray<byte>();
        }

    }
}
