using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundPlayer : MonoBehaviour
{
    //public List<GameObject> sound;
    GameObject Effect1;
    GameObject Effect2;
    GameObject Effect3;
    private Rigidbody _rb;
    public int SoundRandomizer;
    public float velocidade;
    public float velocidadeatual2;
    private int Randomizationrange;
    public int currentsoundID;
    float pitch;
    public void PlaySound(GameObject Effect, bool destroyafter,float volume,float pitchVariation,int VariationType,int randomizationrange)
    {   
        switch (VariationType)
        {
            default:
        break;
            case 1:
            pitch = Random.Range((1 - pitchVariation), (1 + pitchVariation));
            volume = velocidade/50 * volume;
        break;
            case 2:
            pitchVariation = velocidade/120;
            volume = velocidade/50 * volume;
        break;
            case 3:
            pitch = Random.Range((1 - pitchVariation), (1 + pitchVariation));
        break;
            case 4:
            pitch = Random.Range((1 - pitchVariation), (1 + pitchVariation));
        break;
        }
                Effect1 = Instantiate(Effect, transform.position, Quaternion.identity) as GameObject;
                AudioSource audio = Effect1.GetComponent<AudioSource>();
                audio.volume = volume;
                audio.pitch = pitch;
                if (destroyafter == true)
                {
                    Destroy(Effect1, 3f);
                }
            
    }
    public void StopSound()
    {       
        Destroy(Effect1);       
        Destroy(Effect2);       
        Destroy(Effect3);
    }
    public void Start()
    {
        _rb = GetComponent<Rigidbody>();
    }
    private void Update()
    {
       if(_rb != null){
            velocidade = _rb.velocity.magnitude;
       }else{
           velocidade = 120;
       }   
    }
}


