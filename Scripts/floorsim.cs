using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class floorsim : MonoBehaviour {
     
    public string objectA;
    public PlayerController Player;
    public bool hit;
    public Rigidbody2D rb;
    public float velocidade;
    // Use this for initialization
    void Start () {
        Player = GameObject.Find("Player").GetComponent<PlayerController>();
        rb = transform.GetComponent<Rigidbody2D>();
        transform.localPosition = new Vector3(0, 0, 0);
        GameObject.Find("tetach").transform.DetachChildren();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.isTrigger != true)
        {
            if (collision.transform.tag == ("Killable"))
            {
               
                Player.i = 10000;
                objectA = collision.name;
                
                Player.descending = false;
                hit = true;
                Player.o = 0;
                
                
                

            }
        }
    }
    // Update is called once per frame
    void Update () {
        if(Player.descending == true)
        {
            velocidade -= 500;
        }
        if (Player.descending == false)
        {
            velocidade = 10000;
        }
        if (Input.GetKey(KeyCode.W))
        {
            rb.AddForce(transform.up * velocidade);
        }
        if (Input.GetKey(KeyCode.S))
        {
            rb.AddForce(transform.up * -velocidade);
        }

    }
}
