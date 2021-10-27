using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectInput : GenericInput
{
    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
        Stats.GravScale = 1;
    }

  
    protected override void Update()
    {
        base.Update();
       
   
    }
    
   
}
