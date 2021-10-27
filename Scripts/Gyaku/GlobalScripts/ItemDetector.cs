using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDetector : MonoBehaviour
{
    // Start is called before the first frame update
    
      public string Target;
    public bool Left; //0

    public bool Downleft; //45

     public bool Down; //90

     public bool Downright; //135

     public bool Right;  //180  -180

    public bool Upright; //-135
    public bool Up; // -90
    public bool Upleft;  //-45
    
    
    
    public Vector2 Angle;
    
   
   
    public float Orientation;

    public float Distance;
    public float Distance2;
    public float Distance3;

    public GameObject Locked;

    public List<GameObject> Items;
    
    
    private int i;
    public bool ChangeActive;
   
    public float LerpFactor;
    public Vector3 dir;
    
    Vector3 Center;
   
   public virtual void Awake(){
        Locked = null;
         ChangeActive = true;
   }

    // Update is called once per frame
    public virtual void FixedUpdate()
    {
    
        Directions();
        Center = transform.root.GetComponent<Collider>().bounds.center;
        //Items = GameObject.FindGameObjectsWithTag(Target);
        Angle = Quaternion.AngleAxis(Orientation, Vector3.forward) * Vector3.right;

}
    public void Update(){
        if(ChangeActive == true) {
        CalculateDistance();
    }
    }


    public void OnTriggerEnter(Collider col){
        if(col.GetComponent<GenericInput>() != null){
        if(col.GetComponent<GenericInput>().Grababble){

            Items.Add(col.gameObject);
            
        }

            
        }
       

    }

     public void OnTriggerStay(Collider col){
       
        if(col.GetComponent<GenericInput>() != null){

        if(col.GetComponent<GenericInput>().Grababble && col.gameObject == Locked){

           
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Orientation = angle;
           
            Vector3 Closest = col.GetComponent<Collider>().ClosestPoint(transform.root.GetComponent<Collider>().bounds.center);
            Distance = Vector3.Distance(Closest,Center);
        }
        if(col.GetComponent<GenericInput>().Grababble && Locked == null){
             Locked = col.gameObject;

        }

        }
        
    }

    public void OnTriggerExit(Collider col){
        if(col.GetComponent<GenericInput>() != null){
            
            if(col.GetComponent<GenericInput>().Grababble && col.gameObject == Locked){
            Locked = null;
            
        }

            
        }
        
         if(col.gameObject.tag == Target){
             Items.Remove(col.gameObject);
            }
    }




    public void Directions(){
        if(Orientation > -22.5f && Orientation < 22.5f){
            Up =
            Upleft = 
            Upright = 
            Down = 
            Downleft = 
            Downright =
            Left = false;

            Right = true;
            
        }
        if( Orientation > 22.5f  && Orientation < 67.5f){
            Up =
            Upleft = 
            Downleft = 
            Down = 
            Left = 
            Downright =
            Right = false;

            Upright = true;
        }
         if(Orientation > 67.5f  && Orientation < 112.5f){
             Down =
            Upleft = 
            Upright = 
            Downleft = 
            Left = 
            Downright =
            Right = false;
            
            Up = true;
        }
         if(Orientation > 112.5f  && Orientation < 157.5f){
             Up =
            Upleft = 
            Downright = 
            Down = 
            Left = 
            Downleft =
            Right = false;

            Upleft = true;
        }
         if(Orientation > 157.5f  | Orientation < -157.5f){
             Up =
            Upleft = 
            Upright = 
            Down = 
             Right = 
            Downright =
            Downleft = false;
            
           Left = true;
        }
         if(Orientation > -157.5f  && Orientation < -112.5f){
             Up =
            Upleft = 
            Upright = 
            Down = 
            Left = 
            Downright =
            Right = false;
            
            Downleft = true;
        }
         if(Orientation > -112.5f  && Orientation < -67.5f){
             Downleft =
            Upleft = 
            Upright = 
            Up  = 
            Left = 
            Downright =
            Right = false;
            
            Down = true;

        }
         if(Orientation > -67.5f  && Orientation < -22.5f){
             Up =
            Downleft = 
            Upright = 
            Down = 
            Left = 
            Upleft =
            Right = false;
            
           Downright = true;
        }

    }

    public void CalculateDistance(){
        if(i < Items.Count){   
            Vector3 Closest = Items[i].GetComponent<Collider>().ClosestPoint(transform.root.GetComponent<Collider>().bounds.center);
            Distance2 = Vector3.Distance(Closest,Center);
            if(Distance3 > Distance2){
               
                    Debug.DrawRay(Center,Closest - Center,Color.blue,0.05f);
                
                
            }else{
               
                    Debug.DrawRay(Center,Closest - Center,Color.white,0.05f);
                
            }
            
            if(Distance2 < Distance){
                Locked = Items[i].gameObject;
                LerpFactor = 0;
            }else{
                i++;
                Distance3 = Vector3.Distance(Closest,Center);
            }
            
        }else{
            i = 0;
        }

    }
}
