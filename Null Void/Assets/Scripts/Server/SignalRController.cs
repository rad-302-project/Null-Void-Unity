using Assets.Scenes.Default.Classes;
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

    // Connection properties.    
    static string endpoint = "http://localhost:55476/";
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
        ConnectToHub();
    }

    public void ConnectToHub()
    {
        if (!string.IsNullOrEmpty(endpoint) && !string.IsNullOrEmpty(hubName))
        {
            connection = new HubConnection(endpoint);
            proxy = connection.CreateHubProxy(hubName);

            // Add actions to the proxy.
            proxy.On("ReceiveResults", new Action<ApplicationUser, ApplicationUser>(serverListener.OnReceiveResults));
            proxy.On("ReceiveRegistrationMessage", new Action<string, string>(serverListener.OnReceiveRegistrationMessage));
            proxy.On("ReceiveLoginMessage", new Action<string, string, int, int>(serverListener.OnReceiveLoginMessage));
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
        else print("No connection to the server could be established!");
    }

    public void LoginPlayer(string usernameIn, string pwordIn)
    {
        if (connected) proxy.Invoke("PlayerLogin", usernameIn, pwordIn);
        else print("No connection to the server could be established!");
    }

    public void UploadMatchResults()
    {
        if (connected)
        {
            //proxy.Invoke("UploadMatchResults", p1Username, p2Username, p1StockCount, p2StockCount);
        }
    }

    //public void LeaveChat()
    //{
    //    if (connected)
    //    {
    //        proxy.Invoke("Leave", username);
    //    }
    //}

    //public void SendMessageToChat()
    //{
    //    if (connected)
    //    {
    //        proxy.Invoke("SendMessageToOthers", username, message);
    //    }
    //}
}