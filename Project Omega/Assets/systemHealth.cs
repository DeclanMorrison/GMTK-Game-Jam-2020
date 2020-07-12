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
    public int maxHealth = 3;
    public int health = 3;
    public int healAmount = 1;
    public int healMin = 3;

    public ItemType acceptedCargo;
    public RepairType repairType;
    public ShipSubSystemType systemType;
    public AudioSource audio;

    public Sprite functioningSprite;
    public Sprite brokenSprite;

    SpriteRenderer spriteRender;

    private void Start()
    {
        health = maxHealth;
        spriteRender = GetComponent<SpriteRenderer>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (repairType == RepairType.Resource)
        {
            if ((health + healAmount) <= maxHealth && other.gameObject.tag == "Cargo" && other.gameObject.GetComponent<ItemClassification>().itemType == acceptedCargo)
            {
                audio.Play();
                health += healAmount;
                CheckRepaired();
                Destroy(other.gameObject);
            }
        }
    }

    private void CheckRepaired()
    {
        if ((repairType == RepairType.Wrench && health == maxHealth) ||
            (repairType == RepairType.Resource && health >= healMin))
        {
            spriteRender.sprite = functioningSprite;
            GetComponentInParent<ShipSubsystems>().Repair(systemType);
        }
    }

    public void Damage()
    {
        spriteRender.sprite = brokenSprite;
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
