using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartButton : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartButtonClick()
    {
        SceneManager.LoadScene(1);
    }
    public void MainButtonClick()
    {
        SceneManager.LoadScene(0);
    }
}
