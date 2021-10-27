using State;
using UnityEngine;

namespace State
{
	public class ThrownState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		
		private float TrowDuration;
		private float Scale;
		private float Scale2;
		private GameObject gameObject;
		public float Modifier;
		public float ModifierCap;
		public float ModifierCap2;
		public ThrownState(GameObject This)
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
			TrowDuration -= Time.deltaTime;
			if(TrowDuration < 0){
				Keys.Thrown = false;
				Keys.Grabbed = false;
			}
			

		}


		public void OnEnter()
		{
			TrowDuration = 0.5f;
			Debug.Log(gameObject.name + " is in" + "Thrown");
           	GetCompos();
			gameObject.transform.position = Movement.Holder.transform.position + Movement.DirectionTrown * 40 + new Vector3(0,45,0);
			Keys.Landing = false;
			
			Keys.Thrown = true;
			Scale = 1;
			
			Modifier = (Movement.Holder.GetComponent<GenericStats>().velocityMag/ 22);
			ModifierCap = Mathf.Clamp(Modifier,0,12);
			ModifierCap2 = Mathf.Clamp(Modifier,0,18);
			Movement._rb.AddForce((Movement.DirectionTrown * 4000 * Movement._rb.mass * ModifierCap2));
			Movement._rb.AddForce((Movement.GlobalOrientation.up * 3000 * Movement._rb.mass * ModifierCap));
		}
		public void GetCompos(){
			Keys = gameObject.GetComponent<GenericInput>();
   	 		Stats = gameObject.GetComponent<GenericStats>();
   			Anim = gameObject.GetComponent<GenericAnimator>();
    		Movement = gameObject.GetComponent<GenericMovement>();
		}
	
		public void OnExit()
		{
			Keys.Thrown = false;
			Keys.Grabbed = false;
			Keys.checkGround = true;
		}


        public void InputTick(){	
		
		}
		public void StatsTick(){
		
		}
		public void AnimTick(){
			
		}
		public void MovementTick(){
			Scale = Mathf.Clamp(Scale,1,4f);
			Scale += 0.32f + (Scale/8) + (Scale/16);
			Scale2 = Mathf.Clamp(Scale,1,64);
			Scale2 += 4f + (Scale2/4) + (Scale2/16);
			Tool.ChangeMeshColorAll("#ef798a",gameObject);


			TrowDuration -= Time.deltaTime;
			Movement._rb.AddForce((Movement.DirectionTrown * 4000 * Movement._rb.mass * ModifierCap2) / Scale );
			Movement._rb.AddForce((Movement.GlobalOrientation.up * 2000 * Movement._rb.mass * ModifierCap) / Scale2 );
			Movement._rb.AddForce((-Movement.GlobalOrientation.up * 600 * Movement._rb.mass) * Scale2/2);
			Movement._rb.AddRelativeTorque(gameObject.transform.up * 160000000 * ModifierCap * Movement._rb.mass);
            
			gameObject.layer = 0;
		}
		

	}

}
