using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

public class UpdateRoomRequest :BaseRequest
{
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.UpdateRoom;
        roomPanel = GetComponent<RoomPanel>();
        base.Awake();
    }
    public override void OnResponse(string data)
    {
        UserData ud1 = null;
        UserData ud2 = null;

        string[] udstr = data.Split(new string[] { "|" }, StringSplitOptions.RemoveEmptyEntries);
        if (udstr.Length>1)
        {
            ud1 = new UserData(udstr[0]);
            ud2 = new UserData(udstr[1]);
        }
        else
        {
            ud1 = new UserData(udstr[0]);
        }
       
        base.OnResponse(data);
        roomPanel.SetBlueUserSync(ud1);
        roomPanel.SetRedUserSync(ud2);
    }
}
