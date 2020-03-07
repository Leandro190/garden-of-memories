using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    LayerMask defaultLayer;
    LayerMask theLayer { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        defaultLayer = 1;
        theLayer = GetComponent<LayerMask>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
