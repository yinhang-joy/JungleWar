using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

public class CreateRoomRequest :BaseRequest
{
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Room;
        actionCode = ActionCode.CreateRoom;
        base.Awake();
    }
    public void SetPanel(BasePanel panel)
    {
        roomPanel = panel as RoomPanel;
    }
    public void SendRequest()
    {
        base.SendRequest("");
    }
    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        base.OnResponse(data);
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        if (returnCode== ReturnCode.Success)
        {
            RoleType roletype = (RoleType)int.Parse(strs[1]);
            gameFacade.SetRoleType(roletype);
            UserData user = gameFacade.GetUserData();
            roomPanel.SetBlueUserSync(user);
        }
    }
}
