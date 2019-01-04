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
            GatherAttributeValues();
        }

        if (Player != 0 && IsPressed() )
        {
            Debug.Log("Active Player wants to move" + this.name);
        }
    }
    private void GatherAttributeValues()
    {
        ObjectCanvasGameUI = GameObject.Find("CanvasGameUI");
        InitialLoad = false;
    }

    private GameObject ObjectCanvasGameUI;
    private GameObject ButtonOther;
    private GameObject playerPaddle;
    public int Player = 0;
    private bool InitialLoad = true;
}
