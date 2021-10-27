using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sortingwiz : MonoBehaviour
{
    public UnityEngine.Rendering.SortingGroup sort;
    public int offset;
    // Start is called before the first frame update
    void Start()
    {
        sort = gameObject.GetComponent<UnityEngine.Rendering.SortingGroup>();
    }

    // Update is called once per frame
    void Update()
    {
        sort.sortingOrder = offset - (int)transform.position.y ;
    }
}
