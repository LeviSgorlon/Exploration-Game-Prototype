using System.Collections;
using System.Collections.Generic;
using State;
using UnityEngine;

public class GenericStateHandler : MonoBehaviour
{
    public GenericAnimator Anim;
    public	GenericInput Keys;
    public	 GenericMovement Movement;
   	public GenericStats Stats;
    public State.StateMachine StateMachine;
    
    
    public bool Able;
    public IState _state;
    
    // Start is called before the first frame update
    void Start()
    {
             Keys = gameObject.GetComponent<GenericInput>();
   	 		Stats = gameObject.GetComponent<GenericStats>();
   			Anim = gameObject.GetComponent<GenericAnimator>();
    		Movement = gameObject.GetComponent<GenericMovement>();
            SetupStates();
    
   
    
    }
    public void SetupStates(){
        
        StateMachine = new State.StateMachine();
        var IdleState = new IdleState(gameObject);
        var JumpState = new JumpState(gameObject); 
        var RunningState = new RunningState(gameObject);
        var FallingState = new FallingState(gameObject);
        var JumpSquatState = new JumpSquatState(gameObject);
        var LandingState = new LandingState(gameObject);
        var GrabbingState = new GrabbingState(gameObject);
        var HoldingState = new HoldingState(gameObject);
        var ThrowingState = new ThrowingState(gameObject);
        var GrabbedState = new GrabbedState(gameObject);
        var ThrownState = new ThrownState(gameObject);
        var PushingState = new PushingState(gameObject);
        var BeingPushedState = new BeingPushedState(gameObject);

        StateMachine.AddAnyTransition(LandingState,LandingCondition);
        StateMachine.AddAnyTransition(JumpState,JumpCondition);
        StateMachine.AddAnyTransition(RunningState,RunningCondition);
        StateMachine.AddAnyTransition(FallingState,FallingCondition);
        StateMachine.AddAnyTransition(JumpSquatState,JumpSquatCondition);
        StateMachine.AddAnyTransition(GrabbingState,GrabbingCondition);
        StateMachine.AddAnyTransition(HoldingState,HoldingCondition);
        StateMachine.AddAnyTransition(BeingPushedState,BeingPushedCondition);
      
        StateMachine.AddAnyTransition(GrabbedState,GrabbedCondition);
        StateMachine.AddAnyTransition(ThrownState,BeingThrownCondition);

        StateMachine.AddTransition(RunningState,ThrowingState,ThrowCondition);
        StateMachine.AddTransition(HoldingState,ThrowingState,ThrowCondition);
        StateMachine.AddTransition(ThrowingState, IdleState,ThrowCancelCondition);

        StateMachine.AddTransition(ThrownState,IdleState,IdleCondition);
        StateMachine.AddTransition(ThrowingState,IdleState,IdleCondition);
        StateMachine.AddTransition(LandingState,IdleState,IdleCondition);
        StateMachine.AddTransition(LandingState, PushingState, PushingCondition);
        StateMachine.AddTransition(FallingState,IdleState,IdleConditionPlus);
        StateMachine.AddTransition(IdleState,FallingState,FallingCondition);
        StateMachine.AddTransition(RunningState,IdleState,IdleCondition);
        
        StateMachine.AddTransition(JumpState,IdleState,IdleCondition);
        StateMachine.AddTransition(BeingPushedState,IdleState,IdleCondition);
        StateMachine.AddTransition(PushingState, IdleState,IdleCondition);
        StateMachine.AddTransition(IdleState, PushingState,PushingCondition);
        StateMachine.AddTransition(BeingPushedState, PushingState,PushingCondition);
        StateMachine.AddTransition(RunningState, PushingState,PushingCondition);
        StateMachine.AddTransition(HoldingState,PushingState,PushingCondition);

        StateMachine.SetState(FallingState);
    }

    public bool BeingPushedCondition(){
         return  (Keys.CanWalk && Keys.BeingPushed && !Keys.Falling && !Movement.Pushing && Keys.Pushable);
    }
    public bool PushingCondition(){
        return  (Keys.CanWalk && Movement.Pushing && Movement.GoingAgainst);
    }
     public bool ThrowCancelCondition(){
        return  (!Keys.Trowing);
    }
    public bool BeingThrownCondition(){
        return  (Keys.Thrown);
    }
    public bool GrabbedCondition(){
        return (Keys.Grabbed);
    }
    public bool ThrowCondition(){
        return (Keys.TrowEngage | Keys.Trowing);
    }
    public bool HoldingCondition(){
        return (Keys.HoldingItem && !Keys.Trowing && Keys.GTest && !Movement.Pushing && !Movement.GoingAgainst);
    }
    public bool GrabbingCondition(){
        
        if(Movement.ItemDetector){
           DetectDistance();
        return (Keys.Grabbing == true && Able && !Keys.HoldingItem && !Keys.Trowing && !Movement.Pushing && !Keys.JumpStart && !Keys.Jumping);
        }
    
        return (false);

        
    }
    public bool LandingCondition(){
        return (Keys.Landing && Keys.CanWalk && !Keys.Trowing && !Keys.JumpStart && !Keys.Jumping);
    }
    public bool RunningCondition(){
        return (Keys.Running && Keys.CanWalk && !Keys.JumpStart && !Keys.Trowing && !Movement.Pushing);
    }
    
    protected bool JumpCondition(){
        return (Keys.Jumping && !Keys.HoldingItem && !Keys.Trowing && !Keys.Landing);
    }

    protected bool IdleCondition(){
            return (Keys.CanWalk && !Keys.JumpStart && !Keys.Running && !Keys.Trowing && 
            !Keys.Grabbing && !Keys.Grabbed && !Keys.Thrown && !Movement.Pushing && !Keys.HoldingItem && !Keys.Jumping);  
    }
    protected bool IdleConditionPlus(){
            return (Keys.CanWalk && !Keys.JumpStart && !Keys.Running && !Keys.Trowing && 
            !Keys.Grabbing && !Keys.Grabbed && !Keys.Thrown && !Movement.Pushing && !Keys.HoldingItem && !Keys.Landing );  
    }
    public bool FallingCondition(){
        return (!Keys.CanWalk && !Keys.Jumping && !Keys.Landing && !Keys.JumpStart && !Keys.Trowing && !Keys.Grabbed && !Keys.Thrown);
    }

    public bool JumpSquatCondition(){
        return (Keys.JumpStart && Keys.CanWalk && !Keys.Jumping && !Keys.Falling && !Keys.Trowing);
    }
    

    public void DetectDistance(){
         if(Movement.ItemDetector.Locked != null){
            Vector3 Center = gameObject.transform.GetComponent<Collider>().bounds.ClosestPoint(gameObject.transform.position);
            Vector3 closet2 = Movement.ItemDetector.Locked.GetComponent<Collider>().bounds.ClosestPoint(Center);
            float distance2 = Vector3.Distance(Center,closet2);
            if(distance2 < 80){
                Able = true;         
            }else{
                Able = false;
            }
        }
    }
    public void Update()
    {
        StateMachine.Tick();
    }
    public void FixedUpdate(){
        StateMachine.FixedTick();
    }
}

