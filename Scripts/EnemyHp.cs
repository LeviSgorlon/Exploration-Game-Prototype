using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EnemyHp : MonoBehaviour
{
    //Mesmo este script ter o nome de Enemyhp ele só sera usado no Boss 1 pois possui alteraçoes de variaveis diretas
    //Arrumado,agora funciona pra qualquer npc ou monstro,pois nao usa mais alteraçoes de variaveis diretas
    public float health;
    public float fullhealth;
    public Animator _animator;
    public bool blocking;
    public bool Stunned = false;
    public float stunTime = 6;
    public int intensidade;
    
   
    public Vector3 pos1;
    
    private SoundPlayer _sound;
    public Renderer[] rend;
    public Material Dano;
    public Material Padrao;
    public bool hited;
    public int hitsound;
    public int Stunsound;
    public int deathsound;
    public int tauntsound;
    public int hitsoundrange;
    public int fuckhandler4;
    public int Stunsoundrange;
    public int deathsoundrange;
    public int tauntsoundrange;
    public float damageflashtime;
    public Rigidbody2D[] allObjects;
    public PlayerController player;
    private GameObject Effect;
    public GameObject Damagenumbers;
    private GameObject Effect2;
    public GameObject Deathburst;
    public int fuckhandler2;
    public int fuckhandler3;
    public float dmgcd;
    public int numberofhits;
    public TargetEnemyChooser target;
    public float i;
    public Rigidbody2D EnemyOri;
    public Vector3[] Speeds;
    public bool bufferdamage;
    public float bufferdamageamout;
    public float buffertime;

    public bool pushnow;

    public SoundPlayer Sounds;
    public float VKamount;
    float i2;
    float o;
    public float f;
    public bool jumping;
    public bool descending;
    public bool Vbounce;
    float angle;
    Vector2 lookdir;


    public float Kamount;
    public GameObject BounceEffect;

    public GameObject HitEffect;
    public GameObject spritemain;
    public Quaternion rotatoion;
    void Start()
    {
        

        numberofhits = 1;        
        _animator = GetComponent<Animator>();
        
        _sound = GetComponent<SoundPlayer>();
        
        rend = GetComponentsInChildren<Renderer>();
        damageflashtime = 0.1f;
        Padrao = rend[0].material;
        
    }
   


   


    public void Damage(float dmg)
    {
        if (numberofhits == 1)
        {
            i = 0;
            health -= dmg;
            
            
            hited = true;

            Effect = Instantiate(Damagenumbers, transform.position, Quaternion.identity) as GameObject;
            Effect.GetComponentInChildren<TextMeshPro>().text = ("" + dmg.ToString("f0"));
            Destroy(Effect, 1);

            
            GameObject.Find("Camera").gameObject.GetComponent<EZCameraShake.CameraShaker>().DefaultPosInfluence = new Vector3((dmg/10), (dmg/10), 0);
            GameObject.Find("Camera").gameObject.GetComponent<Camerashakeractivator>().Activate = true;


           // _sound.PlaySound(hitsound, true,1,0, true,hitsoundrange);
            numberofhits = 0;
            

        }
       
        if (health < 0)
        {
            return;
        }

    }
    public void FreezeAmount(float Amount)
    {
        
        
        if(Amount > 0)
        {
            GameObject.Find("ScreenFreezer").GetComponent<ScreenFreezer>().StopTime(0.05f, 50, Amount);
            Effect = Instantiate(HitEffect, null, true);
            Effect.transform.position = transform.position;
        }
    }
    public void EDamage(int dmg)
    {
        
        //_sound.PlaySound(Stunsound, true,1,0, true,Stunsoundrange);
            health -= dmg;
            hited = true;
            Stunned = true;
            if (health < 0)
            {
                return;
            }
        
    }
    // Use this for initialization
    public float LAmount;
    public float time;
    // Update is called once per frame
    void Update()
    {
        if (numberofhits == 0)
        {
            buffertime -= Time.deltaTime;
            if (buffertime <= 0)
            {
                numberofhits = 1;
            }
        }
        else { buffertime = 0.2f; }
    }
    void FixedUpdate()
    {
        
       
        




        if (health <= 0 && fuckhandler3 == 0 && Time.timeScale == 1)
        {
            Effect2 = Instantiate(Deathburst, transform.position, Quaternion.identity) as GameObject;
            Effect2.transform.SetParent(transform);
            Destroy(Effect2, 10);
            Effect2.transform.SetParent(null);
            
            

            gameObject.GetComponentInChildren<SpriteRenderer>().enabled = false;
            gameObject.GetComponentInChildren<Collider>().enabled = false;
            Destroy(gameObject, 4);
            fuckhandler3 = 1;
            player.target = false;
        }
        if(health > 0)
        {
            fuckhandler3 = 0;
        }
        if (health > fullhealth)
        {
            health = fullhealth;
        }
        if (Stunned == true)
        {
            stunTime -= Time.deltaTime;
            if(stunTime <= 0)
            {
                Stunned = false;
                stunTime = 6;
            }
        }

        if (hited == true)
        {
            
            foreach (Renderer light in rend)
            {
                if (
            light.transform.tag != ("Hud"))
                {
                    light.material = Dano;
                }
               
            }
           
                damageflashtime -= Time.deltaTime;
            
            if (damageflashtime < 0 && Time.timeScale == 1)
            {

                foreach (Renderer light in rend)
                {
                    if (
            light.transform.tag != ("Hud"))
                    {
                        light.material = Padrao;
                        damageflashtime -= Time.deltaTime;
                    }
                }
                
                damageflashtime = 0.1f;
                
                hited = false;
            }
        
    }
        
        
        
       
    }
 
    
  

}
