using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController instance;
    public Text txtEmail, txtUsernameR, txtPasswordR, txtUsernameL, txtPasswordL;

    SignalRController signalRController;
    public AudioManager audioManager;

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
        signalRController = GameObject.Find("SignalRController").GetComponent<SignalRController>();
        audioManager = GameObject.Find("Controller_Audio").GetComponent<AudioManager>();
        PlaySound("Menu BGM"); // Play the main menu BGM.       
    }

    public void SendRegisterInfo()
    {
        if(txtEmail != null && txtUsernameR != null && txtPasswordR != null)
        {
            signalRController.RegisterPlayer(txtEmail.text, txtUsernameR.text, txtPasswordR.text);
        }
    }

    public void SendLoginInfo()
    {
        if (txtUsernameL != null && txtPasswordL != null)
        {
            signalRController.LoginPlayer(txtUsernameL.text, txtPasswordL.text);
        }
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
        if(audioManager != null) audioManager.Play(soundName);
    }

    public void StopSound(string soundName)
    {
        if(audioManager != null) audioManager.Stop(soundName);
    }

    public void OpenQuitDialog() // Gotta make this into a generic method that is called when you click any button.
    {
        PlaySound("Confirm");
    }
}