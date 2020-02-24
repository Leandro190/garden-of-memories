using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Insect : MonoBehaviour
{
    float speed;
    [SerializeField] private Animator InsectAnimator;
    [SerializeField] private Collider2D InNest;
    [SerializeField] private GameObject OutNest;

    // Start is called before the first frame update
    void Start()
    {
        speed = InsectAnimator.speed;
    }

    // Update is called once per frame
    void Update()
    {
        speed = InsectAnimator.speed;
        this.transform.position = new Vector3(transform.position.x, this.transform.position.y + Time.deltaTime * speed, transform.position.z);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "InNest")
        {
            print("InNest");
            this.transform.position = new Vector3(transform.position.x, OutNest.transform.position.y, transform.position.z);
        }
    }

}
