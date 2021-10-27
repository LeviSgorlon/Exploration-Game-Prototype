using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenericStats : MonoBehaviour
{
    [Header("Hp")]
    public float health;
    public float fullhealth;
    public float Shield;

    [Header("Gravity")]
     public float GforcePadrão;
    public float Gforce;
     public float GravScale;
     [Range(1.0f, 100.0f)]
     public float Weight;
     [Header("Speed")]
    public float velocidade;
    public float velocidadepadrao;
    public float velocityMag;
    public float TrueVel;
    public float velocityMagEnter;
    public float dragPadrao;
    public float TurnSpeed;
    public float TurnSpeedpadrao;
    public float RunningTime;

    [Header("Jump")]

    public float JumpForce;
    public float JumpHAmount;
    public float JumpInit;
    public float JumpDelayDefault;
    public float JumpTime;
    public float JumpScale;
    public float LandingTimeDefault;
    public float LandingJumpTime;
    [HideInInspector]
    public float LandingTime;
    [HideInInspector]
    public float JumpVAmount;
    [Header("Colission Properties")]
    public float groundpoundcd; 
    public float Friction;
    [Header("Rolling")]
    public float RollingTimerPadrao;
    public float RollingTimerOnAir;
    public float RollingForcePadrao; 
    public float RollScale;
    [Header("Other")]
    public int PressedKeys;
    public float TurnTime;
    public float GrabCD;
    public Vector3 HpBarPos;
    public float TrowingForce;

    #region Hidden Variaveis
    [HideInInspector]
    public bool OnControl;

    [HideInInspector]
    public float RollingForce;
    [HideInInspector]
    public float RollingTimer;
    [Header("Facing")]
    public bool Iup;
    public bool Idown;
    public bool Ileft;
    public bool Iright;
    public bool Iupleft;
    public bool Iupright;
    public bool Idownleft;
    public bool Idownright;

   
    
    
    #endregion
     [HideInInspector]
    public GenericInput InputField;
     [HideInInspector]
    public GameObject HpBar;
     [HideInInspector]
    public Object HPbarIcon;

    
    
    protected virtual void Awake()
    {
        InputField = gameObject.GetComponent<GenericInput>();
        HPbarIcon = Resources.Load("hpbarbig");
        HpBar = Instantiate(HPbarIcon,null,true) as GameObject;
        HpBar.transform.localPosition = HpBarPos;
        HpBar.GetComponent<HpHud>().Target = gameObject;
        
    }
    public virtual void FixedUpdate()
    {
       
        TurnTime -= Time.deltaTime;
        HealthMath();
        RollingMath();
        WeighMath();
        SpeedMath();
    }
    public void WeighMath(){
        Weight = Mathf.Clamp(Weight,1,100);
    }
   
    public void SpeedMath()
    {
       if(InputField.Crawl){
            if (InputField.Diagonalfix == true)
            {
               // velocidade = velocidadepadrao / 4.6f;

            }
            else
            {
               // velocidade = velocidadepadrao / 3.3f;
            }
       }
    }

  
    public void RollingMath()
    {
        RollingTimer -= Time.deltaTime;

        RollingForce = RollingForcePadrao;
    }

    public void HealthMath()
    {
        if (health > fullhealth)
        {
            health = fullhealth;
        }

        if (health < 0)
        {
            health = 0;
        }
    }
}


