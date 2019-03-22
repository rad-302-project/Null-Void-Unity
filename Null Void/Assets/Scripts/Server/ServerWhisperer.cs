using Assets.Scenes.Default.Classes;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServerWhisperer : MonoBehaviour
{
    public SignalRController signalRController;    

    // Variables to hold a referennce to each player's respawn script.
    //PlayerRespawn player1Respawn, player2Respawn;

    bool matchInProgress;

    void Start()
    {
        // Find both players in the scene so that we can keep track of them.
        //player1 = GameObject.Find("Player 1").GetComponent<PlayerRespawn>();
        //player2 = GameObject.Find("Player 2").GetComponent<PlayerRespawn>();

        // Find the signalR controller.
        signalRController = GameObject.Find("SignalRController").GetComponent<SignalRController>();

        // Get each player's respawn scripts.
        //player1Respawn = player1.PlayerPrefab.GetComponent<PlayerRespawn>();
        //player2Respawn = player2.NetworkPlayerPrefab.GetComponent<PlayerRespawn>();

        matchInProgress = true;
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
        matchInProgress = false;

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
}