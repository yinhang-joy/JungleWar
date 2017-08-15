using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;
public class RoomPanel : BasePanel {
    private GameObject BluePanel;
    private GameObject RedPanel;
    private Text Blue_Username;
    private Text Blue_TotalCount;
    private Text Blue_WinCount;
    private Text Red_Username;
    private Text Red_TotalCount;
    private Text Red_WinCount;
    private GameObject StartButton;
    private GameObject ExitButton;
    private GameObject BtnGroup;
    private UserData BlueUser=null;
    private UserData RedUser = null;
    private QuitRoomRequest quitRoomRequest;
    private StartGameRequest startGameRequest;
    private bool isPopPanel;
    private void Awake()
    {
        Init();
        UGUIEventListener.Get(StartButton).onClick = StartGameClick;
        UGUIEventListener.Get(ExitButton).onClick = CloseRoomClick;
        quitRoomRequest = GetComponent<QuitRoomRequest>();
        startGameRequest = GetComponent<StartGameRequest>();
    }
    public void Init()
    {
        BluePanel = GameTool.FindTheChild(gameObject, "BluePanel").gameObject;
        RedPanel = GameTool.FindTheChild(gameObject, "RedPanel").gameObject;
        Blue_Username = GameTool.FindTheChild(gameObject, "Blue_Username").GetComponent<Text>();
        Blue_TotalCount = GameTool.FindTheChild(gameObject, "Blue_TotalCount").GetComponent<Text>();
        Blue_WinCount = GameTool.FindTheChild(gameObject, "Blue_WinCount").GetComponent<Text>();
        Red_Username = GameTool.FindTheChild(gameObject, "Red_Username").GetComponent<Text>();
        Red_TotalCount = GameTool.FindTheChild(gameObject, "Red_TotalCount").GetComponent<Text>();
        Red_WinCount = GameTool.FindTheChild(gameObject, "Red_WinCount").GetComponent<Text>();
        StartButton = GameTool.FindTheChild(gameObject, "StartButton").gameObject;
        ExitButton = GameTool.FindTheChild(gameObject, "ExitButton").gameObject;
        BtnGroup = GameTool.FindTheChild(gameObject, "BtnGroup").gameObject;
    }
    public override void ShowAnimator()
    {
        base.ShowAnimator();
        gameObject.SetActive(true);
        BluePanel.transform.localPosition = new Vector3(-1000, 0, 0); ;
        BluePanel.transform.DOLocalMove(new Vector3(-174, 0, 0), 0.4f);
        RedPanel.transform.localPosition = new Vector3(1000, 0, 0);
        RedPanel.transform.DOLocalMove(new Vector3(174, 0, 0), 0.4f);
        BtnGroup.transform.localPosition = new Vector3(0,-1000,0);
        BtnGroup.transform.DOLocalMove(Vector3.zero, 0.4f);
    }
    public override void HideAnimator()
    {
        base.HideAnimator();
        BtnGroup.transform.DOLocalMove(new Vector3(0,-1000,0), 0.4f);
        BluePanel.transform.DOLocalMove(new Vector3(-1000, 0, 0), 0.4f);
        Tweener tweener = RedPanel.transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f);
        tweener.OnComplete(() => { gameObject.SetActive(false); });

    }
    public void StartGameClick(GameObject go)
    {
        startGameRequest.SendRequest();
    }
    public void OnStartResponse(ReturnCode returncode)
    {
        if (returncode== ReturnCode.Fail)
        {
            uiMng.ShowMeeageSync("您不是房主，无法开始游戏");
        }
        else
        {
            uiMng.PushPanelSync(UIPanelType.Game);
            gameFacade.EnterPlayingSync();
            //TODO
        }
    }
    public void CloseRoomClick(GameObject go)
    {
        quitRoomRequest.SendRequest();
    }
    public void OnExitResponse()
    {
        isPopPanel = true;
    }
    public void SetBlueUserSync(UserData user)
    {
        BlueUser = user;
    }
    public void SetRedUserSync(UserData user)
    {
        RedUser = user;
    }
    public override void OnEnter()
    {
        base.OnEnter();
    }
    private void Update()
    {
        SetUserInfo();
    }
    private void SetUserInfo()
    {
        if (isPopPanel)
        {
            uiMng.PopPanel();
            isPopPanel = false;
        }
        if (BlueUser != null)
        {
            Blue_Username.text = BlueUser.UserName;
            Blue_TotalCount.text = "总场数：" + BlueUser.TotalCount;
            Blue_WinCount.text = "胜利：" + BlueUser.WinCount;
            Red_TotalCount.text = "等待玩家加入。。。";
            Red_Username.text = "";
            Red_WinCount.text = "";
           BlueUser = null;
        }
        if (RedUser != null)
        {
            Red_Username.text = RedUser.UserName;
            Red_TotalCount.text = "总场数：" + RedUser.TotalCount;
            Red_WinCount.text = "胜利：" + RedUser.WinCount;
            RedUser = null;
        }
    }
}
