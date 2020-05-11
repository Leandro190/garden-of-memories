using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    SpriteRenderer overlay;
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        overlay = this.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (player.GetComponent<PlayerController>().won)
        {
            overlay.color = new Color (46f,46f,46f,0f);
        }
    }
}
