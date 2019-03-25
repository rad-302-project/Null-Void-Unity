using Assets.Scenes.Default.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ServerListener : MonoBehaviour // This script is solely responsible for obtaining information from the server.
{
    public static ServerListener instance;
    public bool LoggedIn;
    

    SignalRController signalRController;
    UiController uiController;

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
        uiController = GameObject.Find("Controller_Menu").GetComponent<UiController>();
    }

    void Update()
    {
        //if (matchInProgress && player1Respawn.RemainingStocks == 0 || matchInProgress && player2Respawn.RemainingStocks == 0)
        //{
        //    EndMatch();
        //}
    }   
  
    public void OnReceiveResults()
    {
        // Update the UI controller.
        uiController.ServerScoreUpdated = true;
    }

    public void OnReceiveRegistrationMessage(string status, string input)
    {
        if (status.ToUpper() == "EMAIL TAKEN")
        {
            uiController.UpdateServerFeedback(input + " already has a NULL VOID account attached to it!");                      
        }

        else if (status.ToUpper() == "USERNAME TAKEN")
        {
            uiController.UpdateServerFeedback("The username " + "'" + input + "'" + " has already been taken.");           
        }

        else if (status.ToUpper() == "SUCCESS")
        {
            uiController.UpdateServerFeedback("Welcome to NULL VOID, " + input + "! Please log in so you can play the game!");           
        }
    }

    public void OnReceiveLoginMessage(string status, string username, int existingHighScoreIn)
    {
        if (status.ToUpper() == "NOT FOUND")
        {
            uiController.UpdateServerFeedback("No player named " + "'" + username + "'" + " found!");
            print("No player named " + "'" + username + "'" + " found!");
        }

        else if (status.ToUpper() == "PASSWORD INVALID")
        {
            uiController.UpdateServerFeedback("Invalid password.");
            //print("Invalid password.");
        }

        else if (status.ToUpper() == "PASSWORD VALID")
        {
            LoggedIn = true;
            uiController.UpdateServerFeedback("Welcome back, " + username + "!");
            uiController.EnableLoginMode(username, existingHighScoreIn);
            // Give user info to the UI controller. 
        }       
    }
}