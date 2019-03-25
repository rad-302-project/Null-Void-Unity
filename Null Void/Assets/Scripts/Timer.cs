using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;

    public static float t;
    int playerScore;

    UiController uiController;
    AsteroidTumbler asteroidTumbler;

    const int TIME_LIMIT = 60; // Change this to 60 seconds later...

    // Use this for initialization
    void Start()
    {
        startTime = Time.time;
        uiController = GameObject.Find("Controller_Menu").GetComponent<UiController>();  
        playerScore = AsteroidTumbler.Score;
    }

    // Update is called once per frame
    void Update()
    {
       
        t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");
        timerText.text = minutes + ":" + seconds;
        if (t >= TIME_LIMIT) // If the time limit has been reached...
        {
            uiController.LoadResultsScreen();
        }

    }

}
