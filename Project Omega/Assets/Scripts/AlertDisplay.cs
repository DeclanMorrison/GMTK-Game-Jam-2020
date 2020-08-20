using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AlertDisplay : MonoBehaviour
{
    public ObsticalSpawner obsticalSpawner;
    Slider slider;
    public bool isWorking = true;
    System.Random rand = new System.Random();

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = 8;
    }

    // Update is called once per frame
    void Update()
    {
        if (isWorking)
        {
            //slider.value = obsticalSpawner.round;
        }
        else
        {
            slider.value = rand.Next(0,8);
        }
    }
}
