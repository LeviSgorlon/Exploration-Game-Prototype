using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootTowerAnim : MonoBehaviour {
 
   
    private int layerMask = 1 << 12;
    private Animator anim;
    private RaycastHit2D hitinfo;
    // Use this for initialization
    void Start () {
        anim = gameObject.GetComponent<Animator>();

    }
	
	// Update is called once per frame
	void FixedUpdate () {
       
        hitinfo = Physics2D.CircleCast(transform.position,  200, Vector2.up, 1, layerMask);
        if (hitinfo.collider != null)
        {
                      
            anim.SetBool("Detect", true);

        }
        else { anim.SetBool("Detect", false); }
    }
}
