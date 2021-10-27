using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyhpHud : MonoBehaviour {
    public EnemyHp enemystats;
    public Animator _animator;
    public int numberfohorns;
	// Use this for initialization
	void Awake () {
        enemystats = gameObject.GetComponentInParent<EnemyHp>();
        _animator = gameObject.GetComponent<Animator>();
    }
	
	// Update is called once per frame
	void Update () {
        

        _animator.SetInteger("numberofhorns", numberfohorns);
		if(enemystats.fullhealth > 50)
        {
            numberfohorns = 1;
        }
        if (enemystats.fullhealth > 100)
        {
            numberfohorns = 2;
        }
        if (enemystats.fullhealth < 50)
        {
            numberfohorns = 0;
        }

    }
}
