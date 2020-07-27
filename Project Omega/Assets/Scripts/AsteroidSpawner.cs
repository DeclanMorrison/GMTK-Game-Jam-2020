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

    //for selecting a spawn location (will draw line between two spawn points and pick a point on it)

    public Transform spawnPoint1;
    public Transform spawnPoint2;
    public Transform ship;


    public float spawnMultiplier;

    int i = 500;
    public float round = 1;
    int difficulty = 1;


    void FixedUpdate()
    {

        i = i + 1;
        if(i > 300) // is called once every 3 seconds
        {
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
            spawnMultiplier = round / 2;
            i = 0;
        }


        //run once for each asteroid type
        foreach (AsteroidTypes asteroidType in asteroidTypes)
        {
            if(rand.NextDouble()*100 < asteroidType.spawnRate*spawnMultiplier)
            {
                Vector3 spawnLocation = PickLocation();

                //find the angle the asteroids would need to spawn to fly straight at the ship
                float spawnAngle = Mathf.Atan2(ship.position.y - spawnLocation.y, ship.position.x - spawnLocation.x) * Mathf.Rad2Deg - 90;

                GameObject newAsteroid = Instantiate(asteroidType.asteroidObject, spawnLocation, transform.rotation);

                float size = newAsteroid.GetComponent<AsteroidBehavior>().sizeInUnits;
                float squaredSize = Mathf.Pow(size, 2);
                float sqrtSize = Mathf.Sqrt(size);
                newAsteroid.GetComponent<AsteroidBehavior>().startVelocity = (float)rand.NextDouble() * 200/ sqrtSize + 100/ sqrtSize;
                newAsteroid.GetComponent<AsteroidBehavior>().startAngle = spawnAngle + UnityEngine.Random.Range(-15,15);
                newAsteroid.GetComponent<AsteroidBehavior>().startTorque = rand.Next(-30, 30);

                Destroy(newAsteroid, 30);
            }
        }

        //run once for each cargo
        foreach (CargoTypes cargoType in cargoTypes)
        {
            if (rand.NextDouble() * 100 < cargoType.spawnRate * spawnMultiplier)
            {
                Vector3 spawnLocation = PickLocation();

                //find the angle the cargo would need to spawn to fly straight at the ship
                float spawnAngle = Mathf.Atan2(ship.position.y - spawnLocation.y, ship.position.x - spawnLocation.x) * Mathf.Rad2Deg - 90;

                GameObject newCargo = Instantiate(cargoType.cargoObject, spawnLocation, transform.rotation);

                newCargo.GetComponent<cargoInSpace>().startVelocity = ((float)rand.NextDouble() * cargoType.startingSpeed/2) + cargoType.startingSpeed / 2;
                newCargo.GetComponent<cargoInSpace>().startAngle = spawnAngle + UnityEngine.Random.Range(-15, 15);
                newCargo.GetComponent<cargoInSpace>().startTorque = rand.Next(-30, 30);
                
            }
        }
    }

    public Vector3 PickLocation() //returns a location between the two spawn points
    {
        Vector3 location;
        location.x = UnityEngine.Random.Range(spawnPoint1.position.x, spawnPoint2.position.x);
        float slope = (spawnPoint2.position.y - spawnPoint1.position.y) / (spawnPoint2.position.x - spawnPoint1.position.x);
        location.y = slope * location.x - slope * spawnPoint1.position.x + spawnPoint1.position.y;
        location.z = 0;
        return location;
    }
}
