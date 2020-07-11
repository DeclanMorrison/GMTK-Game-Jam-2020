using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMovement : MonoBehaviour
{
    public float thrusterStrengthUp;
    public float thrusterStrengthRight;
    public float thrusterStrengthLeft;
    public float thrusterStrengthDown;

    public float torqueStrength;
    public float resetTorqueStrength;
    public float moveThreshold;

    public float shakeStrength;
    public float minRotation = -45;
    public float maxRotation = 45;

    private Rigidbody2D rb;
    private float shakeFactor;

    public float speed;

    const float lowerBounds = -4.4f,
        upperBounds = 4.4f,
        leftBounds = -7.0f,
        rightBounds = 7.0f;

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
        Vector3 adjustedMouse = Camera.main.ScreenToWorldPoint(mouse);
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
            rb.angularVelocity = torqueStrength;
        }
        else if (diff.x > moveThreshold)
        {
            rb.angularVelocity = -torqueStrength;
        }
        else
        {
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
        }
    }

    private void ClampMovement()
    {
        Vector3 currentRotation = transform.localRotation.eulerAngles;
        if (currentRotation.z > maxRotation && currentRotation.z < 180)
        {
            currentRotation.z = maxRotation;
        }
        if (currentRotation.z > 180 && currentRotation.z < 360 + minRotation)
        {
            currentRotation.z = 360 + minRotation;
        }
        transform.localRotation = Quaternion.Euler(currentRotation);

        Vector2 newPosition;

        if (transform.position.x > rightBounds)
        {
            newPosition = new Vector2(rightBounds, transform.position.y);
            transform.position = newPosition;
        }
        else if (transform.position.x < leftBounds)
        {
            newPosition = new Vector2(leftBounds, transform.position.y);
            transform.position = newPosition;
        }

        if (transform.position.y > upperBounds)
        {
            newPosition = new Vector2(transform.position.x, upperBounds);
            transform.position = newPosition;
        }
        else if (transform.position.y < lowerBounds)
        {
            newPosition = new Vector2(transform.position.x, lowerBounds);
            transform.position = newPosition;
        }
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
