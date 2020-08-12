using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Fuel,
    Bullets,
    Welder,
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
    private ParticleSystem sparker;

    private void Start()
    {
        health = maxHealth;
        spriteRender = GetComponent<SpriteRenderer>();
        sparker = GetComponentInChildren<ParticleSystem>();
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
            if (sparker)
            {
                sparker.Stop();
            }
        }
    }

    public void Damage()
    {
        spriteRender.sprite = brokenSprite;
        health = 0;
        if (sparker)
        {
            sparker.Play();
        }
    }

    public void Repair()
    {
        if (repairType == RepairType.Wrench)
        {
            if (health < maxHealth)
            {
                health++;
                audio.Play();
                CheckRepaired();
            }
        }
    }

}
