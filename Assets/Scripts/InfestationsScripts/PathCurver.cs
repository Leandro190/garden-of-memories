using PathCreation;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathCurver : MonoBehaviour
{
    [SerializeField] private PathCreator pathCreator;

    Vector3 v3;
    float delta;
    Vector3 position;

    // Start is called before the first frame update
    void Start()
    {
        v3 = pathCreator.bezierPath.GetPoint(12);
        position = this.transform.position;

        delta = v3.y - position.y;

        pathCreator.bezierPath.MovePoint(12, new Vector3(v3.x, position.y + delta, v3.z));
    }
    Vector3 v3P;
    // Update is called once per frame
    void Update()
    {
        v3P = new Vector3(v3.x, this.transform.position.y + delta, v3.z);
        Debug.Log(v3P);
        pathCreator.bezierPath.MovePoint(12, v3P);

    }
}
