using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RoomItem : MonoBehaviour {
    public Text Username;
    public Text TotalCount;
    public Text WinCount;
    public Button JoinButton;
    public UserData userData;
    public int id;
    private RoomListPanel panel;
    // Use this for initialization
    private void Awake()
    {
        JoinButton.onClick.AddListener(JoinRoomClick);
    }
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
       
	}
    public void JoinRoomClick()
    {
        panel.OnJoinClick(id);
    }
    //public void SetRoomInfo(int id, string username, string totalcount, string wincount,RoomListPanel roomlistpanel)
    //{
    //    Username.text = userData.UserName;
    //    TotalCount.text = "总场数/n" + userData.TotalCount;
    //    WinCount.text = "胜利/n" + userData.WinCount;
    //    this.panel = roomlistpanel;
    //}
    public void SetRoomInfo(UserData userdata, RoomListPanel roomlistpanel)
    {
        id = userdata.ID;
        Username.text = userdata.UserName;
        TotalCount.text = "总场数" + userdata.TotalCount;
        WinCount.text = "胜利" + userdata.WinCount;
        this.panel = roomlistpanel;
    }
    public void DestroySelf()
    {
        Destroy(gameObject);
    }
}
