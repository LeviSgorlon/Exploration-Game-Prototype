using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StunStarAnim : MonoBehaviour
{
    private Animator anim;
    private EnemyHp EHp;
    private HP HP;
    public bool isEnemy = false;
    public bool isPlayer = false;
    public bool isStunned = false;
    // Use this for initialization
    void Start()
    {
        anim = GetComponent<Animator>();

        if (gameObject.tag == "Hud")
        {
            isEnemy = true;
            EHp = GetComponentInParent<EnemyHp>();
        }
        if (gameObject.tag == "Hudp")
        {
            isPlayer = true;
            HP = GetComponentInParent<HP>();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isEnemy == true)
        {
            if (EHp.Stunned == true)
            {
                anim.SetBool("Stunned", true);
                isStunned = true;
            }
            if(EHp.Stunned == false)
            {
                anim.SetBool("Stunned", false);
                isStunned = false;
            }
        }
        if (isPlayer == true)
        {
            if (HP.Stunned == true)
            {
                anim.SetBool("Stunned", true);
                isStunned = true;
            }
            if(HP.Stunned == false)
            {
                anim.SetBool("Stunned", false);
                isStunned = false;
            }


        }
    }
}
