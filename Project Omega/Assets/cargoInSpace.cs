using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class cargoInSpace : MonoBehaviour
{

    public float startVelocity;
    public float startAngle;
    public float startTorque;

    public Vector3 offset;
    public Vector2 enterVelocity;
    
    public float scaleFactor;

    Rigidbody2D rb;

    // Start is called before the first frame update
    void Start()
    {

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Quaternion.AngleAxis(startAngle, Vector3.forward) * Vector3.up * startVelocity;
        rb.AddTorque(startTorque);
        EnterSpace();
    }

    // Update is called once per frame
    private void OnCollisionEnter2D(Collision2D other)
    {
        Debug.Log("Hit Detetectd");
        if (other.gameObject.tag.Contains("Ship"))
        {
            Debug.Log("ship hit");
            Destroy(GetComponent<ParticleSystem>());
            transform.localScale = transform.localScale / scaleFactor;
            transform.position = other.gameObject.transform.position + offset;
            rb.velocity = other.gameObject.GetComponent<Rigidbody2D>().velocity + enterVelocity;
            rb.rotation = 0;
            rb.gravityScale = 4;
            Destroy(this);
        }
    }
     

    public void EnterSpace()
    {
        transform.localScale = transform.localScale * scaleFactor;
        rb.gravityScale = 0;
    }
}
