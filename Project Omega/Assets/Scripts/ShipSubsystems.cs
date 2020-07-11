using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipSubsystems : MonoBehaviour
{
    // Start is called before the first frame update
    public Dictionary<string, bool> subSystems = new Dictionary<string, bool>();

    public int health = 100;
    public float damageFactor = 1.5f;
    public bool fueled = true;

    public float speed = 2;
    public float brokenSpeed = 0.5f;

    public float brokenRotation = 75;
    public float rotation = 45;

    public float brokenShake = 4;
    public float normalShake = 1;

    public int turretAmmo = 0;
    public int missleAmmo = 0;

    private ShipMovement shipMovement;
    
    void Start()
    {
        shipMovement = GetComponent<ShipMovement>();
        //Add all Subsystems to Dictionary
        subSystems.Add("rightThruster", true);
        subSystems.Add("leftThruster", true);
        subSystems.Add("upThruster", true);
        subSystems.Add("downThruster", true);
        subSystems.Add("rotationLocks", true);
        subSystems.Add("vibrationDampeners", true);
        subSystems.Add("radar", true);
        subSystems.Add("turret", true);
        subSystems.Add("missles", true);
    }

    internal void Damage(float size, float collisonSpeed)
    {
        float severity = size * collisonSpeed * damageFactor;
        Debug.Log(severity);
    }

    // Update is called once per frame
    void FixedUpdate()
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
        if (!subSystems["downThruster"])
        {
            shipMovement.thrusterStrengthDown = brokenSpeed;
        }
        if (!subSystems["upThruster"])
        {
            shipMovement.thrusterStrengthUp = brokenSpeed;
        }
        if (!subSystems["leftThruster"])
        {
            shipMovement.thrusterStrengthLeft = brokenSpeed;
        }
        if (!subSystems["rightThruster"])
        {
            shipMovement.thrusterStrengthRight = brokenSpeed;
        }
        // Rotation locks broken will cause ship to rotate more
        if (subSystems["rotationLocks"])
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
        if (subSystems["vibrationDampeners"])
        {
            shipMovement.shakeStrength = normalShake;
        }
        else
        {
            shipMovement.shakeStrength = brokenShake;
        }
    }
}
