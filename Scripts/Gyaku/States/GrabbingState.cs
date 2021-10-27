using State;
using UnityEngine;

namespace State
{
	public class GrabbingState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		private GameObject gameObject;
		public GrabbingState(GameObject This)
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
           
			GetCompos();
			Debug.Log(gameObject.name + " is in" + " Grabbing");


            Movement.ItemDetector.ChangeActive = false;
            
		}
		public void GetCompos(){
			Keys = gameObject.GetComponent<GenericInput>();
   	 		Stats = gameObject.GetComponent<GenericStats>();
   			Anim = gameObject.GetComponent<GenericAnimator>();
    		Movement = gameObject.GetComponent<GenericMovement>();
		}
		public void OnExit()
		{
			Movement.ItemDetector.ChangeActive = true; 
		}
        public void MoveToItem(){
        //transform.position = ItemToHold.transform.position;
    }
        public void Hold(){
                if(Movement.SelectedToHold == null){
                Movement.SelectedToHold = Movement.ItemDetector.Locked;
                }
                Keys.HoldingItem = true;
                Movement.SelectedToHold.gameObject.GetComponent<GenericMovement>().BeingGrabbed(gameObject); 

        }
     

        public void InputTick(){	
			
		}

		public void StatsTick(){
        	
		}
		public void AnimTick(){
			
		}
		public void MovementTick(){
			Tool.ChangeMeshColorAll("#001dd5",gameObject);
            MoveToItem();
            Hold();
            
			Movement._rb.drag = Stats.dragPadrao;


		GroundPropertys();
		}

		public void GroundPropertys(){
			if(Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup){
				Movement.Col.material = Resources.Load("Slipp") as PhysicMaterial;
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.velocidade * 2);
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
