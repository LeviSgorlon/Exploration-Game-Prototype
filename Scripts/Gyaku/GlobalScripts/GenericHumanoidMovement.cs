using System.Data;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericHumanoidMovement : GenericMovement
{
     
 
   [HideInInspector]
    
    
    public float FlashCd;
    

    public Vector3 NewDir;
    
    
    protected override void Start()
    {
        base.Start();
        _rb = gameObject.GetComponent<Rigidbody>();
        Stats.Friction = 1f;
        
        
        HoldposOffset = new Vector3(0,0,-1);
        HoldDistance = 50;


        GlobalOrientation = GameObject.Find("GlobalOri").transform;
      
    }


    // Update is called once per frame
    protected override void FixedUpdate()
    {
        base.FixedUpdate(); 
        GravHandler();
        MaxVelEffects(); 
        HumanIngroundLogic();
        
        RollingSequence();
        MaxSpeedCap();
        
        
    }
   

    public void Update(){
        //MaxThrowVisual();
        
    }

   public void VelocityAlign(Vector3 Direction){
       
        
        
       NewDir = new Vector3(Direction.x ,0,Direction.z);
       
       if(_rb.velocity.normalized != Vector3.zero && !Pushing && Body != null){
            Body.forward = Vector3.Lerp(Body.forward,NewDir,(Time.deltaTime * Stats.TurnSpeed));
            
       }
        Keys.WalkDir = NewDir;
    
    }

    public void MaxSpeedCap(){
        if(_rb.velocity.magnitude > 1000){
            _rb.velocity /= 2;
        }
    }

    public void MaxThrowVisual(){
        SpriteRenderer[] Sp = transform.root.GetComponentsInChildren<SpriteRenderer>();
        Material DefaultMat = transform.root.GetComponentInChildren<SpriteRenderer>().material;
        FlashCd -= Time.deltaTime;
        if(Keys.TrowMax){   
           if(FlashCd < 0){
                foreach(SpriteRenderer sprite in Sp){ SpriteFlash(sprite); }
                FlashCd = 0.2f;
           }    
           else
           {
                foreach(SpriteRenderer sprite in Sp){ SpriteNormalMat(sprite);}
            }
        }else
        {
                foreach(SpriteRenderer sprite in Sp){
                    SpriteNormalMat(sprite);
                }
        }     
    }

     public void SpriteFlash(SpriteRenderer sprite){
        if(sprite.tag != "Hud"){
                   if(Keys.TrowMax && Keys.TrowMaxBeyond){
                    //sprite.material = Resources.Load("TrowMaxFlashB") as Material;
                   }
                   if(Keys.TrowMax && !Keys.TrowMaxBeyond){
                    //sprite.material = Resources.Load("TrowMaxFlash") as Material;
                   }
        }
    }

    public void SpriteNormalMat(SpriteRenderer sprite){
      if(sprite.tag != "Hud"){
                //sprite.material = Resources.Load("Sprites_Default2") as Material;
        }
    }

    

    
   

    
   
   

    
    
    
    public void MaxVelEffects(){
        if(_rb.velocity.magnitude > 240){

            Keys.MaxRun = true;

        }else{
            Keys.MaxRun = false;
        }

    }  


    public void GravHandler(){
        // if(!Keys.Inground && !Keys.Jumping && !Keys.rollVboost){
        //  Keys.ActivateGrav = true;
        // }
       
    }


  
    public void RollingSequence()
    {
        if (Keys.CanWalk && Keys.RollOnAir == true)
        {
            Stats.RollingTimer = 0;
            
        }

        if(Keys.CanWalk){
            Keys.rollVboost = false;
        }
        if (Stats.RollingTimer > 0)
        {
            if (Keys.RollOnAir == false)
            {
                Roll(1);
            }
            else
            {
                RollVBoost();
            }

        }
        else
        {    
            Keys.RollOnAir = false;
            if (Keys.Rolling == true)
            {

                Stats.OnControl = true;

            }
            //torna falso ao acabar o rolamento
            FlushAnim((int)groups.Roll);



        }
    }
    public void HumanIngroundLogic(){
        if (Keys.CanWalk)
        {
            //Stats.Gforce = 0;
            //_rb.drag = Stats.dragPadrao;
            Keys.RollVM = true;
            //Keys.ActivateGrav = false;
            Anim._anim.speed = 1;
        }
        
    }
    

    public void RollVBoost()
    {
        Keys.Jumping = false;
        if(Keys.RollVM){
            if(Keys.RunningJump == true){          
                _rb.AddForce(_rb.velocity * 160);     
                Keys.RunningJump = false;
            }
            
            _rb.AddForce(GlobalOrientation.up * Stats.JumpVAmount);
            
            Roll(_rb.velocity.magnitude / 100);
            Stats.RollScale = 2;
            Keys.RollVM = false;
        }
       
        
        if (Keys.rollVboost == true)
        {
            
            _rb.AddForce(GlobalOrientation.up * Stats.JumpVAmount);
               
            Roll(Stats.RollScale /= 1.4f);
            Anim._anim.speed += 0.05f;

            if(Stats.RollScale <= 0.6f){
             //   Keys.ActivateGrav = true;
            }
        }
        
         
    }

    

     public void RollHover()
    {
        
        //Keys.ActivateGrav = false;
        if (Keys.rollVboost == true)
        {
            //_rb.AddForce(GlobalOrientation.up * JumpVAmount);
            
            Roll(3);
           
        }
        
         
    }
    public void Roll(float Scale)
    {
       

        //horizontal e vertical

        if (Keys.RollDown == true)
        {
            _rb.AddForce(GlobalOrientation.forward * -Stats.RollingForce * Scale);
        }
        if (Keys.RollUp == true)
        {
            _rb.AddForce(GlobalOrientation.forward * Stats.RollingForce * Scale);
        }
        if (Keys.RollRight == true)
        {
            _rb.AddForce(GlobalOrientation.right * Stats.RollingForce * Scale);
        }
        if (Keys.RollLeft == true)
        {
            _rb.AddForce(GlobalOrientation.right * -Stats.RollingForce * Scale);
        }

        //diagonal

        if (Keys.RollDownLeft == true)
        {
            _rb.AddForce(GlobalOrientation.forward * (-Stats.RollingForce * Scale) / 1.1f);
            _rb.AddForce(GlobalOrientation.right * (-Stats.RollingForce * Scale) / 1.1f);
        }
        if (Keys.RollUpLeft == true)
        {
            _rb.AddForce(GlobalOrientation.forward * (Stats.RollingForce * Scale) / 1.1f);
            _rb.AddForce(GlobalOrientation.right * (-Stats.RollingForce * Scale) / 1.1f);
        }
        if (Keys.RollDownRight == true)
        {
            _rb.AddForce(GlobalOrientation.forward * (-Stats.RollingForce * Scale) / 1.1f);
            _rb.AddForce(GlobalOrientation.right * (Stats.RollingForce * Scale) / 1.1f);
        }
        if (Keys.RollUpRight == true)
        {
            _rb.AddForce(GlobalOrientation.forward * (Stats.RollingForce * Scale) / 1.1f);
            _rb.AddForce(GlobalOrientation.right * (Stats.RollingForce * Scale) / 1.1f);

        }

        // Porra((S) => { Console.WriteLine(S); });
    }





    public void FlushAnim(int State)
    {
        Keys.Rolling =
        Keys.RollDown =
        Keys.RollUp =
        Keys.RollLeft =
        Keys.RollRight =
        Keys.RollDownLeft =
        Keys.RollUpLeft =
        Keys.RollDownRight =
        Keys.RollUpRight = !(State == (int)groups.Roll);
    }


    public enum groups : int
    {
        Roll = 0,
        Walk = 1,
        Jump = 2,
        Attack = 3

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
