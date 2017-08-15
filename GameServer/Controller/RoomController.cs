using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common;
using GameServer.Servers;

namespace GameServer.Controller
{
    public class RoomController :BaseController
    {

        public RoomController()
        {
            requestCode = RequestCode.Room;
        }
        public string CreateRoom(string data, Client client, Server server)
        {
            server.CreateRoom(client);
            return ((int)ReturnCode.Success).ToString()+","+((int)RoleType.Blue).ToString();
        }
        public string ListRoom(string data, Client client, Server server)
        {
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < server.RoomList.Count; i++)
            {
                if (server.RoomList[i].IsWaitingJoin())
                {
                    sb.Append(server.RoomList[i].RoomData()+"|");
                }
            }
            if (sb.Length==0)
            {
            }
            else
            {
                sb.Remove(sb.Length - 1, 1);
            }
            return sb.ToString() ;
        }
        public string JoinRoom(string data, Client client, Server server)
        {
            int id = int.Parse(data);
            Room room = server.GetRoomByID(id);
            if (room==null)
            {
                return ((int)ReturnCode.NotFound).ToString();
            }
            else if (room.IsWaitingJoin()==false)
            {
                return ((int)ReturnCode.Fail).ToString();
            }
            else 
            {
                room.AddClient(client);
                string roomdata = room.GetRoomData();
                room.BroadcastMessage(client, ActionCode.UpdateRoom, roomdata);
                return ((int)ReturnCode.Success).ToString() +"," + ((int)RoleType.Red).ToString()+"-" + roomdata;
            }
        }
        public string QuitRoom(string data, Client client, Server server)
        {
            bool isroomowner = client.IsRoomOwner();
            Room room = client.room;
            if (isroomowner)
            {
                room.BroadcastMessage(client,ActionCode.QuitRoom, ((int)ReturnCode.Success).ToString());
                room.CloseRoom();
                return ((int)ReturnCode.Success).ToString();
            }
            else
            {
                client.room.RemoveClient(client);
                room.BroadcastMessage(client, ActionCode.UpdateRoom, room.GetRoomData());
                return ((int)ReturnCode.Success).ToString();
            }
        }
    }
}
