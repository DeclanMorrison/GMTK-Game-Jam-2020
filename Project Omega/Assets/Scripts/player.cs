using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    //game objects
    private Rigidbody2D rb;
    public Rigidbody2D ship;

    //horizontal movement
    float horMoveInput;
    public float moveAccel = 1;
    public float maxSpeed = 10f;
    public KeyCode sprintKey = KeyCode.None;
    public float sprintMultiplier = 2f;

    //jumping
    public KeyCode jumpKey = KeyCode.None;
    public float jumpForce = 1;
    private bool isOnGround = false;

    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horMoveInput = Input.GetAxisRaw("Horizontal"); // Gets input for left/right movement.  returns -1, 0 , 1

        //sprinting
        if (Input.GetKeyDown(sprintKey))
        {
            moveAccel = moveAccel * sprintMultiplier;
            maxSpeed = maxSpeed * sprintMultiplier;
        }
        if (Input.GetKeyUp(sprintKey))
        {
            moveAccel = moveAccel / sprintMultiplier;
            maxSpeed = maxSpeed / sprintMultiplier;
        }

        //executes jump
        if (Input.GetKeyDown(jumpKey) && isOnGround == true)
        {
            rb.velocity = rb.velocity + Vector2.up * jumpForce;
            isOnGround = false;
        }

    }

    void FixedUpdate()
    {

        //executes horizontal movement
        if (Mathf.Abs(rb.velocity.x - ship.velocity.x) < maxSpeed)
        {
            rb.velocity = rb.velocity + Vector2.right * moveAccel * horMoveInput;
        }
    }


    //check ground collisions
    void OnTriggerStay2D(Collider2D other)
    {
        if ((other.tag == "Ground" || other.tag == "Cargo") && isOnGround == false)
        {
            isOnGround = true;
        }
    }

}
