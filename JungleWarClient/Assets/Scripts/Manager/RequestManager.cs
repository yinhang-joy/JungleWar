using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class RequestManager : BaseManager
{

    public RequestManager(GameFacade facade) : base(facade)
    {
    }
    private Dictionary<ActionCode, BaseRequest> requestDic = new Dictionary<ActionCode, BaseRequest>();
    public void ADDRequest(ActionCode actionCode, BaseRequest baseRequest)
    {
        requestDic.Add(actionCode, baseRequest);
    }
    public void RemoveRequest(ActionCode actionCode)
    {
        requestDic.Remove(actionCode);
    }
    public void HandleReponse(ActionCode actionCode, string data)
    {
        Debug.Log(actionCode+","+data);
        BaseRequest baseRequset = null;
        baseRequset =requestDic[actionCode];
        if (baseRequset!=null)
        {
            baseRequset.OnResponse(data);
        }
        else
        {
            Debug.LogError("无法的到ActionCode[" + actionCode + "]对应的Request类");
        }
    }
}
