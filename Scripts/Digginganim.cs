using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Digginganim : MonoBehaviour {
    private Boss1 bossScript;
    private GameObject BossOne;
    private Vector3 bosspos;
    // Use this for initialization
    void Start () {
        BossOne = GameObject.Find("Boss1");
        bossScript = BossOne.GetComponent<Boss1>();
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        bosspos = bossScript.pos;
        bosspos.z = transform.position.z;
        transform.right = (transform.position - bossScript.pos);


    }
}
