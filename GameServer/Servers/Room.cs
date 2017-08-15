using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using System.Threading;

namespace GameServer.Servers
{
    enum RoomState
    {
        WaitingJoin,//等待加入
        WaitingBattle,//等待开始
        Battle,
        end
    }
    public class Room
    {
        private Server server;
        public List<Client> ClienRoom = new List<Client>();
        private RoomState roomstate = RoomState.WaitingJoin;
        public void AddClient(Client client)
        {
            ClienRoom.Add(client);
            client.room = this;
            if (ClienRoom.Count>=2)
            {
                roomstate = RoomState.WaitingBattle;
            }
        }
        public bool IsWaitingJoin()
        {
            return roomstate == RoomState.WaitingJoin;
        }
        public string  RoomData()
        {
            return ClienRoom[0].GetUserData();
        }
        public void ClientQuitRoom(Client client)
        {
            if (client == ClienRoom[0])
            {
                server.RemoveRoom(ClienRoom[0].room);
            }
            else
            {

                ClienRoom.Remove(client);
            }
        }
        public Room(Server server)
        {
            this.server = server;
        }
        public int GetRoomID()
        {
            if (ClienRoom.Count>0)
            {
                return ClienRoom[0].User.ID;
            }
            return -1;
        }
        public string GetRoomData()
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < ClienRoom.Count; i++)
            {
                sb.Append(ClienRoom[i].GetUserData()+"|");
            }
            if (sb.Length>0)
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString();
        }
        public void BroadcastMessage(Client client,ActionCode actioncode,string data)
        {
            foreach (var item in ClienRoom)
            {
                if (item!=client)
                {
                    server.SendResponse(item,actioncode,data);
                }
            }
        }
        public bool IsRoomOwner(Client client)
        {
            if (client==ClienRoom[0])
            {
                return true;
            }
            return false;
        }
        public void RemoveClient(Client client)
        {
            client.room = null;
            ClienRoom.Remove(client);
            if (ClienRoom.Count >= 2)
            {
                roomstate = RoomState.WaitingBattle;
            }
            else
            {
                roomstate = RoomState.WaitingJoin;
            }
        }
        public void CloseRoom()
        {
            for (int i = 0; i < ClienRoom.Count; i++)
            {
                ClienRoom[i].room = null;
            }
            server.RemoveRoom(this);
        }
        public void StartTimer()
        {
            new Thread(RunTimer).Start();
        }
        private void RunTimer()
        {
            Thread.Sleep(1000);
            for (int i = 3; i >0; i--)
            {
                BroadcastMessage(null,ActionCode.ShowTimer,i.ToString());
                Thread.Sleep(1000);
            }
            //BroadcastMessage(null,ActionCode.StartPlay,"");
        }
    }
}
