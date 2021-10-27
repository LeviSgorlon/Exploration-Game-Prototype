using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Targetdetector : MonoBehaviour {
    public bool ON;
    public bool WeaponMagnet;
    public TargetEnemyChooser chosen;
    public ItemMagnet chosenM;
    private void Update()
    {
        if (WeaponMagnet == false)
        {
            chosen = gameObject.GetComponentInParent<TargetEnemyChooser>();
        }
        if (WeaponMagnet == true)
        {
            chosenM = gameObject.GetComponentInParent<ItemMagnet>();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        
             if (collision.gameObject.tag == "Weapon")
            {
                 ON = true;
                       
            }
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        
       
        if (collision != null )
        {  
             if (collision.gameObject.tag == "Weapon")
            {
            ON = true;
                       
            }
        }
    }

  
}
