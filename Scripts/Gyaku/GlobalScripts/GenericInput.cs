using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericInput : MonoBehaviour
{
    [Header("Walk Directions")]
    public bool walkingright;
    public bool walkingleft;
    public bool walkingdown;
    public bool walkingup;
    public bool WalkingTrigger;
    [Header("Rolling Directions")]
    public bool RollLeft;
    public bool RollRight;
    public bool RollUp;
    public bool RollDown;
    public bool RollUpLeft;
    public bool RollUpRight;
    public bool RollDownLeft;
    public bool RollDownRight;

    [Header("Run State")]

    [Header("Player States")]
    public bool Running;
    public bool RunJumpBoost;
    public bool RunningJump; 
    public bool MaxRun;
    [Header("Crawl States")]
    public bool Crawl;
    public bool Rolling;
    public bool CrawlTrigger;
    
    [Header("Jump States")]
    public bool Jumping;
    public bool HoldingJump;
    public bool JumpHboost;
    public bool JumpStart;
    public bool Falling;
    public bool RollOnAir;
     public bool rollVboost;
     public bool Landing;
    
    [Header("Grab States")]
    public bool Grabbing; 
    public bool Trowing;
    public bool HoldingItem;

    [Header("Neutral States")]
    public bool BeingPushed;
    public bool Pushable;
    public bool Casting;
    public bool Target;  
    public bool CanWalk;
    public bool ActivateGrav;
    public bool Grababble;
    public bool Spawned;
    public bool Dead;
    public bool Thrown;
    public bool Grabbed;

    [Header("Others")]
    public bool GTest;
    public bool checkGround;
    public bool Inground;
    public bool Diagonalfix;
    public bool RollVM;

    public bool Shaking;
    [HideInInspector]
    public GenericStats Stats;  
    public GenericMovement GenericMov;
    public Vector3 GroundColOri;
    public Vector3 GroundColOriPost;
    public Vector3 LookDir;
    public Vector3 WalkDir;
    public Vector3 DirParalel;
    public bool TrowEngage;
    public bool TrowMax;
    public bool TrowMaxBeyond;
    public GenericAnimator Anim;
   
    public float GravStartTimer;
    public Bounds bounds;
    public Collider CurrentGround;
    public Collision CurrentGroundC;
    
    protected virtual void Update()
    {
        GroundInput();
       	//LooDirLogic();
        LookDirLogicNew();
        CheckVelocity();
        bounds = gameObject.GetComponent<Collider>().bounds;
    }
   
    // Start is called before the first frame update
    protected virtual void Start()
    {
        
        GenericMov = gameObject.GetComponent<GenericMovement>();
        Stats = gameObject.GetComponent<GenericStats>();
        Anim = gameObject.GetComponent<GenericAnimator>();
        checkGround = true;
    }
    public void LookDirLogicNew(){
        Vector3 Dir = GenericMov._rb.velocity.normalized;
        if(Dir != Vector3.zero && GenericMov._rb.velocity.magnitude >= 3){
            LookDir = Dir;
        }
        
    }
    // Update is called once per frame
   
    public void CheckVelocity(){
        CreateXYZGraph(new Vector3(0,0,-20), GenericMov._rb.velocity / 50);
    }
    public void LooDirLogic(){
        if(Stats.Iup){
            LookDir = DirUp;
            DirParalel = DirRight;
        }
        if(Stats.Idown){
            LookDir = DirDown;
             DirParalel = DirLeft;
        }
        if(Stats.Iupleft){
            LookDir = DirUpLeft;
            DirParalel = DirUpRight;
        }
        if(Stats.Idownleft){
            LookDir = DirDownLeft;
            DirParalel = DirUpLeft;
        }
         if(Stats.Iupright){
            LookDir = DirUpRight;
            DirParalel = DirDownRight;
        }
        if(Stats.Idownright){
            LookDir = DirDownRight;
            DirParalel = DirDownLeft;
        }
         if(Stats.Iright){
            LookDir = DirRight;
            DirParalel = DirDown;
        }
        if(Stats.Ileft){
            LookDir = DirLeft;
            DirParalel = DirUp;
        }
    }
    
    public void GroundInput(){
        if(Inground == false){
            CanWalk = false;
        }
        if(GTest == false){
            Inground = false;
        }
    }

    protected virtual void OnCollisionExit(Collision collision)
    {
        
        if (collision.collider == CurrentGround)
        {
            GTest = false;
            Inground = false;
        }

    }
    protected virtual void OnCollisionEnter(Collision collision)
    {
        GroundColOri = collision.contacts[0].normal;
        
    }
    protected virtual void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            if (collision.contacts[i].normal.y >= 0.7f)
            {
                CheckGround(collision);
                GroundColOriPost = collision.contacts[i].normal;
            }
            
        }
        

        if(GroundColOriPost.y >= 0.5f){  Inground = true;}
        if(GroundColOriPost.y >= 0.7f){  CanWalk = true;
        }
        
    }
    public void CheckGround(Collision collision){
            CreateXYZGraph(new Vector3(0,0,-90), GroundColOriPost);
            GTest = true;
            CurrentGroundC = collision;
            CurrentGround = collision.collider;
    }
    public void CreateXYZGraph(Vector3 Offset,Vector3 Params){
        Vector3 Pos = bounds.ClosestPoint(bounds.center + Offset);
        Debug.DrawRay(Pos + Offset, Vector3.up * 10, Color.blue,0.02f);
        Debug.DrawRay(Pos + Offset, Vector3.right * 10, Color.red,0.02f);
        Debug.DrawRay(Pos + Offset, Vector3.forward * 10, Color.green,0.02f);
        Debug.DrawRay(Pos + Offset, Params * 30, Color.cyan,0.02f);
    }
    public void WalkingInputGeneric()
    {
        //if (Input.GetKeyDown())
        {
            Stats.PressedKeys += 1;
            walkingleft = true;

            Stats.Ileft = true;
            Stats.Iup = Stats.Idown = Stats.Iright = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }

        //if (Input.GetKeyDown())
        {
            Stats.PressedKeys += 1;
            walkingup = true;
            Stats.Iup = true;
            Stats.Ileft = Stats.Idown = Stats.Iright = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }

        //if (Input.GetKeyDown())
        {
            Stats.PressedKeys += 1;
            walkingdown = true;
             Stats.Idown = true;
            Stats.Ileft = Stats.Iup = Stats.Iright = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }

        //if (Input.GetKeyDown())
        {
            Stats.PressedKeys += 1;
            walkingright = true;
            Stats.Iright = true;
            Stats.Ileft = Stats.Idown = Stats.Iup = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }     
         
        ///////////////////////////////////////////////////////////////////////////
        

        //if (Input.GetKeyUp())
        {
            Stats.PressedKeys -= 1;
            walkingright = false;
           
        }

       // if (Input.GetKeyUp())
        {
            Stats.PressedKeys -= 1;
            walkingup = false;
        }

        //if (Input.GetKeyUp())
        {
            Stats.PressedKeys -= 1;
            walkingleft = false;
        }

       // if (Input.GetKeyUp())
        {
            Stats.PressedKeys -= 1;
            walkingdown = false;
        }
        /////////////////////////////////////////////////////



    }
    
    public static readonly Vector3 DirUp = new Vector3(0,1,0);
    public static readonly Vector3 DirDown = new Vector3(0,-1,0);
    public static readonly Vector3 DirUpLeft = new Vector3(-1,1,0);
    public static readonly Vector3 DirDownLeft = new Vector3(-1,-1,0);
    public static readonly Vector3 DirDownRight = new Vector3(1,-1,0);
    public static readonly Vector3 DirUpRight = new Vector3(1,1,0);
    public static readonly Vector3 DirRight = new Vector3(1,0,0);
    public static readonly Vector3 DirLeft = new Vector3(-1,0,0);
}

           
           
            
       
      
