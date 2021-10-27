using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tongue : MonoBehaviour
{
    int a;
    private RaycastHit2D hitinfo;
    private SpriteRenderer _sprite;
    private Animator anim;
    private Boss1 bossScript;
    private EnemyHp bossHP;
    private GameObject BossOne;
    public Vector3 TargetTransform;
   
    private int layerMask = 1 << 8;
    public bool mirrorZ = true;
    public bool Shooting;
    private float rdShootInterval = 1.3f;
    public float rd = 0;
    public float rdCD = 1.3f;
    public float animtimer = 0;
    public int Ammo = 1;
    private SoundPlayer _sound;
    
    private void Start()
    {
        _sprite = gameObject.GetComponent<SpriteRenderer>();
        anim = gameObject.GetComponent<Animator>();
        BossOne = GameObject.Find("Boss1");
        bossScript = BossOne.GetComponent<Boss1>();
        bossHP = BossOne.GetComponent<EnemyHp>();
        _sound = GetComponent<SoundPlayer>();
    }
    void FixedUpdate()
    {
        if(bossScript.Moving == true || bossHP.Stunned == true ||bossScript.dead == true)
        {
            rd = 0;
            animtimer = 0;          
            anim.SetBool("Shooting", false);
            _sprite.enabled = false;
            Shooting = false;
            Ammo = 1;
        }
        if (bossScript.Moved == true && Ammo == 1 && rd >= 2)
        {
            hitinfo = Physics2D.CircleCast(transform.position,  300, Vector2.up, 1, layerMask);
            Ammo = 0;
        }
        if (bossScript.Moving == false && bossHP.Stunned == false && Shooting == false)
        { rdCD -= Time.deltaTime; }                       
        TargetTransform = hitinfo.point;
        TargetTransform.z = transform.position.z;
        if (rdCD < 0)
        {
            rd += 1;
            rdCD = rdShootInterval;
        }
        if (rd >= 2)
        {
            
                animtimer += Time.deltaTime;
                
                transform.right = (TargetTransform - transform.position);
                if (hitinfo.collider != null && animtimer > 0 && bossScript.Moving == false && bossScript.AnimationEnded == true)
                {
                    
                        anim.SetBool("Shooting", true);
                        _sprite.enabled = true;
                        Shooting = true;
                if (a == 1)
                {
                   // _sound.PlaySound(0, true,1,0, true,3);
                    a = 2;
                }
            }
            
            }
            if (animtimer > 2.0f)
            {
                rd = 0;
                anim.SetBool("Shooting", false);
                _sprite.enabled = false;
            
            Shooting = false;
                animtimer = 0;
            }
if(Shooting == false)
        {
            a = 1;
        }
        }
    }


 
 
     
     

