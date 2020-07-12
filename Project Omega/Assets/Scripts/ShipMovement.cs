using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public Camera camera;

    public float thrusterStrengthUp;
    public float thrusterStrengthRight;
    public float thrusterStrengthLeft;
    public float thrusterStrengthDown;

    public float torqueStrength;
    public float resetTorqueStrength;
    public float moveThreshold;

    public Rigidbody2D background;
    public float backgroundTorqueStrength;
    public float backgroundResetStrength;

    public float shakeStrength;
    public float minRotation = -45;
    public float maxRotation = 45;

    private Rigidbody2D rb;
    private float shakeFactor;

    public float speed;

    public float lowerBounds = -4.4f,
        upperBounds = 4.4f,
        leftBounds = -8.0f,
        rightBounds = 8.0f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
        ClampMovement();
    }

    private void MovePlayer()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 adjustedMouse = camera.ScreenToWorldPoint(mouse);
        Vector2 diff = adjustedMouse - transform.position;

        //Scale difference by thruster strength
        if (diff.x < 0)
        {
            diff.x = diff.x * thrusterStrengthLeft;
        }
        else
        {
            diff.x = diff.x * thrusterStrengthRight;
        }
        if (diff.y < 0)
        {
            diff.y = diff.y * thrusterStrengthDown;
        }
        else
        {
            diff.y = diff.y * thrusterStrengthUp;
        }
        //Apply mouse to velocity
        rb.velocity = diff + Shake();
        
        //Rotation for sudden movement
        if (diff.x < -moveThreshold)
        {
            GetComponent<ShipSubsystems>().ExpendFuel(1);
            rb.angularVelocity = Mathf.Abs(diff.x) * torqueStrength;
            background.angularVelocity = Mathf.Abs(diff.x) * backgroundTorqueStrength;
        }
        else if (diff.x > moveThreshold)
        {
            GetComponent<ShipSubsystems>().ExpendFuel(1);
            rb.angularVelocity = -Mathf.Abs(diff.x) * torqueStrength;
            background.angularVelocity = -Mathf.Abs(diff.x) * backgroundTorqueStrength;
        }
        else
        {
            //Reset own rotation
            Vector3 currentRotation = transform.localRotation.eulerAngles;
            if (currentRotation.z < 1 || currentRotation.z > 359)
            {
                rb.angularVelocity = 0;
            }
            else if (currentRotation.z > 180)
            {
                rb.angularVelocity = resetTorqueStrength;
            }
            else
            {
                rb.angularVelocity = -resetTorqueStrength;
            }
            //Reset start rotations
            Vector3 currentBackRotation = background.gameObject.transform.localRotation.eulerAngles;
            if (currentBackRotation.z < 66 && currentBackRotation.z > 64)
            {
                background.angularVelocity = 0;
            }
            else if (currentBackRotation.z > 245 || currentBackRotation.z < 65)
            {
                background.angularVelocity = backgroundResetStrength;
            }
            else
            {
                background.angularVelocity = -backgroundResetStrength;
            }
        }
    }

    private void ClampMovement()
    {
        Vector3 currentRotation = transform.localRotation.eulerAngles;

        if (currentRotation.z > maxRotation && currentRotation.z < 180 && rb.angularVelocity > 0)
        {
            rb.angularVelocity = 0;
        }
        if (currentRotation.z > 180 && currentRotation.z < 360 + minRotation && rb.angularVelocity < 0)
        {
            rb.angularVelocity = 0;
        }
        transform.localRotation = Quaternion.Euler(currentRotation);

        Vector2 clampedVelocity = rb.velocity;

        if (transform.position.x > rightBounds && rb.velocity.x > 0)
        {
            clampedVelocity.x = 0;
        }
        else if (transform.position.x < leftBounds && rb.velocity.x < 0)
        {
            clampedVelocity.x = 0;
        }

        if (transform.position.y > upperBounds && rb.velocity.y > 0)
        {
            clampedVelocity.y = 0;
        }
        else if (transform.position.y < lowerBounds && rb.velocity.y < 0)
        {
            clampedVelocity.y = 0;
        }

        rb.velocity = clampedVelocity;
    }

    Vector2 Shake()
    {
        // Shake is more severe with bottom down. Nose dive is faster, but more stable
        float shakeFactor = shakeStrength;
        Vector2 randomdir = UnityEngine.Random.insideUnitCircle;
        return randomdir * shakeFactor;
    }

    private void Move()
    {
        Vector3 mouse = Input.mousePosition;
        Vector3 adjustedMouse = Camera.main.ScreenToWorldPoint(mouse);
        Vector2 diff = adjustedMouse - transform.position;

        rb.AddTorque(diff.y * torqueStrength);

        //Scale difference by thruster strength
        if (diff.x < 0)
        {
            diff.x = diff.x * thrusterStrengthLeft;
        }
        else
        {
            diff.x = diff.x * thrusterStrengthRight;
        }
        if (diff.y < 0)
        {
            diff.y = diff.y * thrusterStrengthDown;
        }
        else
        {
            diff.y = diff.y * thrusterStrengthUp;
        }

        rb.AddForce(diff);   
    }
}
