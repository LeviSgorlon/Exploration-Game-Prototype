using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour {

    private SoundPlayer _sound;
    public Rigidbody2D _rb;
    private GameObject Agressor;
    public Vector3 pos1;
    public string AgressorName;
    public float velocidade;
    public Renderer rend;
    public Material Dano;
    public Material Padrao;
    public bool hited;
    public int hitsound;
    public int crushsound;
    public int slidesound;
    public float damageflashtime;
    public int hitsoundrange;
    public int crushsoundrange;
    public int slidesoundrange;

    public float VKamount;
    float i2;
    float o;
    public float f;
    public bool jumping;
    public bool descending;
    public bool Vbounce;
    public int i;
    public GameObject spritemain;
    public SoundPlayer Sounds;
    private GameObject Effect;
    public GameObject BounceEffect;

  
    public void Damage(int dmg)
    {         
       
        hited = true;
        //_sound.PlaySound(hitsound, true,1,0.6f, false, hitsoundrange);
       
        
    }
   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (velocidade >= 90 | velocidade <= -90)
        {
            hited = true;
        }
       // _sound.PlaySound(hitsound, true,1,0.6f, false,hitsoundrange);
           
    }
    // Use this for initialization
    void Start () {
        _sound = GetComponent<SoundPlayer>();
        _rb = GetComponent<Rigidbody2D>();
        rend = GetComponentInChildren<Renderer>();
        Padrao = rend.material;
        damageflashtime = 0.1f;
    }
    private void Update()
    {
       

        velocidade = _rb.velocity.x + _rb.velocity.y;
        if(hited == true)
        {
            rend.material = Dano;

            damageflashtime -= Time.deltaTime;
            if (damageflashtime < 0)
            {

                rend.material = Padrao;
                damageflashtime = 0.1f;
                hited = false;
            }
        }
    }


}
