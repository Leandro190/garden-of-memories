using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] GameObject player;

    int position;
    LineRenderer rope;
    RaycastHit2D hit;
    List<Transform> hits;

    int obstacles = 1;

    float distanceFromHit;
    float totalDistance;

    private void Start()
    {
        position = 0;
        distanceFromHit = 0;
        totalDistance = 0;
        rope = GetComponent<LineRenderer>();
        hits = new List<Transform>();
    }

    private void Update()
    {
        DrawRope();
        FindPoint();
        CalculateTotalLength();
        CheckLimit();
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

    private void CalculateLengthFromHit()
    {
        if (position == 0)
        {
            distanceFromHit += Vector2.Distance(hits[position].position, this.transform.position);
        }
        else
        {
            distanceFromHit += Vector2.Distance(hits[position].position, hits[position - 1].position);
        }
    }

    private void CalculateTotalLength()
    {
        if (position > 0)
        {
            totalDistance = Vector2.Distance(player.transform.position, hits[position-1].position) + distanceFromHit;
        }
        
        print(totalDistance);
    }

    private void CheckLimit()
    {
        if (totalDistance > 15f)
        {
            print("LIMIT");
        }
    }

    private void FindPoint()
    {
        hit = Physics2D.Linecast(this.transform.position, player.transform.position);
        
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

            CalculateLengthFromHit();

            position++;
        }
    }
}
