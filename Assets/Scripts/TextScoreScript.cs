using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextScoreScript : MonoBehaviour {
    public GameObject LeftGoal;
    public GameObject RightGoal;
    public Text ScoreText;

    public void UpdateScore()
    {
        int RightScore = LeftGoal.GetComponent<GoalScript>().goalsConceded;
        int LeftScore = RightGoal.GetComponent<GoalScript>().goalsConceded;
        ScoreText.text = LeftScore + " - " + RightScore;
    }
}
