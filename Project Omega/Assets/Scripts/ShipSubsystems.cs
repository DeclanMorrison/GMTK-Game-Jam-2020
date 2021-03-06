﻿using System;
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
    Fuel,
    Ammo
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

    public AlertDisplay alertDisplay;
    private DamageSystem damageSystem;

    public float health = 100;
    public float damageFactor = 1.5f;

    public float speed = 2;
    public float brokenSpeed = 0.5f;

    public float brokenTorque = 75;
    public float torque = 45;

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

    private ShipMovement shipMovement;
    
    void Start()
    {
        shipMovement = GetComponent<ShipMovement>();
        damageSystem = GetComponentInChildren<DamageSystem>();
    }

    internal void ImpactDamage(float size, float collisonSpeed, ContactPoint2D[] contacts)
    {
        float severity = size * collisonSpeed * damageFactor;
        
        if (severity > 50)
        {
            Damage(severity / 10);
            DamageSubsystem();
        }
        else
        {
            Damage(severity / 10);
        }

        damageSystem.SpawnVisualDamage(health, contacts);
    }

    internal void DamageOverTime(float dps)
    {
        Damage(dps * Time.deltaTime);
    }

    internal void Damage(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            damageSystem.StartGameOver();
        }
    }

    internal void DamageSubsystem()
    {
        Array values = Enum.GetValues(typeof(ShipSubSystemType));
        System.Random rand = new System.Random();

        int randomSystem = rand.Next(values.Length - 2);
        if (subSystems[randomSystem].status)
        {
            subSystems[randomSystem].status = false;
            subSystems[randomSystem].healthScript.Damage();
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        StateEffects();
    }

    private void StateEffects()
    {
        shipMovement.thrusterStrengthDown = speed;
        shipMovement.thrusterStrengthUp = speed;
        shipMovement.thrusterStrengthLeft = speed;
        shipMovement.thrusterStrengthRight = speed;

        foreach (SubSystemClass subSystem in subSystems)
        {
            // If unfueled all thrusters will be zero
            if (subSystem.type == ShipSubSystemType.Fuel && !subSystem.status)
            {
                shipMovement.thrusterStrengthDown = 0;
                shipMovement.thrusterStrengthUp = 0;
                shipMovement.thrusterStrengthLeft = 0;
                shipMovement.thrusterStrengthRight = 0;
            }
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
                shipMovement.torqueStrength = torque;
            }
            else if (subSystem.type == ShipSubSystemType.RotationLocks && !subSystem.status)
            {
                shipMovement.torqueStrength = brokenShake;  
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
            //Can't fire if out of ammo
            if (subSystem.type == ShipSubSystemType.Ammo && subSystem.status)
            {
                GetComponentInChildren<ShipGun>().loaded = true;
            }
            else
            {
                GetComponentInChildren<ShipGun>().loaded = false;
            }
            //Can't see alerts if no radar
            if (subSystem.type == ShipSubSystemType.Radar && subSystem.status)
            {
                alertDisplay.isWorking = true;
            }
            else if (subSystem.type == ShipSubSystemType.Radar && !subSystem.status)
            {
                alertDisplay.isWorking = false;
            }
        }
    }

    internal void ExpendFuel(int amount)
    {
        foreach (SubSystemClass subSystem in subSystems)
        {
            if (subSystem.type == ShipSubSystemType.Fuel && subSystem.healthScript.health >= amount)
            {
                subSystem.healthScript.health -= amount;
                if (subSystem.healthScript.health == 0)
                {
                    subSystem.healthScript.Damage();
                    subSystem.status = false;
                }
            }
        }
    }

    internal void ExpendAmmo(int amount)
    {
        foreach (SubSystemClass subSystem in subSystems)
        {
            if (subSystem.type == ShipSubSystemType.Ammo && subSystem.healthScript.health >= amount)
            {
                subSystem.healthScript.health -= amount;
                if (subSystem.healthScript.health == 0)
                {
                    subSystem.healthScript.Damage();
                    subSystem.status = false;
                }
            }
        }
    }
}
