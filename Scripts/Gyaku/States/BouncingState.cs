using State;
using UnityEngine;

namespace State
{
	public class BouncingState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		
		private GameObject gameObject;
		public BouncingState(GameObject This)
		{
			gameObject = This;
		}

		

		public void Tick()
		{
			AnimTick();
			InputTick();
			StatsTick();
		}
		public void InputTick(){	
			
		}
		public void StatsTick(){
			
		}
		public void AnimTick(){
			Anim.RefreshAnim((int)groups.Facing); 
        	Anim.RefreshAnim((int)groups.Walk);               
        	Anim.RefreshAnim((int)groups.Jump);      
        	Anim.RefreshAnim((int)groups.Crawl); 
        	Anim.RefreshAnim((int)groups.Roll);
        	Anim.RefreshAnim((int)groups.Trow);
		}
		public void MovementTick(){
		
		}
		public void FixedTick()
		{
			MovementTick();
		}

		public void OnEnter()
		{
			Debug.Log("Idle");
			GetCompos();
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

	}

}
