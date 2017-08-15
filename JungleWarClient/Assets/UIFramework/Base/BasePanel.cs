using UnityEngine;
using System.Collections;

public class BasePanel : MonoBehaviour {
    protected UIManager uiMng;
    protected GameFacade gameFacade;
    public GameFacade SetGameFacade
    {
        set { gameFacade = value; }
    }
    protected void PlayClickSound()
    {
        gameFacade.PlayNormalSound(AudioManager.Sound_ButtonClick);
    }
    public UIManager UIMng
    {
        set { uiMng = value; }
    }
    /// <summary>
    /// 界面被显示出来
    /// </summary>
    public virtual void OnEnter()
    {
        ShowAnimator();
    }

    /// <summary>
    /// 界面暂停
    /// </summary>
    public virtual void OnPause()
    {
        HideAnimator();
    }

    /// <summary>
    /// 界面继续
    /// </summary>
    public virtual void OnResume()
    {
        ShowAnimator();
    }

    /// <summary>
    /// 界面不显示,退出这个界面，界面被关系
    /// </summary>
    public virtual void OnExit()
    {
        HideAnimator();
    }
    public virtual void  ShowAnimator()
    {

    }
    public virtual void HideAnimator()
    {

    }
}
