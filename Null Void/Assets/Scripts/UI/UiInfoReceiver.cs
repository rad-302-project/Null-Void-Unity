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

        if (gameObject.name == "txtUsername") uiHost.text = uiController.Username;
        else if (gameObject.name == "txtWinLoss") uiHost.text = uiController.HighScoreDisplay.text;
        else if (gameObject.name == "lbl_New_Score") uiHost.text = string.Format("New High Score: " + uiController.NewHighScore);
    }

    private void Update()
    {
        if(gameObject.name == "lbl_Updated_Score_Msg")
        {
            if(uiController.ServerScoreUpdated && uiHost.text == "") uiHost.text = string.Format("High score uploaded to server!");
        }

        else if (gameObject.name == "txt_end_button")
        {
            if (uiController.ServerScoreUpdated && uiHost.text == "") uiHost.text = string.Format("End Game");
        }
    }
}