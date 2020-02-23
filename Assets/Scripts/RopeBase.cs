using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RopeBase : MonoBehaviour
{
    [SerializeField] GameObject startRope;
    [SerializeField] GameObject rope;
    [SerializeField] GameObject player;

    Transform rotatingTip;
    Transform playerTransform;
    Rope2 ropeComponent;

    // Start is called before the first frame update
    void Start()
    {
        rotatingTip = startRope.GetComponent<Transform>();
        ropeComponent = rope.GetComponent<Rope2>();
        playerTransform = player.GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if(ropeComponent.GetObstacles() == 1)
        {
            Vector3 vectorToTarget = playerTransform.position - rotatingTip.position;
            float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg + 90;
            rotatingTip.rotation = Quaternion.AngleAxis(angle, Vector3.forward);


            //rotatingTip.rotation = Quaternion.Euler(0, 0, Mathf.Atan2((playerTransform.position.y - rotatingTip.position.y),(playerTransform.position.x - rotatingTip.position.x)));
            //print(Mathf.Atan2((playerTransform.position.y - rotatingTip.position.y),(playerTransform.position.x - rotatingTip.position.x)));

        }
    }
}
