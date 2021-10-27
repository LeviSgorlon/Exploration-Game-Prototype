using State;
using UnityEngine;

namespace State
{
	public class JumpSquatState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		
	
		private GameObject gameObject;
		
		public JumpSquatState(GameObject This)
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
            Keys.CanWalk = false;
			Keys.Landing = false;
            Stats.JumpInit -= Time.deltaTime;
			
            if(Stats.JumpInit < 0 && Keys.JumpStart == true){  
            Keys.Jumping = true;  
            }

		}








		public void OnEnter()
		{
			
            Debug.Log(gameObject.name + " is in" + " JumpingSquat");
           	GetCompos();
			Stats.JumpInit = Stats.JumpDelayDefault;
			Stats.Gforce = 0;
            //Movement._rb.velocity /= 1000;
			DropHoldedItem();
			
		}
		public void DropHoldedItem(){
			
			if(Movement.SelectedToHold){
				Movement.SelectedToHold.layer = 0;
				Movement.ItemDetector.ChangeActive = true;
				Movement.ItemDetector.showIcon = true;
				Movement.SelectedToHold.gameObject.GetComponent<GenericMovement>().BeingDropped();
				Movement.SelectedToHold = null;
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
			Keys.JumpStart = false;
            //Keys.Jumping = true;
			Keys.Landing = false;
		}


        public void InputTick(){	
		
		}
		public void StatsTick(){
		
		}
		public void AnimTick(){
			if(Movement._rb.velocity.z < -1) { Anim._anim.SetBool("Jumping", true);} else { Anim._anim.SetBool("Jumping", false);}
            Anim._anim.SetBool("JumpStart",Keys.JumpStart);
			Anim._anim.SetBool("InGround", Keys.CanWalk);
		}
		public void MovementTick(){
			Tool.ChangeMeshColorAll("#eb6534",gameObject);
			Anim._anim.speed = 1;
			Keys.HoldingItem = false;
			Keys.Grabbing = false;
			Keys.Trowing = false;
        
            Stats.velocidade = Stats.velocidadepadrao/2;
	  		//Movement._rb.velocity /= 4;
        	//Movement._rb.drag = Stats.dragPadrao * 8000;
            
		}
		

	}

}
