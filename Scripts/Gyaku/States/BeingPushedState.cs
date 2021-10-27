using State;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace State
{
	public class BeingPushedState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		public List<Component> components;
		private GameObject gameObject;

		private Quaternion Rot;
		public BeingPushedState(GameObject This)
		{
			gameObject = This;
		}

		

		public void Tick()
		{
			
			
			InputTick();
			StatsTick();
            
		}
        public void FixedTick()
		{
			if(Anim) if(Anim._anim) AnimTick();
			MovementTick();
			
		}








		public void OnEnter()
		{
			GetCompos();
			Debug.Log(gameObject.name + " is in" + " beingpushed");
			
			Keys.Landing = false;
			Rot = Movement._rb.rotation;
		}
		public void GetCompos(){
			Keys = gameObject.GetComponent<GenericInput>();
   	 		Stats = gameObject.GetComponent<GenericStats>();
   			Anim = gameObject.GetComponent<GenericAnimator>();
    		Movement = gameObject.GetComponent<GenericMovement>();
		}
		public void OnExit()
		{
			
		}


        public void InputTick(){	
			
		}

		public void StatsTick(){
        	
		}
		public void AnimTick(){
			
            Anim._anim.SetBool("Walking", Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup);
            

			 if(Movement._rb.velocity.z < -1) { Anim._anim.SetBool("Jumping", true);} else { Anim._anim.SetBool("Jumping", false);}
            Anim._anim.SetBool("InGround", Keys.CanWalk);
            Anim._anim.SetBool("JumpStart",Keys.JumpStart);

			if(Anim != null && Anim._anim != null){
				if( Movement._rb.velocity.magnitude > 1){
            		Anim._anim.speed = Movement._rb.velocity.magnitude / 110;
				}else{
					Anim._anim.speed = 0;
				}
			}

			Anim._anim.SetBool("Landing",false);
		}
		public void MovementTick(){
			Keys.HoldingItem = false;
			Keys.Grabbing = false;
			Keys.Trowing = false;
			Tool.ChangeMeshColorAll("#b687ff",gameObject);
			Stats.LandingJumpTime -= Time.deltaTime;
			Movement._rb.angularDrag = Stats.dragPadrao * 8000;
			Keys.checkGround = true;
			
			Movement._rb.drag = Stats.dragPadrao;
			Stats.Gforce = Stats.GforcePadrão;
			Movement._rb.rotation = Rot;
			Movement.Col.material = Resources.Load("Slipp") as PhysicMaterial;
			
			
			

			Keys.GravStartTimer -= Time.deltaTime;
        	
		}

		

	}

}
