using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Caracolanimations : MonoBehaviour
{
    public float raio = 200;
    public RaycastHit2D hitinfo;
    public float distancia = 1;
    public int layer = 1 << 8;
    public float angulo = 0;
    public float numerodefantasmas;
    public Animator _animator;
    public GameObject Objeto;
    public Vector2 Posicao;
    public Quaternion Rotacao;
    public Transform Parente;
    WaitForSeconds wait;
    // Use this for initialization
    void Start()
    {
        _animator.GetComponent<Animator>();
        hitinfo = Physics2D.CircleCast(transform.position, raio = 2000, Vector2.up, distancia, layer);
        if (hitinfo.collider != null)
        {
            while (numerodefantasmas < 3)
            {
                _animator.Play(Animator.StringToHash("Spectrevomit"));
                new WaitForSeconds(2);
                
                numerodefantasmas = +1;

            }
        }

    }
}

