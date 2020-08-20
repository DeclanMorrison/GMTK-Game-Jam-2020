using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaserCannonBehavior : MonoBehaviour
{
    public bool isOnTop = true;
    public bool laserActive = false;
    public Transform firePoint;
    public LineRenderer laserLine;
    private Rigidbody2D rb;
    public float laserLength = 100;
    public float laserDamage = 5;
    public float strength = 5;
    public float moveSpeed = 10;
    LayerMask mask; //layermask for determining which layers will be affected by laser

    public float distFromScreenEdge;
    private float camTop;
    private float camBottom;
    private float camLeft;
    private float camRight;
    private Vector3 destination;
    private Camera shipCam;

    private void Start()
    {
        shipCam = GameObject.Find("ShipCamera").GetComponent<Camera>();
        Vector3 topRight = shipCam.ViewportToWorldPoint(new Vector3(1,1,0));
        camTop = topRight.y;
        camRight = topRight.x;
        Vector3 botLeft = shipCam.ViewportToWorldPoint(new Vector3(0, 0, 0));
        camBottom = botLeft.y;
        camLeft = botLeft.x;

        if(isOnTop)
        {
            destination = new Vector3(UnityEngine.Random.Range(camLeft, camRight), camTop - distFromScreenEdge, 0);
            transform.position = new Vector3(camRight + 10, camTop - distFromScreenEdge, 0);
        }
        else
        {
            transform.rotation = Quaternion.Euler(new Vector3(0, 0, 180));
            destination = new Vector3(UnityEngine.Random.Range(camLeft, camRight), camBottom + distFromScreenEdge, 0);
            transform.position = new Vector3(camRight + 10, camBottom + distFromScreenEdge, 0);
        }

        rb = GetComponent<Rigidbody2D>();
        laserLine = GetComponent<LineRenderer>();
        mask = LayerMask.GetMask("Asteroid", "Default", "Item");
        StartCoroutine(MoveShootMove());
    }
    // Update is called once per frame
    void Update()
    {
        if (laserActive)
        {
            FireLaser();
        }
        else
        {
            laserLine.SetPosition(0, Vector3.zero);
            laserLine.SetPosition(1, Vector3.zero);
        }
    }

    IEnumerator MoveShootMove()
    {
        yield return StartCoroutine(MoveToDestination());

        yield return new WaitForSeconds(1);
        laserActive = true;
        yield return new WaitForSeconds(3);
        laserActive = false;
        moveSpeed = moveSpeed * 2;
        destination.x = camLeft - 10;

        yield return StartCoroutine(MoveToDestination());
        Destroy(this.gameObject);
    }

    IEnumerator MoveToDestination()
    {
        while (Vector3.Distance(destination, transform.position) > 1)
        {
            Vector3 moveDirection = destination - transform.position;
            float distance = moveDirection.magnitude;
            moveDirection.Normalize();
            rb.velocity = moveDirection * moveSpeed * Mathf.Sqrt(distance);
            yield return null;
        }
        rb.velocity = Vector2.zero;
    }


   

    void FireLaser()
    {
        RaycastHit2D hitInfo = Physics2D.Raycast(firePoint.position, -transform.up, laserLength, mask);
        if (hitInfo)
        {
            ShipSubsystems ship = hitInfo.transform.GetComponent<ShipSubsystems>();
            if (ship)
            {
                Debug.Log("Ship hit!");
                ship.DamageOverTime(laserDamage);
            }
            Rigidbody2D hitRB = hitInfo.transform.GetComponent<Rigidbody2D>();
            if (hitRB)
            {
                hitInfo.rigidbody.AddForceAtPosition(strength * -transform.up * Time.deltaTime, hitInfo.point, ForceMode2D.Impulse);
            }
        }

        //draw laser
        laserLine.SetPosition(0, firePoint.position);
        if(hitInfo)
        {
            laserLine.SetPosition(1, hitInfo.point);
        }
        else
        {
            laserLine.SetPosition(1, -transform.up * laserLength + transform.position);
        }

    }
}
