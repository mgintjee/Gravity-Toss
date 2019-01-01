using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectBarrierGoal : MonoBehaviour {

    public GameObject CanvasObject;
    public GameObject ObjectBall;
    public int GoalsConceded = 0;
    public bool GoalConceded = false;
    
    public void HardReset()
    {
        ResetPaddles();
        GoalsConceded = 0;
        UpdateUI();
    }

    public void GoalWasConceded()
    {
        GoalsConceded++;
        GoalConceded = true;
        EmitConfetti();
        StartCoroutine(GoalDelayAnimation());
    }

    private IEnumerator GoalDelayAnimation()
    {
        yield return new WaitForSeconds(GoalDelay);
        Debug.Log("Reset Ball");
        ObjectBall.GetComponent<TrailRenderer>().enabled = false;
        ObjectBall.GetComponent<ObjectBall>().RandomStart();
        yield return new WaitForSeconds(.35f);
        ObjectBall.GetComponent<TrailRenderer>().enabled = true;
        GoalConceded = false;
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

    private float GoalDelay = 2f;
}
