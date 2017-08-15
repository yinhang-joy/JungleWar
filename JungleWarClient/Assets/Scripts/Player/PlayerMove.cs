using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour {
    private float speed = 3;
    private Animator anm;
    public Vector3 target = Vector3.one;
    // Use this for initialization
    void Start () {
        anm = GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(anm.GetCurrentAnimatorStateInfo(0).IsName("Grounded") == false) return;
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");

        if (Mathf.Abs(h)>0||Mathf.Abs(v)>0)
        {
            transform.Translate(new Vector3(h, 0, v) * speed * Time.deltaTime, Space.World);
            target = new Vector3(h, 0, v);
            transform.rotation = Quaternion.LookRotation(target);
            float res = Mathf.Max(Mathf.Abs(h), Mathf.Abs(v));
            anm.SetFloat("Forward", res);
        }
    }
}
