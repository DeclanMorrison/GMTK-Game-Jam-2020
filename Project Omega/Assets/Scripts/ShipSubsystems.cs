using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ShipSubSystemType
{
    RightThruster,
    LeftThruster,
    UpThruster,
    DownThruster,
    RotationLocks,
    VibrationDampeners,
    Radar,
    Turret,
    Missles
}

[Serializable]
public class SubSystemClass
{
    public ShipSubSystemType type;
    public bool status;
    public systemHealth healthScript;
}

public class ShipSubsystems : MonoBehaviour
{
    // Start is called before the first frame update
    public List<SubSystemClass> subSystems = new List<SubSystemClass>();

    public int health = 100;
    public float damageFactor = 1.5f;
    public bool fueled = true;

    public float speed = 2;
    public float brokenSpeed = 0.5f;

    public float brokenRotation = 75;
    public float rotation = 45;

    public float brokenShake = 4;
    public float normalShake = 1;

    internal void Repair(ShipSubSystemType systemType)
    {
        foreach (SubSystemClass system in subSystems)
        {
            if (system.type == systemType)
            {
                system.status = true;
            }
        }
    }

    public int turretAmmo = 0;
    public int missleAmmo = 0;

    private ShipMovement shipMovement;
    
    void Start()
    {
        shipMovement = GetComponent<ShipMovement>();
    }

    internal void Damage(float size, float collisonSpeed)
    {
        float severity = size * collisonSpeed * damageFactor;
        if (severity > 50)
        {
            health -= (int) severity / 10;

            Array values = Enum.GetValues(typeof(ShipSubSystemType));
            System.Random rand = new System.Random();
 
            int randomSystem = rand.Next(values.Length);
            if (subSystems[randomSystem].status)
            {
                subSystems[randomSystem].status = false;
                subSystems[randomSystem].healthScript.Damage();
            }
        }
        else
        {
            health -= (int) (severity / 10);
        }
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

        foreach (SubSystemClass subSystem in subSystems)
        {
            // A thruster will have broken speed if damaged
            if (subSystem.type == ShipSubSystemType.DownThruster && !subSystem.status)
            {
                shipMovement.thrusterStrengthDown = brokenSpeed;
            }
            if (subSystem.type == ShipSubSystemType.UpThruster && !subSystem.status)
            {
                shipMovement.thrusterStrengthUp = brokenSpeed;
            }
            if (subSystem.type == ShipSubSystemType.LeftThruster && !subSystem.status)
            {
                shipMovement.thrusterStrengthLeft = brokenSpeed;
            }
            if (subSystem.type == ShipSubSystemType.RightThruster && !subSystem.status)
            {
                shipMovement.thrusterStrengthRight = brokenSpeed;
            }
            // Rotation locks broken will cause ship to rotate more
            if (subSystem.type == ShipSubSystemType.RotationLocks && subSystem.status)
            {
                shipMovement.maxRotation = rotation;
                shipMovement.minRotation = -rotation;
            }
            else if (subSystem.type == ShipSubSystemType.RotationLocks && !subSystem.status)
            {
                shipMovement.maxRotation = brokenRotation;
                shipMovement.minRotation = -brokenRotation;
            }
            // Vibration Dampening broken will cause ship to shake more
            if (subSystem.type == ShipSubSystemType.VibrationDampeners && subSystem.status)
            {
                shipMovement.shakeStrength = normalShake;
            }
            else if (subSystem.type == ShipSubSystemType.VibrationDampeners && !subSystem.status)
            {
                shipMovement.shakeStrength = brokenShake;
            }
        }
    }
}
