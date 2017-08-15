using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class LoginRequest : BaseRequest {
    private LoginPanel loginPanel;
    // Use this for initialization
    public override void Awake()
    {
        requestCode = RequestCode.User;
        actionCode = ActionCode.Login;
        loginPanel = GetComponent<LoginPanel>();
        base.Awake();
    }
    void Start () {
       

    }
    public  void SendRequest(string username,string passworld)
    {
        string data = username + "," + passworld;
        SendRequest(data);
    }
    public override void OnResponse(string data)
    {
        string[] strs = data.Split(',');
        ReturnCode returnCode = (ReturnCode)int.Parse(strs[0]);
        if (returnCode == ReturnCode.Success)
        {
            string username = strs[1];
            int totalcount = int.Parse(strs[2]);
            int wincount = int.Parse(strs[3]);
            UserData user = new UserData(username,totalcount,wincount);
            gameFacade.SetUserData(user);
        }
        if (strs.Length>1)
        {
            loginPanel.OnLoginResponse(returnCode, strs[1]);
        }
        else
        {
            loginPanel.OnLoginResponse(returnCode);
        }
       
    }
    // Update is called once per frame
    void Update () {
		
	}
}
