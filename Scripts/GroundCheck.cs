using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{
    public bool ON;
    public string Layer;
          
    public GenericInput Keys;
    // Start is called before the first frame update
    void Start()
    {   
        Keys = gameObject.GetComponentInParent<GenericInput>();
    }

    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == Layer)
        {
//            Keys.Jumping = false;
        }
       
    }
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == Layer)
        {
            ON = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == Layer)
        {
            ON = false;
           
        }
    }

  
}
