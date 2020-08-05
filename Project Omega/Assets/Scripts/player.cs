using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class player : MonoBehaviour
{

    //Engineer Controlls (Controller)
    EngineerControls controls;

    //game objects
    public GroundDetection groundDetection;
    private Rigidbody2D rb;
    public Rigidbody2D ship;
    private Animator animator;

    //horizontal movement
    float horMoveInput;
    public float moveAccel = 1;
    public float maxSpeed = 10f;
    public KeyCode sprintKey = KeyCode.None;
    public float sprintMultiplier = 2f;

    //jumping
    public float jumpForce = 1;
    bool isOnGround = false;

    //fixing
    public KeyCode fixKey = KeyCode.None;
    public GameObject wrench;

    //Stabilization
    public float stabilizationFactor; //amount to stabilize by. between 0 and 1
    private Vector2 lastShipVelocity; //for tracking changes in ship velocity

    //arms
    public Transform backArm;
    public Transform frontArm;
    public Vector2 armsInput;
    private float armAngleCorrection;

    //Head
    public Transform head;


    //Holding Settings
    public Vector3 translationOffset;
    public Vector3 rotationOffset;

    //throw settings
    public float throwVelocity;
    public Vector2 throwAngle;

    //Extra Holding Variables
    public GameObject pickupPoint;
    private Collider2D pickupRange;
    ContactFilter2D emptyFilter = new ContactFilter2D();
    public GameObject carriedObject = null;
    private GameObject closestObject = null;
    private GameObject previousClosestObject = null;

    //outline settings
    public Material highlight;
    public Material defaultCargoMaterial;

    void Awake()
    {
        controls = new EngineerControls();
        controls.Gameplay.Jump1.performed += ctx => Jump();
        controls.Gameplay.Arms1.performed += ctx => armsInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.PickupThrow.performed += ctx => PickupThrow();
    }

    void Start()
    {
        pickupRange = pickupPoint.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    void Jump()
    {
        //executes jump
        if (isOnGround == true)
        {
            rb.velocity = rb.velocity + Vector2.up * jumpForce;
            isOnGround = false;
            //GetComponent<Animator>().Play("Player_jump");
        }
    }

    private void OnEnable()
    {
        controls.Gameplay.Enable();
    }

    private void OnDisable()
    {
        controls.Gameplay.Disable();
    }

    // Update is called once per frame
    void Update()
    {
        //fetch the ground status from ground detector
        isOnGround = groundDetection.isOnGround;

        horMoveInput = Input.GetAxisRaw("Horizontal"); // Gets input for left/right movement.  returns -1, 0 , 1

        //calculate rotation of Arms based on input
        float armsAngle = Mathf.Atan2(armsInput.y, armsInput.x) * Mathf.Rad2Deg;

        //flip things vased on direction of move
        if (horMoveInput > 0) //move to right
        {
            head.rotation = Quaternion.Euler(0, 180, 0);
        }
        else if (horMoveInput < 0) //move to left
        {
            head.rotation = Quaternion.Euler(0, 0, 0);
        }
        else //not moving
        {
            head.localRotation = Quaternion.Euler(0, 0, 0);
        }

        //flip things based on directon of arms
        if (armsInput.x >= 0) //arms to right
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            armAngleCorrection = 0;
            armsAngle = armsAngle * -1;
        }
        else if (armsInput.x < 0) //arms to left
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            armsAngle = armsAngle + 180;
        }

        //roate arms
        backArm.localRotation = Quaternion.Euler(0, 0, armsAngle);
        frontArm.localRotation = Quaternion.Euler(0, 0, armsAngle);

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
        if(Input.GetKeyDown(fixKey) && carriedObject == null)
        {
            animator.SetBool("isWalking", true);
            wrench.SetActive(true);
            wrench.GetComponent<WrenchBehavior>().Swing();
        }

        Stabilize();

        //update position to keep object in hands
        if (carriedObject != null)
        {
            carriedObject.transform.position = pickupPoint.transform.position + translationOffset;
            carriedObject.transform.rotation = Quaternion.Euler(rotationOffset) * pickupPoint.transform.rotation;
        }

        //Check for closest object, apply highlight
        if (carriedObject == null)
        {
            Collider2D[] grabableObjects = new Collider2D[50];
            closestObject = null;
            float shortestDist = 1000;

            //generate list of objects in the area, find the closest one.
            int length = Physics2D.OverlapCollider(pickupRange, emptyFilter, grabableObjects);

            for (int i = 0; i < length; i = i + 1)
            {
                if (grabableObjects[i].gameObject.tag == "Cargo")
                {
                    if ((grabableObjects[i].gameObject.transform.position - transform.position).magnitude < shortestDist)
                    {
                        closestObject = grabableObjects[i].gameObject;
                        shortestDist = (grabableObjects[i].gameObject.transform.position - transform.position).magnitude;
                    }
                }
            }
            //if a closest object is found, go ahead ang highlight it. Then remove the highlight of old highlighted cargo
            if (closestObject != null)
            {
                closestObject.GetComponent<SpriteRenderer>().material = highlight;
                if (previousClosestObject != closestObject && previousClosestObject != null)
                {
                    previousClosestObject.GetComponent<SpriteRenderer>().material = defaultCargoMaterial;
                }
                previousClosestObject = closestObject;
            }
            //remove highlight if out of range
            if (closestObject == null && previousClosestObject != null)
            {
                previousClosestObject.GetComponent<SpriteRenderer>().material = defaultCargoMaterial;
            }
        }

    }

    private void PickupThrow()
    {
        //Pick up closest object
        if (!carriedObject && closestObject)
        {
            Debug.Log("Pickup");
            carriedObject = closestObject;
            carriedObject.GetComponent<SpriteRenderer>().material = defaultCargoMaterial;
            carriedObject.layer = LayerMask.NameToLayer("Player");

        }
        //throw
        else if (carriedObject)
        {
            Debug.Log("Drop");
            carriedObject.GetComponent<Rigidbody2D>().velocity = ship.velocity;
            carriedObject.GetComponent<Rigidbody2D>().velocity += throwAngle * throwVelocity;
            carriedObject.layer = LayerMask.NameToLayer("Item");
            carriedObject = null;
        }
    }

    private void Stabilize() 
    {
        //stabize player (give them a part of the ship's velocity)
        if (lastShipVelocity != ship.GetComponent<Rigidbody2D>().velocity)
        {
            Vector2 shipChangeInVelocity = ship.GetComponent<Rigidbody2D>().velocity - lastShipVelocity;
            rb.velocity += shipChangeInVelocity * stabilizationFactor;
        }

        //update last ship velocity
        lastShipVelocity = ship.GetComponent<Rigidbody2D>().velocity;
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
