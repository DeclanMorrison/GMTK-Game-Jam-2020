using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Score : MonoBehaviour
{
    static public float score = 0;
    public float scorePerSecond;
    public Text scoreDisplay;
    public float scoreShow;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name != "EngineerAndShip")
        {
            score = 0;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        scoreShow = score;
        //
        if (SceneManager.GetActiveScene().name != "GameOver")
        {
            score += scorePerSecond / 50f;
        }

        //update score display
        scoreDisplay.text = Mathf.Round(score).ToString();
    }
}
