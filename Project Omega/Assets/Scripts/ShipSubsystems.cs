using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSubsystems : MonoBehaviour
{
    // Start is called before the first frame update
    public int health = 100;
    public bool fueled = true;

    public float speed = 2;
    public float brokenSpeed = 0.5f;
    public bool rightThruster = true;
    public bool leftThruster = true;
    public bool upThruster = true;
    public bool downThruster = true;

    public float brokenRotation = 75;
    public float rotation = 45;
    public bool rotationLocks = true;

    public float brokenShake = 4;
    public float normalShake = 1;
    public bool vibrationDampeners = true;

    public bool radar = true;
    public bool turret = true;
    public int turretAmmo = 0;

    public bool missles = true;
    public int missleAmmo = 0;

    private ShipMovement shipMovement;
    
    void Start()
    {
        shipMovement = GetComponent<ShipMovement>();
    }

    internal void Damage(float size, float collisonSpeed)
    {
        Debug.Log("Ouchie I got hit");
    }

    // Update is called once per frame
    void Update()
    {
        StateEffects();
    }

    private void StateEffects()
    {
        // If unfueled all thrusters will be zero
        if (fueled)
        {
            shipMovement.thrusterStrengthDown = speed;
            shipMovement.thrusterStrengthUp = speed;
            shipMovement.thrusterStrengthLeft = speed;
            shipMovement.thrusterStrengthRight = speed;
        }
        else
        {
            shipMovement.thrusterStrengthDown = 0;
            shipMovement.thrusterStrengthUp = 0;
            shipMovement.thrusterStrengthLeft = 0;
            shipMovement.thrusterStrengthRight = 0;
        }
        // A thruster will have broken speed if damaged
        if (!downThruster)
        {
            shipMovement.thrusterStrengthDown = brokenSpeed;
        }
        if (!upThruster)
        {
            shipMovement.thrusterStrengthUp = brokenSpeed;
        }
        if (!leftThruster)
        {
            shipMovement.thrusterStrengthLeft = brokenSpeed;
        }
        if (!rightThruster)
        {
            shipMovement.thrusterStrengthRight = brokenSpeed;
        }
        // Rotation locks broken will cause ship to rotate more
        if (rotationLocks)
        {
            shipMovement.maxRotation = rotation;
            shipMovement.minRotation = -rotation;
        }
        else
        {
            shipMovement.maxRotation = brokenRotation;
            shipMovement.minRotation = -brokenRotation;
        }
        // Vibration Dampening broken will cause ship to shake more
        if (vibrationDampeners)
        {
            shipMovement.shakeStrength = normalShake;
        }
        else
        {
            shipMovement.shakeStrength = brokenShake;
        }
    }
}
