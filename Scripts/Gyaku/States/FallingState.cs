using State;
using UnityEngine;

namespace State
{
	public class FallingState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		private GameObject gameObject;
		float timer;
        private float Scale;
		public FallingState(GameObject This)
		{
			gameObject = This;
		}
        
        	public void OnEnter()
		{
			
            Scale = 0;
			GetCompos();
			Debug.Log(gameObject.name + " is in" + " Falling");
			
			
			timer = 0.25f;
			if(Movement._rb){
			Movement._rb.isKinematic = false;
			Movement._rb.angularDrag = Stats.dragPadrao / 8;
			}
			
		}
		public void GetCompos(){
			Keys = gameObject.GetComponent<GenericInput>();
   	 		Stats = gameObject.GetComponent<GenericStats>();
   			Anim = gameObject.GetComponent<GenericAnimator>();
    		Movement = gameObject.GetComponent<GenericMovement>();
		}
		public void DropHoldedItem(){
			
			if(Movement.SelectedToHold){
				Movement.ItemDetector.ChangeActive = true;
				Movement.ItemDetector.showIcon = true;
				Movement.SelectedToHold.gameObject.GetComponent<GenericMovement>().BeingDropped();
				Movement.SelectedToHold = null;
				Keys.HoldingItem = false;
				Keys.Grabbing = false;
				Keys.Trowing = false;
			}
		}
		public void Tick()
		{
			Debug.DrawRay(gameObject.transform.position,Movement._rb.velocity.normalized * 10,Color.red,4f);
			InputTick();
			StatsTick();
		}
        public void FixedTick()
		{
			
			gameObject.layer = 0;
			if(Anim) if(Anim._anim) AnimTick();
			MovementTick(); 
			timer -= Time.deltaTime;
			if(timer < 0){
				Keys.Landing = true;
				DropHoldedItem();
			}
			
		}








	

		


        public void InputTick(){	
			
		}

		public void StatsTick(){
        	
		}
		public void AnimTick(){
			Anim._anim.SetBool("Jumping",false);

	
			Anim._anim.SetBool("Walking", Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup);
           
			
		}
		public void MovementTick(){
			
			Tool.ChangeMeshColorAll("#ff0000",gameObject);
		    Scale = Mathf.Clamp(Scale,0,0.5f);
			Scale += 0.24f;
	  		Stats.Gforce = (Stats.GforcePadrão) * Scale;
			
			

			Movement._rb.drag = Stats.dragPadrao/8;
			Stats.velocidade = Stats.velocidadepadrao/2;
			if(Keys.Inground){
				//Movement._rb.velocity /= 1.2f;
			}
			
			

			
			
		}

		

        public void OnExit()
		{
			Keys.Falling = false;
			
		}

	}

}
