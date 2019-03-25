using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text timerText;
    private float startTime;
<<<<<<< HEAD
    public static float t;

    // Use this for initialization
    void Start ()
    {
        startTime = 30f;
	}
	
	// Update is called once per frame
	void Update ()
=======
    UiController uiController;
    AsteroidTumbler asteroidTumbler;
    int playerScore;

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
>>>>>>> cd4c4d5a3c5e929900aa59952ddb9a704a5b8525
    {
        t = Time.time - startTime;
        string minutes = ((int)t / 60).ToString();
        string seconds = (t % 60).ToString("f2");

        timerText.text = minutes + ":" + seconds;

<<<<<<< HEAD
      

	}
=======
        if (t >= TIME_LIMIT) // If the time limit has been reached...
        {
            uiController.LoadResultsScreen();
        }
    }
>>>>>>> cd4c4d5a3c5e929900aa59952ddb9a704a5b8525
}
