using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InterfaceHP : MonoBehaviour {
    private GameObject Player;
    private HP HPstats;
    private Animator _animator;
    public int HP;
	// Use this for initialization
	void Start () {
        Player = GameObject.Find("Player");
        HPstats = Player.GetComponent<HP>();
        _animator = GetComponent<Animator>();
        
	}
	
	// Update is called once per frame
	void Update () {
        HP = HPstats.health;
        _animator.SetInteger("HP", HP);
	}
}
