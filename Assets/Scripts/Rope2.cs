using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rope2 : MonoBehaviour
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

        transform.position = pointA.position;
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
        SetRopePosition(0, player.transform.position);
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
        Vector3 lastTurn;
        lastTurn = (hits.Count == 0) ? pointA.position : hits[obstacles - 2].transform.position;

        hit = Physics2D.Linecast(lastTurn, player.transform.position);

        Debug.DrawLine(lastTurn, player.transform.position);

        if (hit && !hits.Contains(hit.transform))
        {
            hits.Add(hit.transform);
            hit.collider.enabled = false;

            SetRopePosition(1, hits[obstacles - 1].transform.position);

            for (int i = 0; i < (obstacles - 1); i++)
            {
                SetRopePosition(obstacles - i, hits[i].transform.position);
            }

            obstacles++;

        }
    }
    private void SetRopePosition(int index, Vector2 position)
    {
        rope.SetPosition(index, new Vector2(position.x - pointA.position.x, position.y - pointA.position.y));
    }

    public int GetObstacles() { return obstacles; }
}
