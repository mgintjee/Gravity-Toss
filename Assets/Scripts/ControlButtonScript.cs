using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControlButtonScript : Button {

    public void Update()
    {
        canvasObject = GameObject.FindGameObjectWithTag("GameCanvas");
        player = canvasObject.GetComponent<CanvasGameScript>().player;
        playerPaddle = canvasObject.GetComponent<CanvasGameScript>().playerPaddle;
        string otherButtonTag;

        if (tag.Equals("L Button"))
        {
            otherButtonTag = "R Button";
        }
        else
        {
            otherButtonTag = "L Button";
        }

        otherButton = GameObject.FindGameObjectWithTag(otherButtonTag);

        if ( IsPressed() )
        {
            if (!otherButton.GetComponent<ControlButtonScript>().IsPressed())
            {
                WhilePressed();
            }
            else if(player != 0 && playerPaddle != null)
            {
                playerPaddle.GetComponent<PaddleAI>().Move(0);
            }

        }
        else if ( (player != 0 && playerPaddle != null && !otherButton.GetComponent<ControlButtonScript>().IsPressed()) )
        {
            playerPaddle.GetComponent<PaddleAI>().Move(0);
        }
    }

    public void WhilePressed()
    {
        if (player != 0 && playerPaddle != null)
        {
            int direction = (tag.Equals("L Button")) ? -1 : 1;
            playerPaddle.GetComponent<PaddleAI>().Move(direction);
        }
    }

    private GameObject canvasObject;
    private GameObject otherButton;
    private GameObject playerPaddle;
    private int player = 0;
}
