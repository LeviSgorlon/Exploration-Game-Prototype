using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotoesScript : MonoBehaviour
{
    public Animator anim;
    public Animator Fadeanim;
    public Animator Camanim;
    int menuoption = 1;
    public SoundPlayer Sound;
    public bool mainmenu;
    public bool Ready;
    // Start is called before the first frame update
    void Start()
    {
        Sound = GetComponent<SoundPlayer>();
        mainmenu = true;
        anim = GameObject.Find("Menu options").GetComponent<Animator>();
        Fadeanim = GameObject.Find("Image").GetComponent<Animator>();
        Camanim = GameObject.Find("Main Camera").GetComponent<Animator>();
        Ready = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.Return) && Ready == false)
        {
            Time.timeScale = 8;
        }
        else { Time.timeScale = 1; }
        anim.SetInteger("menu", menuoption);

        if (Input.GetKeyDown(KeyCode.S)){
            menuoption += 1;
           // Sound.PlaySound(0, true, 1, 0.3f, true, 0);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
          //  Sound.PlaySound(0, true, 1, 0.3f, true, 0);
            menuoption -= 1;
        }
        if (mainmenu == true)
        {
            if (menuoption <= 0)
            {
                menuoption = 4;
            }
            if (menuoption >= 5)
            {
                menuoption = 1;
            }
        }
        if (Input.GetKeyDown(KeyCode.Return) && Ready == true)
        {
            Menuaction(menuoption);
          //  Sound.PlaySound(1, true, 1, 0f, true, 0);
        }

    }

    void Readyfy()
    {
        Ready = true;
    }

    void Menuaction(int menu)
    {
        switch (menu)
        {
            case 1:
                Fadeanim.SetTrigger("Fadeout");
                break;
            case 2:
                //load game
                break;
            case 3:
                //go to options
                break;
            case 4:
                //exit game
                break;
                
        }
    }

    void OnFadeComplete()
    {
        SceneManager.LoadScene(2);
    }


    }

  

