using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{
    //Controls
    public KeyCode itemKey = KeyCode.None;
    public KeyCode throwKey = KeyCode.None;

    //Holding Settings
    public Vector3 translationOffset;
    public Vector3 rotationOffset;

    //throw settings
    public float throwVelocity;
    public Vector2 throwAngle;
    //the ship (for setting relative velocity for throw)
    public Rigidbody2D ship;

    //Extra Holding Variables
    private Collider2D pickupRange;
    ContactFilter2D emptyFilter = new ContactFilter2D();
    public GameObject carriedObject = null;
    private GameObject closestObject = null;
    private GameObject previousClosestObject = null;

    //outline settings
    public Material highlight;
    public Material defaultCargoMaterial;



    void Start()
    {
        pickupRange = GetComponent<BoxCollider2D>();
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

        //Check for closest object, apply highlight
        if (carriedObject == null)
        {
            Collider2D[] grabableObjects = new Collider2D[50];
            closestObject = null;
            float shortestDist = 1000;

            //generate list of objects in the area, find the closest one.
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
            //if a closest object is found, go ahead ang highlight it. Then remove the highlight of old highlighted cargo
            if (closestObject != null)
            {
                closestObject.GetComponent<SpriteRenderer>().material = highlight;
                if(previousClosestObject != closestObject && previousClosestObject != null)
                {
                    previousClosestObject.GetComponent<SpriteRenderer>().material = defaultCargoMaterial;
                }
                previousClosestObject = closestObject;
            }
            //remove highlight if out of range
            if (closestObject == null && previousClosestObject != null)
            {
                previousClosestObject.GetComponent<SpriteRenderer>().material = defaultCargoMaterial;
            }
        }

        //Pick up closest object
        if (carriedObject == null && Input.GetKeyDown(itemKey))
        {
            if (closestObject != null)
            {
                carriedObject = closestObject;
                carriedObject.GetComponent<SpriteRenderer>().material = defaultCargoMaterial;
                carriedObject.layer = LayerMask.NameToLayer("Default");
                carriedObject.GetComponent<Collider2D>().enabled = false;
            }
        }
        //throw
        else if (carriedObject != null && Input.GetKeyDown(throwKey))
        {
            carriedObject.GetComponent<Rigidbody2D>().velocity = ship.velocity;
            carriedObject.GetComponent<Rigidbody2D>().velocity += throwAngle * throwVelocity;
            carriedObject.layer = LayerMask.NameToLayer("Item");
            carriedObject = null;
        }
        
    }
}
