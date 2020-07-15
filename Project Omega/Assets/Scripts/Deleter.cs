using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Deleter : MonoBehaviour
{
    public GameObject ship;
    public Vector3 respawnOffset;
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "player" || other.name == "pick-up" || other.name == "groundDetector")
        {
            other.gameObject.transform.position = ship.transform.position + respawnOffset;
        }
        else
        {
            Destroy(other.gameObject);
        }
    }
}
