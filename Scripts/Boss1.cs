using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1 : MonoBehaviour
{
    #region Variables
    int a;
    int b;
    int c; 
    private Animator anim;
    private Rigidbody2D rb;
    private RaycastHit2D hitinfo;
    private RaycastHit2D ponto1;
    private RaycastHit2D ponto2;
    private RaycastHit2D ponto3;
    private RaycastHit2D ponto4;
    private RaycastHit2D ponto5;
    private RaycastHit2D ponto1D;
    private RaycastHit2D ponto2D;
    private RaycastHit2D ponto3D;
    private RaycastHit2D ponto4D;
    private RaycastHit2D ponto5D;
    private float distancia = 1;
    private int layer = 1 << 8;
    private int layer1 = 1 << 9;
    private int layer2 = 1 << 13;
    private int layer3 = 1 << 14;
    private int layer4 = 1 << 15;
    private int layer5 = 1 << 16;
    public float pathprogress = 0;
    public float CD;
    private float ShootInterval = 7.1f;
    private float rdShootInterval = 0.5f;
    public float rd = 0;
    public float rdCD;
    private float speed = 3;
    public bool Moved = false;
    public bool Moving = false;
    public float AnimationTimer = 0.7f;
    public bool AnimationEnded;
    public GameObject Effectprefab;
    private GameObject Effect;
    public float Reset = 0;
    public Vector3 pos;
    private Collider2D Col;
    private EnemyHp HPStats;
    private Tongue ShotStats;
    public bool Stunned = false;
    private GameObject arena;
    private Boss1arena Arenastats;
    public bool dead;
    private float deathanimtimer = 0.1f;
    private SoundPlayer _sound;
    #endregion
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Col = GetComponent<Collider2D>();
        HPStats = GetComponentInChildren<EnemyHp>();
        GameObject temp = GameObject.Find("Tongue");
        ShotStats = temp.GetComponent<Tongue>();
        arena = GameObject.Find("B1Arena");
        Arenastats = arena.GetComponent<Boss1arena>();
        _sound = GetComponent<SoundPlayer>();
    }
   

    
    void FixedUpdate()
    {
        #region Raycasts
        pos = transform.position;
        hitinfo = Physics2D.CircleCast(transform.position,  800, Vector2.up, distancia, layer);
        ponto1 = Physics2D.CircleCast(transform.position,  2000, Vector2.up, distancia, layer1);
        ponto2 = Physics2D.CircleCast(transform.position,  2000, Vector2.up, distancia, layer2);
        ponto3 = Physics2D.CircleCast(transform.position,  2000, Vector2.up, distancia, layer3);
        ponto4 = Physics2D.CircleCast(transform.position,  2000, Vector2.up, distancia, layer4);
        ponto5 = Physics2D.CircleCast(transform.position,  2000, Vector2.up, distancia, layer5);
        ponto1D = Physics2D.CircleCast(transform.position, 5, Vector2.up, distancia, layer1);
        ponto2D = Physics2D.CircleCast(transform.position, 5, Vector2.up, distancia, layer2);
        ponto3D = Physics2D.CircleCast(transform.position,  5, Vector2.up, distancia, layer3);
        ponto4D = Physics2D.CircleCast(transform.position,  5, Vector2.up, distancia, layer4);
        ponto5D = Physics2D.CircleCast(transform.position,  5, Vector2.up, distancia, layer5);
        #endregion
        #region Stun
        if (HPStats.Stunned == true) {
            pathprogress = 5;
            CD = 0;
          
        }
        #endregion
        #region Randomness
        if (Moving == false && dead == false)
        {
            CD -= Time.deltaTime;
            rdCD -= Time.deltaTime;
        }



        
        if (rdCD > 0)
        {
            if (Moving == false)
            {
                rdCD -= Time.deltaTime;
            }
        }
        if (rdCD < 0)
        {
            rd += 1;
            rdCD = rdShootInterval;
        }
        if (rd == 5)
        {
            rd = 1;
        }
        if (CD > 0)
        {
            if (Moving == false)
            {
                CD -= Time.deltaTime;
            }
        }
        if (CD < 0 && Stunned == false)
        {
            Reset = 0;
            AnimationTimer = 0.7f;
            AnimationEnded = false;
            Moved = false;            
            pathprogress = rd;
            CD = ShootInterval;
        }
        #endregion
        #region Boss Fighting Pattern
        if(HPStats.health <= 0)
        {
            dead = true;
            anim.SetBool("dead", true);
            if (deathanimtimer > 0)
            {
                deathanimtimer -= Time.deltaTime;
                rb.AddForce(-hitinfo.point * 50);
                rb.rotation += 9;
            }
        }
        if (Arenastats.isplayeronArena == true && ShotStats.Shooting == false && dead == false)
        {
            
                
           
                 if (pathprogress == 1) { 
            

                if (Moved == false && ponto1D.collider == null)
                {
                    Moving = true;
                    if (AnimationEnded == true)
                    {
                        Debug.DrawLine(transform.position, ponto1.point, Color.white, 0, false);
                        rb.AddForce(ponto1.point * speed);

                    }



                }

            }
            if (pathprogress == 2)
            {


                if (Moved == false && ponto2D.collider == null)
                {
                    Moving = true;
                    if (AnimationEnded == true)
                    {
                        Debug.DrawLine(transform.position, ponto2.point, Color.white, 0, false);
                        rb.AddForce(ponto2.point * speed);
                    }
                }



            }
            if (pathprogress == 3)
            {

                if (Moved == false && ponto3D.collider == null)
                {
                    Moving = true;
                    if (AnimationEnded == true)
                    {
                        Debug.DrawLine(transform.position, ponto3.point, Color.white, 0, false);
                        rb.AddForce(ponto3.point * speed);
                    }
                }

            }
            if (pathprogress == 4)
            {

                if (Moved == false && ponto4D.collider == null)
                {
                    Moving = true;
                    if (AnimationEnded == true)
                    {
                        Debug.DrawLine(transform.position, ponto4.point, Color.white, 0, false);
                        rb.AddForce(ponto4.point * speed);
                    }
                }


            }
            if (pathprogress == 5)
            {

                if (Moved == false && ponto5D.collider == null)
                {
                    Moving = true;
                    if (AnimationEnded == true)
                    {
                        Debug.DrawLine(transform.position, ponto5.point, Color.white, 0, false);
                        rb.AddForce(ponto5.point * speed);
                    }
                }
            }
        }
                if (pathprogress == 1 && ponto1D.collider != null || pathprogress == 2 && ponto2D.collider != null || pathprogress == 3 && ponto3D.collider != null || pathprogress == 4 && ponto4D.collider != null || pathprogress == 5 && ponto5D.collider != null )
            {
                
                Moved = true;
                Moving = false;
                AnimationEnded = false;
                if (Reset == 0)
                {
                    CD = ShootInterval;
                    AnimationTimer = 0.7f;
                    Reset = 1;
                }

            }
            if (Moved == true)
            {
            if (b == 1)
            {
                _sound.StopSound();
                b = 2;
            }
                Col.enabled = true;
                anim.SetBool("OutHole", true);
                AnimationTimer -= Time.deltaTime;
                if (AnimationTimer <= 0)
                {
                    AnimationEnded = true;
                    anim.SetBool("OutHole", false);


                }
        }
        else
        {
            b = 1;
        }

            
                if (Moving == true && dead == false)
                {
                Col.enabled = false;
                anim.SetBool("InHole", true);
                    AnimationTimer -= Time.deltaTime;
                    if (AnimationTimer <= 0)
                    {
                        AnimationEnded = true;
                        anim.SetBool("InHole", false);
                        
                    
                    }

                }
                if(Moving == true && AnimationEnded == true)
            {           
            if(a == 1){
               // _sound.PlaySound(6, false,1,0, true,3);
                a = 2;
            }
                Effect = Instantiate(Effectprefab, transform.position, Quaternion.identity) as GameObject;
            Destroy(Effect, 0.3f);
            anim.SetBool("Digging", true);
            }
            else
            {
            a = 1;
                anim.SetBool("Digging", false);
            }
            if(pathprogress == 5 && Moved == true && HPStats.Stunned == true)
            {
                Stunned = true;
            }
            else { Stunned = false; }




            }
            #endregion

        }
    


