using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] GameObject ropeBase;
    [SerializeField] GameObject player;

    Transform pointA;
    Transform pointB;
    LineRenderer rope;

    private void Start()
    {
        pointA = ropeBase.GetComponent<Transform>();
        pointB = player.GetComponent<Transform>();
        rope = GetComponent<LineRenderer>();
    }

    private void Update()
    {
        DrawRope();
    }

    private void DrawRope()
    {
        rope.SetPosition(0, ropeBase.transform.position);
        rope.SetPosition(1, player.transform.position);
    }
}
