using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehavior : MonoBehaviour
{
    public float laserDamage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Ship"))
        {
            collision.gameObject.GetComponent<ShipSubsystems>().Damage(laserDamage, 1, collision.contacts);
        }
    }
}
