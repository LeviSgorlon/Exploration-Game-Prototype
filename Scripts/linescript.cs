using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class linescript : MonoBehaviour {
    private LineRenderer lineRenderer;
    private float counter;
    private float dist;

    public Transform origin;
    public Transform destination;
    public float linedrawspeed = 6f;
    // Use this for initialization
    void Start () {
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.SetPosition(0, destination.position);
        lineRenderer.SetPosition(0, origin.position);

        dist = Vector2.Distance(origin.position, destination.position);

    }
	
	// Update is called once per frame
	void Update () {




        if (origin != null && destination != null)
        {
            Vector3 pointA = origin.position;
            Vector3 pointB = destination.position;
            lineRenderer.SetPosition(1, pointB);
            lineRenderer.SetPosition(0, pointA);
        }


        
      
    }
}
