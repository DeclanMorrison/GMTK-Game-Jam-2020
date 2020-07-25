using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Mathematics;

public class WrenchBehavior : MonoBehaviour
{
    public float time;
    public float speed;

    Rigidbody2D rb;
    Quaternion initialRot;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        initialRot = transform.localRotation;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        transform.localRotation = transform.localRotation * Quaternion.Euler(0, 0, speed);
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
        transform.localRotation = initialRot;
        StartCoroutine(EndSwing());
    }

    private IEnumerator EndSwing()
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }
}
