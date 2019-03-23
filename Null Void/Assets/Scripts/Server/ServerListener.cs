using Assets.Scenes.Default.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerListener : MonoBehaviour // This script is solely responsible for obtaining information from the server.
{
    public static ServerListener instance;
    public bool LoggedIn;

    SignalRController signalRController;    

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
        // Find the signalR controller.
        signalRController = GameObject.Find("SignalRController").GetComponent<SignalRController>();               
    }

    void Update()
    {
        //if (matchInProgress && player1Respawn.RemainingStocks == 0 || matchInProgress && player2Respawn.RemainingStocks == 0)
        //{
        //    EndMatch();
        //}
    }   

    void EndMatch()
    {
        // End the match.
        //matchInProgress = false;

        // We should load a results screen scene here.       

        // Upload results to the database.
        UploadMatchResults();
    }

    // This method sends the match results to the signalR Controller which in turn will send them to the server.
    public void UploadMatchResults()
    {
        // Apply values to signalR variables. I'll likely use a more graceful method later on.
        //signalRController.p1Username = player1.Username;
        //signalRController.p2Username = player2.Username;
        //signalRController.p1StockCount = player1Respawn.RemainingStocks;
        //signalRController.p2StockCount = player2Respawn.RemainingStocks;

        signalRController.UploadMatchResults();
    }

    public void OnReceiveResults(ApplicationUser winner, ApplicationUser loser)
    {
        print(winner.UserName + " has won the match!");

        print("Winner: " + winner.UserName + " Total Wins: " + winner.Wins + " Total Losses: " + winner.Losses);
        print("Runner-up: " + loser.UserName + " Total Wins: " + loser.Wins + " Total Losses: " + loser.Losses);
    }

    public void OnReceiveRegistrationMessage(string status, string input)
    {
        if (status.ToUpper() == "EMAIL TAKEN")
        {
            print(input + " already has a NULL VOID account attached to it!"); // This should be replaced by in-game text.
        }

        else if (status.ToUpper() == "USERNAME TAKEN")
        {
            print("The username " + "'"+input+"'" + " has already been taken."); // This should also be replaced.
        }

        else if (status.ToUpper() == "SUCCESS")
        {
            print("Welcome to NULL VOID, " + input + "! Please log in so you can play the game!");
        }
    }
}