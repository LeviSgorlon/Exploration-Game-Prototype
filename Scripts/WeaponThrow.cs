using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponThrow : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (col.isTrigger != true)
        {
              col.SendMessageUpwards("Agressornm", gameObject.name);
            
          

                col.SendMessageUpwards("Damage", 10);
            
        }
    }


                // Update is called once per frame
                void Update()
    {
        
    }
}
