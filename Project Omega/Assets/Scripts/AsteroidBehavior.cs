using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidBehavior : MonoBehaviour
{
    public float sizeInUnits;

    public float startVelocity;
    public float startAngle;
    public float startTorque;

    private Rigidbody2D rb;
    private AudioSource audio;
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        float squaredSize = Mathf.Pow(sizeInUnits, 2);
        float sqrtSize = Mathf.Sqrt(sizeInUnits);
        float cubeRootSize = Mathf.Pow(sizeInUnits, 1f / 3f);


        audio = GetComponent<AudioSource>();

        rb = GetComponent<Rigidbody2D>();
        rb.velocity = Quaternion.AngleAxis(startAngle, Vector3.forward) * Vector3.up * startVelocity;
        rb.AddTorque(startTorque);

        rb.mass = sizeInUnits*sizeInUnits*sizeInUnits*200000;

        //scale up the particle system based on size
        particleSystem = GetComponent<ParticleSystem>();
        var pSMain = particleSystem.main;
        pSMain.startSize = new ParticleSystem.MinMaxCurve(sqrtSize * .1f, sqrtSize * .5f);
        pSMain.startLifetime = new ParticleSystem.MinMaxCurve(cubeRootSize * .04f, cubeRootSize * 1.5f);
        //pSMain.startSpeed = sizeInUnits * 5;
        var pSEmission = particleSystem.emission;
        pSEmission.rateOverTime = sqrtSize * 50;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Contains("Ship"))
        {
            audio.Play(); //Play sound Effect
            collision.gameObject.GetComponent<ShipSubsystems>().ImpactDamage(sizeInUnits, collision.relativeVelocity.magnitude, collision.contacts);
        }
    }
}
