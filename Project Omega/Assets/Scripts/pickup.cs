﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{

    public KeyCode itemKey = KeyCode.None;
    public KeyCode throwKey = KeyCode.None;
    public bool isCarrying = false;
    public GameObject carriedObject = null;
    public BoxCollider2D carriedCollider = null;
    public Vector3 translationOffset;
    public Vector3 rotationOffset;
    public float throwVelocity;
    public Collider2D pickupRange;
    ContactFilter2D emptyFilter = new ContactFilter2D();
    public Vector2 throwAngle;
    public Rigidbody2D ship;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //update position to keep object in hands
        if (carriedObject != null)
        {
           carriedObject.transform.position = transform.position + translationOffset;
           carriedObject.transform.rotation = Quaternion.Euler(rotationOffset) * transform.rotation;
        }

        //pickup closest opbject
        if (isCarrying == false && Input.GetKeyDown(itemKey))
        {
            Debug.Log("pickup initiated");
            Collider2D[] grabableObjects = new Collider2D[50];
            GameObject closestObject = null;
            float shortestDist = 1000;

            int length = Physics2D.OverlapCollider(pickupRange, emptyFilter, grabableObjects);

            for (int i = 0; i < length; i = i + 1)
            {
                if (grabableObjects[i].gameObject.tag == "Cargo")
                {
                    if ((grabableObjects[i].gameObject.transform.position - transform.position).magnitude < shortestDist)
                    {
                        closestObject = grabableObjects[i].gameObject;
                        shortestDist = (grabableObjects[i].gameObject.transform.position - transform.position).magnitude;
                    }
                }
            }
            if (closestObject != null)
            {
                isCarrying = true;
                carriedObject = closestObject;
                carriedCollider = carriedObject.GetComponent<BoxCollider2D>();
                //carriedCollider.enabled = !carriedCollider.enabled;
            }
            Debug.Log("pickup terminated");
        }

        //throw
        if (isCarrying == true && Input.GetKeyDown(throwKey))
        {
            Debug.Log("Throw initiated");
            carriedObject.GetComponent<Rigidbody2D>().velocity = ship.velocity;
            carriedObject.GetComponent<Rigidbody2D>().velocity += throwAngle * throwVelocity;
            //carriedCollider.enabled = !carriedCollider.enabled;
            isCarrying = false;
            carriedObject = null;
            carriedCollider = null;
        }
    }
}
