using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour
{
    public GameObject spritemain;
    public GameObject ex1;
    public GameObject ex2;
    public GameObject pingeffect;

    public GameObject Effect;

    public Collider2D Attacktrigg;
    public SoundPlayer Sounds;

    
    float i;
    float o;
    public float f;
    public bool jumping;
    public bool descending;
    public bool Vbounce;
    public bool Explode;
    // Start is called before the first frame update
    void Awake()
    {
        jumping = true;
        f = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (jumping == true)
        {
            if(Vbounce == false)
            {
                Vbounce = true;
            }
            spritemain.transform.localPosition = new Vector3(0, i, 0);


            i += o;



            if (o <= 0)
            {
                descending = true;

            }



        }


        if (o > 0 && descending == false)
        {
            o -= 0.8f;

        }
      

        if (descending == true)
        {
            o -= 0.8f;


        }
     

        if (descending == true && jumping == false)
        {
            descending = false;
            jumping = true;
        }
        if (i < 0)
        {

            
            jumping = true;
         
            
            descending = false;
            
            spritemain.transform.localPosition = new Vector3(0, 0, 0);
            if (jumping == true && i <= 0)
            {
                //  Sounds.PlaySound(0, true, 0.7f, 0.2f, true, 0);
               
            }
            jumping = false;
           
        }
        if (jumping == false)
        {
            i = 1;
            if (Vbounce == false)
            {
                o = 8;
                f = 8;
            }
            if (Vbounce == true)
            {
                jumping = true;
                f = f - 1.4f;
                Effect = GameObject.Instantiate(pingeffect, null, false);
                Effect.transform.position = transform.position;
                Object.Destroy(Effect, 1f);
            }
            if (Vbounce == true)
            {
                o = f;
            }
            


        }
        if(f <= 0){
            Vbounce = false;
            jumping = false;
             f = 8;
            Effect = GameObject.Instantiate(ex1,null,false);
            Effect.transform.position = transform.position;
            Object.Destroy(Effect, 3);
            Effect = GameObject.Instantiate(ex2, null, false);
            Effect.transform.position = transform.position;
            Object.Destroy(Effect,3);
            Attacktrigg.enabled = true;
          //  Sounds.PlaySound(1, true, 0.7f, 0.2f, true, 0);
            Object.Destroy(spritemain.transform.root.gameObject, 0.04f);
            

        }
    }
}
