using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    public float startVelocity;
    public float startAngle;
    public float startTorque;
    public float startSize;

    private Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Quaternion.AngleAxis(startAngle, Vector3.forward) * Vector3.up * startVelocity;
        rb.AddTorque(startTorque);
        transform.localScale = Vector3.one * startSize;
        rb.mass = startSize * 40000;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Ship"))
        {
            collision.gameObject.GetComponent<ShipSubsystems>().Damage(startSize, collision.relativeVelocity.magnitude);
        }
    }
}
