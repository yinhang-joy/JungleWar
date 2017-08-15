using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameServer.Model
{
    class Result
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int TotalCount { get; set; }
        public int WinCount { get; set; }
        public Result(int id, int userid , int totalCount,int winCount)
        {
            this.ID = id;
            this.UserID = userid;
            this.TotalCount = totalCount;
            this.WinCount = winCount;
        }
    }
}
