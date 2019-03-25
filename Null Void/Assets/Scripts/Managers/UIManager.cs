using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour {

    public Text scoreTxt;
    public Image healthBar;   

    private void Start()
    {        
    }
    private void Update()
    {
        scoreTxt.text = "Score: " + AsteroidTumbler.Score.ToString();
        healthBar.fillAmount = AsteroidTumbler.Health / PlayerController.startHealth;
    }
}