using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scriptplayer2 : MonoBehaviour {
    public Rigidbody2D rb;
    public Animator anim;
    public HP vida;
    public Collider2D AttackTrigger;
    public bool running;
    public float speed;
    public float speedrun;
    public float speedwalk;
    // Use this for initialization
    void Start () {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        vida = GetComponent<HP>();
	}
	
	// Update is called once per frame
	void FixedUpdate() {
        if (vida.health > 0)
        {
            if (Input.GetKey(KeyCode.UpArrow))
            {
                rb.AddForce(transform.up * speed);
            }
            if (Input.GetKey(KeyCode.DownArrow))
            {
                rb.AddForce(-transform.up * speed);
            }
            if (Input.GetKey(KeyCode.RightArrow))
            {
                rb.AddForce(transform.right * speed);
            }
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                rb.AddForce(-transform.right * speed);
            }
           
            if(running == true)
            {
                speed = speedrun;
            }
            else { speed = speedwalk; }
            
        }
        
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            if (running == false)
            {
                running = true;
            }
            else
            {
                running = false;
            }

        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            anim.SetBool("attack", true);
            rb.AddForce(rb.velocity * 30f);
            AttackTrigger.enabled = true;

        }
        if (Input.GetKeyUp(KeyCode.L))
        {
            anim.SetBool("attack", false);
            AttackTrigger.enabled = false;
        }
    }

    }
