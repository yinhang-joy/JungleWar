using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using Common;

public class LoginPanel : BasePanel {
    public GameObject closeBtn;
    private InputField usernameIF;
    private InputField passworIF;
    private Button loginButton;
    private Button registerButton;
    private LoginRequest loginRequset;
    public void Awake()
    {
        closeBtn = transform.Find("CloseButton").gameObject;
        closeBtn.GetComponent<Button>().onClick.AddListener(CloseBtnClick);
        usernameIF = GameTool.FindTheChild(gameObject, "UsernameInput").GetComponent<InputField>();
        passworIF = GameTool.FindTheChild(gameObject, "PasswordInput").GetComponent<InputField>();
        loginButton = GameTool.FindTheChild(gameObject, "LoginButton").GetComponent<Button>();
        registerButton = GameTool.FindTheChild(gameObject, "RegisterButton").GetComponent<Button>();
        loginRequset = GetComponent<LoginRequest>();
        if (loginRequset==null)
        {
            loginRequset = gameObject.AddComponent<LoginRequest>();
        }
        loginButton.onClick.AddListener(LoginButtonClick);
        registerButton.onClick.AddListener(RegisterButtonClick);
    }
  
    public void CloseBtnClick()
    {
        PlayClickSound();
        uiMng.PopPanel();
    }
    public void LoginButtonClick()
    {
        PlayClickSound();
        string username = usernameIF.text;
        string passworld = passworIF.text;
        ChickUserNameAndPassWorld(username, passworld);
        loginRequset.SendRequest(username,passworld);//发送客户端消息
    }
    public void RegisterButtonClick()
    {
        PlayClickSound();
        uiMng.PushPanel(UIPanelType.Register);
    }
    private void ChickUserNameAndPassWorld(string username, string passworld)
    {
      
        if (string.IsNullOrEmpty(username)||string.IsNullOrEmpty(passworld))
        {
            uiMng.ShowMessage("用户名或密码不能为空");
            return;
        }
    }
    public void OnLoginResponse(ReturnCode returncode,string str = "")
    {
        if (returncode == ReturnCode.Success)
        {
            uiMng.ShowMeeageSync(str+"，欢迎重新登录！");
            uiMng.PushPanelSync(UIPanelType.RoomList);
        }
        else
        {
            uiMng.ShowMeeageSync("用户名或密码错误");
        }
    }
    public override void HideAnimator()
    {
        transform.DOScale(0, 0.2f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.2f);
        tweener.OnComplete(delegate () { gameObject.SetActive(false); });
    }
    public override void ShowAnimator()
    {
        this.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.2f);
    }
}
