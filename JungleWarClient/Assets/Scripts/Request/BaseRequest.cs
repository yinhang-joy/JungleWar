using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class BaseRequest : MonoBehaviour {

    protected RequestCode requestCode = RequestCode.None;
    protected ActionCode actionCode = ActionCode.None;
    protected GameFacade gameFacade;
   
    // Use this for initialization
	public virtual void Awake () {
        GameFacade.Instance.ADDRequest(actionCode, this);
        gameFacade = GameFacade.Instance;
	}
   
    /// <summary>
    /// 发送请求
    /// </summary>
    public virtual void SendRequest(string data)
    {
        gameFacade.SendRequest(requestCode,actionCode,data);
    }
    /// <summary>
    /// 响应请求
    /// </summary>
    public virtual void OnResponse(string data)
    {

    }
    public virtual void OnDestroy()
    {
        GameFacade.Instance.RemoveRequest(actionCode);
    }
}
