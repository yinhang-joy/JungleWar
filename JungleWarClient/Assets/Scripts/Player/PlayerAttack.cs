using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour {
    public GameObject arrowPrefab;
    private Animator anm;
    private Transform LeftHandle;
    private Vector3 dir;
    // Use this for initialization
    void Start () {
        anm = GetComponent<Animator>();
        LeftHandle = GameTool.FindTheChild(gameObject, "Bip001 L Hand");
    }
	
	// Update is called once per frame
	void Update () {
        if (anm.GetCurrentAnimatorStateInfo(0).IsName("Grounded"))
        {
            if (Input.GetMouseButtonDown(0))
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                bool isCollider =Physics.Raycast(ray,out hit);
                if (isCollider)
                {
                    Vector3 target = hit.point;
                    target.y = transform.localPosition.y;
                    dir = target - transform.localPosition;
                    anm.SetTrigger("Attack");
                    transform.rotation = Quaternion.LookRotation(dir);
                    Invoke("Shoot",0.5f);
                }
            }
        }
        //if (anm.GetCurrentAnimatorStateInfo(0).IsName("Attack"))
        //{
        //    transform.rotation = Quaternion.LookRotation(dir);
        //}
	}
    private void Shoot()
    {
        Instantiate(arrowPrefab, LeftHandle.position, Quaternion.LookRotation(dir));
    }
}
