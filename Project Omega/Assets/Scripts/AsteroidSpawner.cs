using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AsteroidTypes
{
    public GameObject asteroidObject;
    public float spawnRate;
}

[Serializable]
public class CargoTypes
{
    public GameObject cargoObject;
    public float spawnRate;
    public float startingSpeed;
}

public class AsteroidSpawner : MonoBehaviour
{
    System.Random rand = new System.Random();
    public List<AsteroidTypes> asteroidTypes = new List<AsteroidTypes>();
    public List<CargoTypes> cargoTypes = new List<CargoTypes>();

    public int spawnBottom;
    public int spawnTop;
    public float spawnSlope;
    public Vector3 spawnOffset;

    Vector3 spawnLocation;
    public float spawnMultiplier;

    int i = 500;
    public float round = 1;
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

            i = 0;
        }
       


        foreach (AsteroidTypes asteroidType in asteroidTypes)
        {
            if(rand.NextDouble()*100 < asteroidType.spawnRate*spawnMultiplier)
            {
                spawnLocation.y = (float)rand.NextDouble() * -100f;
                spawnLocation.x = spawnLocation.y / spawnSlope;

                GameObject newAsteroid = Instantiate(asteroidType.asteroidObject, spawnLocation + spawnOffset, transform.rotation);

                float size = newAsteroid.GetComponent<AsteroidBehavior>().sizeInUnits;
                float squaredSize = Mathf.Pow(size, 2);
                float sqrtSize = Mathf.Sqrt(size);
                newAsteroid.GetComponent<AsteroidBehavior>().startVelocity = (float)rand.NextDouble() * 200/ sqrtSize + 100/ sqrtSize;
                newAsteroid.GetComponent<AsteroidBehavior>().startAngle = rand.Next(55, 85);
                newAsteroid.GetComponent<AsteroidBehavior>().startTorque = rand.Next(-30, 30);
               
            }
        }

        foreach (CargoTypes cargoType in cargoTypes)
        {
            if (rand.NextDouble() * 100 < cargoType.spawnRate * spawnMultiplier)
            {
                spawnLocation.y = (float)rand.NextDouble() * -100f;
                spawnLocation.x = spawnLocation.y / spawnSlope;

                GameObject newCargo = Instantiate(cargoType.cargoObject, spawnLocation + spawnOffset, transform.rotation);
                AsteroidBehavior asteroid = newCargo.GetComponent<AsteroidBehavior>();
                cargoInSpace cargo = newCargo.GetComponent<cargoInSpace>();

                newCargo.GetComponent<cargoInSpace>().startVelocity = ((float)rand.NextDouble() * cargoType.startingSpeed/2) + cargoType.startingSpeed / 2;
                newCargo.GetComponent<cargoInSpace>().startAngle = rand.Next(55, 85);
                newCargo.GetComponent<cargoInSpace>().startTorque = rand.Next(-30, 30);
                
            }
        }


    }
}
