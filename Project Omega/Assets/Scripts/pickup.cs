using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{

    public KeyCode itemKey = KeyCode.None;
    public KeyCode throwKey = KeyCode.None;
    public GameObject carriedObject = null;
    public BoxCollider2D carriedCollider = null;
    public Vector3 translationOffset;
    public Vector3 rotationOffset;
    public float throwVelocity;
    public Collider2D pickupRange;
    ContactFilter2D emptyFilter = new ContactFilter2D();
    public Vector2 throwAngle;
    public Rigidbody2D ship;


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
        if (carriedObject == null && Input.GetKeyDown(itemKey))
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
                carriedObject = closestObject;
                carriedCollider = carriedObject.GetComponent<BoxCollider2D>();
                carriedObject.layer = LayerMask.NameToLayer("Default");
                //carriedCollider.enabled = !carriedCollider.enabled;
            }
            Debug.Log("pickup terminated");
        }

        //throw
        else if (carriedObject != null && Input.GetKeyDown(throwKey))
        {
            Debug.Log("Throw initiated");
            carriedObject.GetComponent<Rigidbody2D>().velocity = ship.velocity;
            carriedObject.GetComponent<Rigidbody2D>().velocity += throwAngle * throwVelocity;
            carriedObject.layer = LayerMask.NameToLayer("Item");
            //carriedCollider.enabled = !carriedCollider.enabled;
            carriedObject = null;
            carriedCollider = null;
        }
    }
}
