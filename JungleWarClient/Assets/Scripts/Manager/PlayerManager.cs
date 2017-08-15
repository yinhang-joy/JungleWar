using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Common;
public class PlayerManager : BaseManager
{
    private UserData userData;
    private Dictionary<RoleType, RoleData> dicRole = new Dictionary<RoleType, RoleData>();
    private Transform playerPosition;
    private RoleType currentRoleType;
    private GameObject currentRoleGameObject;
    private void InitDicRole()
    {
        dicRole.Add(RoleType.Blue, new RoleData(RoleType.Blue, "Hunter_BLUE", "Arrow_BLUE",playerPosition.Find("Position1")));
        dicRole.Add(RoleType.Red, new RoleData(RoleType.Red, "Hunter_RED", "Arrow_RED", playerPosition.Find("Position2")));
    }
    public PlayerManager(GameFacade facade) : base(facade)
    {
    }
    public UserData UserData
    {
        set { userData = value; }
        get { return userData; }
    }
    public override void Init()
    {
        playerPosition = GameObject.Find("PlayerPositions").transform;
        InitDicRole();
        base.Init();
    }

    public void SpawnPlayer()
    {
        foreach (RoleData item  in dicRole.Values)
        {
            GameObject go = GameObject.Instantiate(item.RolePrefab, item.SpawnPosition, Quaternion.identity);
            if (item.RoleType == currentRoleType)
            {
                currentRoleGameObject = go;
            }
        }
    }
    public GameObject GetCurrentRoleGameObject()
    {
        return currentRoleGameObject;
    }
    public void SetRoleType(RoleType roletype)
    {
        currentRoleType = roletype;
    }
}
