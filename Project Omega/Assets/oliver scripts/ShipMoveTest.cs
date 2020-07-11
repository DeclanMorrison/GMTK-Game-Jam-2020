using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipMoveTest : MonoBehaviour
{
    public float moveSpeed = 1f;
    public Rigidbody2D rb;

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            rb.velocity = Vector2.down * moveSpeed;
        }

    }
}
