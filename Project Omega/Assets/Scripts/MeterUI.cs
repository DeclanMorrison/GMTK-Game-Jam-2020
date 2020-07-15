using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeterUI : MonoBehaviour
{
    float initialScale;
    Vector3 initialPosition;
    SpriteRenderer sr;

    public systemHealth subSystemToTrack;

    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        initialScale = transform.localScale.x;
        initialPosition = transform.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float initialBound = sr.bounds.min.x;

        float scalingFactor = (float) subSystemToTrack.health / (float) subSystemToTrack.maxHealth;
        Vector3 newScale = transform.localScale;
        newScale.x = initialScale * scalingFactor;
        transform.localScale = newScale;

        float newBound = sr.bounds.min.x;

        float difference = newBound - initialBound;

        transform.Translate(new Vector3(-difference, 0f, 0f));
    }
}
