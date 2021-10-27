using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericAnimator : MonoBehaviour
{
    // Start is called before the first frame update
    public Animator _anim;
    public GenericInput Keys;
    public GenericStats Stats;

    public GenericMovement Movement;
    protected virtual void Start()
    {
        _anim = gameObject.GetComponent<Animator>();
        Keys = gameObject.GetComponent<GenericInput>();
        Stats = gameObject.GetComponent<GenericStats>();
        Movement = gameObject.GetComponent<GenericMovement>();
    }

    // Update is called once per frame
    protected virtual void LateUpdate()
    {
       
        
    }





    public void RefreshAnim(int State)
    {
       
        if(State == (int)groups.Walk)
        {
            if(Stats.PressedKeys != 0){
            _anim.SetFloat("Horizontal", Keys.LookDir.x);
            _anim.SetFloat("Vertical", Keys.LookDir.y);
            _anim.SetBool("Walking", Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup);
            }else
            {
                _anim.SetBool("Walking",false);
            }
            

            _anim.SetBool("walkingright", Keys.walkingright && !Keys.walkingleft);
            _anim.SetBool("walkingdown", Keys.walkingdown && !Keys.walkingup);
            _anim.SetBool("walkingup", Keys.walkingup && !Keys.walkingdown);
            _anim.SetBool("walkingleft", Keys.walkingleft && !Keys.walkingright);
        }

        if (State == (int)groups.Roll)
        {

            _anim.SetBool("rolling", Keys.Rolling);
        }

        if (State == (int)groups.Crawl)
        {
            if (Keys.Crawl && Keys.CrawlTrigger == true)
            {
                _anim.SetTrigger("crawltrigger");
                Keys.CrawlTrigger = false;
            }
            if (Keys.Crawl == false && Keys.WalkingTrigger == true)
            {
                _anim.SetTrigger("walktrigger");
                Keys.WalkingTrigger = false;
            }

        }
        if (State == (int)groups.Facing)
        {
            _anim.SetBool("Iup", Stats.Iup);
            _anim.SetBool("Idown", Stats.Idown);
            _anim.SetBool("Ileft", Stats.Ileft);
            _anim.SetBool("Iright", Stats.Iright);
            _anim.SetBool("Iupleft", Stats.Iupleft);
            _anim.SetBool("Iupright", Stats.Iupright);
            _anim.SetBool("Idownleft", Stats.Idownleft);
            _anim.SetBool("Idownright", Stats.Idownright);
        }

        if (State == (int)groups.Jump)
        {

            
           
            
            _anim.SetBool("jumpinit", Keys.JumpStart);
            _anim.SetInteger("UpVel", (int)Movement._rb.velocity.z);
            _anim.SetInteger("Zvel", (int)Movement._rb.velocity.y);

            if(Movement._rb.velocity.z < -1) { _anim.SetBool("Jumping", true);} else { _anim.SetBool("Jumping", false);}
            _anim.SetBool("InGround", Keys.CanWalk);
            _anim.SetBool("JumpStart",Keys.JumpStart);
            _anim.SetBool("Inground", Keys.CanWalk);
        }
         if (State == (int)groups.Trow)
        {

            _anim.SetBool("TrowEngage",Keys.TrowEngage);
            _anim.SetBool("Trow",Keys.Trowing);
            _anim.SetBool("TrowMax",Keys.TrowMax);
            _anim.SetBool("HoldingOBJHeavy",Keys.HoldingItem);
        }

    }
    
}
public enum groups : int
    {
        Roll = 0,
        Walk = 1,
        Jump = 2,
        Attack = 3,
        Crawl = 4,
        Facing = 5,
        Trow = 6

    }


   



