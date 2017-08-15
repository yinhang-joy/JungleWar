using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace GameServer.Tool
{
    class ConnHelper
    {
        public const string CONNSTRING = "datasource=127.0.0.1;port=3306;database=junglewargame;user=root;pwd=123456";
        public static MySqlConnection Connect()
        {
            MySqlConnection mySqlConnection = new MySqlConnection(CONNSTRING);

            try
            {
                mySqlConnection.Open();
            }
            catch (Exception e)
            {

                Console.WriteLine(e.Message);
            }
          
            return mySqlConnection;
        }
        public static void CloseConnect(MySqlConnection mySqlConnection)
        {

            if (mySqlConnection!=null)
            {
                mySqlConnection.Close();
            }
            else
            {
                Console.WriteLine("mySqlConnection不能为空");
            }
        }
    }
}
