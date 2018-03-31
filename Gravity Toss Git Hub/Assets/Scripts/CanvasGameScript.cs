using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasGameScript : MonoBehaviour {
    
    // Use this for initialization
    void Start ()
    {
        //GameObject canvasObject = GameObject.FindGameObjectWithTag("CanvasUI");

        //settingCanvas = GameObject.FindGameObjectsWithTag("SettingCanvas")[0];
        //gameCanvas = gameObject;

        LeftButtonListener();
        RightButtonListener();
        SettingButtonListener();
    }
    
    void LeftButtonListener()
    {
        lButton.onClick.AddListener(OnLeftClickAction);
    }

    void RightButtonListener()
    {
        rButton.onClick.AddListener(OnRightClickAction);
    }

    void SettingButtonListener()
    {
        sButton.onClick.AddListener(OnSettingClickAction);
    }

    void OnLeftClickAction()
    {
        if (player == 0)
        {
            player = 1;
            playerPaddle = GameObject.FindGameObjectsWithTag("L Paddle")[0];
            playerPaddle.GetComponent<PaddleAI>().player = true;
            playerPaddle.transform.Find("Canvas").transform.Find("Text").gameObject.GetComponent<Text>().text = "Player";
            UpdateButtonsForPlayer();
        }
    }
    void OnRightClickAction()
    {
        if (player == 0)
        {
            player = 2;
            playerPaddle = GameObject.FindGameObjectsWithTag("R Paddle")[0];
            playerPaddle.GetComponent<PaddleAI>().player = true;
            playerPaddle.transform.Find("Canvas").transform.Find("Text").gameObject.GetComponent<Text>().text = "Player";
            UpdateButtonsForPlayer();
        }
    }

    void OnSettingClickAction()
    {
        Time.timeScale = 0;
        settingCanvas.GetComponent<CanvasSettingScript>().EnableCanvas();
        DisableCanvas();
    }

    private void UpdateButtonsForPlayer()
    {
        lButton.transform.Find("Text").gameObject.GetComponent<Text>().text = "Move\nLeft";
        rButton.transform.Find("Text").gameObject.GetComponent<Text>().text = "Move\nRight";
    }

    public void UpdateButtonsForAI()
    {
        lButton.transform.Find("Text").gameObject.GetComponent<Text>().text = "Play As\nLeft\nPaddle";
        rButton.transform.Find("Text").gameObject.GetComponent<Text>().text = "Play As\nRight\nPaddle";
    }

    public void DisableCanvas()
    {
        gameCanvas.SetActive(false);
    }

    public void EnableCanvas()
    {
        gameCanvas.SetActive(true);
    }

    public int player;
    public GameObject playerPaddle;
    public Button lButton;
    public Button rButton;
    public Button sButton;
    public GameObject settingCanvas;
    public GameObject gameCanvas;

}
