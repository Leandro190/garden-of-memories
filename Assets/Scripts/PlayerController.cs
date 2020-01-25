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
    Animator mAnimator;
    Rigidbody2D rigidBody2D;

    // Start is called before the first frame update
    void Awake()
    {
        isWalking = true;

        movement_vector = Vector2.zero;
        input_vector = Vector2.zero;

        allowed_to_move = 0;

        // Get references to other components and game objects
        mAnimator = GetComponent<Animator>();
        rigidBody2D = GetComponent<Rigidbody2D>();
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

        FaceDirection();
        if (allowed_to_move <= 0)
            MoveCharacter();


    }

    private void HandleInput()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        is_moving = horizontal != 0 || vertical != 0;

        if (Input.GetButtonDown("Run"))
        {
            isWalking = false;
        }
        else
        {
            isWalking = true;
        }

        //if (Input.GetButtonDown("Grab")) grab_pressed = true;
        //drop_pressed = Input.GetButtonDown("Drop");

        input_vector = new Vector2(horizontal, vertical);
    }

    private void FaceDirection()
    {
        if (is_moving)
            this.transform.rotation = Quaternion.Lerp(this.transform.rotation, Quaternion.LookRotation(Vector3.back, input_vector), turnSpeed * Time.fixedDeltaTime);
    }

    //Controller for player movement
    private void MoveCharacter()
    {
        ////Determine movement speed
        float moveSpeed;

        moveSpeed = isWalking ? mWalkSpeed : mRunSpeed;

        if (friction >= 1)
            movement_vector = Vector2.Lerp(movement_vector, input_vector, 0.125f);
        else
            movement_vector = Vector2.Lerp(movement_vector, input_vector, friction * Time.deltaTime);

        rigidBody2D.velocity = (movement_vector * moveSpeed * Time.deltaTime);
    }

    //Animation controller
    void Animate()
    {
        //if (!mGrounded)
        //{
        //    mSwimming = true;
        //}
        //else
        //{
        //    mSwimming = false;
        //}
        //if (mRigidBody2D.velocity.y < -2.0f)
        //{
        //    mFalling = true;
        //}
        //else
        //{
        //    mFalling = false;
        //}

        //mAnimator.SetBool("mSwimming", mSwimming);
        //mAnimator.SetBool("mMoving", mMoving);
        //mAnimator.SetBool("mFalling", mFalling);
        //mAnimator.SetBool("mHurt", mHurt);
    }

}
