using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericMovement : MonoBehaviour
{
    [HideInInspector]
    public Rigidbody _rb;
    [HideInInspector]
    public Transform GlobalOrientation;
    [HideInInspector]
    public Transform Body;
    public GenericInput Keys;
    [HideInInspector]
    public GenericStats Stats;
    [HideInInspector]
    public GenericAnimator Anim;
    
    private GameObject Effect;
    public GameObject GrindingEffect;
    public GameObject BounceEF;
    public GameObject SlamEF;
    [HideInInspector]
    public int i;

   
    [HideInInspector]
    private bool gotonext;

    [HideInInspector]
    public Vector3 HoldposOffset;
    public GrabDetector ItemDetector;
    public GameObject SelectedToHold;
    public float Bounces;
    public bool VBounce;
    
    public Collision MainCol;
    public Vector3 LookDirIns;
    public float BounceForceV;
    public float BounceForceH;
    public float HoldDistance;
    public float HoldDistanceDefault;
    public float DitherStep;
    public SpriteRenderer[] Sprite;
    public MeshRenderer[] Mesh;
    public Rigidbody EX_rb;

    public GameObject Holder;
    public Vector3 DirectionTrown;
    public Collider Col;
    public Collider LastCol;
    public bool EmitGrind;
    public Vector3 ObjectCarriedVel;
    public bool Pushing;
    public bool GoingAgainst;
    public float ObjectPushedWeight;
    public float ObjectPushedRelativeWeight;
    public float RelativeWeight;
    public Vector3 PushingNormal;
    public GameObject ObjectPushed;
    
    Vector3 RightAling;
    Vector3 FowardAlign;
    Vector3 UpAl;
    Vector3 FwdAl;
    public float OldVel;
    public bool OntopCarry;
    public Vector3 Tfoward,Tright;
    public float PushCD;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        Sprite = gameObject.GetComponentsInChildren<SpriteRenderer>();
        Mesh = gameObject.GetComponentsInChildren<MeshRenderer>();
        Keys = gameObject.GetComponent<GenericInput>();
        Stats = gameObject.GetComponent<GenericStats>();
        Anim = gameObject.GetComponent<GenericAnimator>();
        _rb = gameObject.GetComponent<Rigidbody>();
        ItemDetector = gameObject.GetComponentInChildren<GrabDetector>();
        GlobalOrientation = GameObject.Find("GlobalOri").transform;
        
        Stats.Friction = 0;
        Stats.GravScale = 1;
        MassMath();
    }
    public void Awake(){
        BounceEF = Resources.Load("BounceEF") as GameObject;
        SlamEF = Resources.Load("SlamEF") as GameObject;
        GrindingEffect = Instantiate(Resources.Load("StoneGrinding") as GameObject);
        GrindingEffect.name = gameObject.name + " GrindEffect";
        GrindingEffect.GetComponent<AudioSource>().volume = 0;
        Debug.LogError(gameObject.name + " Collider Lenght is: " + GetColliderLenght(gameObject));

       
    }
    protected virtual void FixedUpdate()
    {
        Col = gameObject.GetComponent<Collider>();
        VelocityCheck();
        Gravity();
        MassMath();
        TransparencyEffects();
        Shake();
        AlignObj(GrindingEffect);
        if(LastCol != null) BeingCarried(LastCol);
           
        WalkingLogic();
    }
    protected virtual void WalkingLogic()
    {
        if(Keys.CanWalk){
        Vector3 F = Vector3.Cross(Keys.GroundColOriPost,-transform.right);
        Vector3 R = Vector3.Cross(Keys.GroundColOriPost,transform.forward);
        Tfoward = new Vector3(transform.forward.x,F.y,transform.forward.z);
        Tright = new Vector3(transform.right.x,R.y,transform.right.z);
        }else{
        Tfoward = new Vector3(transform.forward.x,0,transform.forward.z);
        Tright = new Vector3(transform.right.x,0,transform.right.z);
        }
        Directions();
    }
    public void Directions(){
        

            if (Keys.walkingup)
            {
                _rb.AddForce(Tfoward.normalized * Stats.velocidade * _rb.mass/4000);
                Keys.WalkDir = transform.forward;
            }
            if (Keys.walkingdown)
            {
                _rb.AddForce(Tfoward.normalized * -Stats.velocidade * _rb.mass/4000);
                Keys.WalkDir = -transform.forward;
            }
            if (Keys.walkingright)
            {
                _rb.AddForce(Tright.normalized * Stats.velocidade * _rb.mass/4000);
                Keys.WalkDir = transform.right;
            }
            if (Keys.walkingleft)
            {
                Keys.WalkDir = -transform.right;
                _rb.AddForce(Tright.normalized * -Stats.velocidade * _rb.mass/4000);
            }
    }
    
    public void AlignObj(GameObject Obj){
            Obj.transform.position = transform.position;
            ObjectCarriedVel = new Vector3(0,0,0);
    }
    public void MassMath(){
            Stats.GforcePadrão = _rb.mass * 4000 * Stats.GravScale;
            Stats.JumpHAmount = (_rb.mass) * Stats.Weight;
            Stats.JumpVAmount = (_rb.mass) * Stats.Weight;
            Stats.JumpForce = (_rb.mass * 42000) * (1 - (Stats.Weight / 200));
            RelativeWeight = Stats.Weight + ObjectPushedRelativeWeight;
            if(Stats.Weight != 100){
                _rb.mass = 4000 + (Stats.Weight * 1000) * (Stats.Weight/16);
            }else{
                _rb.mass = 4000 + (Stats.Weight * 1000) * 1000;
            }
            
    }
    public void Shake(){
        if(Keys.Shaking){
            Vector3 Shakepos;
            Shakepos.x = transform.position.x + Random.Range(-100,100)/50;
            Shakepos.y = transform.position.y + Random.Range(-100,100)/50;
            Shakepos.z = transform.position.z;

            transform.position = Shakepos;
        }
    }
    public void GrindingSound(Vector3 Position,Vector3 Up){
        
        Vector3 PostVec;
        PostVec.z = Position.z;
        PostVec.x = Keys.bounds.center.x;
        PostVec.y = Keys.bounds.center.y;
        GrindingEffect.transform.position = PostVec;
        GrindingEffect.transform.forward = -Up;

        
        if(EmitGrind){
            float SoundScale;
            if (Stats.velocityMag > 10) {
                SoundScale = Stats.velocityMag;
            }
            else
            {
                SoundScale = 0;
            }
            GrindingEffect.GetComponent<AudioSource>().volume = Mathf.Lerp(GrindingEffect.GetComponent<AudioSource>().volume, SoundScale, Time.deltaTime * 4);
            GrindingEffect.GetComponent<AudioSource>().pitch = 0.5f + Stats.velocityMag / 1000;
            
            GrindingEffect.GetComponent<ParticleSystem>().emissionRate = (int)Stats.velocityMag/10;
        }
        if(!EmitGrind && !Keys.Running){
        GrindingEffect.GetComponent<AudioSource>().volume = 0;
        GrindingEffect.GetComponent<ParticleSystem>().emissionRate = 0;
        }
        
    }
    public void TransparencyEffects(){
       
        DitherStep = Mathf.Clamp(DitherStep,0,1.5f);

        if(!Keys.Spawned && DitherStep != 1.5f && !Keys.Dead){
        DitherStep += 0.02f;
        }
        if(DitherStep == 1.5f){
        Keys.Spawned = true;
        }
        if(Keys.Dead){
        Keys.Spawned = false;
        DitherStep -= 0.02f;
        }
        if(DitherStep != 1.5f){
            foreach(SpriteRenderer sp in Sprite){
                sp.material.SetFloat("_Dither", DitherStep);
            }
            foreach(MeshRenderer ms in Mesh){
                ms.material.SetFloat("_DitherStep", DitherStep);
            }
        }
        
    }

    public void BeingGrabbed(GameObject OB){
                Keys.Grabbed = true;
                Holder = OB;
    }
     public void BeingDropped(){
                gameObject.layer = 0;
                Keys.Grabbed = false;
                Keys.Landing = false;
                //transform.parent = null;
    }
    public void BeingTrown(Vector3 Direction){
        DirectionTrown = Direction;
        transform.parent = null;
        Keys.Grabbed = false;
        Keys.Thrown = true;
        
                
                
    }
    
    public float GetColliderLenght(GameObject bounds){
        return Vector3.Distance(bounds.GetComponent<Collider>().bounds.max,bounds.GetComponent<Collider>().bounds.center);
    }
    public void CarryObjectsOnTop(Vector3 Velocity, Collision collision){
        //ObjectCarriedVel = Velocity;
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            float Dis = Vector3.Distance(Keys.bounds.center,collision.contacts[i].point);
            //Debug.LogError(gameObject.name + " Distance from " + collision.contacts[i].thisCollider.name + " Is " + Dis);
            if(Dis > 2){
            //Stats.Gforce = 0;
            }
        }
    }
    public void BeingCarried(Collider Object){
        OntopCarry = true;
        Rigidbody Col = Object.GetComponent<Rigidbody>();

        if(Col.velocity.magnitude > 1){
            ObjectCarriedVel = Col.velocity;
            _rb.velocity += Col.velocity/(7.333333333333333f);
            _rb.angularVelocity += Col.angularVelocity;
        }
    }
    public void CarrySpeedMath(Rigidbody Col){
        if((Col.GetComponent<GenericStats>().Weight) / Stats.Weight >= 1){
            _rb.velocity /= (Col.GetComponent<GenericStats>().Weight) / Stats.Weight;
        }
    }
    public void VBounceDrag(){  
            // Stats.dragPadrao = Stats.dragPadraoKnocked;  
    }
    public void Bounce(){
        
        if (VBounce)
        {
        
        
        _rb.AddForce((_rb.velocity * BounceForceH));
        _rb.AddForce((MainCol.contacts[0].normal) * (BounceForceV));
        _rb.AddRelativeTorque(transform.forward * 90000);
        BounceForceV /= 1.6f;
        BounceForceH /= 1.2f;
        
        
        
        Bounces--;
        }
        if(_rb.velocity.magnitude < 500)
        {
            
            VBounce = false;
            BounceForceV = (_rb.mass * 32000)+Stats.Gforce;
            BounceForceH = 100;
            
        }
       
    }
    

    
    private void OnCollisionExit(Collision collision){
        if(!collision.collider.isTrigger){
            GrindingEffect.GetComponent<AudioSource>().volume = Mathf.Lerp(GrindingEffect.GetComponent<AudioSource>().volume, 0, Time.deltaTime * 4); ;
        }
        if(collision.gameObject == ObjectPushed)
        {
            Pushing = false;
            ObjectPushed.GetComponent<GenericInput>().BeingPushed = false;
            ObjectPushed = null;
            PushingNormal = Vector3.zero;
        }
        
        GoingAgainst = false;
        Stats.Gforce = Stats.GforcePadrão;
        LastCol = null;   
        OntopCarry = false;
        
    }
    private void OnCollisionStay(Collision collision)
    {
        
        if(!collision.collider.isTrigger){
            GrindingSound(collision.contacts[0].point,collision.contacts[0].normal);
        }
        PushingObjects(collision);
        
        ImpactEffects(collision);
        
            foreach (ContactPoint C in collision.contacts)
            {
        
                if(C.otherCollider.GetComponent<Rigidbody>() != null){
                float Diference = Mathf.Ceil(Keys.bounds.center.y) - (Mathf.Ceil(collision.collider.bounds.center.y));
               
                if(Diference >= 0 && C.normal.y >= 1 && LastCol == null) LastCol = C.otherCollider;
                if(C.normal.y <= -1) LastCol = null;
                CarryObjectsOnTop(C.otherCollider.gameObject.GetComponent<Rigidbody>().velocity,collision);
                }
            }
            
            
            
    }
    private void OnCollisionEnter(Collision collision)
    {
      
        i = 0;
        Stats.velocityMagEnter = Stats.velocityMag;
        if(Stats.Friction > 0){
            _rb.velocity = _rb.velocity / Stats.Friction;
        }
       
        
    Bounce();
        // GroundPound(collision);
    }

    public void ImpactEffects(Collision collision){
        if (i < collision.contactCount && Stats.velocityMagEnter >= 30)
            { 
                GroundPound(collision);
                MainCol = collision;
            }
            if (i < collision.contactCount && Stats.velocityMagEnter >= 900 / (Stats.Weight /20))
            { 
                GroundSlam(collision);
                
                MainCol = collision;
                i = collision.contactCount;
            }
    }
    

    public void PushingObjects(Collision collision){
        if(Pushing == true)
        {
            ObjectPushed.GetComponent<GenericInput>().BeingPushed = true;
        }
        if (!GoingAgainst)
        {
            PushCD = 0.4f;
        }else if(Stats.PressedKeys > 0)
        {
            PushCD -= Time.deltaTime;
        }

        foreach (ContactPoint contact in collision.contacts)
        {
            if(contact.otherCollider.gameObject.GetComponent<GenericStats>() == null) return;
            if(contact.normal.y > 0.5f) return;

            float ContactDot = Vector3.Dot(Keys.WalkDir,contact.normal);

            if(GoingAgainst && Stats.PressedKeys > 0 && PushCD <= 0){
                Pushing = true;
                ObjectPushed = contact.otherCollider.gameObject;
                
                ObjectPushedWeight = collision.gameObject.GetComponent<GenericStats>().Weight;
            }else{
                ObjectPushedWeight = 0;
                Pushing = false;
            }

                if(ContactDot <= -0.3f){
                
                PushingNormal = -contact.normal;
                GoingAgainst = true;
                
                }else{
                PushingNormal = Vector3.zero;
                GoingAgainst = false;   
                ObjectPushedWeight = 0;
                }

            if(GoingAgainst && Keys.CanWalk){
                
                ObjectPushedWeight = contact.otherCollider.GetComponent<GenericStats>().Weight;
                ObjectPushedRelativeWeight = contact.otherCollider.GetComponent<GenericStats>().Weight 
                + contact.otherCollider.GetComponent<GenericMovement>().ObjectPushedWeight 
                + contact.otherCollider.GetComponent<GenericMovement>().ObjectPushedRelativeWeight;
            }
        }
    }
    public void SurfaceImpactSound(Collider collision, int Index){
        SoundPlayer Sound = gameObject.GetComponent<SoundPlayer>();
        switch (collision.tag)
            {
                case "Ground(Stone)":
                    //Sound.PlaySound(Resources.Load("StoneStep") as GameObject,true,_rb.velocity.magnitude/120,0,1,0);
                    Sound.PlaySound(Resources.Load("StoneImpact")as GameObject,true,Stats.velocityMagEnter/120,0,1,0);
                break;
                case "Ground":
                     //Sound.PlaySound(Resources.Load("StoneStep") as GameObject,true,_rb.velocity.magnitude/120,0,1,0);
                    Sound.PlaySound(Resources.Load("StoneImpact")as GameObject,true,Stats.velocityMagEnter/120,0,1,0);
                break;
                case "Ground(Grass)":
                    //Sound.PlaySound(Resources.Load("GrassStep") as GameObject,true,_rb.velocity.magnitude/120,0,1,0);
                    Sound.PlaySound(Resources.Load("GrassImpact")as GameObject,true,Stats.velocityMagEnter/120,0,1,0);
                break;
                default:
                    //Sound.PlaySound(Resources.Load("StoneStep") as GameObject,true,_rb.velocity.magnitude/120,0,1,0);
                    Sound.PlaySound(Resources.Load("StoneImpact")as GameObject,true,Stats.velocityMagEnter/120,0,1,0);
                break;
            }
        
    }
    public void SurfaceSlamSound(Collider collision, int Index){
        SoundPlayer Sound = gameObject.GetComponent<SoundPlayer>();
        
        float SoundScale = 0.13f;
        switch (collision.tag)
            {
                case "Ground(Stone)":
                    Sound.PlaySound(Resources.Load("StoneSlam")as GameObject,true, SoundScale, 0,3,0);
                break;
                case "Ground":
                    Sound.PlaySound(Resources.Load("StoneSlam")as GameObject,true, SoundScale, 0,3,0);
                break;
                case "Ground(Grass)":
                    Sound.PlaySound(Resources.Load("GrassSlam")as GameObject,true, SoundScale, 0,3,0);
                break;
                default:
                    Sound.PlaySound(Resources.Load("StoneSlam")as GameObject,true, SoundScale, 0,3,0);
                break;
            }
        
    }
    

    void GroundPound(Collision Col)
    {
        if (gotonext == false) { 
            Effect = Instantiate(BounceEF, null, true);
            Effect.transform.position = Col.contacts[i].point;
            Effect.transform.up = Col.contacts[i].normal;
            Effect.GetComponent<ParticleSystem>().maxParticles = (int)Stats.velocityMag / 80;
            Effect.gameObject.SetActive(true);
            //Debug.Log(transform.root.name + " Hitted " + Col.contacts[i].otherCollider.name + " and casted " + Effect.GetComponent<ParticleSystem>().maxParticles +" particles");
            
            Destroy(Effect, 1f);
            SurfaceImpactSound(Col.collider, 1);
            gotonext = true;
        }
        if (Time.frameCount % 2 == 0 && gotonext)
        {
            i++;
            gotonext = false;
            
        }
    }
     void GroundSlam(Collision Col)
    {
        if (gotonext == false) { 
            Effect = Instantiate(SlamEF, null, true);
            Effect.transform.position = Col.contacts[i].point;
            Effect.transform.up = Col.contacts[i].normal;
            Effect.GetComponent<ParticleSystem>().maxParticles = 40;
            Effect.gameObject.SetActive(true);
            Destroy(Effect, 1);
            SurfaceSlamSound(Col.collider, 1);
            gotonext = true;
            
        }
        if (Time.frameCount % 1 == 0 && gotonext)
        {
            i++;
            gotonext = false;
            
        }
    }
    public void VelocityCheck()
    {

        Stats.groundpoundcd -= Time.deltaTime;
        if (_rb.velocity.magnitude > 1)
        {
            Stats.groundpoundcd = 0.2f;
        }
        if (Time.frameCount % 5 == 0 && _rb.velocity.magnitude > 1 && Stats.groundpoundcd > 0)
        {
            Stats.velocityMag = Mathf.Abs(_rb.velocity.magnitude - ObjectCarriedVel.magnitude);
        }
        if (Stats.groundpoundcd < 0)
        {
            Stats.velocityMag = 0;
        }
        if(_rb.velocity.magnitude > 30000){
            //_rb.velocity /= 4;
        }
    }

    public void Gravity()
    {
        _rb.AddForce(GameObject.Find("GlobalOri").transform.up * -Stats.Gforce);
    }

   

    
}
