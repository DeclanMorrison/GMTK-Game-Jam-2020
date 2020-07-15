using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drifter : MonoBehaviour
{
    System.Random rand = new System.Random();
    Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.AddTorque(200);
        rb.AddForce(new Vector2(200,-100));
        //rb.AddTorque((float)rand.Next(100,1000));
        //  (new Vector2((float)rand.NextDouble() - .5f, (float)rand.NextDouble() - .5f));
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
