using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Rendering;

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

    private Vector2 moveInput;

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
    private float desiredArmAngle;
    public float armSwingSmooth;
    private float lastArmAngle;

    //Head
    public Transform head;

    public enum BodyDirection {Left,Right}
    public BodyDirection bodyDirection = BodyDirection.Left;

    //Holding Settings
    public Vector3 translationOffset;
    public Vector3 rotationOffset;

    //throw settings
    public float throwVelocity;
    public Vector2 throwAngle;
    public GameObject thrownObject;
    private float throwHoldSeconds;
    public float maxThrowHold = 3;
    private bool isThrowing = false;

    //Extra Holding Variables
    public GameObject pickupPoint;
    private Collider2D pickupRange;
    private ContactFilter2D emptyFilter = new ContactFilter2D();
    public GameObject carriedObject = null;
    private GameObject closestObject = null;
    private GameObject previousClosestObject = null;

    //outline settings
    public Material highlight;
    public Material defaultCargoMaterial;


    public Vector2 throwDirection;
    void Awake()
    {
        controls = new EngineerControls();
        controls.Gameplay.Jump1.performed += ctx => Jump();
        controls.Gameplay.Arms1.performed += ctx => armsInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Arms1.canceled += ctx => armsInput = Vector2.zero;
        controls.Gameplay.PickupThrow.performed += ctx => PickupThrowPress();
        controls.Gameplay.PickupThrow.canceled += ctx => PickupThrowRelease();

        controls.Gameplay.Movement.performed += ctx => moveInput = ctx.ReadValue<Vector2>();
        controls.Gameplay.Movement.canceled += ctx => moveInput = Vector2.zero;
    }

    void Start()
    {
        pickupRange = pickupPoint.GetComponent<BoxCollider2D>();
        rb = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
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


        if (armsInput == Vector2.zero) //default turning settings(when player is not overridding the hand locations)
        {
            if(moveInput.x > 0)//moving right
            {
                bodyDirection = BodyDirection.Right;
                desiredArmAngle = 160;
            }
            else if (moveInput.x < 0) //moving left
            {
                bodyDirection = BodyDirection.Left;
                desiredArmAngle = 20;
            }
        }
        else //arm-based turn settings (when player is overridding arm position)
        {
            desiredArmAngle = Mathf.Atan2(armsInput.y, armsInput.x) * Mathf.Rad2Deg + 180;

            if(armsInput.x > 0) //arms right
            {
                bodyDirection = BodyDirection.Right;
            }
            else if (armsInput.x < 0) //arms left
            {
                bodyDirection = BodyDirection.Left;
            }


            if (moveInput.x > 0)//moving right
            {
                head.rotation = Quaternion.Euler(0, 180, 0);
            }
            else if (moveInput.x < 0)//moving left
            {
                head.rotation = Quaternion.Euler(0, 0, 0);
            }
            else
            {
                head.localRotation = Quaternion.Euler(0, 0, 0);
            }
        }

        //roate arms
        float armsAngle = Mathf.LerpAngle(lastArmAngle, desiredArmAngle, armSwingSmooth);
        lastArmAngle = armsAngle;
        if (bodyDirection == BodyDirection.Right)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            armsAngle = (armsAngle * -1) + 180;
        }
        else if(bodyDirection == BodyDirection.Left)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }


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

        PickupScan();

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
    private void PickupScan()
    {
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

    private void PickupThrowPress()
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
            StartCoroutine(ThrowHold());
            isThrowing = true;
        }
    }

    IEnumerator ThrowHold()
    {
        for (throwHoldSeconds = 0; throwHoldSeconds < maxThrowHold; throwHoldSeconds+= Time.deltaTime)
        {
            yield return null;
        }
    }

    private void PickupThrowRelease()
    {
        if(isThrowing)
        {
            Debug.Log("Drop");
            carriedObject.GetComponent<Rigidbody2D>().velocity = ship.velocity;
            carriedObject.GetComponent<Rigidbody2D>().angularVelocity = 0;
            throwDirection = pickupPoint.GetComponent<Transform>().right; //right from the hands
            throwDirection *= -1f;  //flip for left
            throwDirection.y = throwDirection.y + .8f;  //add some upward
            throwDirection.Normalize(); //make unit vector
            carriedObject.GetComponent<Rigidbody2D>().velocity += throwDirection * throwVelocity * throwHoldSeconds/maxThrowHold;
            thrownObject = carriedObject;
            Invoke("ReactivateCollider", .3f);
            carriedObject = null;
            isThrowing = false;
        }
    }

    private void ReactivateCollider() //reactivates the item to collide with the player
    {
        thrownObject.layer = LayerMask.NameToLayer("Item");
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
            rb.velocity = rb.velocity + Vector2.right * moveAccel * moveInput.x;
        }

    }
}
