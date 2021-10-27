using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyhorns : MonoBehaviour
{

    public GenericStats enemystats;

    public HpHud Hud;
    public float rangebegin, rangeend;
    public float currentscale;
    public float cc;
    public bool yellowmask;
    public bool redmask;
    public bool Shieldmask;

   

    // Use this for initialization
    void Awake()
    {
        
        currentscale = gameObject.transform.localScale.x;
        Hud = gameObject.GetComponentInParent<HpHud>();
        
        
    }

    // Update is called once per frame

    void Update()
    {
        enemystats = Hud.Target.GetComponentInParent<GenericStats>();
        if (enemystats != null)
        {
            rangeend = enemystats.fullhealth;

            if (enemystats.health > 0)
            {

                if (redmask == true)
                {
                    cc = currentscale / (rangeend / enemystats.health);
                }
                if (yellowmask == true) { cc += ((currentscale / (rangeend / enemystats.health)) - cc) * 0.1f; }

                if (Shieldmask == true)
                {
                    cc = currentscale / (rangeend / enemystats.Shield);
                }
            }
            else
            {

                if (redmask == true)
                {
                    cc = currentscale / (rangeend / 0.1f);
                }
                if (yellowmask == true) { cc += ((currentscale / (rangeend / 0)) - cc) * 0.05f; }
            }


        }
        if (enemystats == null)
        {
            cc = 0;
        }
        gameObject.transform.localScale = new Vector3(cc, gameObject.transform.localScale.y,1);


    }
}
        
        
    

