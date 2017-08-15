using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class StartPanel : BasePanel {

    public Button loginBtn;
    public override void OnEnter()
    {
        base.OnEnter();
        loginBtn = transform.Find("LoginButton").GetComponent<Button>();
        loginBtn.onClick.AddListener(OnLoginClick);
    }
    private void OnLoginClick()
    {
        PlayClickSound();
        uiMng.PushPanel(UIPanelType.Login);
    }
    public override void ShowAnimator()
    {
        this.gameObject.SetActive(true);
        transform.localScale = Vector3.zero;
        transform.DOScale(1, 0.2f);
        transform.localPosition = new Vector3(1000, 0, 0);
        transform.DOLocalMove(Vector3.zero, 0.2f);
    }
    public override void HideAnimator()
    {
        base.HideAnimator();
        loginBtn.transform.DOScale(0, 0.2f).OnComplete(() => { gameObject.SetActive(false); });
        gameObject.transform.DOLocalMove(new Vector3(1000, 0, 0), 0.2f);
 
    }
}
