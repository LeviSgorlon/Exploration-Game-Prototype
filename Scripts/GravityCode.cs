using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravityCode : MonoBehaviour
{
    // Start is called before the first frame update

    public Rigidbody _rb;
    public float Gforce;
    private GameObject Effect;
    public GameObject BounceEF;
    void Start()
    {
        _rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        _rb.AddForce(GameObject.Find("GlobalOri").transform.up * -Gforce);
    }

  
}
