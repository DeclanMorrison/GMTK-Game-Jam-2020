using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pickup : MonoBehaviour
{

    public KeyCode itemKey = KeyCode.None;
    public bool isCarrying = false;
    public GameObject carriedObject;
    public BoxCollider2D carriedCollider;
    public Vector3 translationOffset;
    public Vector3 rotationOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        carriedObject.transform.position = transform.position + translationOffset;
        carriedObject.transform.rotation = Quaternion.Euler(rotationOffset) * transform.rotation;
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Cargo" && isCarrying == false && Input.GetKeyDown(itemKey))
        {
            isCarrying = true;
            carriedObject = other.gameObject;
            carriedCollider = carriedObject.GetComponent<BoxCollider2D>();
            carriedCollider.enabled = !carriedCollider.enabled;
        }
    }
}
