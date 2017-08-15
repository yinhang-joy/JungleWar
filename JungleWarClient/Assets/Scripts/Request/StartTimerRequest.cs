using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

public class StartTimerRequest:BaseRequest
{
    private GamePanel gamePanel;
    public override void Awake()
    {
        //requestCode = RequestCode.Game;
        actionCode = ActionCode.ShowTimer;
        gamePanel = GetComponent<GamePanel>();
        print(actionCode+"...........");
        base.Awake();
    }
    public override void OnResponse(string data)
    {
        int time = int.Parse(data);
        gamePanel.timer = time;
        base.OnResponse(data);
    }
}

