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
        scoreTxt.text = "Score: " + AsteroidTumbler.score.ToString();
        healthBar.fillAmount = AsteroidTumbler.health / PlayerController.startHealth;


    }

}
