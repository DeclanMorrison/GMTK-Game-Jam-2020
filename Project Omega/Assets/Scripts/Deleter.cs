using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deleter : MonoBehaviour
{
    public GameObject ship;
    public Vector3 respawnOffset;
    private void OnTriggerEnter2D(Collider2D other)
    {
        
            Destroy(other.gameObject);
       
    }
}
