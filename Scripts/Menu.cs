using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;


public class Menu : MonoBehaviour
{
    public Animator _animator;
    public float Animtimer;
    public GameObject player;
    public PlayerController controlador;
    public int menuoption = 1;
    public bool introanim = false;
    public bool Inmenu;
    
    void Start()
    {
        _animator = GetComponent<Animator>();
        player = GameObject.Find("Player");
        controlador = player.gameObject.GetComponent<PlayerController>();
        Animtimer = 2.3f;
        Inmenu = true;
    }
    private void Update()
    {
        if (Input.GetKey(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (controlador.PlayerOnControl == false)
        {
            _animator.SetInteger("menu", menuoption);
            if (menuoption == 0)
            {
                menuoption = 3;
            }
            if (menuoption == 4)
            {
                menuoption = 1;
            }
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                menuoption -= 1;
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                menuoption += 1;
            }
        }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (menuoption == 1)
                {
                    controlador.PlayerOnControl = true;
                    _animator.SetBool("Starting",true);
                introanim = true;
                Inmenu = false;
                }
            }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (menuoption == 3)
            {
                Application.Quit();
            }
        }
        if (introanim == true)
        {
            Animtimer -= Time.deltaTime;
            if (Animtimer <= 0)
            {
                _animator.SetBool("Starting", false);
            }
        }
        
    }
    public void Newgame()
    {
      
    }
    public void Load()
    {

    }
    public void Exit()
    {

    }

}
