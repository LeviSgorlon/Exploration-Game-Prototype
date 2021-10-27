using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EletricPillar : MonoBehaviour {
    public int dmg = 25;
    public bool NotReady;
    public bool Used;
    public bool GettingReady;
    public bool Ready;
    private Collider2D col;
    private Animator anim;
    public float animCD = 1;
    public float anim2CD = 1;
    public float CD = 10;
    private SoundPlayer _sound;

    private void OnTriggerEnter2D(Collider2D col)
    {




        if (col.gameObject.tag == "Boss1T" && Used == false)
        {
            //_sound.PlaySound(0, true,1,0,true,3);
            col.SendMessageUpwards("EDamage", dmg);
            Used = true;
            Ready = false;
            animCD = 1;
            anim2CD = 1;

        }
        else { return; }
        if (col.gameObject.tag == "Player" && Used == false)
        {
            col.SendMessageUpwards("Damage", dmg);
            Used = true;
            Ready = false;
            animCD = 1;
            anim2CD = 1;

        }
        else { return; }



    }


    
    void Start()
    {
        anim = GetComponent<Animator>();
        col = GetComponent<Collider2D>();
        _sound = GetComponent<SoundPlayer>();
    }
    void FixedUpdate()
    {
        if(Used == true)
        {
            anim.SetBool("Ready", false);
            anim.SetBool("Used", true);
            animCD -= Time.deltaTime;
            col.enabled = false;
            if(animCD <= 0)
            {
                anim.SetBool("Used", false);
                anim.SetBool("NotReady", true);
                CD -= Time.deltaTime;
                
            }

        }
        if (CD <= 0)
        {
            anim.SetBool("NotReady", false);           
            GettingReady = true;
            Used = false;
            anim.SetBool("GettingReady", true);
            anim2CD -= Time.deltaTime;
            if (anim2CD <= 0)
            {
                anim.SetBool("GettingReady", false);
                anim.SetBool("Ready", true);
                col.enabled = true;
                CD = 10;
            }
        }
    }
} 

