using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenFreezer : MonoBehaviour
{
    public bool Freeze;
        
    public float Speed;
    public float Freezetime;
    public bool RestoreTime;
    public float LAmount;
    // Start is called before the first frame update
    void Start()
    {
        RestoreTime = false;

    }
   

    // Update is called once per frame
    void Update()
    {
        if (RestoreTime == true)
        {
            if(Time.timeScale < 1f)
            {
                Time.timeScale += Time.deltaTime * Speed;
            }
            else
            {
                Time.timeScale = 1;
                RestoreTime = false;
            }
        }
        
    }
    public void StopTime(float ChangeTime, int RestoreSpeed, float Delay)
    {
        Speed = RestoreSpeed;

        if(Delay > 0)
        {
            StopCoroutine(StartTimeAgain(Delay));
            StartCoroutine(StartTimeAgain(Delay));
        }
        Time.timeScale = ChangeTime;
    }
    IEnumerator StartTimeAgain(float amt)
    {
       
        yield return new WaitForSeconds(amt);
        RestoreTime = true;
    }
}
