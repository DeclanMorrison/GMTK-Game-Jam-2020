using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AsteroidTypes
{
    public GameObject asteroidObject;
    public float spawnRate;
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
    public float spawnMultiplier;

    int i = 0;
    float round = 1;
    int difficulty = 1;


    void FixedUpdate()
    {
        i = i + 1;
        if(i > 500) // is called once every 5 seconds
        {
            spawnMultiplier = round/2;


            if (round > difficulty)
            {
                round = 1;
                difficulty += 1;
            }

            if (round == 1 && rand.Next(0, 100) < 70)
            {
                round = 1;
            }
            else
            {
                round = round + 1;
            }

            //update warning
            i = 0;
        }
       


        foreach (AsteroidTypes asteroidType in asteroidTypes)
        {
            if(rand.NextDouble()*100 < asteroidType.spawnRate*spawnMultiplier)
            {
                spawnLocation.y = rand.Next(spawnBottom, spawnTop);
                spawnLocation.x = spawnLocation.y / spawnSlope;
                GameObject newAsteroid = Instantiate(asteroidType.asteroidObject, spawnLocation + spawnOffset, transform.rotation);
                newAsteroid.GetComponent<AsteroidBehavior>().startVelocity = rand.Next(asteroidType.startingSpeed/5, asteroidType.startingSpeed);
                newAsteroid.GetComponent<AsteroidBehavior>().startAngle = rand.Next(55, 85);
                newAsteroid.GetComponent<AsteroidBehavior>().startTorque = rand.Next(-30, 30);
            }
        }
    }
}
