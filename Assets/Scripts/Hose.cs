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
    public List<GameObject> hits { get; set; }
    Vector3 target_line;
    Vector3 player_line;
    Vector3 offset;
    int hitPostion;
    float distanceFromHit;

    public Transform startingPoint { set; get; }
    public float totalDistance { set; get; }
    public float length { set; get; }
    public Transform lastHitObstacle {set; get;} 

    private void Start()
    {
        target_line = new Vector3(0, 0, 0);
        offset = new Vector3(-0.05f, -0.52f, 0f);
        length = Hose_Length;
        player = GameObject.FindWithTag("Player");
        hitPostion = 0;
        distanceFromHit = 0;
        totalDistance = 0;
        hose = GetComponent<LineRenderer>();
        hose.positionCount = 2;
        hits = new List<GameObject>();
        hose.SetPosition(0, startingPoint.position);
        hose.SetPosition(hose.positionCount - 1, player.transform.position);
        lastHitObstacle = this.transform;
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
            lastHitObstacle = hit.collider.gameObject.transform;

            hose.SetPosition(hose.positionCount-1, player.transform.position);
            hose.SetPosition(hitPostion+1, (hit.transform.position + offset));

            CalculateLengthFromHit();

            hitPostion++;

            hit.collider.gameObject.GetComponent<Tree>().DettachRope_Positive_x = false;
            hit.collider.gameObject.GetComponent<Tree>().DettachRope_Negative_x = false;
            hit.collider.gameObject.GetComponent<Tree>().DettachRope_Positive_y = false;
            hit.collider.gameObject.GetComponent<Tree>().DettachRope_Negative_y = false;
            if (PlayerController.movement_vector.x == 1)
            {
                hit.collider.gameObject.GetComponent<Tree>().DettachRope_Positive_x = true;
            }
            if (PlayerController.movement_vector.x == -1)
            {
                hit.collider.gameObject.GetComponent<Tree>().DettachRope_Negative_x = true;
            }
            if (PlayerController.movement_vector.y == 1)
            {
                hit.collider.gameObject.GetComponent<Tree>().DettachRope_Positive_y = true;
            }
            if (PlayerController.movement_vector.y == -1)
            {
                hit.collider.gameObject.GetComponent<Tree>().DettachRope_Negative_y = true;
            }
        }
    }

    private void Unattach()
    {
        if (hose.positionCount > 3)
        {
            target_line = hits[hitPostion - 1].transform.position - hits[hitPostion - 2].transform.position;
        }
        else if (hose.positionCount == 3)
        {
            target_line = hits[hitPostion - 1].transform.position + offset - startingPoint.position;
        }
        else
        {
            target_line = new Vector3(0, 0, 0);
        }

        player_line = player.transform.position - hits[hitPostion - 1].transform.position - offset;
       
        if (((hits[hitPostion - 1].GetComponent<Tree>().DettachRope_Negative_x || hits[hitPostion - 1].GetComponent<Tree>().DettachRope_Positive_y) 
            && FindAngle(target_line, player_line) <= -Angle_of_disconnect && FindAngle(target_line, player_line) > -180f)
            || ((hits[hitPostion - 1].GetComponent<Tree>().DettachRope_Negative_y || hits[hitPostion - 1].GetComponent<Tree>().DettachRope_Positive_x) 
            && FindAngle(target_line, player_line) >= Angle_of_disconnect && FindAngle(target_line, player_line) < 180f))
        {
            hose.positionCount--;
            hose.SetPosition(hose.positionCount - 1, player.transform.position);
            hits[hitPostion - 1].GetComponent<Tree>().tiedUp = false;
            hits[hitPostion - 1].layer = 8;

            if (hitPostion <= 1)
            {
                distanceFromHit -= Vector2.Distance(hits[hitPostion - 1].transform.position + offset, startingPoint.position);
            }
            else
            {
                distanceFromHit -= Vector2.Distance(hits[hitPostion - 1].transform.position + offset, hits[hitPostion - 2].transform.position + offset);
            }

            hits.Remove(hits[hitPostion - 1]);
            hitPostion--;
        }
    }

    private float FindAngle(Vector3 target_line, Vector3 player_line)
    {
        if (hits[hitPostion - 1].GetComponent<Tree>().DettachRope_Negative_y || hits[hitPostion - 1].GetComponent<Tree>().DettachRope_Positive_y)
        {
            if ((player_line.y + target_line.y) > target_line.y)
            {
                return Mathf.Acos(Vector3.Dot(target_line, player_line) / (Vector3.Magnitude(target_line) * Vector3.Magnitude(player_line))) * Mathf.Rad2Deg;
            }
            else
            {
                return -(Mathf.Acos(Vector3.Dot(target_line, player_line) / (Vector3.Magnitude(target_line) * Vector3.Magnitude(player_line))) * Mathf.Rad2Deg);
            }
        }
        if (hits[hitPostion - 1].GetComponent<Tree>().DettachRope_Negative_x || hits[hitPostion - 1].GetComponent<Tree>().DettachRope_Positive_x)
        {
            if ((player_line.x + target_line.x) > target_line.x)
            {
                return -(Mathf.Acos(Vector3.Dot(target_line, player_line) / (Vector3.Magnitude(target_line) * Vector3.Magnitude(player_line))) * Mathf.Rad2Deg);
            }
            else
            {
                return Mathf.Acos(Vector3.Dot(target_line, player_line) / (Vector3.Magnitude(target_line) * Vector3.Magnitude(player_line))) * Mathf.Rad2Deg;
            }
        }

        return 0;
    }
}
