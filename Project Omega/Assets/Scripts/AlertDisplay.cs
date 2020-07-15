using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertDisplay : MonoBehaviour
{
    public AsteroidSpawner asteroidSpawner;
    public Slider slider;
    public bool isWorking = true;
    System.Random rand = new System.Random();

    void Start()
    {
        slider.maxValue = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorking)
        {
            slider.value = asteroidSpawner.round;
        }
        else
        {
            slider.value = rand.Next(0,8);
        }
    }
}
