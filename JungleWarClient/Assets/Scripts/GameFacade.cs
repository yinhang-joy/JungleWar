using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;

public class GameFacade : MonoBehaviour {
    private static GameFacade _instance;
    public static GameFacade Instance { get { return _instance; } }
    private UIManager uiMgr;
    private AudioManager audioMgr;
    private PlayerManager playerMgr;
    private CameraManager cameraMgr;
    private RequestManager requestMgr;
    private ClientManager clientMgr;
    private List<BaseManager> managerList = new List<BaseManager>();
    private bool isEnterPlayer = false;
    private void Awake()
    {
        if (_instance!=null)
        {
            Destroy(this);
            return;
        }
        _instance = this;
        uiMgr = new UIManager(this);
        audioMgr = new AudioManager(this);
        playerMgr = new PlayerManager(this);
        cameraMgr = new CameraManager(this);
        requestMgr = new RequestManager(this);
        clientMgr = new ClientManager(this);
        managerList = new List<BaseManager>();
        managerList.Add(uiMgr);
        managerList.Add(audioMgr);
        managerList.Add(playerMgr);
        managerList.Add(cameraMgr);
        managerList.Add(requestMgr);
        managerList.Add(clientMgr);

    }
    // Use this for initialization
    void Start () {
        InitManager();
    }
	private void InitManager()
    {
        foreach (var item in managerList)
        {
            item.Init();
        }
        //uiMgr = new UIManager(this);
        //audioMgr = new AudioManager(this);
        //playerMgr = new PlayerManager(this);
        //cameraMgr = new CameraManager(this);
        //requestMgr = new RequestManager(this);
        //clientMgr = new ClientManager(this);

        //uiMgr.Init();
        //audioMgr.Init();
        //playerMgr.Init();
        //cameraMgr.Init();
        //requestMgr.Init();
        //clientMgr.Init();
    }
    private void OnDestroy()
    {
        DestroyManager();
    }
    private void DestroyManager()
    {
        foreach (var item in managerList)
        {
            item.OnDestroy();
        }
    }
	// Update is called once per frame
	void Update () {
        UpdataManager();
        if (isEnterPlayer)
        {
            EnterPlaying();
            isEnterPlayer = false;
        }
    }
    void UpdataManager()
    {
        foreach (var item in managerList)
        {
            item.UpData();
        }
    }
    public void ADDRequest(ActionCode actionCode, BaseRequest baseRequest)
    {
        requestMgr.ADDRequest(actionCode, baseRequest);
    }
    public void RemoveRequest(ActionCode actionCode)
    {
        requestMgr.RemoveRequest(actionCode);   
    }
    public void HandleRequest(ActionCode actionCode, string data)
    {
        requestMgr.HandleReponse(actionCode, data);
    }
    public void ShowMessage(string msg)
    {
        uiMgr.ShowMessage(msg);
    }
    public void SendRequest(RequestCode requestCode, ActionCode actionCode, string data)
    {
        clientMgr.SendRequest( requestCode,  actionCode,  data);

    }
    public void PlayBgSound(string soundName)
    {
        audioMgr.PlayBgSound(soundName);
    }
    public void PlayNormalSound(string soundName)
    {
        audioMgr.PlayNormalSound(soundName);
    }

    public void SetUserData(UserData user)
    {
        playerMgr.UserData = user;
    }
    public UserData GetUserData()
    {
        return playerMgr.UserData;
    }
    public void SetRoleType(RoleType roletype)
    {
        playerMgr.SetRoleType(roletype);
    }
    public GameObject GetCurrentRoleGameobject()
    {
        return playerMgr.GetCurrentRoleGameObject();
    }
    public void EnterPlayingSync()
    {
        isEnterPlayer = true;
    }
    public void EnterPlaying()
    {
        playerMgr.SpawnPlayer();
        cameraMgr.FollowTarget();
    }
}
