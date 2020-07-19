using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class cargoInSpace : MonoBehaviour
{
    public bool startsInSpace = true;

    public float startVelocity;
    public float startAngle;
    public float startTorque;

    public Vector3 offset;
    public Vector2 enterVelocity;
    
    public float scaleFactor;
    private Vector3 endScale;

    public GameObject sparkle;
    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        if (startsInSpace == true)
        {
            rb.velocity = Quaternion.AngleAxis(startAngle, Vector3.forward) * Vector3.up * startVelocity;
            rb.AddTorque(startTorque);
            EnterSpace();
        }
        if (startsInSpace == false)
        {
            Destroy(this);
        }
    }

    void Update()
    {
        sparkle.transform.rotation = Quaternion.Euler(0.0f, 0.0f, transform.rotation.z * -1.0f);
    }


    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.tag.Contains("Ship"))
        {
            Destroy(sparkle);
            transform.localScale = endScale;
            transform.position = other.gameObject.transform.position + offset;
            rb.velocity = other.gameObject.GetComponent<Rigidbody2D>().velocity + enterVelocity;
            rb.rotation = 0;
            rb.gravityScale = 4;
            Destroy(this);
        }
    }
     

    public void EnterSpace()
    {
        endScale = transform.localScale;
        transform.localScale = transform.localScale * scaleFactor;
        sparkle.transform.localScale = sparkle.transform.localScale * scaleFactor;
        rb.gravityScale = 0;
    }
}
