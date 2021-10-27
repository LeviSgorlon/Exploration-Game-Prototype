using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class debughotkeys : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject[] BatchSpawn;
    private GameObject Effect;
    public Transform PlayerOri;
    public float ThrowForce;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Effect = GameObject.Instantiate(BatchSpawn[0], null, false);
           
            Effect.transform.position = transform.position;
            
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Effect = GameObject.Instantiate(BatchSpawn[0], null, false);
            Effect.transform.position = PlayerOri.position;
            Effect.gameObject.GetComponent<Rigidbody2D>().AddForce(PlayerOri.up * ThrowForce) ;
            Effect.transform.position = transform.position;

        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Effect = GameObject.Instantiate(BatchSpawn[1], null, false);
            Effect.transform.position = PlayerOri.position;

        }
    }
}
