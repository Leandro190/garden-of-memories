using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LogHandler : MonoBehaviour
{
    public List<Sprite> LogSprites;

    public float integrity = 1000;
    private float initIntegrity;

    private SpriteRenderer spriteRenderer;

    private int currentSprite = 0;

    void Start()
    {
        initIntegrity = integrity;
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = LogSprites[currentSprite];
        currentSprite++;
    }

    // Update is called once per frame
    void Update()
    {
        if(currentSprite == LogSprites.Count)
        {
            Destroy(this.gameObject);
            return;
        }
        if(integrity <= ((LogSprites.Count - (float)currentSprite)/LogSprites.Count) * initIntegrity)
        {
            spriteRenderer.sprite = LogSprites[currentSprite];
            currentSprite++;
            Debug.Log((LogSprites.Count - currentSprite) / LogSprites.Count * integrity);
        }
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "insect")
        {
            integrity -= 1;
            //Debug.Log(integrity);
        }

    }


}
