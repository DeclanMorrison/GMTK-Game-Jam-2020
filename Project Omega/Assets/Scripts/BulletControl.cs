using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletControl : MonoBehaviour
{
    public float strength;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Contains("Asteroid"))
        {
            Vector2 pushDirection = transform.rotation * Vector3.right;
            collision.GetComponent<Rigidbody2D>().AddForce(strength * Vector2.right, ForceMode2D.Impulse);
        }
    }
}
