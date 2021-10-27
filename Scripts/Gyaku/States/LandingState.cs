using State;
using UnityEngine;

namespace State
{
	public class LandingState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		
	
		private GameObject gameObject;
		
		public LandingState(GameObject This)
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
            Keys.checkGround = false;
            //Keys.Inground = false;
           
            Stats.LandingTime -= Time.deltaTime;
			
            if(Stats.LandingTime < 0){  
            Keys.Landing = false;
			Keys.checkGround = true; 
            }

		}

		public void DropHoldedItem(){
			
			if(Movement.SelectedToHold){
				Keys.HoldingItem = false;
				Keys.Grabbing = false;
				Keys.Trowing = false;
				Movement.ItemDetector.ChangeActive = true;
				Movement.ItemDetector.showIcon = true;
				Movement.SelectedToHold.gameObject.GetComponent<GenericMovement>().BeingDropped();
				Movement.SelectedToHold = null;
			}
		}






		public void OnEnter()
		{
            Debug.Log(gameObject.name + " is in" + " LandingSquat");
           	GetCompos();
			Prep();
			if(Anim != null)  {
				Anim._anim.SetBool("Landing",true);
			} 
			DropHoldedItem();
		}
	
		public void GetCompos(){
			Keys = gameObject.GetComponent<GenericInput>();
   	 		Stats = gameObject.GetComponent<GenericStats>();
   			Anim = gameObject.GetComponent<GenericAnimator>();
    		Movement = gameObject.GetComponent<GenericMovement>();
		}
		public void Prep(){
			Stats.LandingTime = Stats.LandingTimeDefault;
            Stats.LandingJumpTime = 0.2f;
		}
		public void OnExit()
		{
			if(Anim != null)   Anim._anim.SetBool("Landing",false);
			
			if(Keys.JumpStart){
				//Keys.Jumping = true;
			}
		}


        public void InputTick(){	
		
		}
		public void StatsTick(){
		
		}
		public void AnimTick(){
			if(Movement._rb.velocity.z < -1) { Anim._anim.SetBool("Jumping", true);} else { Anim._anim.SetBool("Jumping", false);}
            Anim._anim.SetBool("JumpStart",Keys.JumpStart);
			Anim._anim.SetBool("InGround", Keys.CanWalk); 

			Anim._anim.SetBool("Landing",true);
		}
		public void MovementTick(){
			Tool.ChangeMeshColorAll("#11abc1",gameObject);
			if(Anim != null){
			Anim._anim.speed = 1;
			}
			//Stats.velocidade = Stats.velocidadepadrao/32;
        	Movement._rb.drag = Stats.dragPadrao * 2.5f;
			Stats.Gforce = Stats.GforcePadrão;
			//Movement._rb.velocity /= 80;
			

			GroundPropertys();
		}

		public void GroundPropertys(){
			if(Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup){
				Movement.Col.material = Resources.Load("Slipp") as PhysicMaterial;
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.velocidade * 8);
				Stats.Gforce = 0;
			}else if (Movement.OldVel <= 0){
				Movement._rb.AddForce(-Movement.GlobalOrientation.up * (Stats.Gforce / 2) * Stats.GravScale);
				Movement.Col.material = Resources.Load("Stop") as PhysicMaterial;	
				
			}
		}

	}

}
