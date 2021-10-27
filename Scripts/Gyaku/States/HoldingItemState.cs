using State;
using UnityEngine;

namespace State
{
	public class HoldingState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		private GameObject gameObject;
		public HoldingState(GameObject This)
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
			MovementTick();
			if(Anim) if(Anim._anim) AnimTick();
		}








		public void OnEnter()
		{
           
			GetCompos();
			if(Keys.Jumping){
				Keys.Jumping = false;
			}

			HoldingSequence();
			Debug.Log(gameObject.name + " is in" + "Holding");
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

        public void HoldingSequence(){
            Keys.Grabbing = false;
            Movement.ItemDetector.showIcon = false;
            Movement.SelectedToHold.layer = 9;     
            
            
            Movement.SelectedToHold.TryGetComponent<Rigidbody>(out Rigidbody a);
            if(a != null){
                //a.isKinematic = true;
            }
            Movement.CarrySpeedMath(Movement.SelectedToHold.GetComponent<Rigidbody>());
        }
        public void InputTick(){	
			
		}


        
		public void StatsTick(){
        	
		}
		public void AnimTick(){
            Anim._anim.SetBool("Walking", Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup);
            

			if(Movement._rb.velocity.z < -1) { Anim._anim.SetBool("Jumping", true);} else { Anim._anim.SetBool("Jumping", false);}
            Anim._anim.SetBool("JumpStart",Keys.JumpStart);
			Anim._anim.SetBool("InGround", Keys.CanWalk);  

			if(Anim != null && Anim._anim != null){
				if( Movement._rb.velocity.magnitude > 1){
            		Anim._anim.speed = Movement._rb.velocity.magnitude / 110;
				}else{
					Anim._anim.speed = 0;
				}
			}
		}
		public void MovementTick(){
            HoldingSequence();
			
			if(Keys.Jumping){
				Keys.HoldingItem = false;
			}
			Tool.ChangeMeshColorAll("#2bc760",gameObject);

			Stats.LandingJumpTime -= Time.deltaTime;
			Movement._rb.angularDrag = Stats.dragPadrao * 4;
			Keys.checkGround = true;
			
			Movement._rb.drag = Stats.dragPadrao;
			Stats.Gforce = 0;
			if(Movement._rb.velocity.magnitude > 5){
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.velocidade );
			}

			Stats.velocidade = Stats.velocidadepadrao;


			if (Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup){
				Movement.OldVel= Stats.velocityMag;
				
			}else{
				Movement._rb.velocity += Keys.LookDir * Movement.OldVel/500;
				Movement.OldVel /= 1.1f;
			}
		GroundPropertys();
		}

		public void GroundPropertys(){
			if(Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup){
				Movement.Col.material = Resources.Load("Slipp") as PhysicMaterial;
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.velocidade * 8);
				Movement.OldVel = Stats.velocityMag;
			}else{
				Movement._rb.AddForce(-Movement.GlobalOrientation.up * (Stats.Gforce / 2) * Stats.GravScale);
				Movement.Col.material = Resources.Load("Stop") as PhysicMaterial;	
			}
			
			if(!Keys.walkingdown && !Keys.walkingleft && !Keys.walkingright && !Keys.walkingup){
				Movement._rb.velocity += Movement._rb.velocity.normalized * Movement.OldVel/500;
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.velocidade * 8);
				Movement.OldVel /= 1.2f;
				
			}
		}

		

	}

}
