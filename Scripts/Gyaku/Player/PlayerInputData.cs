using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInputData : GenericInput
{
    // Start is called before the first frame update
    [Header("Keys")]
    public KeyCode Up;
    public KeyCode Down;
    public KeyCode Left;
    public KeyCode Right;

    public KeyCode Jump;
    public KeyCode Grab;
    public KeyCode Crounch;

    public KeyCode Run;
    public KeyCode Spell;
    

    public float JumpBuffer;
    
    
    /// <summary>
    /// Animation bools
    /// </summary>
    
  

    protected override void Start()
    {
        base.Start();
       
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        WalkingInput();
        
        CrawlInput();
        RollingInput();
        JumpInput();
        RunInput();
        TrowInput();
        GrabInput();
        SpellInput();
        
    }

    public void SpellInput(){
        if(Input.GetKey(Spell) && Casting == false){
            Casting = true;
            
        }
    }
    public void GrabInput(){
        
        if(Input.GetKey(Grab) && CanWalk == true && Stats.GrabCD < 0){
            Grabbing = true;
            
        }
        if(!Input.GetKey(Grab)){
            Grabbing = false;
        }
    }
    public void TrowInput(){
        Stats.GrabCD -= Time.deltaTime;
         if(Input.GetKeyDown(Grab) && HoldingItem == true){
          TrowEngage = true;
          if(TrowEngage == true){
                 Stats.GrabCD = 0.2f;
                Trowing = true;
                Running = false; 
             }
            }
    }

    public void RunInput(){
        if(Input.GetKeyDown(Run) && CanWalk == true && Crawl == false){
            
            if(Running == false){
                
                Running = true;
            }
            else
            {
                
                Running = false;
            }
        }
        if(Stats.PressedKeys == 0 | Crawl == true){
            Running = false;
        }
      
    }

    public void JumpInput()
    {
        JumpBuffer -= Time.deltaTime;
        if (Input.GetKeyDown(Jump) && GTest && Crawl == false && Jumping == false && HoldingItem == true)
        {
            HoldingItem = false;
            Grabbed = false;
        }
         if (Input.GetKeyDown(Jump) && GTest== false && Crawl == false && JumpBuffer < 0)
        {
            JumpBuffer = 0.3f;
            
        }
        if(GTest && JumpBuffer > 0 && JumpStart == false && !Grabbing && !HoldingItem){
            JumpStart = true;
            JumpBuffer = 0;
        }
        if (Input.GetKeyDown(Jump) && GTest && Crawl == false && Jumping == false && HoldingItem == false)
        {
            JumpStart = true;   
        }
        
        if (Input.GetKey(Jump))
        {
            HoldingJump = true;
        }
        else
        {
            HoldingJump = false;
        }
    }
    public void RollingInput()
    {
        if (Input.GetKeyDown(Jump) && Crawl == true && Stats.RollingTimer <= -0.2f)
        {
            
            Rolling = true;
            if (CanWalk)
            {
                Stats.RollingTimer = Stats.RollingTimerPadrao;

            }
            else
            {
                //make it chargeable
                RollOnAir = true;
                rollVboost = true;
                Stats.RollingTimer = Stats.RollingTimerOnAir;
            }
            //vertical e horizontal
            if (Stats.Ileft == true) { RollLeft = true; }

            if (Stats.Iup == true) { RollUp = true; }

            if (Stats.Idown == true) { RollDown = true; }

            if (Stats.Iright == true) { RollRight = true; }

            //diagonal
            if (Stats.Iupleft == true) { RollUpLeft = true; }

            if (Stats.Iupright == true) { RollUpRight = true; }

            if (Stats.Idownleft == true) { RollDownLeft = true; }

            if (Stats.Idownright == true) { RollDownRight = true; }
        }
    }

    public void CrawlInput()
    {
   
        if (Input.GetKey(Crounch))
        {
            Crawl = true;
        }
        if (Input.GetKey(Crounch) == false)
        {
            Crawl = false;
        }
    

        if (Input.GetKeyUp(Crounch))
        {
            CrawlTrigger = false;
            WalkingTrigger = true;
        }
        if (Input.GetKeyDown(Crounch))
        {
            CrawlTrigger = true;
            WalkingTrigger = false;
        }

    }
    public void WalkingInput()
    {
       
        
             
         
        StraightWalk();
        StraightWalkPressedKeys();
        if(Stats.PressedKeys >= 2){
        DiagonalWalk();
        }

       

    }

    public void StraightWalk(){
         if (Input.GetKey(Left))
        {
           
            walkingleft = true;

            Stats.Ileft = true;
            Stats.Iup = Stats.Idown = Stats.Iright = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }

        if (Input.GetKey(Up))
        {
            
            walkingup = true;
            Stats.Iup = true;
            Stats.Ileft = Stats.Idown = Stats.Iright = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }

        if (Input.GetKey(Down))
        {
            
            walkingdown = true;
             Stats.Idown = true;
            Stats.Ileft = Stats.Iup = Stats.Iright = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }

        if (Input.GetKey(Right))
        {
            
            walkingright = true;
            Stats.Iright = true;
            Stats.Ileft = Stats.Idown = Stats.Iup = Stats.Idownleft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }
    }

    public void StraightWalkPressedKeys(){

        if(Stats.PressedKeys >= 1 && (!walkingdown && !walkingleft && !walkingright && !walkingup)){
            Stats.PressedKeys = 0;
        }
          if (Input.GetKeyDown(Left))
        {
            Stats.PressedKeys += 1;
           
        }

        if (Input.GetKeyDown(Up))
        {
            Stats.PressedKeys += 1;
            
        }

        if (Input.GetKeyDown(Down))
        {
            Stats.PressedKeys += 1;
            
        }

        if (Input.GetKeyDown(Right))
        {
            Stats.PressedKeys += 1;
            
        }
         ///////////////////////////////////////////////////////////////////////////
        

        if (Input.GetKeyUp(Right))
        {
            Stats.PressedKeys -= 1;
            walkingright = false;
           
        }

        if (Input.GetKeyUp(Up))
        {
            Stats.PressedKeys -= 1;
            walkingup = false;
        }

        if (Input.GetKeyUp(Left))
        {
            Stats.PressedKeys -= 1;
            walkingleft = false;
        }

        if (Input.GetKeyUp(Down))
        {
            Stats.PressedKeys -= 1;
            walkingdown = false;
        }
        /////////////////////////////////////////////////////
         if (!Input.GetKey(Right))
        {
           
            walkingright = false;
           
        }

        if (!Input.GetKey(Up))
        {
            
            walkingup = false;
        }

        if (!Input.GetKey(Left))
        {
            
            walkingleft = false;
        }

        if (!Input.GetKey(Down))
        {
            walkingdown = false;
        }
    }
    public void DiagonalWalk(){
         if (Input.GetKey(Left) && Input.GetKey(Up))
        {
            Stats.Iupleft = true;
            Stats.Iup = Stats.Idown = Stats.Iright = Stats.Idownleft = Stats.Idownright = 
            Stats.Ileft = Stats.Iupright = false;
        }

       if (Input.GetKey(Left) && Input.GetKey(Down))
        {
            Stats.Idownleft = true;
            Stats.Iup = Stats.Idown = Stats.Iright = Stats.Ileft = Stats.Idownright = 
            Stats.Iupleft = Stats.Iupright = false;
        }

        if (Input.GetKey(Right) && Input.GetKey(Up))
        {
            Stats.Iupright = true;
            Stats.Iup = Stats.Idown = Stats.Iupleft = Stats.Idownleft = Stats.Idownright = 
            Stats.Ileft = Stats.Iright = false;
        }

        if (Input.GetKey(Right) && Input.GetKey(Down))
        {
            Stats.Idownright = true;
            Stats.Iup = Stats.Idown = Stats.Iupleft = Stats.Idownleft = Stats.Iright = 
            Stats.Ileft = Stats.Iupleft = false;
        }
    }
    
    public void a(){
       
    }
       

}
