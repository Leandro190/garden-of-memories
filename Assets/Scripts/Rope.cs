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
    RaycastHit2D hit;
    List<Transform> hits;

    int obstacles = 1;
    int decrement = 0;

    private void Start()
    {
        pointA = ropeBase.GetComponent<Transform>();
        pointB = player.GetComponent<Transform>();
        rope = GetComponent<LineRenderer>();
        hits = new List<Transform>();
    }

    private void Update()
    {
        DrawRope();
        FindPoint();
    }

    private void DrawRope()
    {
        rope.SetPosition(0, player.transform.position);
        //rope.SetPosition(0, player.transform.position);
    }

    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.collider.tag == "obstacle")
    //    {
    //        rope.SetPosition(1, collision.collider.transform.position);
    //        rope.SetPosition(2, player.transform.position);
    //    }

        
    //}

    private void FindPoint()
    {
        
        
        hit = Physics2D.Linecast(ropeBase.transform.position, player.transform.position);
        
        if (hit && !hits.Contains(hit.transform))
        {
            hits.Add(hit.collider.transform);
            print("hit");
            
            rope.SetPosition(1, hits[obstacles - 1].position);
            for (int i= 0; i< (obstacles - 1); i++)
            {
                rope.SetPosition(obstacles-i, hits[i].position);
            }

            obstacles++;
        }
    }
}
