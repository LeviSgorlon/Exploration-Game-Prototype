using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wind : MonoBehaviour
{
    public BoxCollider2D WindZone;
    public BoxCollider2D WindZonePlayer;
    public ParticleSystem WindParticules1;
    public ParticleSystem WindParticules2;
    public ParticleSystem WindParticules3;
    public AudioSource WindSound;
    public Animator[] AllAnim;
    public bool wind;
    public int i;
    public bool toggle;
    public bool finished;
    void Start()
    {
        AllAnim = UnityEngine.Object.FindObjectsOfType<Animator>();
    }
   
    

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            if (wind == false)
            {
                wind = true;
            }
            else {
                if (wind == true)
                {
                    wind = false;
                }
            }
            
        }

        if (wind == true && toggle == false)
        {
            
                i = 0;
                toggle = true;

            
           
        }
        if (wind == false && toggle == true)
        {

            i = 0;
            toggle = false;



        }


        if (wind == true)
        {
            if (i < AllAnim.Length)
            {              
               foreach (Animator An in AllAnim)
                {
                    AllAnim[i].SetBool("wind", true);
                    i += 1;
                }
            }
            if(WindSound.volume < 0.05f){
                WindSound.volume += 0.001f;
            }
            
            WindZone.enabled = true;
            WindZonePlayer.enabled = true;
            if(WindParticules1.playbackSpeed < 2){
                WindParticules1.playbackSpeed += 0.04f;
            }
            if (WindParticules2.playbackSpeed < 2)
            {
                WindParticules2.playbackSpeed += 0.04f;
            }
            if (WindParticules3.playbackSpeed < 2)
            {
                WindParticules3.playbackSpeed += 0.04f;
            }
        }
        else {
            if (i < AllAnim.Length)
            {
                foreach (Animator An in AllAnim)
                {
                    AllAnim[i].SetBool("wind", false);
                    i += 1;
                }
            }
            if (WindSound.volume > 0.02f)
            {
                WindSound.volume -= 0.001f;
            }
            WindZone.enabled = false;
            WindZonePlayer.enabled = false;
            if (WindParticules1.playbackSpeed > 1)
            {
                WindParticules1.playbackSpeed -= 0.04f;
            }
            if (WindParticules3.playbackSpeed > 1)
            {
                WindParticules3.playbackSpeed -= 0.04f;
            }
            if (WindParticules2.playbackSpeed > 1)
            {
                WindParticules2.playbackSpeed -= 0.04f;
            }

        }
    }
}
