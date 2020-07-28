using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundDetection : MonoBehaviour
{
    ContactFilter2D emptyFilter = new ContactFilter2D();
    private Collider2D detectorRange;
    public bool isOnGround = false;

    void Start()
    {
        detectorRange = GetComponent<BoxCollider2D>();
    }

    //check ground collisions
    void Update()
    {
        Collider2D[] standingObjects = new Collider2D[50];
        bool groundDetected = false;

        int length = Physics2D.OverlapCollider(detectorRange, emptyFilter, standingObjects);

        for (int i = 0; i < length; i = i + 1)
        {
            if (standingObjects[i].gameObject.tag == "Cargo" || standingObjects[i].gameObject.tag == "Ground")
            {
                groundDetected = true;
                break;
            }
        }

        if(groundDetected)
        {
            isOnGround = true;
        }
        else
        {
            isOnGround = false;
        }

    }
}
