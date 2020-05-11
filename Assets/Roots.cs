using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Roots : MonoBehaviour
{
    [SerializeField] Sprite dead_tree_root;
    [SerializeField] Sprite alive_tree_root;

    SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        sprite = GetComponent<SpriteRenderer>();
        sprite.sprite = dead_tree_root;
    }

    // Update is called once per frame
    void Update()
    {
        if (this.transform.parent.GetComponent<Tree>().isAlive)
        {
            sprite.sprite = alive_tree_root;
        }
        else
        {
            sprite.sprite = dead_tree_root;
        }
    }
}
