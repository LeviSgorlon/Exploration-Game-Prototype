using State;
using UnityEngine;

namespace State
{
	public class ThrowingState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		private GameObject gameObject;
		private float Forcecap;

		private float Timer;

		private float Scale;
		public ThrowingState(GameObject This)
		{
			gameObject = This;
		}

		

		public void Tick()
		{
			AnimTick();
			InputTick();
			StatsTick();
            
		}
        public void FixedTick()
		{
			MovementTick();
			
		}








		public void OnEnter()
		{
            Timer = 0.3f;
			Scale = 0;
			GetCompos();
			Keys.HoldingItem = false;
			Debug.Log(gameObject.name + " is in" + " Throwing");
            TrowSequence();
			Forcecap = Mathf.Clamp(Movement._rb.velocity.magnitude/30,0,15);
		}
		public void GetCompos(){
			Keys = gameObject.GetComponent<GenericInput>();
   	 		Stats = gameObject.GetComponent<GenericStats>();
   			Anim = gameObject.GetComponent<GenericAnimator>();
    		Movement = gameObject.GetComponent<GenericMovement>();
		}
		public void OnExit()
		{
			Movement.SelectedToHold = null;
			Keys.Trowing = false;
		}

        public void TrowSequence(){
            ItemTrowPrep(Movement.SelectedToHold);
            Movement.SelectedToHold.gameObject.GetComponent<GenericMovement>().BeingTrown(Keys.LookDir);
            Movement.SelectedToHold = null;
    	}
   
    public void ItemTrowPrep(GameObject Object){
            Object.layer = 0;
            Movement.ItemDetector.ChangeActive = true;
            Movement.ItemDetector.showIcon = true;
    }
        public void InputTick(){	
			
		}

		public void StatsTick(){
        	
		}
		public void AnimTick(){
			
		}
		public void MovementTick(){
			Tool.ChangeMeshColorAll("#7dd5ff",gameObject);
			if(Timer < 0){
				Keys.Landing = false;
				Keys.Trowing = false;
				Keys.TrowEngage = false;
				Keys.HoldingItem = false;
			    Keys.Trowing = false;
			}
			if(Timer <= 0.29f && Timer >= 0.28f){
				//Movement._rb.AddForce(-Keys.LookDir * Movement._rb.mass * 6000 * Forcecap);
			}

			Scale = Mathf.Clamp(Scale,0,1);
			Scale += 0.08f;
        	
	  		Stats.Gforce = 0;
			Movement._rb.drag = Stats.dragPadrao;

			Timer -= Time.deltaTime;
			Stats.velocidade = Stats.velocidadepadrao/16;
			//Stats.TurnTime = 0.2f;
			
			GroundPropertys();
		}

		public void GroundPropertys(){
			if(Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup){
				Movement.Col.material = Resources.Load("Slipp") as PhysicMaterial;
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.velocidade );
				Stats.Gforce = 0;
			}else if (Movement.OldVel <= 0){
				Movement._rb.AddForce(-Movement.GlobalOrientation.up * (Stats.Gforce / 2) * Stats.GravScale);
				Movement.Col.material = Resources.Load("Stop") as PhysicMaterial;	
				
			}
			if(Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup){
				Movement.OldVel= Stats.velocityMag;
				
			}else{
				Movement._rb.velocity += Keys.LookDir * Movement.OldVel/500;
				Movement.OldVel /= 1.1f;
			}
		}

		

	}

}
