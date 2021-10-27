using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlubluIA : MonoBehaviour
{

    //Script basico de movimento,primeiramente é necessario criar uma variavel com o tipo do componente responsavel por movimento
    //e dps acessa-lo  pelo void desejado dependendo do tipo de movimento desejado,para esse script vamos fazer um simples movimento pra
    //direita que ao colidir com qualquer coisa muda de direçao para a esquerda
    //primeiro declare as variaveis
    private Rigidbody2D rb;
    public bool Vaipradireita = true;
    public bool Vaipraesquerda = false;
    private int layer1 = 1 << 17;
    private int layer2 = 1 << 18;
    private int layer3 = 1 << 19;
    private int lplayer = 1 << 8;
    private SpriteRenderer sr;
    private SpriteRenderer rb1;
    public float cd = 0;
    public RaycastHit2D p1;
    public RaycastHit2D p2;
    public RaycastHit2D p3;
    public RaycastHit2D p1D;
    public RaycastHit2D p2D;
    public RaycastHit2D p3D;
    public RaycastHit2D player;
    public int trajeto;
    public bool atk;
    private Animator _anim;
    void Start()
    {
       
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        _anim = GetComponent<Animator>(); // get component é para pega os conponentes dos objetos
       
        
    }   
    void Update()
    {
        p1 = Physics2D.CircleCast(transform.position, 600, Vector2.up, 0, layer1);
        p2 = Physics2D.CircleCast(transform.position, 600, Vector2.up, 0, layer2);
        p3 = Physics2D.CircleCast(transform.position, 600, Vector2.up, 0, layer3);
        p1D = Physics2D.CircleCast(transform.position, 30, Vector2.up, 0, layer1);
        p2D = Physics2D.CircleCast(transform.position, 30, Vector2.up, 0, layer2);
        p3D = Physics2D.CircleCast(transform.position, 30, Vector2.up, 0, layer3);// é pARA O RADAR
        player = Physics2D.CircleCast(transform.position, 30000, Vector2.up, 0,lplayer);
        cd += Time.deltaTime;// É O CONOMETRO
        
        if(cd >= 3f)
        {
            trajeto = Random.Range(1, 4);
            cd = 0;
        }
        if(trajeto == 1 && p1D.collider == null)
        {
            rb.AddForce(p1.transform.position - transform.position);
        }
        if (trajeto == 2 && p2D.collider == null) {
              rb.AddForce(p2.transform.position - transform.position);
            
        }
        if (trajeto == 3 && p3D.collider == null)
        {
           //para ir para um lugar coloque o lugar que vc quer ir - o lugar que voce esta
            rb.AddForce(p3.transform.position - transform.position);
        }
        if(p3D.collider != null | p2D.collider != null | p1D.collider != null)
        {
            atk = true;

        }
        else { atk = false; }
    }
}


