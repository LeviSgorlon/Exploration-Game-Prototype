using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Talkarea : MonoBehaviour {
    public string npcname;
    public Talkativeboi talkstats;
    public bool oncolli;
	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.tag == "NPC")
        {
            npcname = collision.name;
            oncolli = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "NPC")
        {
            npcname = null;
            talkstats.showI = false;
            oncolli = false;
        }
    }

    // Update is called once per frame
    void Update () {
        if (oncolli == true) {
            talkstats = GameObject.Find(npcname).GetComponent<Talkativeboi>();
            talkstats.showI = true;
        }
	}
}
