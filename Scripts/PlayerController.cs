using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerController : MonoBehaviour
{
    #region Variaveis
    public bool idle;
    public bool got;

    public float speed;

    bool rolldown;
    bool rollup;
    bool rollleft;
    bool rollright;

    bool rollupleft = false;
    bool rollupright = false;
    bool rolldownright = false;
    bool rolldownleft = false;
    float mult;

    private Animator _animator;
    public Rigidbody rb;
    public bool Running = false;
    public float movimento = 0;
    private float velocidade;
    private float velocidadepadrao = 150000;
    private bool walkingup = false;
    private bool walkingdown = false;
    private bool walkingleft = false;
    private bool walkingright = false;
    
    float AnimationTimer;   
    public float ACD = 1f;
    float AnimationTimer2;
    public float ACD2 = 1f;

    public bool Atkright = false;
    public bool Atkleft = false;
    public bool Atkup = false;
    public bool Atkdown = false;
    public bool Atkupleft;
    public bool Atkupright;
   
    private Collider AttackCol;
    private Collider AttackColSweet;
    public GameObject Attacktrig;
   
    public bool closetograb;
    public bool Atacking;
   
    public Attacktrigger Dmgstats;
    
    public bool Charging = false;
    private float RChargeinit = 0.2f;
    private float RChargereset = 0.2f;
    private bool OnChargeAtkAnim;
    private float LChargeinit = 0.2f;
    private float LChargereset = 0.2f;

    private float UChargeinit = 0.2f;
    private float UChargereset = 0.2f;

    private float DChargeinit = 0.2f;
    private float DChargereset = 0.2f;
    private bool RChargeatk;
    private bool LChargeatk;
    private bool DChargeatk;
    private bool UChargeatk;

    public bool target;
    public Vector3 pos;
    private GameObject camera1;
    private SmoothFollow camstats;
    public bool PlayerOnControl = false;
    private SoundPlayer Sounds;
    private SoundPlayer Sounds2;
    public float Stepinterval;
    public float StepintervalCD;
    private HP Hpstats;
    public bool throwing;
    public bool Push = false;
    //target
    public Targetdetector left;
    public Targetdetector right;
    public Targetdetector down;
    public Targetdetector up;
    public Targetdetector upleft;
    public Targetdetector downleft;
    public Targetdetector upright;
    public Targetdetector downright;
    public TargetEnemyChooser targetstats;
    public float actualspeed;

    public bool fuckhandler;

    public float jumpingCd;

        public bool Iupleft;
        public bool Iupright;
        public bool Idownright;
        public bool Idownleft;

    public bool Iup;
    public bool Iright;
    public bool Idown;
    public bool Ileft;

    public bool faceright;
    public bool faceleft;
    public bool faceup;
    public bool facedown;
    public bool facerightup;
    public bool faceleftup;
    public bool facerightdown;
    public bool faceleftdown;
    public float targettimer;
    
    public float runouttimer;
    public float timer9;
    public float i;
    public bool descending;
    public bool jumping;
    public CircleCollider2D Pcol;
    public float o;
    public float iR;
    public GameObject spritemain;
    public bool headslam;
    public Vector3 headslamdirection;
    public float headslamangle;
    public float Ptimer;
    public HingeJoint2D attackcenter;
    public BoxCollider2D SwordCollider;

    bool a;
    bool b;

    private bool Atkdownright;
    private bool Atkdownleft;
    public bool craw;
    public int crawlimiter;
    public bool jumpengage;
    public int jumpfuckhandler;
    public bool jumpafter;
    public ParticleSystem SwordTrail;
    public ParticleSystem RunningDust;
    public GameObject Attackcenter;
    public GameObject Dustslam;
    public bool Armed;
    public Pickupzone PickupZone;
    public float Timeuntilatk;
    private ParticleSystem.EmissionModule EM;
    private float timeuntilturn;
    private float atkresettime;
    public ParticleSystem.EmissionModule Runningemit;
    public float turntime;
    public bool backflip;
    public int soundlimit;
    private int flurry;
    private float fsoundinterval;

    public float crawlpicktime;
    public bool crawlfuck;

    private bool equip;

    public float ThrowCd;

    public ItemMagnet MagnetStats;
    public Transform GlobalOrientation;

    public GroundCheck GCheck;
    public float DustCD;

    private GravityCode gravity;
    public Vector3 VelocityVector;

    //target

    #endregion
    //Void awake
    void Awake()
    {PlayerOnControl = false;
        crawlpicktime = 0.2f;
        AttackCol = Attacktrig.GetComponent<Collider>();
        Dmgstats = Attacktrig.GetComponent<Attacktrigger>();
        _animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody>();
        rb.drag = 8f;
        gravity = GetComponent<GravityCode>();
        camera1 = GameObject.Find("CameraNest");
        camstats = camera1.GetComponent<SmoothFollow>();
        Sounds = GetComponent<SoundPlayer>();
        Sounds2 = transform.Find("Body").GetComponent<SoundPlayer>();
        Hpstats = GetComponent<HP>();
        Pcol = GetComponent<CircleCollider2D>();
        ThrowCd = 0.5f;

    }
    public float freezetimer;
    public bool AtkAnimWindow;
    int headslamlimiter;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == ("Cenario") && velocidade >= 300 && Running == true)
        {
            headslam = true;
            Dustslam.GetComponent<ParticleSystem>().emissionRate = 800;
            Ptimer = 0.5f;
        }
            
            
        
    }


    private void Update()
    {
        VelocityVector = rb.velocity;
      
        if (AtkAnimWindow == false)
        {
            _animator.SetBool("attacking", Atacking);
        }
        _animator.SetBool("charging", Charging);


        #region SwordTrail logic
        EM = SwordTrail.emission;

        if (Atacking == false)
        {
            SwordCollider.enabled = false;

            EM.rateOverDistance = 0;


        }
        else { SwordCollider.enabled = true;
            EM.rateOverDistance = 1;
        }
        #endregion
        
        #region R button reset debug mechanic
      
        if (Input.anyKeyDown && Hpstats.dead == true)
        {
            SceneManager.LoadScene("DemoScene");
        }
        #endregion

        //Freezes the player if the fucker is dead
        if (Hpstats.dead == true)
        {
            PlayerOnControl = false;
        }
        //Freezes the player if the fucker is dead

        //Freezes the player
        freezetimer -= Time.deltaTime;
        if (freezetimer >= 0)
        {
            PlayerOnControl = false;
        }
        else { PlayerOnControl = true; }
        //Freezes the player

        #region Pick up logic
        _animator.SetBool("liftup down", got);
        ThrowCd -= Time.deltaTime;
        if (PickupZone.CurrentWeapon != null)
        {
            if (PickupZone.CurrentWeapon.gameObject.name.Contains("LongSword") && Armed == true)
            {
                _animator.SetBool("UsingSwordActive", true);
                _animator.SetBool("UsingHandActive", false);

                if (equip == false)
                {
                    _animator.SetTrigger("UsingSword");
                    equip = true;
                }

            }
            if (PickupZone.CurrentWeapon.gameObject.name.Contains("BombStick") && Armed == true)
            {
                    _animator.SetBool("UsingSwordActive", true);
                    _animator.SetBool("UsingHandActive", false);

                    if (equip == false)
                    {
                        _animator.SetTrigger("UsingSword");
                        equip = true;
                    }

            }
            
        }
        if (Armed == false)
        {
            _animator.SetBool("UsingHandActive", true);
            _animator.SetBool("UsingSwordActive", false);
            if (equip == true)
            {
                _animator.SetTrigger("UsingHand");
                equip = false;
            }
        }
        if (Input.GetKeyDown(KeyCode.E) && PlayerOnControl == true)
        {
            crawlfuck = false;
            if (movimento == 0)
            {
                freezetimer = 0.2f;
            }
           
            if (got == true)
            {
                ThrowCd = 0.5f;
                throwing = true;             
                _animator.SetBool("throw down", true);
                
            }
        }
       if(closetograb == true && crawlfuck == false)
        {
            crawlpicktime = 0.2f;
            crawlfuck = true;
        }
        if (Input.GetKey(KeyCode.E) && crawlpicktime < 0.2f && MagnetStats.chosentra == null)
        {
            _animator.SetTrigger("walktrigger");
        }
        if (movimento == 1 && crawlpicktime < 0.2f)
        {
            _animator.SetTrigger("walktrigger");
            crawlpicktime = 0.2f;
        }


        if (Input.GetKey(KeyCode.E) && ThrowCd <= 0 && MagnetStats.chosen != null)
        {
            freezetimer = 0.1f;
            _animator.SetFloat("movimento", 1);
            _animator.SetBool("target", true);
            _animator.SetBool("faceleft", MagnetStats.left.ON);
            _animator.SetBool("faceright", MagnetStats.right.ON);
            _animator.SetBool("faceup", MagnetStats.up.ON);
            _animator.SetBool("facedown", MagnetStats.down.ON);
            _animator.SetBool("faceupleft", MagnetStats.upleft.ON);
            _animator.SetBool("facedownleft", MagnetStats.downleft.ON);
            _animator.SetBool("faceupright", MagnetStats.upright.ON);
            _animator.SetBool("facedownright", MagnetStats.downright.ON);
          


            if (got == false && Armed == false && closetograb == true)
            {
                crawlpicktime -= Time.deltaTime;
                _animator.SetTrigger("crawltrigger");
                if (crawlpicktime <= 0) {
                    
                    _animator.SetBool("pickupdown", true);
                    
                    _animator.SetTrigger("walktrigger");
                    
                    }
                fuckhandler = true;


            }
            else {
                _animator.SetTrigger("walktrigger");

            }
            
            if (Armed == true && fuckhandler == false)
            {
                ThrowCd = 0.5f;
                PickupZone.CurrentWeapon.SetActive(false);
                PickupZone.LastWeapon.SetActive(true);
                PickupZone.LastWeapon.GetComponent<Attacktrigger>().numberofhits = 1;
                _animator.SetBool("throw down", true);
                _animator.SetTrigger("UsingHand");
                crawlpicktime = 0.2f;
                if (Iup) {
                    PickupZone.LastWeapon.transform.localPosition = new Vector3(0, 0);
                    PickupZone.LastWeapon.transform.parent.localPosition = new Vector3(0, 15);
                    
                   
                    PickupZone.LastWeapon.transform.parent.rotation = new Quaternion(0, 0, 0, 0);
                    PickupZone.LastWeapon.transform.rotation = new Quaternion(0, 0, -0, 0);
                    PickupZone.LastWeapon.transform.parent.SetParent(null);
                    PickupZone.LastWeapon.transform.GetComponent<Rigidbody2D>().AddForce(transform.up * 150000);
            }
                if (Idown)
                {
                    PickupZone.LastWeapon.transform.localPosition = new Vector3(0, 0);
                    PickupZone.LastWeapon.transform.parent.localPosition = new Vector3(0, -15);
                    
                   
                    PickupZone.LastWeapon.transform.parent.rotation = new Quaternion(0, 0, 180, 0);
                    PickupZone.LastWeapon.transform.rotation = new Quaternion(0, 0, 0, 0);
                    PickupZone.LastWeapon.transform.parent.SetParent(null);
                    PickupZone.LastWeapon.transform.GetComponent<Rigidbody2D>().AddForce(transform.up * -150000);
                }
                if (Ileft)
                {
                    PickupZone.LastWeapon.transform.localPosition = new Vector3(0, 0);
                    PickupZone.LastWeapon.transform.parent.localPosition = new Vector3(15, 0);
                    
                   
                    PickupZone.LastWeapon.transform.parent.rotation = new Quaternion(0, 0, 90, 90);
                    PickupZone.LastWeapon.transform.rotation = new Quaternion(0, 0, 0, 0);
                    PickupZone.LastWeapon.transform.parent.SetParent(null);
                    PickupZone.LastWeapon.transform.GetComponent<Rigidbody2D>().AddForce(transform.right * -150000);
                }
                if (Iright)
                {
                    PickupZone.LastWeapon.transform.localPosition = new Vector3(0, 0);
                    PickupZone.LastWeapon.transform.parent.localPosition = new Vector3(-15, 0);
                    
                   
                    PickupZone.LastWeapon.transform.parent.rotation = new Quaternion(0, 0, -90, 90);
                    PickupZone.LastWeapon.transform.rotation = new Quaternion(0, 0, 0, 0);
                    PickupZone.LastWeapon.transform.parent.SetParent(null);
                    PickupZone.LastWeapon.transform.GetComponent<Rigidbody2D>().AddForce(transform.right * 150000);
                }
                if (got == true && fuckhandler == false )
                {
                    throwing = true;
                    _animator.SetBool("throw down", true);
                }
                Armed = false;
                got = false;
                
            }
           
        }
        if (Input.GetKeyUp(KeyCode.E) )
        {
            got = false;
            fuckhandler = false;
            _animator.SetBool("pickupdown", false);
            throwing = false;
            _animator.SetBool("throw down", false);
        }

        #endregion
        Runningemit = RunningDust.emission;

        if (backflip == true && soundlimit == 0)
        {
            //Sounds2.PlaySound(0, true, 0.6f, 0, true, 0);
            soundlimit = 1;
        }

        //animation timer
        if (Atacking == false)
        {
            AnimationTimer = ACD;
            AnimationTimer2 = ACD2;
        }
      
        AnimationTimer -= Time.deltaTime;
        AnimationTimer2 -= Time.deltaTime;
        if (AnimationTimer <= 0)
        {
            _animator.SetBool("atkdownleft", false);
            _animator.SetBool("atkright", false);
            _animator.SetBool("atkdown", false);
            _animator.SetBool("atkleft", false);
            _animator.SetBool("atkup", false);
            _animator.SetBool("atkdownright", false);
            Atacking = false;
        }
        if (PlayerOnControl == true)
        {

            #region Atk Reset when unarmed
            //atk resetor


            if (Armed == false)
            {
                if (Input.GetKey(KeyCode.O) == false)
                {
                    AtkAnimWindow = false;
                }
                if (Input.GetKey(KeyCode.O) == false && _animator.GetInteger("FlurryCount") >= 5)
                {
                    _animator.SetInteger("FlurryCount", 0);

                }
                if (_animator.GetInteger("FlurryCount") >= 5)
                {
                    fsoundinterval -= Time.deltaTime;
                    GameObject.Find("Camera").gameObject.GetComponent<Camerashakeractivator>().Activate = false;
                    GameObject.Find("Camera").gameObject.GetComponent<EZCameraShake.CameraShaker>().DefaultPosInfluence = new Vector3((2), (2), 0);
                    if (fsoundinterval <= 0)
                    {
                       // Sounds2.PlaySound(4, true, 0.7f, 0.2f, true, 3);
                        GameObject.Find("Camera").gameObject.GetComponent<Camerashakeractivator>().Activate = true;
                        fsoundinterval = 0.1f;

                    }

                }
                if (Input.GetKeyDown(KeyCode.O) && Armed ==false)
                {
                    flurry = flurry + 1;
                    _animator.SetInteger("FlurryCount", flurry);
                    Push = false;

                    Atkright = false;
                    Atkdown = false;
                    Atkdownleft = false;
                    Atkdownright = false;
                    Atkleft = false;
                    Atkup = false;
                    Atkupleft = false;
                    Atkupright = false;
                    AtkAnimWindow = true;
                    Atacking = true;


                    #region Attack Realign


                    

                    if (Idownleft == true)
                    {
                        _animator.SetBool("atkdownleft", true);

                        _animator.SetBool("atkright", false);
                        _animator.SetBool("atkdown", false);
                        _animator.SetBool("atkleft", false);
                        _animator.SetBool("atkup", false);
                        _animator.SetBool("atkup", false);
                        _animator.SetBool("atkupleft", false);
                        _animator.SetBool("atkdownright", false);
                    }
                    if (Idown == true)
                    {
                        _animator.SetBool("atkdown", true);

                        _animator.SetBool("atkright", false);
                        _animator.SetBool("atkup", false);
                        _animator.SetBool("atkupleft", false);
                        _animator.SetBool("atkleft", false);
                        _animator.SetBool("atkup", false);
                        _animator.SetBool("atkdownleft", false);
                        _animator.SetBool("atkdownright", false);
                    }
                    if (Ileft == true)
                    {
                        _animator.SetBool("atkleft", true);

                        _animator.SetBool("atkright", false);
                        _animator.SetBool("atkdown", false);
                        _animator.SetBool("atkup", false);
                        _animator.SetBool("atkupleft", false);
                        _animator.SetBool("atkup", false);
                        _animator.SetBool("atkdownleft", false);
                        _animator.SetBool("atkdownright", false);
                    }
                    if (Iright == true)
                    {
                        _animator.SetBool("atkright", true);

                        _animator.SetBool("atkleft", false);                       
                        _animator.SetBool("atkdown", false);
                        _animator.SetBool("atkup", false);
                        _animator.SetBool("atkupleft", false);
                        _animator.SetBool("atkup", false);
                        _animator.SetBool("atkdownleft", false);
                        _animator.SetBool("atkdownright", false);
                    }
                    if (Iupleft == true)
                    {
                        _animator.SetBool("atkupleft", true);

                        _animator.SetBool("atkdownleft", false);
                        _animator.SetBool("atkright", false);
                        _animator.SetBool("atkdown", false);
                        _animator.SetBool("atkleft", false);
                        _animator.SetBool("atkup", false);                        
                        _animator.SetBool("atkupright", false);
                        _animator.SetBool("atkdownright", false);
                    }
                    if (Iupright == true)
                    {
                        
                        _animator.SetBool("atkupright", true);

                        _animator.SetBool("atkright", false);
                        _animator.SetBool("atkupleft", false);
                        _animator.SetBool("atkdown", false);
                        _animator.SetBool("atkleft", false);
                        _animator.SetBool("atkup", false);
                        _animator.SetBool("atkdownleft", false);
                        _animator.SetBool("atkdownright", false);
                    }
                    if (Iup == true)
                    {
                        _animator.SetBool("atkup", true);

                        _animator.SetBool("atkleft", false);
                        _animator.SetBool("atkright", false);
                        _animator.SetBool("atkdown", false);
                        _animator.SetBool("atkupleft", false);
                        _animator.SetBool("atkupright", false);                        
                        _animator.SetBool("atkdownleft", false);
                        _animator.SetBool("atkdownright", false);
                    }
                    _animator.SetTrigger("AttackFollowUp");
                    #endregion



                    RChargeinit = RChargereset;
                    DChargeinit = DChargereset;
                    LChargeinit = LChargereset;
                    UChargeinit = LChargereset;
                    AnimationTimer = ACD;


                }
            }
            #endregion
            #region 8-Directional Atacks Armed
            if (Armed == true)
            {


                //atkright
                pos = transform.position;
                if (Input.GetKeyUp(KeyCode.O) && Atacking == false && Charging == false && Running == false && Iright == true)
                {


                    Atkright = true;
                    Atacking = true;
                   // Sounds.PlaySound(0, true, 1, 0.3f, true, 3);
                }
                if (Atkright == true)
                {


                    if (AnimationTimer2 <= Timeuntilatk)
                    {
                        AttackCol.enabled = true;

                    }
                    if (AnimationTimer2 >= 0)
                    {
                        _animator.SetBool("atkright", true);

                    }
                    else
                    {
                        _animator.SetBool("atkright", false);
                        Atkright = false;
                        AttackCol.enabled = false;
                        Atacking = false;

                    }
                }



                //atkleft
                if (Input.GetKeyUp(KeyCode.O) && Atacking == false && Charging == false && Running == false && Ileft == true)
                {


                    Atkleft = true;
                    Atacking = true;
                   // Sounds.PlaySound(0, true, 1, 0.3f, true, 3);

                }
                if (Atkleft == true)
                {

                    if (AnimationTimer2 <= Timeuntilatk)
                    {
                        AttackCol.enabled = true;

                    }
                    if (AnimationTimer2 >= 0)
                    {
                        _animator.SetBool("atkleft", true);
                    }
                    else
                    {
                        _animator.SetBool("atkleft", false);
                        Atkleft = false;
                        AttackCol.enabled = false;
                        Atacking = false;
                    }
                }



                //atkdown
                if (Input.GetKeyUp(KeyCode.O) && Atacking == false && Charging == false && Running == false && Idown == true)
                {


                    Atkdown = true;
                    Atacking = true;
                   // Sounds.PlaySound(0, true, 1, 0.3f, true, 3);
                }
                if (Atkdown == true)
                {

                    if (AnimationTimer2 <= Timeuntilatk)
                    {
                        AttackCol.enabled = true;

                    }
                    if (AnimationTimer2 >= 0)
                    {
                        _animator.SetBool("atkdown", true);
                    }
                    else
                    {
                        _animator.SetBool("atkdown", false);
                        Atkdown = false;
                        AttackCol.enabled = false;
                        Atacking = false;
                    }
                }


                //atkup
                if (Input.GetKeyUp(KeyCode.O) && Atacking == false && Charging == false && Running == false && Iup == true)
                {

                    Atkup = true;
                    Atacking = true;
                   // Sounds.PlaySound(0, true, 1, 0.3f, true, 3);
                }
                if (Atkup == true)
                {

                    if (AnimationTimer2 <= Timeuntilatk)
                    {
                        AttackCol.enabled = true;

                    }
                    if (AnimationTimer >= 0)
                    {
                        _animator.SetBool("atkup", true);
                    }
                    else
                    {
                        _animator.SetBool("atkup", false);
                        Atkup = false;
                        AttackCol.enabled = false;
                        Atacking = false;
                    }
                }


                //atkupleft
                if (Input.GetKeyUp(KeyCode.O) && Atacking == false && Charging == false && Running == false && Iupleft == true)
                {

                    Atkupleft = true;
                    Atacking = true;
                   // Sounds.PlaySound(0, true, 1, 0.3f, true, 3);
                }
                if (Atkupleft == true)
                {
                   
                    if (AnimationTimer2 <= Timeuntilatk)
                    {
                        AttackCol.enabled = true;

                    }
                    if (AnimationTimer2 >= 0)
                    {
                        _animator.SetBool("atkupleft", true);
                    }
                    else
                    {
                        _animator.SetBool("atkupleft", false);
                        Atkupleft = false;
                        AttackCol.enabled = false;
                        Atacking = false;
                    }
                }





                //atkupleft
                //atkupright
                if (Input.GetKeyUp(KeyCode.O) && Atacking == false && Charging == false && Running == false && Iupright == true)
                {
                    Dmgstats.dmg = 2;
                    Atkupright = true;
                    Atacking = true;
                    //.PlaySound(0, true, 1, 0.3f, true, 3);
                }
                if (Atkupright == true)
                {

                    if (AnimationTimer2 <= Timeuntilatk)
                    {
                        AttackCol.enabled = true;

                    }
                    if (AnimationTimer2 >= 0)
                    {
                        _animator.SetBool("atkupright", true);
                    }
                    else
                    {
                        _animator.SetBool("atkupright", false);
                        Atkupright = false;
                        AttackCol.enabled = false;
                        Atacking = false;
                    }
                }

                //atkupright
                //atkdownright
                if (Input.GetKeyUp(KeyCode.O) && Atacking == false && Charging == false && Running == false && Idownright == true)
                {

                    Atkdownright = true;
                    Atacking = true;
                  //  Sounds.PlaySound(0, true, 1, 0.3f, true, 3);
                }
                if (Atkdownright == true)
                {
                   
                    if (AnimationTimer2 <= Timeuntilatk)
                    {
                        AttackCol.enabled = true;

                    }
                    if (AnimationTimer2 >= 0)
                    {
                        _animator.SetBool("atkdownright", true);
                    }
                    else
                    {
                        _animator.SetBool("atkdownright", false);
                        Atkdownright = false;
                        AttackCol.enabled = false;
                        Atacking = false;


                    }
                }

                //atkdownright
                //atkdownleft
                if (Input.GetKeyUp(KeyCode.O) && Atacking == false && Charging == false && Running == false && Idownleft == true)
                {

                    Atkdownleft = true;
                    Atacking = true;
                   // Sounds.PlaySound(0, true, 1, 0.3f, true, 3);
                }
                if (Atkdownleft == true)
                {
                   
                    if (AnimationTimer2 <= Timeuntilatk)
                    {
                        AttackCol.enabled = true;

                    }
                    if (AnimationTimer >= 0)
                    {
                        _animator.SetBool("atkdownleft", true);
                    }
                    else
                    {
                        _animator.SetBool("atkdownleft", false);
                        Atkdownleft = false;
                        AttackCol.enabled = false;
                        Atacking = false;
                    }
                }

            }
            #endregion
         
            #region 8-Directional Charge Atacks
            if (RChargeinit <= -0.3f | LChargeinit <= -0.3f | DChargeinit <= -0.3f | UChargeinit <= -0.3f)
            {
                velocidade = 500;
            }
            else { velocidade = velocidadepadrao; }
            //right charge
            if (Input.GetKey(KeyCode.P) && DChargeatk == false && UChargeatk == false && LChargeatk == false && RChargeatk == false && Running == false & Iright == true)
            {
                RChargeinit -= Time.deltaTime;
                if (RChargeinit <= 0f)
                {
                    AnimationTimer = 1.4f;
                    _animator.SetBool("RCharging", true);
                    Charging = true;

                }
                else
                {
                    _animator.SetBool("RCharging", false);

                }
                if (RChargeinit <= -0.5f)
                {
                    _animator.SetBool("RCharged", true);
                }
                else { _animator.SetBool("RCharged", false); }

            }
            else
            {
                if (RChargeinit > -0.5f)
                {
                    _animator.SetBool("RCharging", false);
                    RChargeinit = RChargereset;


                }
                if (RChargeinit <= -0.5f)
                {
                   // Sounds.PlaySound(0, true, 1, 0.3f, true, 3);
                    RChargeatk = true;
                    rb.AddForce(transform.right * 15000);
                    RChargeinit = RChargereset;

                }
            }
            if (RChargeatk == true)
            {
                Atacking = true;

                AnimationTimer -= Time.deltaTime;
                _animator.SetBool("RChargeattack", true);
                AttackCol.enabled = true;
                if (AnimationTimer <= 0)
                {
                    Atacking = false;
                    _animator.SetBool("RChargeattack", false);
                    RChargeatk = false;
                    AttackCol.enabled = false;
                    RChargeinit = RChargereset;

                }

            }
            //Right charge end
            //Left charge
            if (Input.GetKey(KeyCode.P) && DChargeatk == false && UChargeatk == false && LChargeatk == false && RChargeatk == false && Running == false & Ileft == true)
            {
                LChargeinit -= Time.deltaTime;
                if (LChargeinit <= 0f)
                {
                    AnimationTimer = 1.4f;
                    _animator.SetBool("LCharging", true);
                    Charging = true;
                }
                else
                {
                    _animator.SetBool("LCharging", false);

                }
                if (LChargeinit <= -0.5f)
                {
                    _animator.SetBool("LCharged", true);
                }
                else { _animator.SetBool("LCharged", false); }

            }
            else
            {
                if (LChargeinit > -0.5f)
                {
                    _animator.SetBool("LCharging", false);
                    LChargeinit = LChargereset;

                }
                if (LChargeinit <= -0.5f)
                {
                    LChargeatk = true;
                    rb.AddForce(transform.right * -15000);
                    LChargeinit = LChargereset;
                   // Sounds.PlaySound(0, true, 1, 0.3f, true, 3);

                }
            }
            if (LChargeatk == true)
            {
                Atacking = true;
                AttackCol.enabled = true;
                AnimationTimer -= Time.deltaTime;
                _animator.SetBool("LChargeattack", true);

                if (AnimationTimer <= 0)
                {
                    AttackCol.enabled = false;
                    Atacking = false;
                    _animator.SetBool("LChargeattack", false);
                    LChargeatk = false;
                    LChargeinit = LChargereset;

                }

            }
            //Left Charge end
            //Up Charge
            if (Input.GetKey(KeyCode.P) && DChargeatk == false && UChargeatk == false && LChargeatk == false && RChargeatk == false && Running == false && Iup == true)
            {
                UChargeinit -= Time.deltaTime;
                if (UChargeinit <= 0f)
                {
                    AnimationTimer = 1.4f;
                    _animator.SetBool("UCharging", true);
                    Charging = true;
                }
                else
                {
                    _animator.SetBool("UCharging", false);

                }
                if (UChargeinit <= -0.5f)
                {
                    _animator.SetBool("UCharged", true);
                }
                else { _animator.SetBool("UCharged", false); }

            }
            else
            {
                if (UChargeinit > -0.5f)
                {
                    _animator.SetBool("UCharging", false);
                    UChargeinit = UChargereset;

                }
                if (UChargeinit <= -0.5f)
                {
                    //Sounds.PlaySound(0, true, 1, 0.3f, true, 3);
                    UChargeatk = true;
                    rb.AddForce(transform.up * 15000);
                    UChargeinit = UChargereset;

                }
            }
            if (UChargeatk == true)
            {
                Atacking = true;
                AttackCol.enabled = true;

                AnimationTimer -= Time.deltaTime;
                _animator.SetBool("UChargeattack", true);

                if (AnimationTimer <= 0)
                {
                    AttackCol.enabled = false;
                    Atacking = false;
                    _animator.SetBool("UChargeattack", false);
                    UChargeatk = false;
                    UChargeinit = UChargereset;

                }

            }
            //Up charge end
            //Down charge
            if (Input.GetKey(KeyCode.P) && DChargeatk == false && UChargeatk == false && LChargeatk == false && RChargeatk == false && Running == false && Idown == true)
            {
                DChargeinit -= Time.deltaTime;
                if (DChargeinit <= 0f)
                {
                    AnimationTimer = 1.4f;
                    _animator.SetBool("DCharging", true);
                    Charging = true;
                }
                else
                {
                    _animator.SetBool("DCharging", false);

                }
                if (DChargeinit <= -0.5f)
                {
                    _animator.SetBool("DCharged", true);
                }
                else { _animator.SetBool("DCharged", false); }

            }
            else
            {
                if (DChargeinit > -0.5f)
                {
                    _animator.SetBool("DCharging", false);
                    DChargeinit = DChargereset;

                }
                if (DChargeinit <= -0.5f)
                {
                    //Sounds.PlaySound(0, true, 1, 0.3f, true, 3);
                    DChargeatk = true;
                    rb.AddForce(transform.up * -15000);
                    DChargeinit = DChargereset;

                }
            }
            if (DChargeatk == true)
            {
                AttackCol.enabled = true;
                AnimationTimer -= Time.deltaTime;
                _animator.SetBool("DChargeattack", true);
                Atacking = true;
                if (AnimationTimer <= 0)
                {
                    AttackCol.enabled = false;
                    Atacking = false;
                    _animator.SetBool("DChargeattack", false);
                    DChargeatk = false;
                    DChargeinit = DChargereset;

                }

            }
            if (Input.GetKey(KeyCode.O) == false)
            {
                Charging = false;
            }
            #endregion
            #region target
            //target

            if (Input.GetKeyDown(KeyCode.G) && targettimer <= 0)
            {
                targeton();
                targettimer = 0.2f;
            }

            targettimer -= Time.deltaTime;

            if (target == true && _animator.GetBool("rolling") == false )
            {
                if (Armed == false)
                {


                    faceleft = left.ON;

                    _animator.SetBool("faceleft", faceleft);



                    facedown = down.ON;


                    _animator.SetBool("facedown", facedown);

                    faceup = up.ON;



                    _animator.SetBool("faceup", faceup);



                    faceright = right.ON;

                    _animator.SetBool("faceright", faceright);
                }
                if(Armed == true && Atacking == false && turntime <= 0)
                {
                    faceleft = left.ON;

                    _animator.SetBool("faceleft", faceleft);



                    facedown = down.ON;


                    _animator.SetBool("facedown", facedown);

                    faceup = up.ON;



                    _animator.SetBool("faceup", faceup);



                    faceright = right.ON;

                    _animator.SetBool("faceright", faceright);
                }

                //upright
                if ((upright.ON == true && up.ON == false && right.ON == false)
                    )
                {
                    facerightup = true;
                    _animator.SetBool("facerightup", facerightup);
                }
                else
                {
                    if ((upright.ON == false && up.ON == true && right.ON == true)
                 )
                    {
                        facerightup = true;
                        _animator.SetBool("facerightup", facerightup);
                        faceup = false;
                        faceright = false;

                    }
                    else
                    {
                        if ((upright.ON == true && up.ON == true && right.ON == true)
                 )
                        {
                            facerightup = true;
                            _animator.SetBool("facerightup", facerightup);
                            faceup = false;
                            faceright = false;
                        }
                        else
                        {
                            facerightup = false;
                            _animator.SetBool("facerightup", facerightup);
                        }
                    }
                }




                //upleft
                if ((upleft.ON == true && up.ON == false && left.ON == false)
                    )
                {
                    faceleftup = true;
                    _animator.SetBool("faceleftup", faceleftup);
                }
                else
                {
                    if ((upleft.ON == false && up.ON == true && left.ON == true)
                 )
                    {
                        faceleftup = true;
                        _animator.SetBool("faceleftup", faceleftup);
                        faceup = false;
                        faceleft = false;
                    }
                    else
                    {
                        if ((upleft.ON == true && up.ON == true && left.ON == true)
                 )
                        {
                            faceleftup = true;
                            _animator.SetBool("faceleftup", faceleftup);
                            faceup = false;
                            faceleft = false;
                        }
                        else
                        {
                            faceleftup = false;
                            _animator.SetBool("faceleftup", faceleftup);
                        }
                    }
                }

                //downright            
                if ((downright.ON == true && down.ON == false && right.ON == false)
                    )
                {
                    facerightdown = true;
                    _animator.SetBool("facerightdown", facerightdown);
                }
                else
                {
                    if ((downright.ON == false && down.ON == true && right.ON == true)
                 )
                    {
                        facerightdown = true;
                        _animator.SetBool("facerightdown", facerightdown);
                        facedown = false;
                        faceright = false;
                    }
                    else
                    {
                        if ((downright.ON == true && down.ON == true && right.ON == true)
                 )
                        {
                            facerightdown = true;
                            _animator.SetBool("facerightdown", facerightdown);
                            facedown = false;
                            faceright = false;
                        }
                        else
                        {
                            facerightdown = false;
                            _animator.SetBool("facerightdown", facerightdown);
                        }
                    }
                }


                //downleft
                if ((downleft.ON == true && down.ON == false && left.ON == false)
                  )
                {
                    faceleftdown = true;
                    _animator.SetBool("faceleftdown", faceleftdown);
                }
                else
                {
                    if ((downleft.ON == false && down.ON == true && left.ON == true)
                 )
                    {
                        faceleftdown = true;
                        _animator.SetBool("faceleftdown", faceleftdown);
                        facedown = false;
                        faceleft = false;
                    }
                    else
                    {
                        if ((upleft.ON == true && down.ON == true && left.ON == true)
                 )
                        {
                            faceleftdown = true;
                            _animator.SetBool("faceleftdown", faceleftdown);
                            facedown = false;
                            faceleft = false;
                        }
                        else
                        {
                            faceleftdown = false;
                            _animator.SetBool("faceleftdown", faceleftdown);
                        }
                    }
                }


            }
            #endregion target
            #region Jump Input
            //jump in update to avoid missed data
            if(jumping == false)
            {
                backflip = false;
                soundlimit = 0;
            }
            
            _animator.SetBool("backflip", backflip);



            if (Input.GetKeyDown(KeyCode.Space) && Input.GetKey(KeyCode.LeftControl) == false && jumping == false && GCheck.ON == true)
            {
                _animator.SetBool("jumpinit", true);
                jumpafter = false;
                jumpengage = true;
                
            }
            if (jumpengage == true)
            {
                jumpingCd -= Time.deltaTime;
            }
            if (jumpingCd <= 0 && jumpfuckhandler == 0)
            {
                jumping = true;
                jumpengage = false;
                _animator.SetBool("jumpinit", false);
                rb.AddForce(rb.velocity * 50000);
                rb.AddForce(GlobalOrientation.up * 9000000);
                if (Iup == true && Input.GetKey(KeyCode.S) == true)
                {
                    backflip = true;
                    rb.AddForce(rb.velocity * 90000);
                }
                if (Idown == true && Input.GetKey(KeyCode.W) == true)
                {
                    backflip = true;
                    rb.AddForce(rb.velocity * 90000);
                }
                if (Ileft == true && Input.GetKey(KeyCode.D) == true)
                {
                    backflip = true;
                    rb.AddForce(rb.velocity * 90000);
                }
                if (Iright == true && Input.GetKey(KeyCode.A) == true)
                {
                    
                    backflip = true;
                    rb.AddForce(rb.velocity * 90000);
                    

                }
                jumpfuckhandler = 1;
                
                
                
            }
            _animator.SetBool("jumpinit", jumpengage);
            #endregion
            #region Running and hitting your face on the walls

            if (Input.GetKeyDown(KeyCode.LeftShift) && movimento > 0)
            {
                if (Running == true)
                {
                    Running = false;

                    _animator.SetBool("drift", true);
                    freezetimer = 0.3f;
                    Runningemit.rateOverTime = 0;
                }
                else
                {
                    if (Running == false)
                    {
                        Running = true;
                       

                        Runningemit.rateOverTime = 30;
                    }
                }


            }
            if (movimento <= 0 && Running == true)
            {
                _animator.SetBool("drift", true);
                Runningemit.rateOverTime = 0;
                freezetimer = 0.4f;
                Running = false;

            }
            //headslam
            Ptimer -= Time.deltaTime;
            if (headslam == true && headslamlimiter == 0)
            {
                Running = false;
                Runningemit.rateOverTime = 0;
                freezetimer = 0.5f;
                headslamlimiter = 1;
            }

            _animator.SetBool("headslam", headslam);

            if (movimento > 0 && Running)
            {

                Runningemit.rateOverTime = 30;
            }
            if (freezetimer < 0)
            {
                headslam = false;
                headslamlimiter = 0;
                _animator.SetBool("drift", false);
            }
            if (freezetimer > 0 && _animator.GetBool("drift") == true)
            {
                Runningemit.rateOverTime = 90;
            }
            if (Running == false && freezetimer < 0)
            {
                Runningemit.rateOverTime = 0;
            }
            if(craw == true | jumping == true)
            {
                Running = false;
            }
            #endregion
            #region Crawling
            if (Input.GetKey(KeyCode.LeftControl))
            {
                craw = true;
                if (jumping == false && descending == false && Atacking == false)
                {
                    Attackcenter.GetComponentInChildren<DistanceJoint2D>().connectedAnchor = new Vector2(0, -10);
                }
            }
            if (Input.GetKey(KeyCode.LeftControl) == false && jumping == false && descending == false)
            {
                if (_animator.GetBool("rolling") == false)
                {
                    craw = false;
                }

                Attackcenter.GetComponentInChildren<DistanceJoint2D>().connectedAnchor = new Vector2(0, 0);
            }

            if (craw == true && crawlimiter == 0)
            {
                crawlimiter = 1;
                _animator.SetTrigger("crawltrigger");

            }
            if (craw == false && crawlimiter == 1)
            {
                crawlimiter = 0;
                _animator.SetTrigger("walktrigger");


            }

            #endregion
            #region Rolling
            if (Input.GetKeyDown(KeyCode.Space) && craw == true && timer9 <= -0.3)
            {
                timer9 = 0.50f;
               // Sounds2.PlaySound(1, true, 0.8f, 0.2f, true, 2);

                if (movimento == 1)
                {
                    if (Input.GetKey(KeyCode.S))
                    {
                        rolldown = true;                    
                    }
                    if (Input.GetKey(KeyCode.W))
                    {
                        rollup = true;

                    }
                    if (Input.GetKey(KeyCode.D))
                    {
                        rollright = true;
                    }
                    if (Input.GetKey(KeyCode.A))
                    {
                        rollleft = true;
                    }
                }
                if (movimento == 2)
                {
                    if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.W))
                    {
                        rollupleft = true;
                    }
                    if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.W))
                    {
                        rollupright = true;
                    }
                    if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.S))
                    {
                        rolldownleft = true;
                    }
                    if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.S))
                    {
                        rolldownright = true;
                    }

                }
                
                AnimationTimer = 0;


            }
            #endregion
           

            //Camera Drag if running
            if (Running == true)
            {
                StepintervalCD = 0.2f;
                
                camstats.smoothDampTime = 0.3f;
            }
            else
            {               
                camstats.smoothDampTime = 0.2f;                
            }
            //Camera Drag if running
            //Animation Acelerates when running fast cuz thats just obvious
            if (Running == false)
            {
                _animator.speed = 1;
            }
            if (Running == true)
            {
                _animator.speed = 1;
            }
            //Animation Acelerates when running fast cuz thats just obvious


            

        }
    }
    void FixedUpdate()
    {
        #region Rolling Movement Logic
        timer9 -= Time.deltaTime;
        if (timer9 > -0.3)
        {
            _animator.SetBool("rolling", true);
        }
        if (timer9 > 0)
        {

            freezetimer = timer9;
            if (movimento > 0 & b == false)
            {
                a = true;
            }
            if (movimento == 0 && a == false)
            {
                b = true;
            }
            if (a == true && b == false)
            {
                if (rolldown == true)
                {
                    rb.AddForce(GlobalOrientation.forward * -7080000);
                }
                if (rollup == true)
                {
                    rb.AddForce(GlobalOrientation.forward * 7080000);
                }
                if (rollright == true)
                {
                    rb.AddForce(GlobalOrientation.right * 7080000);
                }
                if (rollleft == true)
                {
                    rb.AddForce(GlobalOrientation.right * -7080000);
                }

                //diagonal
                if (rollupleft == true)
                {
                    rb.AddForce(GlobalOrientation.forward * (7080000 / 1.3f));
                    rb.AddForce(GlobalOrientation.right * (-7080000 / 1.3f));
                }
                if (rollupright == true)
                {
                    rb.AddForce(GlobalOrientation.forward * (7080000 / 1.3f));
                    rb.AddForce(GlobalOrientation.right * (7080000 / 1.3f));
                }
                if (rolldownleft == true)
                {
                    rb.AddForce(GlobalOrientation.forward * (-7080000 / 1.3f));
                    rb.AddForce(GlobalOrientation.right * (-7080000 / 1.3f));
                }
                if (rolldownright == true)
                {
                    rb.AddForce(GlobalOrientation.forward * (-7080000 / 1.3f));
                    rb.AddForce(GlobalOrientation.right * (7080000 / 1.3f));
                }

            }

            if (b == true && a == false)
            {
                if (Idown == true)
                {
                    rb.AddForce(GlobalOrientation.forward * -7080000);
                }
                if (Iup == true)
                {
                    rb.AddForce(GlobalOrientation.forward * 7080000);
                }
                if (Iright == true)
                {
                    rb.AddForce(GlobalOrientation.right * 7080000);
                }
                if (Ileft == true)
                {
                    rb.AddForce(GlobalOrientation.right * -7080000);

                }

                //diagonal
                if (Iupleft == true)
                {
                    rb.AddForce(GlobalOrientation.forward * (7080000 / 1.3f));
                    rb.AddForce(GlobalOrientation.right * (-7080000 / 1.3f));
                }
                if (Iupright == true)
                {
                    rb.AddForce(GlobalOrientation.forward * (7080000 / 1.3f));
                    rb.AddForce(GlobalOrientation.right * (7080000 / 1.3f));
                }
                if (Idownleft == true)
                {
                    rb.AddForce(GlobalOrientation.forward * (-7080000 / 1.3f));
                    rb.AddForce(GlobalOrientation.right * (-7080000 / 1.3f));
                }
                if (Idownright == true)
                {
                    rb.AddForce(GlobalOrientation.forward * (-7080000 / 1.3f));
                    rb.AddForce(GlobalOrientation.right * (7080000 / 1.3f));
                }

            }
        }

        if (timer9 <= 0)
        {
            _animator.SetBool("rolling", false);
            rollleft = false;
            rollright = false;
            rollup = false;
            rolldown = false;

            rollupleft = false;
            rollupright = false;
            rolldownright = false;
            rolldownleft = false;
            a = false;
            b = false;
        }
        #endregion
        #region Jumping logic
        
        if (jumping == true)
        {
            //gravity.Gforce = 0;
            mult = 1;
            rb.AddForce(GlobalOrientation.up * i * 120000);
            DustCD = 0.1f;

            i += o;



            if (o <= 0)
            {
                descending = true;

            }
            


        }
        

        if (Input.GetKey(KeyCode.Space) == true && o > 0 && descending == false)
        {
            o -= 0.4f;

        }
        if (Input.GetKey(KeyCode.Space) == false && o > 0 && descending == false)
        {
            o -= 0.8f;
        }

        if (descending == true)
        {
            o -= 0.2f;
           

        }
        if (i != 1 && descending == false)
        {
            rb.AddForce(rb.velocity * 10000);
        }

        if (descending == true && jumping == false)
        {
            descending = false;
            jumping = true;
        }
        if (i < 0 && GCheck.ON == true)
        {
           
           
            jumpafter = true;
            jumping = true;
            jumpengage = false;
            jumpingCd = 0.08f;
            descending = false;
            jumpfuckhandler = 0;
            
          
         
            
            jumping = false;
        }
        if (jumping == false)
        {
            
            i = 0;
            o = 4;
            //gravity.Gforce = 30000000;

        }
      
        
        

        _animator.SetBool("jumping", jumping);
        if (i < 0)
        {
            _animator.SetBool("descending", true);
        }
        else
        {
            _animator.SetBool("descending", false);
        }
        _animator.SetBool("jumpafter", jumpafter);


        if(VelocityVector.y < 1)
        {
            _animator.SetInteger("UpVel", -1);
        }
        if (VelocityVector.y > -1)
        {
            _animator.SetInteger("UpVel", 1);
        }






        #endregion
        #region Dinamic Direction Facing
        //animator facing


        if (_animator.GetBool("rolling") == false)
        {
            _animator.SetBool("Iup", Iup);
            _animator.SetBool("Idown", Idown);
            _animator.SetBool("Ileft", Ileft);
            _animator.SetBool("Iright", Iright);
            _animator.SetBool("Iupleft", Iupleft);
            _animator.SetBool("Iupright", Iupright);
            _animator.SetBool("Idownleft", Idownleft);
            _animator.SetBool("Idownright", Idownright);
        }
        _animator.SetBool("target", target);

        turntime -= Time.deltaTime;
        if(jumping == true) { turntime = 0.3f; }
       

        if (turntime < 0)
        {
            _animator.SetBool("walkingright", walkingright);
            _animator.SetBool("walkingdown", walkingdown);
            _animator.SetBool("walkingup", walkingup);
            _animator.SetBool("walkingleft", walkingleft);
            _animator.SetBool("Running", Running);
        }
        #endregion
        if (PlayerOnControl == true)
        {
            if (jumping == false)
            {
                Stepinterval -= Time.deltaTime;
            }
            #region Running Specs and Movement Restrictions

            if (Running == false && rb.drag < 8f && jumping == false)
            {
                rb.drag += 0.5f;

            }          
            if (Running == true && rb.drag > 4f && jumping == false)
            {
                rb.drag -= 0.5f;

            }
            if (rb.drag > 2f && backflip == true)
            {
                rb.drag -= 0.5f;

            }
            if (jumping == true && rb.drag > 2f)
            {
                rb.drag -= 0.5f;
            }



            if (Running == true && velocidadepadrao <= 550)
            {
                velocidadepadrao += 2.2f;
            }


            if (movimento >= 2)
            {
                if (velocidadepadrao == speed)
                {
                    velocidadepadrao = speed / 1.3f;
                }

            }
            

            if (movimento < 2 && Charging == false && Running == false && craw == false) { velocidadepadrao = speed; }
            if (movimento >= 2 && Charging == false && Running == false && craw == false) { velocidadepadrao = speed / 1.3f; }
            if (movimento < 2 && Charging == false && Running == true && craw == false) { velocidadepadrao = speed; }
            if (movimento >= 2 && Charging == false && Running == true && craw == false) { velocidadepadrao = speed / 1.3f; }
            if (movimento < 2 && (craw == true | Atacking == true | _animator.GetInteger("FlurryCount") >= 5)) { velocidadepadrao = speed / 3.3f; }
            if (movimento >= 2 && (craw == true | Atacking == true | _animator.GetInteger("FlurryCount") >= 5)) { velocidadepadrao = speed / 5.6f; }
            //turn slow
            if (movimento < 2 && turntime > 0 && jumping == false) { velocidadepadrao = speed / 3.3f; }
            if (movimento >= 2 && turntime > 0 && jumping == false) { velocidadepadrao = speed / 5.6f; }

            if (movimento < 2 && jumping == true) { velocidadepadrao = speed / 1.3f; ; }
            if (movimento >= 2 && jumping == true) { velocidadepadrao = speed / 1.6f; }

            if (movimento < 2 && Charging == true && Running == false && craw == false) { velocidadepadrao = speed / 2.3f; }
            if (movimento >= 2 && Charging == true && Running == false && craw == false) { velocidadepadrao = speed / 3.6f; }
            #endregion
            #region walkin

            if (Running == false)
            {
                StepintervalCD = 0.4f / (velocidade / 2080000);
            }
            if (craw == true)
            {
                StepintervalCD = 0.2f;
            }
           

            //direita
            if (Input.GetKey(KeyCode.D))
            {
                if (Stepinterval <= 0)
                {
                   // Sounds.PlaySound(15, true, 0.5f, 0.2f, true, 3);
                    Stepinterval = StepintervalCD;
                }

                rb.AddForce(GlobalOrientation.right * velocidade);

                if (target == false)
                {
                    walkingright = true;

                }
            }
            else
            {
                if (target == false)
                {
                    walkingright = false;
                }
            }

            //baixo
            if (Input.GetKey(KeyCode.S))
            {

                rb.AddForce(GlobalOrientation.forward * -velocidade);

                if (Stepinterval <= 0)
                {
                   // Sounds.PlaySound(15, true, 0.5f, 0.2f, true, 3);
                    Stepinterval = StepintervalCD;
                }

                if (target == false)
                {
                    walkingdown = true;
                }

            }
            else
            {
                if (target == false)
                {
                    walkingdown = false;
                }
            }

            //cima
            if (Input.GetKey(KeyCode.W))
            {

                rb.AddForce(GlobalOrientation.forward * velocidade);

                if (Stepinterval <= 0)
                {
                    //Sounds.PlaySound(15, true, 0.5f, 0.2f, true, 3);
                    Stepinterval = StepintervalCD;
                }

                if (target == false)
                {
                    walkingup = true;
                }
            }
            else
            {
                if (target == false)
                {
                    walkingup = false;
                }
            }

            //esquerda
            if (Input.GetKey(KeyCode.A))
            {
                rb.AddForce(GlobalOrientation.right * -velocidade);

                if (Stepinterval <= 0)
                {
                   // Sounds.PlaySound(15, true, 0.5f, 0.2f, true, 3);
                    Stepinterval = StepintervalCD;
                }



                if (target == false)
                {
                    walkingleft = true;
                }

            }
            else
            {
                if (target == false)
                {
                    walkingleft = false;
                }

            }
        }
        #endregion walkin
        if (Atacking == false)
        {
            Push = false;
        }



    }


    void StepSoundSync()
    {
       // Sounds.PlaySound(15, true, 0.5f, 0.2f, true, 3);
    }


    void PushAttack()
    {
        #region Attack pushes the player
        // atk litte pushy push
       
        if (Push == false)
        {
           
            if (Idown == true)
            {
                rb.AddForce(GlobalOrientation.forward * -17080000);
            }
            if (Iup == true)
            {
                rb.AddForce(GlobalOrientation.forward * 17080000);
            }
            if (Iright == true)
            {
                rb.AddForce(GlobalOrientation.right * 17080000);
            }
            if (Ileft == true)
            {
                rb.AddForce(GlobalOrientation.right * -17080000);

            }

            //diagonal
            if (Iupleft == true)
            {
                rb.AddForce(GlobalOrientation.forward * (17080000 / 1.3f));
                rb.AddForce(GlobalOrientation.right * (-17080000 / 1.3f));
            }
            if (Iupright == true)
            {
                rb.AddForce(GlobalOrientation.forward * (17080000 / 1.3f));
                rb.AddForce(GlobalOrientation.right * (17080000 / 1.3f));
            }
            if (Idownleft == true)
            {
                rb.AddForce(GlobalOrientation.forward * (-17080000 / 1.3f));
                rb.AddForce(GlobalOrientation.right * (-17080000 / 1.3f));
            }
            if (Idownright == true)
            {
                rb.AddForce(GlobalOrientation.forward * (-17080000 / 1.3f));
                rb.AddForce(GlobalOrientation.right * (17080000 / 1.3f));
            }
            Push = true;
        }
        //atk little push
        #endregion
    }
    /// Target Method
    void targeton()
    {
        if (target == false)
        {
            target = true;
            walkingdown = false;
            walkingleft = false;
            walkingup = false;
            walkingright = false;
        }
        else { target = false;
            walkingdown = false;
            walkingleft = false;
            walkingup = false;
            walkingright = false;

            faceright = false;
            faceleft = false;
            faceup = false;
            facedown = false;
            facerightup = false;
            faceleftup = false;
            facerightdown = false;
            faceleftdown = false;         
        }

    }
    




  



















}

            









