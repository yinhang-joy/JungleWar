using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using UnityEngine;

public class ListRoomRequest : BaseRequest
{
    private RoomListPanel roomListPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.ListRoom;
        roomListPanel = GetComponent<RoomListPanel>();
        base.Awake();
    }
    public void SendRequest()
    {
        base.SendRequest("");
    }
    public override void OnResponse(string data)
    {
        string[] roomstrs = data.Split(new string[] {"|"},StringSplitOptions.RemoveEmptyEntries);
        List<UserData> userdataList= new List<UserData>(); ;
        for (int i = 0; i < roomstrs.Length; i++)
        {
            string[] strs = roomstrs[i].Split(',');
            userdataList.Add(new UserData(int.Parse(strs[0]),strs[1], int.Parse(strs[2]), int.Parse(strs[3])));
        }
        roomListPanel.userDataList = userdataList;
        base.OnResponse(data);
    }
}

