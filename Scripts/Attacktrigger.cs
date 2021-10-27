using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Attacktrigger : MonoBehaviour
{
    public float dmg = 2;
    public float dmgmodulator;
    public string Taginimiga;
    private GameObject player;
    public int fuckhandler2;
    
    public GameObject Effectprefab;
    private GameObject Effect;
    public GameObject Damagenumbers;
    public float cd = 0.1f;
    private Vector3 pos;
    private Collider2D colisor;
    public string Nome;
    private Transform top;
    public int tocou;
    private SoundPlayer som;
    public int hitsound;
    public int hitsoundrange;
    public int objecthitsound;
    public int objecthitsoundrange;
    public bool Timedilation;
    public int numberofhits;
    
    public float velocidade;
    public WaitForSecondsRealtime timewait;
    public Rigidbody2D rb;
    public PointEffector2D[] SwordP;
    public int o;
    public float returntimer;
    public float HKamount;
    public float Famount;
    public float VKamount;
    // Use this for initialization
    void Awake()
    {
        numberofhits = 1;
        som = transform.root.GetComponent<SoundPlayer>();
        Nome = gameObject.transform.root.name;
        
        rb = GetComponentInParent<Rigidbody2D>();
    }
    float i;
     

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.name != gameObject.name )
        {

            if (col.gameObject.tag == Taginimiga)
            {
                
                col.GetComponent<EnemyHp>().numberofhits = 1;
            
            }
        }



        }
    
        void OnTriggerEnter(Collider col)
    {

        
        if (col.isTrigger != true )
        {
            if (Taginimiga == "ALL")
            {
                if (col.tag == "Weapon" | col.tag == "Enemy" | col.tag == "Killable" | col.tag == "Object")
                {
                    if (som != null)
                    {
                        //som.PlaySound(hitsound, true, 0.5f, 0.2f, true, hitsoundrange);
                    }
                        col.SendMessageUpwards("Agressornm", Nome);
                    col.SendMessageUpwards("Damage", dmg);
                    if (gameObject.transform.root.name == "Player")
                    {

                        Effect = Instantiate(Effectprefab, col.transform.position, Quaternion.identity) as GameObject;
                        Destroy(Effect, 3f);
                    }
                    col.SendMessageUpwards("VKnockBack", VKamount);
                    col.SendMessageUpwards("KnockBack", HKamount);
                }
                if(col.tag == "Cenario")
                {
                  //  som.PlaySound(objecthitsound, true, 0.5f, 0.2f, true, objecthitsoundrange);


                    Effect = Instantiate(Effectprefab, transform.position, Quaternion.identity) as GameObject;
                    Destroy(Effect, 3f);
                }
            }
            o = 0;
            //sword goes numb when hitting other sword(Player)
            if ( transform.root.name.Contains("Player"))
            {
                if (col.tag == "Weapon")
                {
                    SwordP = transform.root.GetComponentsInChildren<PointEffector2D>();
                    foreach (PointEffector2D P in SwordP)
                    {
                        SwordP[o].forceMagnitude = 0;
                        o += 1;

                    }
                    returntimer = 2;
                    numberofhits = 0;
                }
            }
            #region Throed Weapon
            //WeaponThrow (can damage self)
            if (transform.root.name.Contains("WeaponShell") )
            {
                
                if (col.gameObject.tag == Taginimiga)
                {
                    //Player Hits Enemy
                    if (col.gameObject.tag == "Enemy") { 
                    if (numberofhits == 1)
                    {
                        col.SendMessageUpwards("Agressornm", Nome);

                        col.GetComponent<EnemyHp>().player = null;

                        i = 0.1f;
                        pos = transform.position;

                        col.SendMessageUpwards("KnockBack", HKamount);
                        col.SendMessageUpwards("VKnockBack", VKamount);

                        col.SendMessageUpwards("Damage", dmg);






                        numberofhits = 0;
                    }
                }
                    //Enemy hits Player
                    if (col.gameObject.tag == "Killable")
                    {
                        if (numberofhits == 1)
                        {
                            col.SendMessageUpwards("Agressornm", Nome);

                            

                            i = 0.1f;
                            pos = transform.position;

                            col.SendMessageUpwards("KnockBack", HKamount);
                            col.SendMessageUpwards("VKnockBack", VKamount);

                            col.SendMessageUpwards("Damage", dmg);






                            numberofhits = 0;
                        }
                    }
                }
            }
            #endregion
            else
            {
                //people cant hit themselfs with their on attacks
                if ( col.gameObject.name != transform.parent.name)
                {

                    if (col.gameObject.tag == Taginimiga)
                    {



                        col.SendMessageUpwards("Agressornm", Nome);
                        i = 0.1f;
                        pos = transform.position;
                        col.SendMessageUpwards("KnockBack", HKamount);
                        col.SendMessageUpwards("Damage", dmg);
                        if (Famount > 0f) { 
                        col.SendMessageUpwards("FreezeAmount", Famount);
                    }
                        col.SendMessageUpwards("VKnockBack", VKamount);
                        



                    }



                }
            }
            //everybody can hit objects
            if (col.gameObject.tag == "Object" )
            {
                if (som != null)
                {
                    //som.PlaySound(hitsound, true, 0.5f, 0.2f, true, hitsoundrange);
                }
                col.SendMessageUpwards("Agressornm", Nome);
                col.SendMessageUpwards("Damage", dmg);
                if (gameObject.transform.root.name == "Player")
                {
                   
                    Effect = Instantiate(Effectprefab, col.transform.position, Quaternion.identity) as GameObject;
                    Destroy(Effect, 3f);
                }
                col.SendMessageUpwards("VKnockBack", VKamount);
                col.SendMessageUpwards("KnockBack", HKamount);

            }
            if (col.gameObject.tag == "Cenario" | col.gameObject.tag == "Weapon")
            {
                if (som != null)
                {
                    //som.PlaySound(objecthitsound, true, 0.5f, 0.2f, true, objecthitsoundrange);
                }
                    
                    Effect = Instantiate(Effectprefab, transform.position, Quaternion.identity) as GameObject;
                    Destroy(Effect, 3f);
                
            }
            }
        else
        {
            return;
        }

        
        }
    public float framedelay = 0.005f;
    public float fuckhandler;

    private void Update()
    {

        Nome = gameObject.transform.root.name;
        returntimer -= Time.deltaTime;
        if (returntimer <= 0 && transform.root.name.Contains("Player") | transform.root.name.Contains("Armed"))
        {
            transform.root.Find("swordatractor").GetComponent<PointEffector2D>().forceMagnitude = -600;
            transform.root.Find("swordatractor (1)").GetComponent<PointEffector2D>().forceMagnitude = 300;
            transform.root.Find("swordatractor (2)").GetComponent<PointEffector2D>().forceMagnitude = -5000;
        }

        Nome = gameObject.transform.root.name;
        if (Nome.Contains("Player"))
        {
            numberofhits = 1;
        }


        i -= Time.deltaTime;
        if (rb != null) { 
        if (rb.velocity.x < 0 && rb.velocity.y > 0)
        {
            velocidade = (rb.velocity.x * -1) + rb.velocity.y;
        }
        if (rb.velocity.x > 0 && rb.velocity.y > 0)
        {
            velocidade = rb.velocity.x + rb.velocity.y;
        }
        if (rb.velocity.y < 0 && rb.velocity.x > 0)
        {
            velocidade = rb.velocity.x + (rb.velocity.y * -1);
        }
        if (rb.velocity.y < 0 && rb.velocity.x < 0)
        {
            velocidade = (rb.velocity.x * -1) + (rb.velocity.y * -1);
        }


        dmg = dmgmodulator + (velocidade / 100);
        }
        else
        {
            dmg = dmgmodulator;
        }
    }


}
