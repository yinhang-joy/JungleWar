using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JungleWarClient
{
    class Message
    {
        public static byte [] GetBytes(string str)
        {
            byte[] byteStr = Encoding.UTF8.GetBytes(str);
            int Lenght = byteStr.Length;
            byte[] LenghtByte = BitConverter.GetBytes(Lenght);
            return LenghtByte.Concat(byteStr).ToArray();
        }
    }
}
