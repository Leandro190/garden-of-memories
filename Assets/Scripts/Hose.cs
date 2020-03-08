using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hose : MonoBehaviour
{
    GameObject player;

    LineRenderer hose;
    RaycastHit2D hit;
    List<Transform> hits;

    int hitPostion;
    float distanceFromHit;
    public float totalDistance { set; get; }
    public Transform lastHitTransform { set; get; }

    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        hitPostion = 0;
        distanceFromHit = 0;
        totalDistance = 0;
        hose = GetComponent<LineRenderer>();
        hose.positionCount = 2;
        hits = new List<Transform>();
        hose.SetPosition(0, this.transform.position);
        hose.SetPosition(hose.positionCount - 1, player.transform.position);
    }

    private void Update()
    {
        Drawhose();
        FindPoint();
        CalculateTotalLength();
    }

    private void Drawhose()
    {
        hose.SetPosition(0, this.transform.position);
        hose.SetPosition(hose.positionCount-1, player.transform.position);
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
            hose.positionCount++;

            hit.collider.gameObject.layer = 1 >> 8;

            hits.Add(hit.collider.transform);

            lastHitTransform = hits[hitPostion];
            
            hose.SetPosition(hose.positionCount-1, player.transform.position);
            hose.SetPosition(hitPostion+1, hit.transform.position);

            CalculateLengthFromHit();

            hitPostion++;
        }
    }
}
