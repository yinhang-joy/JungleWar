using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Common;
public class RoomListPanel : BasePanel {
    private GameObject BattleRes;
    private GameObject RoomList;
    private Text Username;
    private Text TotalCount;
    private Text WinCount;
    private GameObject CloseBtn;
    private GameObject RefreshButton;
    private GameObject CreateRoomButton;
    private VerticalLayoutGroup Layout;
    private GameObject roomItem;
    private ListRoomRequest listRoomRequest;
    public List<RoomItem> RoomItemList = new List<RoomItem>();
    public List<UserData> userDataList = new List<UserData>();
    private CreateRoomRequest createRoomRequestce;
    private JoinRoomRequest joinRoomRequest;
    private UserData ud1;
    private UserData ud2;

    public int count = 0;
    private void Awake()
    {
        Init();
        UGUIEventListener.Get(CloseBtn).onClick = CloseRoomListPanelClick;
        UGUIEventListener.Get(RefreshButton).onClick = RefreshButtonClick;
        UGUIEventListener.Get(CreateRoomButton).onClick = CreateRoomButtonClick;
        listRoomRequest = GetComponent<ListRoomRequest>();
        createRoomRequestce = GetComponent<CreateRoomRequest>();
        joinRoomRequest = GetComponent<JoinRoomRequest>();
    }
    private void Init()
    {
        BattleRes = GameTool.FindTheChild(gameObject, "BattleRes").gameObject;
        RoomList = GameTool.FindTheChild(gameObject, "RoomList").gameObject;
        CloseBtn = GameTool.FindTheChild(gameObject, "CloseButton").gameObject;
        RefreshButton = GameTool.FindTheChild(gameObject, "RefreshButton").gameObject;
        CreateRoomButton = GameTool.FindTheChild(gameObject, "CreateRoomButton").gameObject;
        Username = GameTool.FindTheChild(gameObject, "Username").GetComponent<Text>();
        TotalCount = GameTool.FindTheChild(gameObject, "TotalCount").GetComponent<Text>();
        WinCount = GameTool.FindTheChild(gameObject, "WinCount").GetComponent<Text>();
        Layout = GameTool.FindTheChild(gameObject, "Layout").GetComponent<VerticalLayoutGroup>();
        roomItem = Resources.Load("UIPrefabs/RoomItem") as GameObject;
    }
    public void RefreshButtonClick(GameObject go)
    {
        listRoomRequest.SendRequest();
    }

    public void CreateRoomButtonClick(GameObject go)
    {
        BasePanel panel= uiMng.PushPanel(UIPanelType.Room);
        createRoomRequestce.SetPanel(panel);
        createRoomRequestce.SendRequest();
    }
    public void CloseRoomListPanelClick(GameObject go)
    {
        uiMng.PopPanel();
    }
    private void GetUserInfo()
    {
        UserData user = gameFacade.GetUserData();
        Username.text = user.UserName;
        TotalCount.text = "总场数："+user.TotalCount.ToString() ;
        WinCount.text = "胜利："+user.WinCount.ToString();
    }
    public override void OnEnter()
    {
        base.OnEnter();
        GetUserInfo();
        listRoomRequest.SendRequest();
    }
    public override void OnResume()
    {
        base.OnResume();
        listRoomRequest.SendRequest();
    }
    public override void ShowAnimator()
    {
        base.ShowAnimator();
        gameObject.SetActive(true);
        BattleRes.transform.localPosition = new Vector3(-1000,0,0); ;
        BattleRes.transform.DOLocalMove(new Vector3(-290,0,0),0.4f);
        RoomList.transform.localPosition = new Vector3(1000, 0, 0);
        RoomList.transform.DOLocalMove(new Vector3(171, 0, 0),0.4f);
    }
    public override void HideAnimator()
    {
        base.HideAnimator();
        BattleRes.transform.DOLocalMove(new Vector3(-1000, 0, 0), 0.4f);
        Tweener tweener = RoomList.transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f);
        tweener.OnComplete(() => { gameObject.SetActive(false); });
    }
    public void LoadRoomItem(List<UserData> userdata)
    {
        RoomItem[] riArray = Layout.GetComponentsInChildren<RoomItem>();
        foreach (RoomItem ri in riArray)
        {
            ri.DestroySelf();
        }
        for (int i =0;i< userdata.Count; i++)
        {
            GameObject roomitem = Instantiate(roomItem);

            roomitem.transform.parent = Layout.transform;
            RoomItemList.Add(roomitem.GetComponent<RoomItem>());
            roomitem.GetComponent<RoomItem>().SetRoomInfo(userdata[i], this);
            roomitem.transform.localScale= Vector3.one;
        }
        int roomcount = GetComponentsInChildren<RoomItem>().Length;
        Vector2 size = Layout.GetComponent<RectTransform>().sizeDelta;
        Layout.GetComponent<RectTransform>().sizeDelta = new Vector2(size.x, roomcount * (roomItem.GetComponent<RectTransform>().sizeDelta.y + Layout.spacing));
    }
    public void OnJoinClick(int id)
    {
        joinRoomRequest.SendRequest(id);
    }
    private void Update()
    {
        if (userDataList!=null)
        {
            LoadRoomItem(userDataList);
            userDataList = null;
        }
        if (ud1!=null &&ud2!=null)
        {
            RoomPanel roompanel = uiMng.PushPanel(UIPanelType.Room) as RoomPanel;
            roompanel.SetBlueUserSync(ud1);
            roompanel.SetRedUserSync(ud2);
            ud1 = null; ud2 = null;
        }
    }
    public void OnJoinResponse(ReturnCode returncode,UserData ud1, UserData ud2)
    {
        switch (returncode)
        {
            case ReturnCode.Success:
                this.ud1 = ud1;
                this.ud2 = ud2;
                break;
            case ReturnCode.Fail:
                uiMng.ShowMeeageSync("房间已满");
                break;
            case ReturnCode.NotFound:
                uiMng.ShowMeeageSync("房间被关闭，无法加入");
                break;
            default:
                break;
        }
    }
}
