using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope : MonoBehaviour
{
    [SerializeField] GameObject player;

    LineRenderer rope;
    RaycastHit2D hit;
    List<Transform> hits;

    int hitPostion;
    float distanceFromHit;
    public static float totalDistance;
    public static Transform lastHitTransform;
    LayerMask defaultLayer;
    LayerMask targetLayer;
    Obstacle obstacle;

    private void Start()
    {
        defaultLayer = 1;
        hitPostion = 0;
        distanceFromHit = 0;
        totalDistance = 0;
        rope = GetComponent<LineRenderer>();
        rope.positionCount = 2;
        hits = new List<Transform>();
    }

    private void Update()
    {
        DrawRope();
        FindPoint();
        CalculateTotalLength();
    }

    private void DrawRope()
    {
        rope.SetPosition(0, this.transform.position);
        rope.SetPosition(rope.positionCount-1, player.transform.position);
    }

    private void CalculateLengthFromHit()
    {
        if (hitPostion == 0)
        {
            distanceFromHit += Vector2.Distance(hits[hitPostion].position, this.transform.position);
        }
        else
        {
            distanceFromHit += Vector2.Distance(hits[hitPostion].position, hits[hitPostion - 1].position);
        }
    }

    private void CalculateTotalLength()
    {
        if (hitPostion == 0)
        {
            totalDistance = Vector2.Distance(player.transform.position, this.transform.position);
        }
        else
        {
            totalDistance = Vector2.Distance(player.transform.position, hits[hitPostion - 1].position) + distanceFromHit;
        }
    }

    private void FindPoint()
    {
        if (hitPostion == 0)
        {
            hit = Physics2D.Linecast(this.transform.position, player.transform.position, 1<<8);
        }
        else
        {
            hit = Physics2D.Linecast(hits[hitPostion - 1].position, player.transform.position, 1<<8);
        }
        
        if (hit && !hits.Contains(hit.transform))
        {
            rope.positionCount++;

            hit.collider.gameObject.layer = 1 >> 8;

            hits.Add(hit.collider.transform);

            lastHitTransform = hits[hitPostion];
            
            rope.SetPosition(rope.positionCount-1, player.transform.position);
            rope.SetPosition(hitPostion+1, hit.transform.position);

            CalculateLengthFromHit();

            hitPostion++;
        }
    }
}
