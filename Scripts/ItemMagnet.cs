using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemMagnet : MonoBehaviour
{
    // Start is called before the first frame update
    public string Agressorname;
    public string AgressornameID;
    public EnemyHp agressorhpstats;
    public string chosen = null;
    public int enemycount;

    public Transform chosentra;
    public Targetdetector left;
    public Targetdetector right;
    public Targetdetector down;
    public Targetdetector up;
    public Targetdetector upleft;
    public Targetdetector downleft;
    public Targetdetector upright;
    public Targetdetector downright;
    
    public PlayerController playerstats;
    public string previousname;
    public GameObject Effect;
    
    public GameObject Effectprefab;
    public int fuckhandler;
    public float timer;
    public float pos1;
    public float pos2;
    public float distance;
    public Transform direction;
    Quaternion Rot;
    float Angle;
    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.tag == "Weapon" && enemycount == 0)
        {
            
            Agressorname = col.transform.name;
            AgressornameID = col.transform.parent.name;




            if (enemycount == 0)
            {

                chosen = Agressorname;
                chosentra = chosentra = GameObject.Find(AgressornameID + "/" + chosen).transform;
                enemycount = 1;

            }
            else
            {

                chosen = null;
                chosentra = null;





            }

            
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            Agressorname = previousname;
            if (AgressornameID != null)
            {
                previousname = AgressornameID;
            }
            enemycount = 0;
           
            
           
            fuckhandler = 0;
            transform.root.GetComponent<PlayerController>().closetograb = false;
        }

        if (enemycount == 0 && distance <= 60 && chosen != "") { 
        chosentra = GameObject.Find(chosen).transform;
    }

    }
    void FixedUpdate()
    {
        if (chosentra != null)
        {
            Vector2 Dir = (chosentra.position - transform.position);

            Angle = Mathf.Atan2(Dir.y, Dir.x) * Mathf.Rad2Deg - 90f;
        }

        direction.gameObject.GetComponent<Rigidbody2D>().rotation = Angle;
        if (chosentra != null)
        {
            distance = Vector3.Distance(chosentra.position, transform.position);
        }
       
        if (Input.GetKey(KeyCode.E) && chosen != null && transform.root.GetComponent<PlayerController>().Armed == false && fuckhandler == 0)
        {
            
            transform.root.GetComponent<Rigidbody2D>().AddForce((direction.up) * 2000000);
        }


        
        if (chosentra != null)
        {
            if (distance >= 80)
            {
                enemycount = 0;
                chosen = null;
                chosentra = null;
                Agressorname = null;
                AgressornameID = null;
                previousname = null;
            }
        
        if (distance <= 20)
        {

            transform.root.GetComponent<PlayerController>().closetograb = true;
                fuckhandler = 1;
            }
    }
        }
        void Start()
        {


            //target
            left = transform.root.Find("ItemMagnet/left").GetComponent<Targetdetector>();
            right = transform.root.Find("ItemMagnet/right").GetComponent<Targetdetector>();
            down = transform.root.Find("ItemMagnet/down").GetComponent<Targetdetector>();
            up = transform.root.Find("ItemMagnet/up").GetComponent<Targetdetector>();
            upleft = transform.root.Find("ItemMagnet/up left").GetComponent<Targetdetector>();
            downleft = transform.root.Find("ItemMagnet/down left").GetComponent<Targetdetector>();
            upright = transform.root.Find("ItemMagnet/up right").GetComponent<Targetdetector>();
            downright = transform.root.Find("ItemMagnet/down right").GetComponent<Targetdetector>();

        //target
        if (transform.root.name.Contains("Player"))
        {
            playerstats = transform.root.gameObject.GetComponent<PlayerController>();
        }
    }
    }

