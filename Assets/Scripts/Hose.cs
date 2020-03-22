using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hose : MonoBehaviour
{
    [SerializeField] float Hose_Length;
    [SerializeField] float Angle_of_disconnect;

    GameObject player;

    LineRenderer hose;
    RaycastHit2D hit;
    List<GameObject> hits;
    Vector3 target_line;
    Vector3 player_line;
    Vector3 offset;
    int hitPostion;
    float distanceFromHit;

    public Transform startingPoint { set; get; }
    public float totalDistance { set; get; }
    public Transform lastHitTransform { set; get; }
    public float length { set; get; }

    private void Start()
    {
        target_line = new Vector3(0, 0, 0);
        offset = new Vector3(-0.05f, -0.52f, 0f);
        length = Hose_Length;
        player = GameObject.FindWithTag("Player");
        hitPostion = 0;
        distanceFromHit = 0;
        totalDistance = 0;
        lastHitTransform = this.transform;
        hose = GetComponent<LineRenderer>();
        hose.positionCount = 2;
        hits = new List<GameObject>();
        hose.SetPosition(0, startingPoint.position);
        hose.SetPosition(hose.positionCount - 1, player.transform.position);
    }

    private void Update()
    {
        Drawhose();
        FindPoint();
        CalculateTotalLength();
        if (hose.positionCount >= 3)
        {
            Unattach();
        }
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
            distanceFromHit += Vector2.Distance(hits[hitPostion].transform.position + offset, startingPoint.position);
        }
        else
        {
            distanceFromHit += Vector2.Distance(hits[hitPostion].transform.position + offset, hits[hitPostion - 1].transform.position + offset);
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
            totalDistance = Vector2.Distance(player.transform.position, hits[hitPostion - 1].transform.position + offset) + distanceFromHit;
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
            hit = Physics2D.Linecast(hits[hitPostion - 1].transform.position + offset, player.transform.position, 1<<8);
        }

        if (hit && !hits.Contains(hit.collider.gameObject))
        {
            hose.positionCount++;

            hit.collider.gameObject.layer = 1 >> 8;
            if (hit.collider.gameObject.GetComponent<Tree>())
            {
                hit.collider.gameObject.GetComponent<Tree>().tiedUp = true;
            }

            hits.Add(hit.collider.gameObject);
            lastHitTransform = hit.collider.transform;

            hose.SetPosition(hose.positionCount-1, player.transform.position);
            hose.SetPosition(hitPostion+1, (hit.transform.position + offset));

            CalculateLengthFromHit();

            hitPostion++;
        }
    }

    private void Unattach()
    {
        if (hose.positionCount > 3)
        {
            target_line = hits[hitPostion - 2].transform.position - offset - hits[hitPostion - 1].transform.position - offset;
        }
        else if (hose.positionCount == 3)
        {
            target_line = startingPoint.position - hits[hitPostion - 1].transform.position - offset;
        }
        else
        {
            target_line = new Vector3(0, 0, 0);
        }

        player_line = hits[hitPostion - 1].transform.position + offset - player.transform.position;
        
        print(FindAngle(target_line, player_line));
        if (FindAngle(target_line, player_line) <= Mathf.Cos(Angle_of_disconnect * Mathf.Deg2Rad))
        {
            hose.positionCount--;
            hose.SetPosition(hose.positionCount - 1, player.transform.position);
            hits[hitPostion - 1].GetComponent<Tree>().tiedUp = false;
            hits[hitPostion - 1].layer = 8;
            hits.Remove(hits[hitPostion - 1]);
            hitPostion--;

            lastHitTransform = hits[hitPostion - 1].transform;
        }
    }

    private float FindAngle(Vector3 target_line, Vector3 player_line)
    {
        return Vector3.Dot(target_line, player_line) / (Vector3.Magnitude(target_line) * Vector3.Magnitude(player_line));
    }
}
