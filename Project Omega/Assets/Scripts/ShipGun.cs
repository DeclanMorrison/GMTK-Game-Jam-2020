using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;

public class ShipGun : MonoBehaviour
{
    System.Random rand = new System.Random();

    public KeyCode fireKey = KeyCode.Mouse0;
    public float maxAngle = 5; //total cone of fire in degrees
    public float fireRate = 0.5f; //shots per second
    public float range = 100f; //range in meters
    public float rangeNoise = .10f; //range noise in percent of total range
    public float strength; //pushing force of bullets

    public bool loaded = true;
    public GameObject bulletPrefab;
    public ParticleSystem bulletSpewer;
    public GameObject bulletHitEffect;
    private AudioSource audio;
    private int i = 0; //iterator for tracking firerate
    LayerMask mask; //layermask for determining which layers will be affected by guns (important that ship is not on this layer)


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
            if (i >= 50/fireRate) //shot is fired
            {
                //add effects
                audio.Play();
                bulletSpewer.Play();

                GetComponentInParent<ShipSubsystems>().ExpendAmmo(5);

                //generate the actual range of the guns (with some slight variance to make gun look more wild)
                //(at the moment, range noise only affects missed bullets, meaning it's only cosmetic. Could change)
                float noisyRange = range + range * (((float)rand.NextDouble() * rangeNoise * 2) - rangeNoise);

                //generate a random angle within the arc (in degrees)
                float fireAngle = ((float)rand.NextDouble() * maxAngle) - (maxAngle / 2);
                //convert angle to quaternion
                Quaternion fireAngleQuat = transform.rotation * Quaternion.Euler(new Vector3(0, 0, fireAngle));
                //Convert angle to vector2
                Vector2 fireAngleVec2 = (Vector2)(Quaternion.Euler(0, 0, fireAngle) * transform.right);


                //create raycast and bulletstreak
                RaycastHit2D hitInfo = Physics2D.Raycast(transform.position, fireAngleVec2, range, mask);
                GameObject bulletStreak = Instantiate(bulletPrefab, transform.position, fireAngleQuat, transform);
                ParticleSystem bulletTrialParticles = bulletStreak.GetComponent<ParticleSystem>();
                ParticleSystem.ShapeModule bTPShape = bulletTrialParticles.shape;

                //if hit
                if (hitInfo)
                {
                    //apply force
                    hitInfo.rigidbody.AddForceAtPosition(strength * Vector2.right, hitInfo.point, ForceMode2D.Impulse);

                    //adjust the bulletstreak to stop at the asteroid
                    float lengthToHit = (new Vector2(hitInfo.point.x - transform.position.x, hitInfo.point.y - transform.position.y)).magnitude;
                    bTPShape.radius = lengthToHit/2;
                    bTPShape.position = new Vector3(lengthToHit/2,0,0);

                    //create a bullethit effect
                    GameObject newBulletHitEffect = Instantiate(bulletHitEffect, hitInfo.point, transform.rotation);
                    Destroy(newBulletHitEffect, 2);
                }
                else //if not hit
                {

                    //draw the bullet particles for the full range
                    bTPShape.radius = noisyRange / 2;
                    bTPShape.position = new Vector3(noisyRange / 2, 0, 0);

                }

                i = 0;
            }
        }
    }
}
