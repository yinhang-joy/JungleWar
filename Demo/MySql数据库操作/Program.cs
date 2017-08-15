using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MySql.Data.MySqlClient;

namespace MySql数据库操作
{
    class Program
    {
        static void Main(string[] args)
        {
            string str = "Database =mydatabase;Data Source = 127.0.0.1;port =3306;User Id=root;Password =123456;";
            MySqlConnection mysqlconn = new MySqlConnection(str);

            mysqlconn.Open();

            #region //查询
            //MySqlCommand cmd = new MySqlCommand("select * from user where id = 1", mysqlconn);

            //MySqlDataReader reader = cmd.ExecuteReader();

            //while (reader.Read())
            //{
            //    string username = reader.GetString("username");
            //    string passwork = reader.GetString("password");
            //    Console.WriteLine(username + ":" + passwork);
            //}
            //reader.Close();

            #endregion
            #region 插入
            string username = "小明同学"; string password = "123456";
            MySqlCommand cmd = new MySqlCommand("insert into user set username =@un, password =@pa;", mysqlconn);//防止sql语句注入；
            cmd.Parameters.AddWithValue("un", username);
            cmd.Parameters.AddWithValue("pa",password);

            cmd.ExecuteNonQuery();

            #endregion
            mysqlconn.Close();
            Console.ReadKey();
        }
    }
}
