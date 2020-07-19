using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGun : MonoBehaviour
{
    System.Random rand = new System.Random();
    public KeyCode fireKey = KeyCode.Mouse0;
    public float maxAngle = 5;
    public float fireRate = 0.5f;
    public float soundRate = 0.01f;
    public bool loaded = true;
    public GameObject bulletPrefab;
    public ParticleSystem bulletSpewer;
    private AudioSource audio;
    private int i = 0;
    public float strength;
    LayerMask mask;
    public LineRenderer lineRenderer;


    // Start is called before the first frame update
    void Start()
    {
        audio = GetComponent<AudioSource>();
        mask = LayerMask.GetMask("Asteroid");
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(fireKey) && loaded)
        {
            i = i + 1;
            if (i >= 50/fireRate)
            {
                audio.Play();
                bulletSpewer.Play();
                GetComponentInParent<ShipSubsystems>().ExpendAmmo(5);
                Quaternion spawnRot;
                if (rand.NextDouble() < 0.5)
                {
                    spawnRot = transform.rotation * Quaternion.Euler(new Vector3(0, 0, (float)rand.NextDouble() * maxAngle));
                }
                else
                {
                    spawnRot = transform.rotation * Quaternion.Euler(new Vector3(0, 0, 360 - ((float) rand.NextDouble() * maxAngle)));
                }
                //GameObject bulletStreak = Instantiate(bulletPrefab, transform.position, spawnRot, transform);
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, transform.right, 100f, mask);
                if(hitInfo)
                {
                    Debug.Log(hitInfo.transform.name);
                    hitInfo.rigidbody.AddForceAtPosition(strength * Vector2.right, hitInfo.point, ForceMode2D.Impulse);
                    lineRenderer.SetPosition(0, Vector3.zero);
                    lineRenderer.SetPosition(1, hitInfo.point);
                }
                else
                {
                    lineRenderer.SetPosition(0, Vector3.zero);
                    lineRenderer.SetPosition(1, Vector3.right * 100f);
                }
                lineRenderer.enabled = true;
                i = 0;
            }
            if (i>30)
            {
                lineRenderer.enabled = false;
            }
        }
    }
}
