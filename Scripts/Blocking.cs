using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocking : MonoBehaviour {
    private Animator _animator;
    public bool blockinganim = false;
    
    // Use this for initialization
    void Start () {
        _animator = GetComponent<Animator>();
    }
   
    // Update is called once per frame
    void FixedUpdate () {
        if (Input.GetMouseButton(1))
        {
            blockinganim = true;
            _animator.SetBool("Blocking", blockinganim);
        }
        else
        {
            blockinganim = false;
            _animator.SetBool("Blocking", blockinganim);
        }
        
    }
}
