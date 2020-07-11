using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ItemType
{
    Fuel,
    Bullets,
    Rockets,
    Sensors,
}

public class systemHealth : MonoBehaviour
{
    public ItemType acceptedCargo;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Cargo" && other.gameObject.GetComponent<ItemClassification>().itemType == acceptedCargo)
        {
            Destroy(other.gameObject);
        }
    }


    public void Damage()
    {

    }


}
