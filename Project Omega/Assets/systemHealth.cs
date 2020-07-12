using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Fuel,
    Bullets,
    Rockets,
    Sensors,
}

public enum RepairType
{
    Resource,
    Wrench
}

public class systemHealth : MonoBehaviour
{
    public int maxHealth;
    public int health;

    public ItemType acceptedCargo;
    public RepairType repairType;
    public ShipSubSystemType systemType;

    private void Start()
    {
        health = maxHealth;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (repairType == RepairType.Resource)
        {
            if (health < maxHealth && other.gameObject.tag == "Cargo" && other.gameObject.GetComponent<ItemClassification>().itemType == acceptedCargo)
            {
                health++;
                CheckRepaired();
                Destroy(other.gameObject);
            }
        }
    }

    private void CheckRepaired()
    {
        if (health == maxHealth)
        {
            GetComponentInParent<ShipSubsystems>().Repair(systemType);
        }
    }

    public void Damage()
    {
        health = 0;
    }

    public void Repair()
    {
        if (repairType == RepairType.Wrench)
        {
            health++;
            CheckRepaired();
        }
    }

}
