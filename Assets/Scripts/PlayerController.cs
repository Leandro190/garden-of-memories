using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Speed and Force Variables
    [SerializeField] float mWalkSpeed;
    [SerializeField] float mRunSpeed;
    [SerializeField] private float turnSpeed = 10;
    [SerializeField] GameObject hose_prefab;

    [Range(0, 1f)] public float friction = 1;

    public static Vector2 movement_vector;
    private Vector2 input_vector;

    bool isWalking;
    bool is_moving;
    bool triggerEntered;

    public int allowed_to_move;

    Vector2 previousPosition;

    GameObject hose_object;
    Hose hose;
    GameObject[] obstacles;
    Transform hose_startingPoint;

    // References to other components (can be from other game objects!)
    Animator animator;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
        hose_object = null;
        hose_startingPoint = null;
        hose = hose_prefab.GetComponent<Hose>();
        obstacles = GameObject.FindGameObjectsWithTag("obstacle");
        triggerEntered = false;

        isWalking = true;

        movement_vector = Vector2.zero;
        input_vector = Vector2.zero;

        allowed_to_move = 0;

        // Get references to other components and game objects
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Animate();
        Inputs();
        HandleInput();
    }

    private void FixedUpdate()
    {
        //is_grabbing = grab_pressed;
        //grab_pressed = false;

        MoveCharacter();
    }

    private void HandleInput()
    {
        
        movement_vector.x = Input.GetAxisRaw("Horizontal");
        movement_vector.y = Input.GetAxisRaw("Vertical");

        if (hose.totalDistance <= hose.length || hose_object == null)
        {
            if (movement_vector.x != 0) { movement_vector.y = 0; }
            if (movement_vector.y != 0) { movement_vector.x = 0; }
        }
        
        // This is to limit player movement when you reach rope length limit
        else
        {
            if (hose_object != null)
            {
                if (Mathf.Abs(this.transform.position.x) - Mathf.Abs(hose.lastHitObstacle.position.x) >= Mathf.Abs(this.transform.position.y) - Mathf.Abs(hose.lastHitObstacle.position.y))
                {
                    if (this.transform.position.x <= hose.lastHitObstacle.transform.position.x)
                    {
                        if (movement_vector.x == -1) { movement_vector.x = 0; }
                        else { movement_vector.x = 1; }
                        movement_vector.y = 0;
                    }
                    else
                    {
                        if (movement_vector.x == 1) { movement_vector.x = 0; }
                        else { movement_vector.x = -1; }
                        movement_vector.y = 0;
                    }
                }

                else
                {
                    if (this.transform.position.y >= hose.lastHitObstacle.position.y)
                    {
                        movement_vector.x = 0;
                        if (movement_vector.y == 1) { movement_vector.y = 0; }
                        else { movement_vector.y = -1; }
                    }
                    else
                    {
                        movement_vector.x = 0;
                        if (movement_vector.y == -1) { movement_vector.y = 0; }
                        else { movement_vector.y = 1; }
                    }
                }
            }
        }

        is_moving = movement_vector.x != 0 || movement_vector.y != 0;

        if (Input.GetButtonDown("Run")) { isWalking = false;}
        else { isWalking = true; }

        //if (Input.GetButtonDown("Grab")) grab_pressed = true;
        //drop_pressed = Input.GetButtonDown("Drop");
    }

    //Controller for player movement
    private void MoveCharacter()
    {
        ////Determine movement speed
        float moveSpeed;

        moveSpeed = isWalking ? mWalkSpeed : mRunSpeed;

        rb.MovePosition(rb.position + movement_vector * moveSpeed * Time.deltaTime);
    }

    //Animation controller
    void Animate()
    {
        animator.SetFloat("horizontal", movement_vector.x);
        animator.SetFloat("vertical", movement_vector.y);
        animator.SetFloat("speed", movement_vector.sqrMagnitude);
    }

    void Inputs()
    {
        if (Input.GetMouseButtonDown(0) && hose_object != null)
        {
            DestroyHose();
        }
        if (Input.GetMouseButtonDown(0) && hose_object == null && triggerEntered)
        {
            triggerEntered = false;
            hose_object = Instantiate(hose_prefab) as GameObject;
            hose = hose_object.GetComponent<Hose>();
            hose.startingPoint = hose_startingPoint;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "hose" && hose_object == null)
        {
            hose_startingPoint = collision.transform;
            triggerEntered = true;
        }
        if (collision.tag == "Goal" && hose_object != null)
        {
            print("GOAL! YOU DID IT!");
            DestroyHose();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "hose")
        {
            triggerEntered = false;
        }
    }

    private void DestroyHose()
    {
        for (int i = 0; i < obstacles.Length; i++)
        {
            if (obstacles[i].layer == 0)
            {
                obstacles[i].layer = 8;
                if (obstacles[i].GetComponent<Tree>())
                {
                    obstacles[i].GetComponent<Tree>().tiedUp = false;
                }
            }
        }
        Destroy(hose_object);
    }
}
