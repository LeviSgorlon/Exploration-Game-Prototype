using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathAI : MonoBehaviour
{
    public float raio = 200;
    private RaycastHit2D hitinfo;
    private RaycastHit2D ponto1;
    public float distancia = 1;
    public int layer = 99;
    public int layer2 = 9;
    public float empurrao;
    public float smooth = 2.0F;
    public Transform path1;
    private float pathprogress = 0;
    public float smoothTime = 2.0F;
    private Vector3 velocity = Vector3.zero;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        hitinfo = Physics2D.CircleCast(transform.position, raio = 2000, Vector2.up, distancia, layer);

        if (hitinfo.collider != null)
        {
            
            if (pathprogress == 0)
            {
                ponto1 = Physics2D.CircleCast(transform.position, raio = 2000, Vector2.up, distancia, layer2);
                Vector2 path1 = ponto1.point;
                transform.position = Vector3.SmoothDamp(transform.position, path1, ref velocity, smoothTime);
               

            }
        }
    }
}

