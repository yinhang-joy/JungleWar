using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class MessagePanel : BasePanel {
    private string messageStr;
    private Text messageText;
    private float showTime=0.8f;
    private bool isShow=false;
    private string message = null;
    public override void OnEnter()
    {
        base.OnEnter();
        messageText = GetComponent<Text>();
        messageText.enabled = false;
        uiMng.InjectMsgPanel(this);
    }
    public void ShowMessage(string str)
    {
        if (isShow)
        {
            return;
        }
        this.messageStr = str;
        isShow = true;
        messageText.DOFade(1, 0.2f);
        messageText.text = messageStr;
        messageText.enabled = true;
        Invoke("Hide", showTime);
    }
    public void ShowMessageSync(string msg)
    {
        message = msg;
    }
    private void Update()
    {
        if (message!=null)
        {
            ShowMessage(message);
            message = null;
        }
    }
    private void Hide()
    {
        Tweener tweener =  messageText.DOFade(0,0.8f);
        tweener.OnComplete(() => { isShow = false; });
    }
 
}
