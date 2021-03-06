﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.UIElements;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UiController : MonoBehaviour
{
    public static UiController instance;
    SignalRController signalRController;
    public AudioManager audioManager;
    public AsteroidTumbler asteroidTumbler;
    public Text txtEmail, txtUsernameR, txtPasswordR, txtUsernameL, txtPasswordL; // For input fields.
    public Text RegistrationFeedback, LoginFeedback, UsernameDisplay, HighScoreDisplay; // To display in-game.
    public UnityEngine.UI.Button btnReturnFromRegistration, btnReturnFromLogin; // Set in Inspector.
    public int NewHighScore;
    public bool ServerScoreUpdated = false;
    public string Username;

    UnityEngine.UI.Button btnPlay, btnLogin, btnLogout, btnRegister; // Set automatically, since they are active in the scene from the beginning.

    string serverMessage;
    int existingScore;


    bool feedbackUpdated, playerInfoUpdated;    
    ServerListener serverListener;


    enum UserState
    {
        LOGGEDIN,
        LOGGEDOUT
    }

    UserState userState = UserState.LOGGEDOUT;

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
        serverListener = GameObject.Find("ServerListener").GetComponent<ServerListener>();
        audioManager = GameObject.Find("Controller_Audio").GetComponent<AudioManager>();

        btnPlay = GameObject.Find("Canvas/pnl_MainMenu/btn_Play").GetComponent<UnityEngine.UI.Button>();
        btnLogin = GameObject.Find("Canvas/pnl_MainMenu/btn_Login").GetComponent<UnityEngine.UI.Button>();
        btnLogout = GameObject.Find("Canvas/pnl_MainMenu/btn_Logout").GetComponent<UnityEngine.UI.Button>();
        btnRegister = GameObject.Find("Canvas/pnl_MainMenu/btn_Register").GetComponent<UnityEngine.UI.Button>();

        //btnPlay.gameObject.SetActive(false);
        //btnLogout.gameObject.SetActive(false);

        PlaySound("Menu BGM"); // Play the main menu BGM.      

        RegistrationFeedback.text = "Registering...";
    }

    private void Update()
    {
        if(feedbackUpdated)
        {
            RegistrationFeedback.text = serverMessage;
            btnReturnFromRegistration.gameObject.SetActive(true); // I'll keep an eye on this.

            LoginFeedback.text = serverMessage;
            btnReturnFromLogin.gameObject.SetActive(true);

            feedbackUpdated = false;
        }

        switch (userState)
        {
            case UserState.LOGGEDOUT:
                // Disable what shouldn't be visible in this state.
                if (btnPlay.isActiveAndEnabled) btnPlay.gameObject.SetActive(false);
                if (btnLogout.isActiveAndEnabled) btnLogout.gameObject.SetActive(false);
                if (UsernameDisplay.isActiveAndEnabled)
                {
                    UsernameDisplay.text = "";
                    UsernameDisplay.gameObject.SetActive(false);
                }
                if (HighScoreDisplay.isActiveAndEnabled)
                {
                    HighScoreDisplay.text = "";
                    HighScoreDisplay.gameObject.SetActive(false);
                }

                // Enable what should be visible in this state.
                if (!btnLogin.gameObject.activeSelf) btnLogin.gameObject.SetActive(true);
                if (!btnRegister.gameObject.activeSelf) btnRegister.gameObject.SetActive(true);
                break;

            case UserState.LOGGEDIN:
                // Disable what shouldn't be visible in this state.
                if (btnLogin != null && btnLogin.isActiveAndEnabled) btnLogin.gameObject.SetActive(false);
                if (btnRegister != null && btnRegister.isActiveAndEnabled) btnRegister.gameObject.SetActive(false);

                // Enable what should be visible in this state.
                if (btnPlay != null && !btnPlay.isActiveAndEnabled) btnPlay.gameObject.SetActive(true);
                if (btnLogout != null && !btnLogout.isActiveAndEnabled) btnLogout.gameObject.SetActive(true);
                if (UsernameDisplay != null && !UsernameDisplay.isActiveAndEnabled) UsernameDisplay.gameObject.SetActive(true);
                if (HighScoreDisplay != null && !HighScoreDisplay.isActiveAndEnabled) HighScoreDisplay.gameObject.SetActive(true);

                // Display user info.
                if (UsernameDisplay != null && UsernameDisplay.text == "") UsernameDisplay.text = Username;
                if (HighScoreDisplay != null && HighScoreDisplay.text == "") HighScoreDisplay.text = string.Format("High Score: " + existingScore);
                break;
        }
    }

    #region Registration methods and coroutines.
    public void CheckRegisterInfo()
    {
        if(txtEmail.text != "" && txtUsernameR.text != "" && txtPasswordR.text != "")
        {
            StartCoroutine("SendRegisterInfo");
        }

        else UpdateServerFeedback("Login failed: Please enter a valid email address, username and password.");
    }

    public IEnumerator SendRegisterInfo()
    {
        yield return new WaitForSeconds(1.5f);
        signalRController.RegisterPlayer(txtEmail.text, txtUsernameR.text, txtPasswordR.text);
    }
    #endregion

    #region Login methods and coroutines.
    public void CheckLoginInfo()
    {
        if (txtUsernameL.text != "" && txtPasswordL.text != "")
        {
            StartCoroutine("SendLoginInfo");
        }

        else UpdateServerFeedback("Login failed: Please enter a username and password.");
    }

    public IEnumerator SendLoginInfo()
    {
        yield return new WaitForSeconds(1.5f);
        signalRController.LoginPlayer(txtUsernameL.text, txtPasswordL.text);
    }

    public void EnableLoginMode(string usernameIn, int highScoreIn)
    {
        Username = usernameIn;
        existingScore = highScoreIn;
        userState = UserState.LOGGEDIN;
    }
    
    public void DisableLoginMode()
    {
        userState = UserState.LOGGEDOUT;
    }
    #endregion

    #region Feedback methods.
    public void UpdateServerFeedback(string serverFeedback)
    {
        serverMessage = serverFeedback;
        feedbackUpdated = true;
    }

    public void RevertFeedback()
    {
        if (RegistrationFeedback.text != "Registering...") RegistrationFeedback.text = "Registering...";
        if (LoginFeedback.text != "Logging in...") LoginFeedback.text = "Logging in...";

        btnReturnFromRegistration.gameObject.SetActive(false);
        btnReturnFromLogin.gameObject.SetActive(false);
    }
    #endregion

    public void LoadResultsScreen()
    {
        NewHighScore = AsteroidTumbler.Score;

        // Now send this score to the signal R controller.
        signalRController.UploadMatchResults(Username, NewHighScore);

        StopSound("Gameplay BGM");
        PlaySound("Results BGM");       

        SceneManager.LoadScene("sc_Results"); // Load the results screen.               
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

    public void PlaySound(string soundName) // I should probably move these next two methods to the AudioManager class.
    {
        if(audioManager != null) audioManager.Play(soundName);
    }

    public void StopSound(string soundName)
    {
        if(audioManager != null) audioManager.Stop(soundName);
    }    
}