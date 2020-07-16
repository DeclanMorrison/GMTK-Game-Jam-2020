using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HullHealthController : MonoBehaviour
{

    public ShipSubsystems shipSubsystem;
    Slider slider;

    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = shipSubsystem.health;
        slider.value = shipSubsystem.health;
    }

    // Update is called once per frame
    void Update()
    {
        slider.value = shipSubsystem.health;

        if (shipSubsystem.health <= 0)
        {
            SceneManager.LoadScene(2);
        }
     }
}
