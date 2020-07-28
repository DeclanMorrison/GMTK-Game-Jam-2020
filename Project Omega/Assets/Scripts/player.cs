using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class player : MonoBehaviour
{

    //game objects
    public GroundDetection groundDetection;
    private Rigidbody2D rb;
    public Rigidbody2D ship;
    public pickup pickup;
    private Animator animator;

    //horizontal movement
    float horMoveInput;
    public float moveAccel = 1;
    public float maxSpeed = 10f;
    public KeyCode sprintKey = KeyCode.None;
    public float sprintMultiplier = 2f;

    //jumping
    public KeyCode jumpKey = KeyCode.None;
    public float jumpForce = 1;
    bool isOnGround = false;

    //fixing
    public KeyCode fixKey = KeyCode.None;
    public GameObject wrench;


    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //fetch the ground status from ground detector
        isOnGround = groundDetection.isOnGround;

        horMoveInput = Input.GetAxisRaw("Horizontal"); // Gets input for left/right movement.  returns -1, 0 , 1

        //flip things vased on direction of move
        if (horMoveInput > 0) //move to right
        {
            transform.rotation = quaternion.Euler(0, Mathf.PI, 0);
            pickup.throwAngle.x = 1f;
            pickup.translationOffset.x = .2f;
        }
        else if (horMoveInput < 0) //move to left
        {
            transform.rotation = quaternion.Euler(0, 0, 0);
            pickup.throwAngle.x = -1f;
            pickup.translationOffset.x = -.2f;
        }   

        //sprinting
        if (Input.GetKeyDown(sprintKey))
        {
            moveAccel = moveAccel * sprintMultiplier;
            maxSpeed = maxSpeed * sprintMultiplier;
            animator.SetBool("isSprinting", true);
        }
        if (Input.GetKeyUp(sprintKey))
        {
            moveAccel = moveAccel / sprintMultiplier;
            maxSpeed = maxSpeed / sprintMultiplier;
            animator.SetBool("isSprinting", false);
        }

        //executes jump
        if (Input.GetKeyDown(jumpKey) && isOnGround == true)
        {
            rb.velocity = rb.velocity + Vector2.up * jumpForce;
            isOnGround = false;
            GetComponent<Animator>().Play("Player_jump");
        }

        //update move animation
        if (horMoveInput != 0 && isOnGround == true)
        {
            animator.SetBool("isWalking", true);
        }
        else
        {
            animator.SetBool("isWalking", false);
        }

        //swinging wrench
        if(Input.GetKeyDown(fixKey) && pickup.carriedObject == null)
        {
            animator.SetBool("isWalking", true);
            wrench.SetActive(true);
            wrench.GetComponent<WrenchBehavior>().Swing();
        }
    }

    void FixedUpdate()
    {
        //executes horizontal movement
        if (Mathf.Abs(rb.velocity.x - ship.velocity.x) < maxSpeed && isOnGround == true)
        {
            rb.velocity = rb.velocity + Vector2.right * moveAccel * horMoveInput;
        }
    }
}
