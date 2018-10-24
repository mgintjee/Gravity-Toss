using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasSettingScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ButtonListeners();
    }

    void ButtonListeners()
    {
        ResumeButtonListener();
        QuitButtonListener();
        ResetButtonListener();
        ModifyButtonListener();
        AboutButtonListener();
    }

    void ResumeButtonListener()
    {
        string rName = "ResumeButton";
        resumeButton = settingCanvas.transform.Find("Panel").Find(rName);
        Button rBtn = resumeButton.GetComponent<Button>();
        rBtn.onClick.AddListener(OnResumeClickAction);
    }
    void ModifyButtonListener()
    {
        string mName = "ModifyButton";
        modifyButton = settingCanvas.transform.Find("Panel").Find(mName);
        Button mBtn = modifyButton.GetComponent<Button>();
        mBtn.onClick.AddListener(OnModifyClickAction);
    }
    void AboutButtonListener()
    {
        string aName = "AboutButton";
        aboutButton = settingCanvas.transform.Find("Panel").Find(aName);
        Button aBtn = aboutButton.GetComponent<Button>();
        aBtn.onClick.AddListener(OnAboutClickAction);
    }
    void QuitButtonListener()
    {
        string qName = "QuitButton";
        quitButton = settingCanvas.transform.Find("Panel").Find(qName);
        Button qBtn = quitButton.GetComponent<Button>();
        qBtn.onClick.AddListener(OnQuitClickAction);
    }
    void ResetButtonListener()
    {
        string rName = "ResetButton";
        resetButton = settingCanvas.transform.Find("Panel").Find(rName);
        Button rBtn = resetButton.GetComponent<Button>();
        rBtn.onClick.AddListener(OnResetClickAction);
    }
    void OnResumeClickAction()
    {
        DisableCanvas();
        gameCanvas.GetComponent<CanvasGameScript>().EnableCanvas();
        Time.timeScale = 1;
    }
    void OnQuitClickAction()
    {
        int player = gameCanvas.GetComponent<CanvasGameScript>().player;

        if (player != 0)
        {
            if( player == 1)
            {
                GameObject playerPaddle = GameObject.FindGameObjectsWithTag("L Paddle")[0];
                playerPaddle.GetComponent<PaddleAI>().player = false;
                playerPaddle.transform.Find("Canvas").transform.Find("Text").gameObject.GetComponent<Text>().text = "A.I.";

            }
            else
            {
                GameObject playerPaddle = GameObject.FindGameObjectsWithTag("R Paddle")[0];
                playerPaddle.GetComponent<PaddleAI>().player = false;
                playerPaddle.transform.Find("Canvas").transform.Find("Text").gameObject.GetComponent<Text>().text = "A.I.";

            }
            gameCanvas.GetComponent<CanvasGameScript>().player = 0;
            gameCanvas.GetComponent<CanvasGameScript>().UpdateButtonsForAI();
        }
    }
    void OnResetClickAction()
    {
        GameObject lGoal = GameObject.FindGameObjectsWithTag("L Goal")[0];
        GameObject rGoal = GameObject.FindGameObjectsWithTag("R Goal")[0];
        GameObject ball = GameObject.FindGameObjectsWithTag("Ball")[0];

        lGoal.GetComponent<GoalScript>().HardReset();
        rGoal.GetComponent<GoalScript>().HardReset();
        ball.GetComponent<BallScript>().HardReset();
    }
    void OnModifyClickAction()
    {
        DisableCanvas();
        modCanvas.GetComponent<CanvasModifyScript>().EnableCanvas();
    }
    void OnAboutClickAction()
    {
        DisableCanvas();
        aboutCanvas.GetComponent<CanvasAboutScript>().EnableCanvas();
    }

    public void DisableCanvas()
    {
        settingCanvas.SetActive(false);
    }

    public void EnableCanvas()
    {
        settingCanvas.SetActive(true);
    }
    private Transform quitButton;
    private Transform resetButton;
    private Transform modifyButton;
    private Transform aboutButton;
    private Transform resumeButton;
    public GameObject settingCanvas;
    public GameObject gameCanvas;
    public GameObject modCanvas;
    public GameObject aboutCanvas;
}
