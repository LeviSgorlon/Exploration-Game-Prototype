using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAIKillKill : MonoBehaviour
{
    public float raio = 200;
    public RaycastHit2D hitinfo;
    public float distancia = 1;
    public int layer = 8;
    public Rigidbody2D rb;
    public Transform target;
    public Animator _animator;
    public int health = 5;
    public Animation dano;


    
       

    

    // Use this for initialization
    void Awake()
    {
        _animator = GetComponent<Animator>();
       


    }

    
    void FixedUpdate()
    {
        hitinfo = Physics2D.CircleCast(transform.position, raio = 2000, Vector2.up, distancia, layer);
        if (hitinfo.collider != null)
        {
           
               
        Vector2 targetPosition = hitinfo.point;
        rb.AddForce(targetPosition);
            
        }
    }
}






    

