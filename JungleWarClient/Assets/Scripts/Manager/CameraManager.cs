using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraManager : BaseManager
{
    private GameObject camera;
    private Animator anm;
    private FollowTarget followTarget;
    private Vector3 SrcPosition;
    private Vector3 SrcRotate;
    public CameraManager(GameFacade facade) : base(facade)
    {
    }
    public override void Init()
    {
        camera = Camera.main.gameObject;
        anm = camera.GetComponent<Animator>();
        followTarget = camera.GetComponent<FollowTarget>();
       
    }
    public override void UpData()
    {
        //base.UpData();
        //if (Input.GetMouseButtonDown(0))
        //{
        //    FollowTarget(null);
        //}
        //if (Input.GetMouseButtonDown(1))
        //{
        //    WalkAround();
        //}
    }
    public void FollowTarget()
    {
        followTarget.target = facade.GetCurrentRoleGameobject().transform;
        anm.enabled = false;
        SrcPosition = camera.transform.position;
        SrcRotate = camera.transform.eulerAngles;
        Quaternion targetQuaternion = Quaternion.LookRotation(followTarget.target.position - camera.transform.position);
        camera.transform.DORotateQuaternion(targetQuaternion, 1f).OnComplete(delegate ()
        {
            followTarget.enabled = true;
        });
    }
    public void WalkAround()
    {
        followTarget.enabled = false;
        camera.transform.DOMove(SrcPosition,1);
        camera.transform.DORotate(SrcRotate, 1).OnComplete(delegate () {anm.enabled = true; });
    }
}
