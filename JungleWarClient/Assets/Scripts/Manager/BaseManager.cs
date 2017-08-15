using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseManager {
    protected GameFacade facade;
    public virtual void Init(){}
    public virtual void OnDestroy() { }
    public BaseManager(GameFacade facade)
    {
        this.facade = facade;
    }
    public virtual void UpData()
    {

    }
}
