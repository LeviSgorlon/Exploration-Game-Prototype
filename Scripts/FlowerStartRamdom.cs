using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlowerStartRamdom : MonoBehaviour
{
    // Start is called before the first frame update
    public float delay;
    private float delaytrue;
    public int i;
    void Start()
    {
        anims = GameObject.FindGameObjectsWithTag("Flowers");
        foreach (GameObject A in anims)
        {
            A.GetComponent<Animator>().speed = 0;
        }
        i = 0;
    }
    public GameObject[] anims;
    // Update is called once per frame
    void Update()
    {
        if (i < anims.Length)
        {
            delaytrue -= Time.deltaTime;
        }
        if (delaytrue < 0)
        {           
            anims[i].GetComponent<Animator>().speed = 1;
            i++;
            delaytrue = delay;
        }
        
    }
}
