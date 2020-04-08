using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tree : MonoBehaviour
{
    [SerializeField] Sprite dead_tree_bare;
    [SerializeField] Sprite dead_tree_hose;

    SpriteRenderer sprite;

    public bool tiedUp { get; set; }
    public bool DettachRope_Positive_x { get; set; }
    public bool DettachRope_Positive_y { get; set; }
    public bool DettachRope_Negative_x { get; set; }
    public bool DettachRope_Negative_y { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        DettachRope_Positive_x = false;
        DettachRope_Positive_y = false;
        DettachRope_Negative_x = false;
        DettachRope_Negative_y = false;
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
