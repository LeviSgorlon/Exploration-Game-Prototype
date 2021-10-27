using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupzone : MonoBehaviour {
    public Vector3 liftposition;
    GameObject liftupzone;
    GameObject Player;
    PlayerController Vari;
    Vector2 zero;
    public GameObject item;
    public bool got;
    Rigidbody2D rg;
    public GameObject CurrentWeapon;
    public GameObject LastWeapon;
    public bool HasItem;
    // Use this for initialization
    void Start () {
        
        liftupzone = GameObject.Find("liftzone");
        Player = transform.root.gameObject;
        Vari = Player.GetComponent<PlayerController>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(HasItem == false) { 
        if (other.transform.parent.name == transform.root.GetComponentInChildren<ItemMagnet>().chosentra.parent.name)
        {
            if (other.gameObject.tag == "Object")
            {
                item = other.gameObject;
                got = true;
                rg = item.GetComponent<Rigidbody2D>();
                rg.simulated = false;
                other.transform.SetParent(liftupzone.transform);
                other.transform.position = liftposition;






            }
            if (other.gameObject.tag == "Weapon")
            {
                if (other.name == "LongSword")
                {
                    got = true;
                    LastWeapon = other.gameObject;
                   LastWeapon.transform.parent.SetParent(transform.root);
                    other.gameObject.SetActive(false);
                    CurrentWeapon = GameObject.Find(transform.root.name + "/Body/LongSword");
                    GameObject.Find(transform.root.name + "/Body/LongSword").SetActive(true);

                    Vari.Armed = true;
                }
                    if (other.name == "BombStick")
                    {
                        got = true;
                        LastWeapon = other.gameObject;
                        LastWeapon.transform.parent.SetParent(transform.root);
                        other.gameObject.SetActive(false);
                        CurrentWeapon = GameObject.Find(transform.root.name + "/Body/BombStick");
                        GameObject.Find(transform.root.name + "/Body/BombStick").SetActive(true);

                        Vari.Armed = true;
                    }






                }
        }
    }

    }
    void FixedUpdate()
    {
        if (CurrentWeapon != null) { 
        if (CurrentWeapon.gameObject.activeInHierarchy == false)
        {
            CurrentWeapon = null;
        } }
        liftposition = liftupzone.transform.position;
        Vari.got = got;
        if(Vari.Armed == true)
        {
            HasItem = true;
        }
        else { HasItem = false; }
        if(Input.GetKey(KeyCode.E) == false)
        {
            got = false;
        }
        if(Vari.throwing == true)
        {
            liftupzone.transform.DetachChildren();
            item.transform.position = transform.position;
            rg.simulated = true;
            rg.AddForce(-transform.up * 1000);
            got = false;
        }

    }
}
