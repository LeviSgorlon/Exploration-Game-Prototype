using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HVKnockback : MonoBehaviour
{
    public bool pushnow;

    public SoundPlayer Sounds;
    public float VKamount;
    float i2;
    float o;
    public float f;
    public bool jumping;
    public bool descending;
    public bool Vbounce;
    public Vector3 pos1;
    public float Kamount;
    public GameObject BounceEffect;
    public string AgressorName;
    private GameObject Agressor;
    public GameObject spritemain;
    public Quaternion rotatoion;
    public Rigidbody _rb;
    private GameObject Effect;
    public Transform GlobalOri;
    float i;
    public GroundCheck GCheck;
    public float groundCd;
    private Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        
    }
    public void Agressornm(string TempAgressorname)
    {

        Agressor = GameObject.Find(TempAgressorname);

        AgressorName = TempAgressorname;





    }
    public void KnockBack(float amount)
    {

        Kamount = amount;
        pos1 = Agressor.transform.position;
        
        

        pushnow = true;

    }

    public void VKnockBack(float Vamount)
    {
        anim.SetBool("Knocked", true);
        VKamount = Vamount;
        i2 = VKamount;
        f = VKamount;
        jumping = true;
        Vbounce = true;
        groundCd = 0.2f;
    }


    // Update is called once per frame
    void FixedUpdate()
    {
        groundCd -= Time.deltaTime;
        if (pushnow == true)
        {

            _rb.AddForce((transform.position - Agressor.transform.position) * (Kamount) * _rb.mass);
            pushnow = false;
        }
        #region Vknockback

        if (jumping == true)
        {

            _rb.AddForce(GlobalOri.up * (i2 * 1600));
           
            if (i2 > 0)
            {
                i2 -= 0.8f;
            }
        }
        if (groundCd > 0)
        {
            anim.SetBool("Knocked", true);
        }
        else
        {
            anim.SetBool("Knocked", false);
        }

        if (GCheck.ON == true && groundCd < 0)
        {
            jumping = false;
            
            if (Vbounce == true == jumping == false)
            {
                anim.SetBool("Knocked", false);
                groundCd = 0.2f;
                f /= 1.3f;
                i2 = f;
                Kamount /= 1.4f;
                _rb.AddForce((transform.position - Agressor.transform.position) * (Kamount) * _rb.mass);
                Effect = Instantiate(BounceEffect, null, true);
                Effect.transform.position = transform.position;
                Effect.transform.rotation = GlobalOri.rotation;

                

                jumping = true;
                
            }
        }
        if(f < 2.4f | Kamount < 50)
        {
            
            anim.SetBool("Knocked", false);
            Vbounce = false;
        }
        
        #endregion
    }
}
