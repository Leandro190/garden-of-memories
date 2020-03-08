using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] Sprite dead_tree_bare;
    [SerializeField] Sprite dead_tree_hose;

    SpriteRenderer sprite;

    public bool tiedUp { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        tiedUp = false;
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = dead_tree_bare;
    }

    // Update is called once per frame
    void Update()
    {
        if (tiedUp)
        {
            sprite.sprite = dead_tree_hose;
        }
        else
        {
            sprite.sprite = dead_tree_bare;
        }
    }
}
