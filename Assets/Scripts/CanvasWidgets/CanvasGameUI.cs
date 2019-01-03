using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CanvasGameUI : MonoBehaviour {
    public int Player = 0;
    public bool mouseDown;
    // Use this for initialization
    void Start ()
    {
        //GameObject canvasObject = GameObject.FindGameObjectWithTag("CanvasUI");

        //settingCanvas = GameObject.FindGameObjectsWithTag("SettingCanvas")[0];
        //gameCanvas = gameObject;
        GatherButtons();
        ListenerButtonLeft();
        ListenerButtonRight();
        ListenerButtonSettings();
    }

    private void GatherButtons()
    {
        ObjectButtonLeft = GameObject.Find("ButtonLeft");
        ObjectButtonRight = GameObject.Find("ButtonRight");
        ObjectButtonSettings = GameObject.Find("ButtonSettings");
    }
    
    private void ListenerButtonLeft()
    {
        ObjectButtonLeft.transform.GetComponent<Button>().onClick.AddListener(ButtonActionLeft);
    }

    private void ListenerButtonRight()
    {
        ObjectButtonRight.transform.GetComponent<Button>().onClick.AddListener(ButtonActionRight);
    }

    private void ListenerButtonSettings()
    {
        ObjectButtonSettings.transform.GetComponent<Button>().onClick.AddListener(ButtonActionSettings);
    }

    private void ButtonActionLeft()
    {
        if(Player == 0)
        {
            Player = -1;
            UpdateButtonsForPlayer();
            ObjectButtonLeft.GetComponent<CanvasGameUIButtonControl>().Player = Player;
            ObjectButtonRight.GetComponent<CanvasGameUIButtonControl>().Player = Player;
        }
        else
        {
        }
    }
    private void ButtonActionRight()
    { 
        if (Player == 0)
        {
            Player = 1;
            UpdateButtonsForPlayer();
            ObjectButtonLeft.GetComponent<CanvasGameUIButtonControl>().Player = Player;
            ObjectButtonRight.GetComponent<CanvasGameUIButtonControl>().Player = Player;
        }
        else
        {
        }

    }
    private void ButtonActionSettings()
    {
        this.gameObject.SetActive(false);
        GameObject.Find("CanvasSettings").SetActive(true);
    }

    private void UpdateButtonsForPlayer()
    {
        ObjectButtonLeft.transform.Find("Text").gameObject.GetComponent<Text>().text = "Move Left";
        ObjectButtonRight.transform.Find("Text").gameObject.GetComponent<Text>().text = "Move Right";
    }

    public void UpdateButtonsForAI()
    {
        ObjectButtonLeft.transform.Find("Text").gameObject.GetComponent<Text>().text = "Play As\nLeft\nPaddle";
        ObjectButtonRight.transform.Find("Text").gameObject.GetComponent<Text>().text = "Play As\nRight\nPaddle";
    }

    private GameObject ObjectButtonLeft, ObjectButtonRight, ObjectButtonSettings;
    /*
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
    */
}
