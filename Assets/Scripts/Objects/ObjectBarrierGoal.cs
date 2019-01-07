using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

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
        UpdateUI();
        EmitConfetti();
        StartCoroutine(GoalDelayAnimation());
    }

    private IEnumerator GoalDelayAnimation()
    {
        yield return new WaitForSeconds(GoalDelay);
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
        GameObject TextScore = GameObject.Find("TextScore");
        string StringText = TextScore.transform.Find("Text").GetComponent<Text>().text;
        string[] StringTextParts = StringText.Split('-');
        string NewTextScore = "";
        if (this.name.Contains("Right"))
        {
            NewTextScore += GoalsConceded + " -" + StringTextParts[1];
        }
        else
        {
            NewTextScore += StringTextParts[0] + "- " + GoalsConceded;
        }
        TextScore.transform.Find("Text").GetComponent<Text>().text = NewTextScore;
    }

    private float GoalDelay = 2f;
}
