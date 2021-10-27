using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attackplayer : MonoBehaviour {
    private bool attacking = false;
    private float attacktimer = 0.2f;
    private float attackCD = 0.2f;
    public PolygonCollider2D attackTrigger;
    private bool blocking = false;

    private Animator anim;
    
	// Use this for initialization
	void Awake () {
        anim = gameObject.GetComponent<Animator>();
        attackTrigger.enabled = false;
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetMouseButton(1))
        {
            blocking = true;
            
        }
        else
        {
            blocking = false;
            
        }
        if (Input.GetMouseButtonDown(0) && !attacking && !blocking)
        {
            attacking = true;
            attacktimer = attackCD;
            attackTrigger.enabled = true;
        }
        if (attacking)
        {
            if (attacktimer > 0)
            {
                attacktimer -= Time.deltaTime;

            }
            else
            {
                attacking = false;
                attackTrigger.enabled = false;
            }
           
        }
        anim.SetBool("Attacking", attacking);


    }
}
