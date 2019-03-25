using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour {

    public static bool GamePaused = false;//can call this 
    public GameObject pauseMeuUI;
    
	
	// Update is called once per frame
	void Update ()
    {
        

        if (AsteroidTumbler.Health <= 0)
        {
            Pause();
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (GamePaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
	}

    public void Resume()
    {
        pauseMeuUI.SetActive(false);
        Time.timeScale = 1f;
        GamePaused = false;
    }
    void Pause()
    {
        pauseMeuUI.SetActive(true);
        Time.timeScale = 0f;
        GamePaused = true;
    }

    public void LoadMenu()
    {
        SceneManager.LoadScene("0");
    }
    public void Quit()
    {
        Application.Quit();
    }
}
