using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBarrierGoal : MonoBehaviour {

    public GameObject CanvasObject;
    public GameObject Ball;
    public int goalsConceded = 0;

    public void HardReset()
    {
        ResetPaddles();
        goalsConceded = 0;
        UpdateUI();
    }

    private void EmitConfetti()
    {
        Transform CollectionConfettiEmitters = this.transform.Find("CollectionConfettiEmitters");
        int ChildCount = CollectionConfettiEmitters.childCount;
        for(int i = 0; i < ChildCount; ++i)
        {
            Transform ChildEmitter = CollectionConfettiEmitters.GetChild(i);
            ChildEmitter.GetComponent<ObjectConfettiEmitter>().EmitAllConfetti();
        }
    }
    private void ResetPaddles()
    {
        Transform Paddle = this.transform.Find("ObjectPaddle");
        Paddle.GetComponent<ObjectPaddleAI>().ResetPosition();
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
}
