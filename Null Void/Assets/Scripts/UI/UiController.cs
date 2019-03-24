using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController instance;
    public Text txtEmail, txtUsernameR, txtPasswordR, txtUsernameL, txtPasswordL; // For input fields.
    public Text RegistrationFeedback, LoginFeedback, UsernameDisplay, WinLossDisplay; // To display in-game.
    public UnityEngine.UI.Button ReturnFromRegistration, ReturnFromLogin;

    string serverMessage = "";
    bool feedbackUpdated;

    SignalRController signalRController;
    AudioManager audioManager;

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

        RegistrationFeedback.text = "Registering...";
    }

    private void Update()
    {
        if(feedbackUpdated)
        {
            RegistrationFeedback.text = serverMessage;
            ReturnFromRegistration.gameObject.SetActive(true); // I'll keep an eye on this.
            feedbackUpdated = false;
        }
    }

    #region Registration methods and coroutines.
    public void CheckRegisterInfo()
    {
        if(txtEmail != null && txtUsernameR != null && txtPasswordR != null)
        {
            StartCoroutine("SendRegisterInfo");
        }
    }

    public IEnumerator SendRegisterInfo()
    {
        yield return new WaitForSeconds(2);
        signalRController.RegisterPlayer(txtEmail.text, txtUsernameR.text, txtPasswordR.text);
    }

    public void UpdateRegFeedback(string serverFeedback)
    {
        serverMessage = serverFeedback;
        feedbackUpdated = true;
    }
    #endregion

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
        PlaySound("Gameplay BGM"); // Play the gameplay BGM.
        SceneManager.LoadScene("sc_gameplay");
    }

    public void QuitGame()
    {       
        Application.Quit(); // Close the game.       
    }

    public void RevertFeedback()
    {
        if (RegistrationFeedback.text != "Registering...") RegistrationFeedback.text = "Registering...";
        //if (LoginFeedback.text != "Logging in...") LoginFeedback.text = "Logging in...";

        ReturnFromRegistration.gameObject.SetActive(false);
    }
   
    public void PlaySound(string soundName) // I should probably move these next two methods to the AudioManager class.
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