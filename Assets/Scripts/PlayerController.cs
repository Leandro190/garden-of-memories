using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    //Speed and Force Variables
    [SerializeField] float mWalkSpeed;
    [SerializeField] float mRunSpeed;
    [SerializeField] private float turnSpeed = 10;

    [Range(0, 1f)] public float friction = 1;

    private Vector2 movement_vector;
    private Vector2 input_vector;

    bool isWalking;
    bool is_moving;

    public int allowed_to_move;

    // References to other components (can be from other game objects!)
    Animator animator;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Awake()
    {
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
        HandleInput();
        Animate();
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

}
