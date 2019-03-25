using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UiInfoReceiver : MonoBehaviour {

    UiController uiController;
    Text uiHost;

	void Start () {
        uiController = GameObject.Find("Controller_Menu").GetComponent<UiController>();
        uiHost = gameObject.GetComponent<Text>();

        if (gameObject.name == "txtUsername") uiHost.text = uiController.UsernameDisplay.text;
        else if (gameObject.name == "txtWinLoss") uiHost.text = uiController.WinLossDisplay.text;
    }
}