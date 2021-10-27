using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretShoot : MonoBehaviour
{
  
    private int layerMask = 1 << 8;
    private int layerMask2 = 1 << 11;
    private RaycastHit2D hitinfo;
    private RaycastHit2D hitinfo2;
    private Vector3 ReturnTransform;
    private Vector3 TargetTransform;
    private float speed = 1;
    private float boomerangTimer;
    bool returning = false;   
    // Use this for initialization
    void Start()
    {
     
    }

    // Update is called once per frame
    private void OnEnable()
    {
        hitinfo = Physics2D.CircleCast(transform.position,  2000, Vector2.up,  1, layerMask);
       
        boomerangTimer = 0.0f;
    }
    
void FixedUpdate()
    {
        hitinfo2 = Physics2D.CircleCast(transform.position, 2000, Vector2.up,  1, layerMask2);


        TargetTransform = hitinfo.point;
        ReturnTransform = hitinfo2.point;
        TargetTransform.z = transform.position.z;
        ReturnTransform.z = transform.position.z;

        boomerangTimer += Time.deltaTime;
        if (boomerangTimer >= 0.6f)
        {
            if (hitinfo2.collider != null) { Debug.DrawLine(transform.position, hitinfo2.point, Color.blue, 0, false); }
            returning = true;
        }
        if (returning == false)
        {
            if (hitinfo.collider != null) { Debug.DrawLine(transform.position, hitinfo.point, Color.green, 0, false); }
            transform.right = (TargetTransform - transform.position);
            transform.Translate(Vector2.right * speed);


        }
        else
        {

            transform.right = (ReturnTransform - transform.position);
            transform.Translate(Vector2.right * speed);

        }
        if (boomerangTimer >= 1.2f)
        {
            Destroy(this.gameObject);
        }








    }
    
}