using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CopySprite : MonoBehaviour
{
    public SpriteRenderer sprite;
    public float xScale;
    public float yScale;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.GetComponent<SpriteMask>().sprite = sprite.sprite;
        transform.localPosition = sprite.transform.localPosition;
        if(sprite.flipX == true)
        {
            transform.localScale = new Vector3(-sprite.transform.localScale.x - 0.2f - xScale, sprite.transform.localScale.y + .3f + yScale, 1);
        }
        else {
            transform.localScale = new Vector3(sprite.transform.localScale.x + 0.2f + xScale, sprite.transform.localScale.y +.3f + yScale, 1);
        }
    }
}
