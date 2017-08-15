using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class RegisterRequest :BaseRequest {
    private RegisterPanel registerPanel;
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Register;
        registerPanel = GetComponent<RegisterPanel>();
        base.Awake();
    }
    public void SendRequest(string username,string password)
    {
        string data = username + "," + password;
        base.SendRequest(data);
    }
    public override void OnResponse(string data)
    {
        base.OnResponse(data);
        string[] strs = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        if (returnCode == ReturnCode.Success)
        {
            string username = strs[1];
            int totalcount = int.Parse(strs[2]);
            int wincount = int.Parse(strs[3]);
            UserData user = new UserData(username, totalcount, wincount);
            gameFacade.SetUserData(user);
        }
        registerPanel.OnRegisterResponse(returnCode);
    }
}
