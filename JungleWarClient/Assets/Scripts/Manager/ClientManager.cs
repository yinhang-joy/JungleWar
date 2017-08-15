using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Net.Sockets;
using System.Net;
using Common;
using System;

public class ClientManager :BaseManager {
    private const string IP = "127.0.0.1";
    private const int Point = 6666;
    private Socket socket;
    private Message message = new Message();

    public ClientManager(GameFacade facade) : base(facade)
    {
    }


    public override void Init()
    {
        base.Init();
         socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        try
        {
            socket.Connect(new IPEndPoint(IPAddress.Parse(IP), Point));
            Start();
        }
        catch (System.Exception e)
        {
            Debug.LogError(e);
        } 
    }
    private void Start()
    {
        socket.BeginReceive(message.Data, message.StartIndex, message.RemainSize, SocketFlags.None, ReceiveCallBack, null);
    }
    private void ReceiveCallBack(IAsyncResult ar)
    {
        try
        {
            if ( socket==null||socket.Connected==false)
            {
                return;
            }
            int count = socket.EndReceive(ar);
            message.ReadMessage(count, OnProcessDataCallBack);
            socket.BeginReceive(message.Data, message.StartIndex, message.RemainSize, SocketFlags.None, ReceiveCallBack, null);
        }
        catch (Exception e)
        {
            Debug.LogError(e);
        }
    }
    private void OnProcessDataCallBack(ActionCode actionCode,string data)
    {
        facade.HandleRequest(actionCode, data);
    }
    /// <summary>
    /// 向服务端发送消息
    /// </summary>
    /// <param name="requestCode"></param>
    /// <param name="actionCode"></param>
    /// <param name="data"></param>
    public void SendRequest(RequestCode requestCode,ActionCode actionCode,string data)
    {
        byte[] bytes = Message.PackData(requestCode, actionCode, data);
        socket.Send(bytes);
    }
    public override void OnDestroy()
    {
        base.OnDestroy();
        try
        {
            socket.Close();
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
            throw;
        }
    }
}
