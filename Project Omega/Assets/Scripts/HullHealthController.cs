using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class HullHealthController : MonoBehaviour
{
    private RectTransform rTrans;
    public ShipSubsystems shipSubsystem;
    public GameObject lostHealthChunk;
    Slider slider;
    private float lastFrameHealth;

    private float maxHealth;
    public Vector2 breakVelocity;


    // Start is called before the first frame update
    void Start()
    {
        rTrans = GetComponent<RectTransform>();
        slider = GetComponent<Slider>();
        maxHealth = shipSubsystem.health;
        slider.maxValue = shipSubsystem.health;
        slider.value = shipSubsystem.health;
        lastFrameHealth = shipSubsystem.health;
    }

    // Update is called once per frame
    void Update()
    {
        if (lastFrameHealth > shipSubsystem.health)
        {
            Debug.Log("Health Lost!");

            GameObject newLostHealthChunk = Instantiate(lostHealthChunk);
            newLostHealthChunk.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            newLostHealthChunk.GetComponent<RectTransform>().anchoredPosition = new Vector3(Scale(0, maxHealth, 13, 13 + 224, shipSubsystem.health), -35, 0);
            newLostHealthChunk.GetComponent<RectTransform>().localScale = new Vector3((Mathf.Clamp((lastFrameHealth-shipSubsystem.health)/maxHealth,0,1)),1,1);
            newLostHealthChunk.GetComponent<Rigidbody2D>().velocity = breakVelocity;
            Destroy(newLostHealthChunk, 3);



            //Mathf.Lerp(rTrans.position.x, rTrans.rect.width + rTrans.position.x, Mathf.Lerp(0, maxHealth, shipSubsystem.health));
            //Left position of chunk : Mathf.Lerp(rTrans.position.x, rTrans.rect.width + rTrans.position.x, Mathf.Lerp(0, maxHealth, shipSubsystem.health));
            //Instantiate()

            //rTrans.position.x;
            //rTrans.rect.width;
            // shipSubsystem.health


        }

        slider.value = shipSubsystem.health;



        if (shipSubsystem.health <= 0)
        {
            SceneManager.LoadScene(2);
        }

        lastFrameHealth = shipSubsystem.health;
     }

    public float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

}
