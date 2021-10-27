using State;
using UnityEngine;

namespace State
{
	public class IdleState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		private GameObject gameObject;
		
		public IdleState(GameObject This)
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
			Debug.Log(gameObject.name + " is in" + " Idle");
			
			Keys.Landing = false;
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
			Tool.ChangeMeshColorAll("#0f0f0f",gameObject);
			Stats.LandingJumpTime -= Time.deltaTime;
			Movement._rb.angularDrag = Stats.dragPadrao * 4;
			Stats.velocidade = Stats.velocidadepadrao;
			Keys.checkGround = true;
			Movement._rb.drag = Stats.dragPadrao;

			
			Stats.Gforce = 0;
			Stats.TurnSpeed = Stats.TurnSpeedpadrao;
			Keys.GravStartTimer -= Time.deltaTime;
        	GroundPropertys();
		}

		public void GroundPropertys(){
			if(Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup){
				Movement.Col.material = Resources.Load("Slipp") as PhysicMaterial;
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.GforcePadrão/2);
				
				Movement.OldVel = Stats.velocityMag;
			}else{
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.GforcePadrão/2);
				Movement.Col.material = Resources.Load("Stop") as PhysicMaterial;	
			}
			
			if(!Keys.walkingdown && !Keys.walkingleft && !Keys.walkingright && !Keys.walkingup){
				Movement._rb.velocity += Movement._rb.velocity.normalized * Movement.OldVel/500;
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.GforcePadrão/2);
				Movement.OldVel /= 1.2f;
				
			}
		}

	}

}
