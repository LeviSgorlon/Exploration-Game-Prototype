using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HP : MonoBehaviour {
    public int health = 5;
    public Animator _animator;
    public Rigidbody2D _rb;
    public bool blocking;
    public bool Stunned = false;
    public float stunTime = 6f;
    public int intensidade;
    private GameObject Agressor;
    public Vector3 pos1;
    public string AgressorName;
    public bool dead = false;
    public bool hited;
    public float Animtimer1 = 0.1f;
    public GameObject player;
    public PlayerController playerC;
    public PlayerController controlador;
    public SoundPlayer _sound;
    int a = 0;
    public float dmgcd;
    public Renderer[] rend;
    public Material Dano;
    public Material Padrao;
    public float damageflashtime;
    public int hitsound;
    public int Stunsound;
    public int deathsound;
    public int tauntsound;
    public int hitsoundrange;
    public int Stunsoundrange;
    public int deathsoundrange;
    public int tauntsoundrange;
    
    public int fuckhandler2;
    public int fuckhandler3;
    public int fuckhandler4;
    public float i;

    public float Freezetime;
    public bool Frozen;
    public int fuckhandler;

    public Rigidbody2D PlayerOri;

    public float LAmount;
    private GameObject Effect;
    public SoundPlayer Sounds;
    public float VKamount;
    float i2;
    float o;
    public float f;
    public bool jumping;
    public bool descending;
    public bool Vbounce;
    public GameObject HitEffect;
    public GameObject BounceEffect;

    public GameObject spritemain;
    public void Agressornm(string TempAgressorname)
    {
        Agressor = GameObject.Find(TempAgressorname);
        AgressorName = TempAgressorname;
       
    }
    public void Damage(int dmg)
    {

        if (blocking == false && dmgcd <= 0)
        {
            if (Agressor != null)
            {
                dmgcd = 0.5f;              
                hited = true;
                for (int i = 1; i < rend.Length; i++)
                {
                    if (rend[i].gameObject.tag != "Hudp")
                    {
                        rend[i].material = Dano;
                    }
                }
                health -= dmg;
                pos1 = Agressor.transform.position;
                
               // _sound.PlaySound(hitsound, true,1,0, true,hitsoundrange);
                
            }
           




            if (health < 0)
            {
                pos1 = Agressor.transform.position;
                _rb.AddForce((transform.position - pos1) * (90 * 40) *_rb.mass );
                return;
            }
        }
    }

   

    // Use this for initialization
    void Start () {
        
        _animator = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody2D>();
        _sound = GetComponent<SoundPlayer>();
        rend = GetComponentsInChildren<Renderer>();
        Padrao = rend[3].material;
        




    }
	
	// Update is called once per frame
	void FixedUpdate () {
        Vector2 lookdir = transform.position - pos1;
        float angle = Mathf.Atan2(lookdir.y, lookdir.x) * Mathf.Rad2Deg - 90f;
       
        if (dmgcd >= 0)
        {
            dmgcd -= Time.deltaTime;
        }
        if (hited == true)
        {
            Animtimer1 -= Time.deltaTime;
            if(Animtimer1 <= 0)
            {
                for (int i = 1; i < rend.Length; i++)
                {
                    if (rend[i].gameObject.tag != "Hudp")
                    {

                        rend[i].material = Padrao;
                    }
                }
                Animtimer1 = 0.1f;
                hited = false;
            }
        }
        if(health <= 0)
        {
            _animator.SetBool("Dead", true);
            dead = true;           
            if(a == 0)
            {
               // _sound.PlaySound(deathsound, true,1,0, true, deathsoundrange);
                a = 1;
            }
        }
        else { _animator.SetBool("Dead", false);
            dead = false;
        }
        if (Stunned == true)
        {
            stunTime -= Time.deltaTime;
            if (stunTime <= 0)
            {
                Stunned = false;
                stunTime = 6f;
            }
        }
        if (Input.GetMouseButton(1))
        {
            blocking = true;
            
        }
        else
        {
            blocking = false;
            
        }
       
    }

    public void Update()
    {
       



        playerC = transform.root.GetComponent<PlayerController>();
    }

   
    public void FreezeAmount(float Amount)
    {
        
        
        if (Amount > 0)
        {
            GameObject.Find("ScreenFreezer").GetComponent<ScreenFreezer>().StopTime(0.05f, 50, Amount);
            Effect = Instantiate(HitEffect, null, true);
            Effect.transform.position = transform.position;
        }
    }

}
