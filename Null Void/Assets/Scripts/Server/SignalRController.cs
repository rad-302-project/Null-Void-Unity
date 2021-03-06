﻿using Assets.Scenes.Default.Classes;
using Microsoft.AspNet.SignalR.Client;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SignalRController : MonoBehaviour
{
    public static SignalRController instance;

    ServerListener serverListener;
    UiController uiController;

    // Connection properties.    
    //static string endpoint = "http://localhost:55476/";
    static string endpoint = "http://radicalwebapp.azurewebsites.net/"; // Now hosted on Azure.
    static string hubName = "RADicalHub";
    HubConnection connection;
    IHubProxy proxy;
    bool connected;

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

    void OnApplicationQuit()
    {
        if (connected) connection.Stop();
    }  

    void Start()
    {
        DontDestroyOnLoad(this);       
        serverListener = GameObject.Find("ServerListener").GetComponent<ServerListener>();
        uiController = GameObject.Find("Controller_Menu").GetComponent<UiController>();
        ConnectToHub();
    }

    public void ConnectToHub()
    {
        if (!string.IsNullOrEmpty(endpoint) && !string.IsNullOrEmpty(hubName))
        {
            connection = new HubConnection(endpoint);
            proxy = connection.CreateHubProxy(hubName);

            // Add actions to the proxy.
            proxy.On("ReceiveResults", new Action(serverListener.OnReceiveResults));
            proxy.On("ReceiveRegistrationMessage", new Action<string, string>(serverListener.OnReceiveRegistrationMessage));
            proxy.On("ReceiveLoginMessage", new Action<string, string, int>(serverListener.OnReceiveLoginMessage));
            //proxy.On("PlayerJoined", new Action<string>(serverTalk.OnPlayerJoined));
            //proxy.On("PlayerLeft", new Action<string>(serverTalk.OnPlayerLeft));

            // Connect to server.
            connection.Start().Wait();

            connected = true;

            Debug.Log("Connected");
        }
    }

    public void RegisterPlayer(string emailIn, string usernameIn, string pwordIn)
    {
        if (connected) proxy.Invoke("RegisterNewPlayer", emailIn, usernameIn, pwordIn);       
        else uiController.UpdateServerFeedback("No connection to the server could be established!");
    }

    public void LoginPlayer(string usernameIn, string pwordIn)
    {
        if (connected) proxy.Invoke("PlayerLogin", usernameIn, pwordIn);
        else uiController.UpdateServerFeedback("No connection to the server could be established!");
    }

    public void UploadMatchResults(string usernameIn, int resultsIn)
    {
        if (connected)
        {
            proxy.Invoke("UploadHighScore", usernameIn, resultsIn);
        }
    } 
}