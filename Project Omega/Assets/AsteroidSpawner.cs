using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AsteroidTypes
{
    public GameObject asteroidObject;
    public int spawnRate;
    public int startingSpeed;
}

public class AsteroidSpawner : MonoBehaviour
{
    System.Random rand = new System.Random();
    public List<AsteroidTypes> asteroidTypes = new List<AsteroidTypes>();
    public int spawnBottom;
    public int spawnTop;
    public float spawnSlope;
    Vector3 spawnLocation;
    public Vector3 spawnOffset;
    public int spawnMultiplier;



    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        foreach (AsteroidTypes asteroidType in asteroidTypes)
        {
            if(rand.Next(0,10000) < asteroidType.spawnRate)
            {
                spawnLocation.y = rand.Next(spawnBottom, spawnTop);
                spawnLocation.x = spawnLocation.y / spawnSlope;
                GameObject newAsteroid = Instantiate(asteroidType.asteroidObject, spawnLocation + spawnOffset, transform.rotation);
                newAsteroid.GetComponent<AsteroidBehavior>().startVelocity = rand.Next(asteroidType.startingSpeed/5, asteroidType.startingSpeed);
                newAsteroid.GetComponent<AsteroidBehavior>().startAngle = rand.Next(45, 85);
                newAsteroid.GetComponent<AsteroidBehavior>().startTorque = rand.Next(-30, 30);
            }
        }
    }
}
