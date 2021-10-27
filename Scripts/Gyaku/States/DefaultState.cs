using State;
using UnityEngine;

namespace State
{
	public class DefaultState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		private GameObject gameObject;
		public DefaultState(GameObject This)
		{
			gameObject = This;
		}
        
        	public void OnEnter()
		{
			GetCompos();
		}
		public void GetCompos(){
			Keys = gameObject.GetComponent<GenericInput>();
   	 		Stats = gameObject.GetComponent<GenericStats>();
   			Anim = gameObject.GetComponent<GenericAnimator>();
    		Movement = gameObject.GetComponent<GenericMovement>();
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








	

		


        public void InputTick(){	
			
		}

		public void StatsTick(){
        	
		}
		public void AnimTick(){
			
		}
		public void MovementTick(){
		
        	
		}

		

        public void OnExit()
		{
			
		}

	}

}
