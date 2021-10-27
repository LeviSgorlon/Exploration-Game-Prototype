using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetEnemyChooser : MonoBehaviour
{

    public string Agressorname;
    public GenericStats agressorhpstats;
    public string chosen = null;
    public int enemycount;

    public Transform chosentra;
    private Targetdetector left;
    private Targetdetector right;
    private Targetdetector down;
    private Targetdetector up;
    private Targetdetector upleft;
    private Targetdetector downleft;
    private Targetdetector upright;
    private Targetdetector downright;
    public GenericInput playerstats;
    public string previousname;
    public GameObject Effect;
    public GameObject Effectprefab;
    public int fuckhandler;
    private GenericStats Enemyx;
    public float timer;
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Enemy")
        {
            Enemyx = col.gameObject.GetComponent<GenericStats>();
        }
        if (col.tag == "Enemy" && enemycount == 0 && col.name != previousname && Enemyx != null)
        {
                left.ON = false;
                right.ON = false;
                up.ON = false;
                down.ON = false;
                upleft.ON = false;
                downleft.ON = false;
                upright.ON = false;
                downright.ON = false;
                Agressorname = col.name;
                agressorhpstats = GameObject.Find(Agressorname).GetComponent<GenericStats>();
            if (agressorhpstats != null)
            {
                if (enemycount == 0)
                {

                    chosen = Agressorname;
                    chosentra = GameObject.Find(chosen).transform;
                    enemycount = 1;
                }

            }
           


        }
    }

    void Awake()
    {
        //target
        left = transform.Find("left").GetComponent<Targetdetector>();
        right = transform.Find("right").GetComponent<Targetdetector>();
        down = transform.Find("down").GetComponent<Targetdetector>();
        up = transform.Find("up").GetComponent<Targetdetector>();
        upleft = transform.Find("up left").GetComponent<Targetdetector>();
        downleft = transform.Find("down left").GetComponent<Targetdetector>();
        upright = transform.Find("up right").GetComponent<Targetdetector>();
        downright = transform.Find("down right").GetComponent<Targetdetector>();

        //target
        playerstats = transform.root.GetComponent<GenericInput>();
    }

    // Update is called once per frame
    void Update()
    {
        if(timer <= 0)
        {
            playerstats.Target = false;
        }
        if (agressorhpstats.health <= 0)
        {
            timer -= Time.deltaTime;
        }
        else
        {
            
            timer = 0.1f;
        }
        if (Agressorname != null && agressorhpstats != null)
        {


            if (agressorhpstats.health <= 0 | playerstats.Target == false | Input.GetKeyDown(KeyCode.H))
            {
                previousname = chosen;
                Agressorname = previousname;
                agressorhpstats = GameObject.Find(Agressorname).GetComponent<GenericStats>();
               
                enemycount = 0;
                
                
                fuckhandler = 0;
                if(agressorhpstats.health <= 0) {
                    Destroy(Effect, 2);
                    
                } else
                {
                Destroy(Effect);
                }
                




                
               

            }
            if (playerstats.Target == true && fuckhandler == 0)
            {
                Effect = Instantiate(Effectprefab, chosentra.localPosition, Quaternion.identity, chosentra) as GameObject;
                fuckhandler = 1;


            }

            if (Effect != null)
            {
                Effect.transform.parent = GameObject.Find(chosen).transform;
                Effect.transform.localPosition = new Vector3(-30, -16, 0);
            }

        }
    }

}

