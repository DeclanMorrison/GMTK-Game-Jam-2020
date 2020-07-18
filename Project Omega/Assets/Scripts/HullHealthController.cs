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
    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
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
            //create a chunk of health to drop off the health bar.
            GameObject newLostHealthChunk = Instantiate(lostHealthChunk);
            newLostHealthChunk.transform.SetParent(GameObject.FindGameObjectWithTag("Canvas").transform, false);
            //place new box where at the position of the current health
            newLostHealthChunk.GetComponent<RectTransform>().anchoredPosition = new Vector3(Scale(0, maxHealth, rTrans.anchoredPosition.x, rTrans.anchoredPosition.x + rTrans.rect.width, shipSubsystem.health), rTrans.anchoredPosition.y, 0);
            //scale it to be the size of the total lost health
            newLostHealthChunk.GetComponent<RectTransform>().localScale = new Vector3((Mathf.Clamp((lastFrameHealth-shipSubsystem.health)/maxHealth,0,1)),1,1);

            newLostHealthChunk.GetComponent<Rigidbody2D>().velocity = breakVelocity;
            Destroy(newLostHealthChunk, 3);

            //trigger a opacity flash
            animator.Play("default");
            animator.Play("UI_opacityPulse");
        }

        //update the slider value
        slider.value = shipSubsystem.health;


        //go to gameover if health below 0
        if (shipSubsystem.health <= 0)
        {
            SceneManager.LoadScene(2);
        }

        //update the lastfram health. Used for checking for health changes.
        lastFrameHealth = shipSubsystem.health;
     }


    //scaling function. Used for scaling one range to another.
    public float Scale(float OldMin, float OldMax, float NewMin, float NewMax, float OldValue)
    {

        float OldRange = (OldMax - OldMin);
        float NewRange = (NewMax - NewMin);
        float NewValue = (((OldValue - OldMin) * NewRange) / OldRange) + NewMin;

        return (NewValue);
    }

}
