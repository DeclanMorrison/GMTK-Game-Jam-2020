using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WrenchBehavior : MonoBehaviour
{
    public float time;
    public float speed;

    Rigidbody2D rb;
    Quaternion initialRot;

    internal bool left;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialRot = transform.rotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (left)
        {
            transform.Rotate(Vector3.forward, -Time.deltaTime * speed);
        }
        else
        {
            transform.Rotate(Vector3.forward, Time.deltaTime * speed);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag.Contains("Subsystem"))
        {
            collision.gameObject.GetComponent<systemHealth>().Repair();
        }
    }

    internal void Swing()
    {
        transform.rotation = initialRot;
        StartCoroutine(EndSwing());
    }

    private IEnumerator EndSwing()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
