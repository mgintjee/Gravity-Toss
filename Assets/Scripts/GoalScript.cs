using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoalScript : MonoBehaviour
{

    public GameObject canvasObject;
    public GameObject ball;
    public int goalsConceded = 0;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Ball"))
        {
            goalsConceded++;
            EmitConfetti();
            ResetPaddles();
            UpdateUI();
            ball.GetComponent<BallScript>().ResetBall();
        }
    }

    private void UpdateUI()
    {
        /*
        string textName = (this.tag.Equals("L Goal")) ?"R Score":"L Score";
        Transform textTr = canvasObject.transform.Find(textName);
        Text text = textTr.GetComponent<Text>();
        text.text = textName + ": " + goalsConceded;
        */
        this.GetComponent<TextScoreScript>().UpdateScore();
    }

    private void EmitConfetti()
    {
        Transform leftTopEmitter = transform.Find("confetti_emitter_left_top");
        Transform rightTopEmitter = transform.Find("confetti_emitter_right_top");
        Transform leftBotEmitter = transform.Find("confetti_emitter_left_bot");
        Transform rightBotEmitter = transform.Find("confetti_emitter_right_bot");
        leftTopEmitter.GetComponent<ConfettiEmitterScript>().EmitConfetti();
        rightTopEmitter.GetComponent<ConfettiEmitterScript>().EmitConfetti();
        leftBotEmitter.GetComponent<ConfettiEmitterScript>().EmitConfetti();
        rightBotEmitter.GetComponent<ConfettiEmitterScript>().EmitConfetti();
    }

    void ResetPaddles()
    {
        GameObject L_Paddle = GameObject.FindGameObjectsWithTag("L Paddle")[0];
        GameObject R_Paddle = GameObject.FindGameObjectsWithTag("R Paddle")[0];
        Vector2 L_Spawn = L_Paddle.GetComponent<PaddleAI>().defaultSpawn;
        Vector2 R_Spawn = R_Paddle.GetComponent<PaddleAI>().defaultSpawn;
        L_Paddle.transform.position = new Vector3(L_Spawn.x, L_Spawn.y, 0);
        R_Paddle.transform.position = new Vector3(R_Spawn.x, R_Spawn.y, 0);
    }

    public void HardReset()
    {
        ResetPaddles();
        goalsConceded = 0;
        UpdateUI();
    }
}
