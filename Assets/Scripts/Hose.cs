using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hose : MonoBehaviour
{
    [SerializeField] float Hose_Length;

    GameObject player;

    LineRenderer hose;
    RaycastHit2D hit;
    List<Transform> hits;
    Vector3 offset;
    int hitPostion;
    float distanceFromHit;

    public Transform startingPoint { set; get; }
    public float totalDistance { set; get; }
    public Transform lastHitTransform { set; get; }
    public float length { set; get; }

    private void Start()
    {
        offset = new Vector3(-0.05f, -0.52f, 0f);
        length = Hose_Length;
        player = GameObject.FindWithTag("Player");
        hitPostion = 0;
        distanceFromHit = 0;
        totalDistance = 0;
        hose = GetComponent<LineRenderer>();
        hose.positionCount = 2;
        hits = new List<Transform>();
        hose.SetPosition(0, startingPoint.position);
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
        hose.SetPosition(0, startingPoint.position);
        hose.SetPosition(hose.positionCount-1, player.transform.position);
    }

    private void CalculateLengthFromHit()
    {
        if (hitPostion == 0)
        {
            distanceFromHit += Vector2.Distance(hits[hitPostion].position+offset, startingPoint.position);
        }
        else
        {
            distanceFromHit += Vector2.Distance(hits[hitPostion].position + offset, hits[hitPostion - 1].position);
        }
    }

    private void CalculateTotalLength()
    {
        if (hitPostion == 0)
        {
            totalDistance = Vector2.Distance(player.transform.position, startingPoint.position);
        }
        else
        {
            totalDistance = Vector2.Distance(player.transform.position, hits[hitPostion - 1].position + offset) + distanceFromHit;
        }
    }

    private void FindPoint()
    {
        if (hitPostion == 0)
        {
            hit = Physics2D.Linecast(startingPoint.position, player.transform.position, 1<<8);
        }
        else
        {
            print("hit: " + hits[hitPostion - 1].position);
            print("player: " +player.transform.position);
            
            hit = Physics2D.Linecast(hits[hitPostion - 1].position+offset, player.transform.position, 1<<8);
        }
        
        if (hit && !hits.Contains(hit.transform))
        {
            hose.positionCount++;

            hit.collider.gameObject.layer = 1 >> 8;
            if (hit.collider.gameObject.GetComponent<Tree>())
            {
                hit.collider.gameObject.GetComponent<Tree>().tiedUp = true;
            }

            hits.Add(hit.collider.transform);

            lastHitTransform = hits[hitPostion];
            
            hose.SetPosition(hose.positionCount-1, player.transform.position);
            hose.SetPosition(hitPostion+1, (hit.transform.position+offset));

            CalculateLengthFromHit();

            hitPostion++;
        }
    }
}
