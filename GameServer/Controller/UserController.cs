using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;
using GameServer.DAO;
using GameServer.Model;

namespace GameServer.Controller
{
    class UserController:BaseController
    {
        private UserDAO userDAO = new UserDAO();
        private ResultDAO resultDAO = new ResultDAO();
        public UserController()
        {
            requestCode = RequestCode.User;
        }
        public string Login(string data, Client client, Server server)
        {
            string[] strs = data.Split(',');
            User user = userDAO.VerifyUser( client.MySQLConn,strs[0],strs[1]);
            if (user==null)
            {
                //Console.WriteLine("登陆失败");
                //Enum.GetName(typeof(ReturnCode),ReturnCode.Fail);
                 return ((int)ReturnCode.Fail).ToString();
            }
            else
            {
                Result result = resultDAO.GetResultByUserID(client.MySQLConn,user.ID);
                string str = string.Format("{0},{1},{2},{3}", ((int)ReturnCode.Success).ToString(), user.Username, result.TotalCount, result.WinCount);
                client.User = user;
                client.Result = result;
                return str;
            }
        }
        public string Regist(string data,Client client, Server server)
        {
            bool flag = false;
            string[] strs = data.Split(',');
            if (!userDAO.GetUserByUserName(client.MySQLConn,strs[0]))
            {
                flag = userDAO.Register(client.MySQLConn, strs[0], strs[1]);
            }
           
            if (flag)
            {
                User user = userDAO.VerifyUser(client.MySQLConn, strs[0], strs[1]);
                Result result = resultDAO.GetResultByUserID(client.MySQLConn, user.ID);
                string str = string.Format("{0},{1},{2},{3}", ((int)ReturnCode.Success).ToString(), user.Username, result.TotalCount, result.WinCount);
                return str;
            }
            else
            {
                return ((int)ReturnCode.Fail).ToString();
            }
        }
    }
}
