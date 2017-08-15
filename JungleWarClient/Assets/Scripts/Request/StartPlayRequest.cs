using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;

public class StartPlayRequest : BaseRequest
{
    public override void Awake()
    {
        requestCode = RequestCode.Game;
        actionCode = ActionCode.StartPlay;
        base.Awake();
    }
    public override void OnResponse(string data)
    {

        base.OnResponse(data);
    }
}

