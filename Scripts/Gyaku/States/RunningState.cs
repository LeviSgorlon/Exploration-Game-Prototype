using State;
using UnityEngine;

namespace State
{
	public class RunningState: IState
	{
		
		private GenericAnimator Anim;
    	private GenericInput Keys;
    	private GenericMovement Movement;
   		private GenericStats Stats;
		
		private GameObject gameObject;

		private float VelDif;
		private Vector3 newVel2;
		private float SpeedScale;
		private float SpeedScale2;
		public RunningState(GameObject This)
		{
			gameObject = This;
		}
		public void OnEnter()
		{
			GetCompos();
			Debug.Log(gameObject.name + " is in" + " Run");
			Movement.GrindingEffect.GetComponent<ParticleSystem>().emissionRate = 100;
			Keys.Landing = false;
			SpeedScale = 1.2f;
			SpeedScale2 = 1.2f;
			Stats.RunningTime = 0;
		}
		public void GetCompos(){
			Keys = gameObject.GetComponent<GenericInput>();
   	 		Stats = gameObject.GetComponent<GenericStats>();
   			Anim = gameObject.GetComponent<GenericAnimator>();
    		Movement = gameObject.GetComponent<GenericMovement>();
		}
		public void FixedTick()
		{
			if(Anim) if(Anim._anim){ AnimTick();}
			MovementTick();

			SpeedScale = Mathf.Clamp(SpeedScale2, 1, 2.7f);
			SpeedScale2 += 0.035f;
		}

		public void Tick()
		{
			
			InputTick();
			StatsTick();
		}
		public void InputTick(){	
			
		}
		public void StatsTick(){
			
		}
		public void AnimTick(){
			
            Anim._anim.SetBool("Walking", Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup);
            

			 
		}
		public void MovementTick(){
			Tool.ChangeMeshColorAll("#948db3",gameObject);
			Stats.Gforce = 0;
			Stats.RunningTime += Time.deltaTime;
			if(!Keys.GTest){
			Movement._rb.drag = Stats.dragPadrao;
			}

			Movement._rb.angularDrag = Stats.dragPadrao * 4;
			if (Anim._anim != null){
            Anim._anim.speed = Stats.velocityMag / 150;
			}
			Movement._rb.drag = Stats.dragPadrao;
			Stats.TurnSpeed = Stats.TurnSpeedpadrao / (1.35f * SpeedScale);
			Stats.velocidade = Stats.velocidadepadrao * SpeedScale;

			//Movement._rb.velocity *= (SpeedScale + (Stats.Weight / 6000)) * 0.8f;
			

			if (Movement.SelectedToHold){
			Movement.CarrySpeedMath(Movement.SelectedToHold.GetComponent<Rigidbody>());
			}
		GroundPropertys();
		}

		public void GroundPropertys(){
			if(Keys.walkingdown | Keys.walkingleft | Keys.walkingright | Keys.walkingup){
				Movement.Col.material = Resources.Load("Slipp") as PhysicMaterial;
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.GforcePadrão/3);
				
				Movement.OldVel = Stats.velocityMag;
			}else{
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.GforcePadrão/3);
				Movement.Col.material = Resources.Load("Stop") as PhysicMaterial;	
			}
			
			if(!Keys.walkingdown && !Keys.walkingleft && !Keys.walkingright && !Keys.walkingup){
				Movement._rb.velocity += Movement._rb.velocity.normalized * Movement.OldVel/500;
				Movement._rb.AddForce(-Keys.GroundColOriPost * Stats.GforcePadrão/3);
				Movement.OldVel /= 1.2f;
				
			}
		}

		

		public void OnExit()
		{
			Movement.GrindingEffect.GetComponent<ParticleSystem>().emissionRate = 0;
			Stats.TurnSpeed = Stats.TurnSpeedpadrao;
		}

	}

}
