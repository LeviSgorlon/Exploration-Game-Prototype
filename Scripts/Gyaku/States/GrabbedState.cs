using State;
using UnityEngine;

namespace State
{
	public class GrabbedState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		private GameObject gameObject;
		public Vector3 NewPos;
		public GrabbedState(GameObject This)
		{
			gameObject = This;
		}

		

		public void Tick()
		{
			AnimTick();
			InputTick();
			StatsTick();
            

			if(gameObject.transform.root != gameObject){
				gameObject.transform.position = Movement.Holder.transform.position + NewPos;
			}
		}
        public void FixedTick()
		{
			MovementTick();
			
		}

		public void OnEnter()
		{
			GetCompos();
			Keys.Grabbed = true;
			Debug.Log(gameObject.name + " is in" + " Grabbed");
			Movement.BeingGrabbed(Movement.Holder);
			Movement.Holder.GetComponent<GenericInput>().HoldingItem = true;
			//gameObject.transform.parent = Movement.Holder.transform;
		}
		public void GetCompos(){
			Keys = gameObject.GetComponent<GenericInput>();
   	 		Stats = gameObject.GetComponent<GenericStats>();
   			Anim = gameObject.GetComponent<GenericAnimator>();
    		Movement = gameObject.GetComponent<GenericMovement>();
		}
		public void OnExit()
		{
			//gameObject.transform.parent = null;
			//Movement._rb.isKinematic = false;
		}


        public void InputTick(){	
			
		}

		public void StatsTick(){
        	
		}
		public void AnimTick(){
			
		}
		public void MovementTick(){


			Tool.ChangeMeshColorAll("#fff121",gameObject);

			Collider collider = gameObject.GetComponent<Collider>();
			float Girth = Vector3.Distance(collider.bounds.center,collider.bounds.max);
			float Height = Vector3.Distance(collider.bounds.center,collider.bounds.min);
			Vector3 HoldingPos = new Vector3(
			Movement.Holder.GetComponent<GenericMovement>().Body.forward.normalized.x * Girth ,
			Height, Movement.Holder.GetComponent<GenericMovement>().Body.forward.normalized.z * Girth);

			//Movement._rb.isKinematic = true;
			Movement.Holder.GetComponent<GenericInput>().HoldingItem = true;
			gameObject.layer = 9;
			Stats.Gforce = 0;
			NewPos = Vector3.LerpUnclamped(NewPos,HoldingPos,Time.deltaTime * 8);
			
        	
		}

		

	}

}
