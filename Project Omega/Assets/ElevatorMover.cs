using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ElevatorMover : MonoBehaviour
{
    public bool mouseInput;
    public Vector3 elevatorPosition;
    public float moveSpeed;
    public Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        mouseInput = Input.GetKey(KeyCode.Mouse1);

    }

    private void FixedUpdate()
    {
        if (mouseInput && transform.localPosition.y < -0.05f)
        {
            transform.localPosition = transform.localPosition + Vector3.up * moveSpeed;
        }
        else if (!mouseInput && transform.localPosition.y > -3.95f)
        {
            transform.localPosition = transform.localPosition + Vector3.down * moveSpeed;
        }

    }
}
