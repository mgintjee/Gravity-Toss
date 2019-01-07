using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameUIButtonControl : Button
{	
	// Update is called once per frame
	void Update () {
        if (InitialLoad)
        {
            Direction = (this.name.Contains("Left") ? -1 : 1);
            GatherAttributeValues();
        }

        if (Player != 0 && IsPressed() )
        {
            Paddle.GetComponent<ObjectPaddleAI>().Move(Direction);
        }
    }
    private void GatherAttributeValues()
    {
        ObjectCanvasGameUI = GameObject.Find("CanvasGameUI");
        InitialLoad = false;
    }
    public void EnablePlayer(int player)
    {
        UpdateTextForPlayer();
        Player = player;
        string BarrierGoalName = "ObjectBarrierGoal";
        BarrierGoalName += (Player == -1)?"Left":"Right";
        Paddle = GameObject.Find(BarrierGoalName).transform.Find("ObjectPaddle").gameObject;
        Paddle.GetComponent<ObjectPaddleAI>().ActivePlayer = true;
        Paddle.GetComponent<ObjectPaddleAI>().UpdateTextForPlayer();
    }
    public void DisablePlayer()
    {
        UpdateTextForAI();
        Player = 0;
        string BarrierGoalName = "ObjectBarrierGoal";
        BarrierGoalName += (Player == -1) ? "Left" : "Right";
        Paddle = GameObject.Find(BarrierGoalName).transform.Find("ObjectPaddle").gameObject;
        Paddle.GetComponent<ObjectPaddleAI>().ActivePlayer = false;
        Paddle.GetComponent<ObjectPaddleAI>().UpdateTextForAI();
    }
    public void UpdateTextForPlayer()
    {
        string StringDirection = (this.name.Contains("Left") ? "Left" : "Right");
        this.transform.Find("Text").GetComponent<Text>().text = "Move " + StringDirection;
    }

    public void UpdateTextForAI()
    {
        string StringDirection = (this.name.Contains("Left") ? "Left" : "Right");
        this.transform.Find("Text").GetComponent<Text>().text = "Play As " + StringDirection + " Paddle";
    }
    private int Direction;
    private GameObject ObjectCanvasGameUI;
    private GameObject ButtonOther;
    private GameObject Paddle;
    public int Player = 0;
    private bool InitialLoad = true;
}
