using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : GenericHumanoidMovement
{
    // Start is called before the first frame update
    public Transform CameraT;
    
    Vector3 Foward;
    Vector3 Right;
    protected override void Start(){
        base.Start();
        CameraT = GameObject.Find("Camera").transform;
        
    }
    protected override void FixedUpdate()
    {
        Col = gameObject.GetComponent<Collider>();
        VelocityCheck();
        Gravity();
        MassMath();
        TransparencyEffects();
        Shake();
        AlignObj(GrindingEffect);
       if(LastCol != null) BeingCarried(LastCol);
        
        WalkingLogicP();

    }
    public void WalkingLogicP()
    {
        if(Keys.CanWalk){
        Vector3 F = Vector3.Cross(Keys.GroundColOriPost,-CameraT.right);
        Vector3 R = Vector3.Cross(Keys.GroundColOriPost,CameraT.forward);
        Tfoward = new Vector3(CameraT.forward.x,F.y,CameraT.forward.z);
        Tright = new Vector3(CameraT.right.x,R.y,CameraT.right.z);
        }else{
        Tfoward = new Vector3(CameraT.forward.x,0,CameraT.forward.z);
        Tright = new Vector3(CameraT.right.x,0,CameraT.right.z);
        }
        Directions();
    }
    public void Directions(){
                
            Foward = CameraT.forward;
                Right = CameraT.right;
        if (Stats.PressedKeys > 0)
        {
            _rb.AddForce(Body.forward.normalized * Stats.velocidade * _rb.mass / 4000);
        }
            if (Keys.walkingup)
            {
                VelocityAlign(Foward);
                
                Debug.DrawRay(transform.position,Tfoward.normalized * 300,Color.cyan,0.01f);
                Debug.DrawRay(transform.position + Tfoward.normalized * 20,Vector3.up * 30,Color.red,0.1f);
            }
            if (Keys.walkingdown)
            {
                VelocityAlign(-Foward);
                
            }
            if (Keys.walkingright)
            {
                VelocityAlign(Right);
                
            }
            if (Keys.walkingleft)
            {
                VelocityAlign(-Right);
                Debug.DrawRay(transform.position,Tright.normalized * -300,Color.cyan,0.01f);
                Debug.DrawRay(transform.position + Tright.normalized * -20,Vector3.up * 30,Color.red,0.1f);
                
        }
    }



    

    
  

   
    
 
}




