using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreController : MonoBehaviour {

    public Canvas canvas;
    int Score = 0;
    public Text ScoreTxt;
    
    public void addScore(int score)
    {
        Score += score;
        ScoreTxt.text = "Score:" + Score;
    }
}
