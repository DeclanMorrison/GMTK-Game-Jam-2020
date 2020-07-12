using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipGun : MonoBehaviour
{
    System.Random rand = new System.Random();
    public KeyCode fireKey = KeyCode.Mouse0;
    public float maxAngle = 15;
    public float rate = 0.5f;
    public bool loaded = true;
    public GameObject bulletPrefab;
    public ParticleSystem bulletSpewer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKey(fireKey) && loaded)
        {
            if (rand.NextDouble() < rate)
            {
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
                GameObject bulletStreak = Instantiate(bulletPrefab, transform.position, spawnRot, transform);
            }
        }
    }
}
