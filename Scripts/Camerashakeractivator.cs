using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;

public class Camerashakeractivator : MonoBehaviour
{
    public bool Activate;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void FixedUpdate()
    {
        if(Activate == true)
        {
            Shake();
            Activate = false;
        }
    }
    // Update is called once per frame
    void Shake()
    {   
            CameraShaker.Instance.ShakeOnce(4,4,0.1f,1);
      
    }
}
