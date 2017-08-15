using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Common;
using DG.Tweening;
public class RegisterPanel : BasePanel {

    private InputField Username;
    private InputField Password;
    private InputField RepeatPassword;
    private GameObject RegistBtn;
    private GameObject CloseBtn;
    private RegisterRequest registerRequest;
    private void Awake()
    {
        Username = GameTool.FindTheChild(gameObject, "UsernameInput").GetComponent<InputField>();
        Password = GameTool.FindTheChild(gameObject, "PasswordInput").GetComponent<InputField>();
        RepeatPassword = GameTool.FindTheChild(gameObject, "RePasswordInput").GetComponent<InputField>();
        RegistBtn = GameTool.FindTheChild(gameObject, "RegisterButton").gameObject;
        CloseBtn = GameTool.FindTheChild(gameObject, "CloseButton").gameObject;
        UGUIEventListener.Get(RegistBtn).onClick += RegisterBtnClick;
        UGUIEventListener.Get(CloseBtn).onClick += CloseBtnClick;
        registerRequest = GetComponent<RegisterRequest>();
        if (registerRequest==null)
        {
            registerRequest = gameObject.AddComponent<RegisterRequest>();
        }
    }

    private void RegisterBtnClick(GameObject go)
    {
        PlayClickSound();
        string userName = Username.text;
        string passWord = Password.text;
        string repeatPassword = RepeatPassword.text;

        if (string.IsNullOrEmpty(userName))
        {
            uiMng.ShowMessage("用户名不能为空");
            return;
        }
        if (!string.IsNullOrEmpty(passWord))
        {
            if (passWord!=repeatPassword)
            {
                Debug.Log(passWord+","+repeatPassword);
                uiMng.ShowMessage("两次密码输入不一致，请重新输入");
            }
            else
            {
                registerRequest.SendRequest(userName, passWord);
            }
        }
        else
        {
            uiMng.ShowMessage("密码不能为空");
        }
    }
    public void OnRegisterResponse(ReturnCode returncode)
    {
        if (returncode == ReturnCode.Success)
        {
            uiMng.ShowMeeageSync("注册成功");
            uiMng.PushPanelSync(UIPanelType.RoomList);
        }
        else
        {
            uiMng.ShowMeeageSync("用户名重复");
        }
    }
    private void CloseBtnClick(GameObject go )
    {
        PlayClickSound();
        uiMng.PopPanel();
    }
    public override void ShowAnimator()
    {
        this.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.4f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.4f);
    }
    public override void HideAnimator()
    {
        transform.DOScale(0, 0.4f);
        Tweener tweener = transform.DOLocalMove(new Vector3(1000, 0, 0), 0.4f);
        tweener.OnComplete(() => { gameObject.SetActive(false); });
    }
}
