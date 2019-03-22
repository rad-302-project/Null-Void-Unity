using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public static MenuController instance;

    void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    void Start()
    {
        PlaySound("Menu BGM"); // Play the main menu BGM.
    }

    public void OpenQuitDialog() // Gotta make this into a generic method that is called when you click any button.
    {
        PlaySound("Confirm");
    }

    public void StartGame()
    {
        StopSound("Menu BGM");
        PlaySound("Confirm");
        PlaySound("Gameplay BGM"); // Play the gameplay BGM.
        SceneManager.LoadScene("sc_gameplay");
    }

    public void QuitGame()
    {
        PlaySound("Confirm");
        Application.Quit(); // Close the game.       
    }

    public void PlaySound(string soundName)
    {
        FindObjectOfType<AudioManager>().Play(soundName);
    }

    public void StopSound(string soundName)
    {
        FindObjectOfType<AudioManager>().Stop(soundName);
    }
}