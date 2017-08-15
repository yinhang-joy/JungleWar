using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
public class GamePanel : BasePanel {
    private Text Timer;
    private GameObject SuccessButton;
    private GameObject FailButton;
    private GameObject ExitButton;
    public int timer = -1;
    private void Awake()
    {
        Timer = GameTool.FindTheChild(gameObject, "Timer").GetComponent<Text>();
        SuccessButton = GameTool.FindTheChild(gameObject, "SuccessButton").gameObject;
        FailButton = GameTool.FindTheChild(gameObject, "FailButton").gameObject;
        ExitButton = GameTool.FindTheChild(gameObject, "ExitButton").gameObject;
        UGUIEventListener.Get(SuccessButton).onClick = SuccessButtonClick;
        UGUIEventListener.Get(FailButton).onClick = FailButtonClick;
        UGUIEventListener.Get(ExitButton).onClick = ExitButtonClick;
    }
    public void ShowTimer(int time)
    {
        if (!Timer.IsActive())
        {
            Timer.gameObject.SetActive(true);
        }
        if (time == 3)
        {
            ExitButton.gameObject.SetActive(true);
        }
        Timer.gameObject.SetActive(true);
        Timer.text = time.ToString();
        Timer.transform.localScale = Vector3.one;
        Color tempColor = Timer.color;
        tempColor.a = 1;
        Timer.color = tempColor;
        Timer.transform.DOScale(2, 0.3f).SetDelay(0.3f);
        Timer.DOFade(0, 0.3f).SetDelay(0.3f).OnComplete(() => Timer.gameObject.SetActive(false));
        gameFacade.PlayNormalSound(AudioManager.Sound_Alert);
    }
    public void SuccessButtonClick(GameObject go)
    {

    }
    public void FailButtonClick(GameObject go)
    {

    }
    public void ExitButtonClick(GameObject go)
    {

    }
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (timer>-1)
        {
            ShowTimer(timer);
            timer = -1;
        }
	}
}
