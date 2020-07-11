using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowTarget : MonoBehaviour
{
    public GameObject target;
    public float scaling;

    private Rigidbody2D rb;
    private Rigidbody2D targetRb;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        targetRb = target.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        FollowTarget();
    }

    private void FollowTarget()
    {
        Vector3 toTarget = target.transform.position - transform.position;
        toTarget.z = 0;
        transform.position = transform.position + toTarget * scaling;
    }
}
