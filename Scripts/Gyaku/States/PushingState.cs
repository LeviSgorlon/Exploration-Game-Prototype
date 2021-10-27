using State;
using UnityEngine;

namespace State
{
	public class PushingState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		private GameObject gameObject;
		public PushingState(GameObject This)
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
			Debug.Log(gameObject.name + " is in" + "Pushing");
			
			Keys.Landing = false;
            Keys.Running = false;
			DropHoldedItem();

			
		}
		public void AligntoObject()
        {
			Vector3 Dir;
			if (Movement._rb.velocity.normalized != Vector3.zero && Keys.CanWalk && Movement.Pushing && Movement.Body != null)
			{
				Dir.z = Movement.PushingNormal.z;
				Dir.x = Movement.PushingNormal.x;
				Dir.y = 0;
				
				Movement.Body.forward = Dir;
			}
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

		public void DropHoldedItem(){
			
			if(Movement.SelectedToHold){
				Movement.SelectedToHold.gameObject.GetComponent<GenericMovement>().BeingDropped();
				Movement.ItemDetector.ChangeActive = true;
				Movement.ItemDetector.showIcon = true;
				Keys.HoldingItem = false;
				Keys.Grabbing = false;
				Keys.Trowing = false;
				Movement.SelectedToHold = null;
			}
		}
		public void MovementTick(){

			Vector3 DirectionPush;
			if (Movement.Body != null && Movement.ObjectPushed != null)
			{
				AligntoObject();
				DirectionPush = Movement.Body.forward;
				Movement.ObjectPushed.GetComponent<Rigidbody>().AddForce(DirectionPush * Movement.ObjectPushed.GetComponent<Rigidbody>().mass * 120);
				Debug.DrawRay(gameObject.transform.position, DirectionPush * 800, Color.blue, 0.1f);
			}
			
			Movement._rb.velocity *= 1.04f;
			Tool.ChangeMeshColorAll("#df3062",gameObject);
			Stats.LandingJumpTime -= Time.deltaTime;
			Movement._rb.angularDrag = Stats.dragPadrao * 4;
			
			Keys.checkGround = true;
			DropHoldedItem();
			Movement._rb.drag = Stats.dragPadrao;
			
			float Influence = Mathf.Clamp(Movement.ObjectPushedRelativeWeight/70,1,100);
			
			Keys.GravStartTimer -= Time.deltaTime;
        	GroundPropertys();
		}

		public void GroundPropertys(){
			if(Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup){
				Movement.Col.material = Resources.Load("Slipp") as PhysicMaterial;
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.velocidade * 4);
				Stats.Gforce = 0;
				Movement.OldVel = Stats.velocityMag;
			}else if(Movement.OldVel <= 10){
				Movement._rb.AddForce(-Movement.GlobalOrientation.up * (Stats.Gforce / 2) * Stats.GravScale);
				Movement.Col.material = Resources.Load("Stop") as PhysicMaterial;	
			}
			
			if(!Keys.walkingdown && !Keys.walkingleft && !Keys.walkingright && !Keys.walkingup){
				//Movement._rb.velocity += Movement._rb.velocity.normalized * Movement.OldVel/500;
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.velocidade * 4);
				Movement.OldVel /= 1.2f;
				
			}
		}
		

	}

}
