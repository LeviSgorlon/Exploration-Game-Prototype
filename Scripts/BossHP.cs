using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHP : MonoBehaviour {

    private GameObject CurrentBoss;
    private EnemyHp BossHpstats;
    private GameObject Arena1;
    private GameObject Arena2;
    private GameObject Arena3;
    private GameObject Arena4;
    public GameObject HPbar;
    private Boss1arena Arena1Stats;
    private Boss1arena Arena2Stats;
    private Boss1arena Arena3Stats;
    private Boss1arena Arena4Stats;
    private GameObject Arena1cam;
    private GameObject Arena2cam;
    private GameObject Arena3cam;
    private GameObject Arena4cam;
    public Transform _transform;
    private Animator _anim;
    private Vector3 hpscale;
    public float bosshp;
    public bool BarON;
    private GameObject camera1;
    private SmoothFollow poscam;
    public Transform Boss1campos;
    private GameObject Player;
   
    // Use this for initialization
    void Start()
    {
        Arena1 = GameObject.Find("B1Arena");
        Arena2 = GameObject.Find("B2Arena");
        Arena3 = GameObject.Find("B3Arena");
        Arena4 = GameObject.Find("B4Arena");
        HPbar = GameObject.Find("BossBar");
        camera1 = GameObject.Find("Camera");
        poscam = camera1.GetComponent<SmoothFollow>();
        Arena1cam = GameObject.Find("Bos1arenacampos");
        Arena2cam = GameObject.Find("Bos2arenacampos");
        Arena3cam = GameObject.Find("Bos3arenacampos");
        Arena4cam = GameObject.Find("Bos4arenacampos");
        _transform = HPbar.GetComponent<Transform>();
        Arena1Stats = Arena1.GetComponent<Boss1arena>();
        Arena2Stats = Arena2.GetComponent<Boss1arena>();
        Arena3Stats = Arena3.GetComponent<Boss1arena>();
        Player = GameObject.Find("Player");

        _anim = GetComponent<Animator>();
       
    }

    // Update is called once per frame
    void Update()
    {
        if(BarON == false)
        {
           // poscam.target = Player.transform;
        }
        if (Arena1Stats.isplayeronArena == true) {
            CurrentBoss = GameObject.Find("Boss1");
            if (CurrentBoss != null)
            {
                BossHpstats = CurrentBoss.GetComponent<EnemyHp>();
                BarON = true;
                poscam.target = Arena1.transform;
            }
           
        }
        else { BarON = false; }
        if (Arena2Stats.isplayeronArena == true)
        {
            CurrentBoss = GameObject.Find("Boss2");
            if (CurrentBoss != null)
            {
                BossHpstats = CurrentBoss.GetComponent<EnemyHp>();
                BarON = true;
                poscam.target = Arena2cam.transform;
            }
        }
        if (Arena3Stats.isplayeronArena == true)
        {
            CurrentBoss = GameObject.Find("Boss3");
            if (CurrentBoss != null)
            {
                BossHpstats = CurrentBoss.GetComponent<EnemyHp>();
                BarON = true;
                poscam.target = Arena3cam.transform;
            }
        }


        if (BarON == true)
        {
            _anim.SetBool("off", false);
            if (BossHpstats.health >= 0) {
                bosshp = (BossHpstats.health / 100);
                hpscale = new Vector3(bosshp, 1, 0);
                _transform.localScale = hpscale;
            }
        }
        if(BarON == false || BossHpstats.health <= 0)
        {
            _anim.SetBool("off", true);
        }







    }
}



