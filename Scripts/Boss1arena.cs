using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1arena : MonoBehaviour
{
    public bool isplayeronArena;
    // Use this for initialization	
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.name == "Player")
        {
            isplayeronArena = true;
        }
        return;
    }
    void OnTriggerExit2D(Collider2D col)
    {
        if (col.name == "Player")
        {
            isplayeronArena = false;
        }
        return;
    }
}     
        
