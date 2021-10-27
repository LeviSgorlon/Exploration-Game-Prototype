using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Talkativeboi : MonoBehaviour {
    public bool showI;
    public string[] text;
    public TextMeshPro textstats;
    public float fadetimer;
    public int currenttext;
    public bool fuckhandler;
    public int maxvisiblec;
    public int counter = 0;
    public GameObject Effect;
    public GameObject Effectprefab;
  
    // Use this for initialization
    void Start () {
        textstats = gameObject.GetComponentInChildren<TextMeshPro>();
    }
    private void FixedUpdate()
    {

        if (fadetimer <= 0)
        {
            textstats.text = ("");
            currenttext = 0;
            counter = 0;
        }
       
    }
    // Update is called once per frame
    void Update () {
        if (counter <= textstats.maxVisibleCharacters)
        {
            counter += 1;
            textstats.maxVisibleCharacters = counter;
        }


        if (showI == true)
        {
            fadetimer = 1f;
            fuckhandler = false;
            textstats.text = (text[currenttext]);
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (currenttext < (text.Length - 1))
                {
                    
                    Effect = Instantiate(Effectprefab, textstats.transform.position, Quaternion.identity) as GameObject;
                    Effect.GetComponent<TextMeshPro>().text = ("<#CBCBCB>" + text[currenttext]+"</color>");
                    Effect.transform.Translate(+0, +12, +0);
                    

                    counter = 0;
                    currenttext += 1;
                }
                fadetimer = 3f;
                


            }
        }
       
        if (showI == false)
        {
            fadetimer -= Time.deltaTime;
            Destroy(Effect, 1f);
        }
        
	}
}
