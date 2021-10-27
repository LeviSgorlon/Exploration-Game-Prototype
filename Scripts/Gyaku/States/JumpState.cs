using State;
using UnityEngine;

namespace State
{
	public class JumpState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		
		private float JumpDuration;
		private float JumpScale;
		private GameObject gameObject;
		private float Scale;
		private float Scale2;
		public float Modifier;
		public float ModifierCap;
		public float ModifierCap2;
		public Vector3 Direction;

		public bool CanCancel;
		public JumpState(GameObject This)
		{
			gameObject = This;
		}

		

		public void Tick()
		{
			AnimTick();
			InputTick();
			StatsTick();
			
            
			JumpDuration -= Time.deltaTime;
			if(CanCancel == true){
				
				
				if(Movement._rb.velocity.y <= 60 && Keys.CanWalk && JumpDuration <= 0)
				{
					Keys.Jumping = false;
				}
			}
		}
        public void FixedTick()
		{
			MovementTick();

			

		}








		public void OnEnter()
		{
			CanCancel = false;
			Scale = Scale2 = 1;
			Debug.Log(gameObject.name + " is in" + " Jumping");
            GetCompos();
			Keys.Landing = false;
			JumpStart();
			Keys.Landing = true;
			Keys.Running = false;
			
		
		if(Keys.Running && Stats.RunningTime >= 0.3f){          
            RunningJump();
		}
		if(Stats.LandingJumpTime > 0){
			LandingJump();
		}
		}
		public void GetCompos(){
			Keys = gameObject.GetComponent<GenericInput>();
   	 		Stats = gameObject.GetComponent<GenericStats>();
   			Anim = gameObject.GetComponent<GenericAnimator>();
    		Movement = gameObject.GetComponent<GenericMovement>();
		}
		public void LandingJump(){
			
		}
		public void RunningJump(){
			Movement._rb.AddForce((Keys.WalkDir * Stats.JumpHAmount * ModifierCap * 3));
			Movement._rb.AddForce((Movement.GlobalOrientation.up * Stats.JumpForce) * ModifierCap/4000);
            	
				Keys.RunningJump = true;   
             	Keys.Running = false;
		}
		public void JumpStart(){
			JumpDuration = Stats.JumpTime;
			Movement.Col.material = Resources.Load("Slipp") as PhysicMaterial;
			CanCancel = true;
			Modifier = (Stats.velocityMag);
			ModifierCap = Mathf.Clamp(Modifier,1,300);
			ModifierCap2 = Mathf.Clamp(Modifier,1,300);

			Movement._rb.AddForce((Keys.WalkDir * Stats.JumpHAmount * ModifierCap2 * 2));
		}
		public void OnExit()
		{
			Keys.checkGround = true;
			
		}


        public void InputTick(){	
		
		}
		public void StatsTick(){
		
		}
		public void AnimTick(){
			 if(Movement._rb.velocity.z < -1) { Anim._anim.SetBool("Jumping", true);} else { Anim._anim.SetBool("Jumping", false);}
            Anim._anim.SetBool("InGround", Keys.CanWalk);
            Anim._anim.SetBool("JumpStart",Keys.JumpStart);
            Anim._anim.SetBool("Inground", Keys.CanWalk);
		}
		public void MovementTick(){
			Tool.ChangeMeshColorAll("#acbea3",gameObject);
			Debug.DrawRay(gameObject.transform.position,Movement._rb.velocity.normalized * 10,Color.red,4f);
			Stats.JumpScale = Scale2;
			Scale2 += 120 * Time.deltaTime;
			Scale += 0.32f + (Scale/8) + (Scale/16);

			//Movement._rb.AddForce((Direction * Stats.JumpHAmount * ModifierCap2) / Scale );
			Movement._rb.AddForce(Movement.GlobalOrientation.up * (Stats.JumpForce / Scale2 ));
			Movement._rb.AddForce(-Movement.GlobalOrientation.up * (Stats.GforcePadrão * Scale2/40));
			//Movement._rb.velocity = Movement._rb.velocity + (Direction * 2);
			
			
			

			Anim._anim.speed = 1;
			Stats.Gforce = 0;
        	Movement._rb.drag = Stats.dragPadrao/6;
			Stats.TurnSpeed = Stats.TurnSpeedpadrao / 2;

			

            
			
		}
		

	}

}
