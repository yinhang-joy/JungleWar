using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

public class StartGameRequest:BaseRequest
{
    private RoomPanel roomPanel;
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.StartGame;
        roomPanel = GetComponent<RoomPanel>();
        base.Awake();
    }
    public void SendRequest()
    {
        base.SendRequest("");
    }
    public override void OnResponse(string data)
    {
        ReturnCode returncode = (ReturnCode)int.Parse(data);
        roomPanel.OnStartResponse(returncode);
        base.OnResponse(data);
    }
}

