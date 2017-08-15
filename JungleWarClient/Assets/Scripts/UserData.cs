using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

   public class UserData
    {
        public int ID { get; private set; }
        public string UserName { get;private set; }
        public int TotalCount { get; private set; }
        public int WinCount { get; private set; }
        public UserData(string username,int totalcount,int wincount)
        {
            this.UserName = username;
            this.TotalCount = totalcount;
            this.WinCount = wincount;
        }
        public UserData(int id,string username, int totalcount, int wincount)
        {
            this.ID = id;
            this.UserName = username;
            this.TotalCount = totalcount;
            this.WinCount = wincount;
        }
        public UserData(string user)
    {
        string[] strs = user.Split(',');
        this.ID = int.Parse(strs[0]);
        this.UserName = strs[1];
        this.TotalCount = int.Parse(strs[2]);
        this.WinCount = int.Parse(strs[3]);
    }
}
