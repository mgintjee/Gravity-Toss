using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasModifications : MonoBehaviour {

    // Use this for initialization
    public bool Gathered = false;
	void Start ()
    {
        GatherPreloadAttributes();
        SetListeners();
    }
    public void GatherGameAttributes()
    {
        GameObject Ball = GameObject.Find("ObjectBall");
        GameObject PaddleLeft = GameObject.Find("ObjectBarrierGoalLeft").transform.Find("ObjectPaddle").gameObject;
        GameObject PaddleRight = GameObject.Find("ObjectBarrierGoalRight").transform.Find("ObjectPaddle").gameObject;
        OldBallSpeed = Ball.GetComponent<ObjectBall>().MaxSpeed;
        OldBallGravity = Ball.GetComponent<ToolCustomGravity>().GravityScale;
        OldPaddleReflectLeft = PaddleLeft.GetComponent<ObjectPaddleAI>().Reflect;
    }

    private void GatherPreloadAttributes()
    {
        GatherButtons();
        GatherCanvases();
        Gathered = true;
    }
    private void GatherButtons()
    {
        ObjectButtonApply = GameObject.Find("ButtonApply");
        ObjectButtonBarrier = GameObject.Find("ButtonBarrierType");
    }
    private void GatherCanvases()
    {
        ObjectCanvasSettings = GameObject.Find("CanvasSettings");
    }
    private void GatherSliders()
    {
        ObjectSliderPaddleReflectLeft = GameObject.Find("SliderLeftPaddleReflect");
    }
    private void SetListeners()
    {
        ListenerButtonBarrierType();
        ListenerButtonApply();
    }
    private void ListenerButtonApply()
    {
        ObjectButtonApply.transform.GetComponent<Button>().onClick.AddListener(ButtonActionApply);
    }
    private void ListenerButtonBarrierType()
    {
        ObjectButtonBarrier.transform.GetComponent<Button>().onClick.AddListener(ButtonActionBarrier);
    }
    private void ButtonActionApply()
    {
        this.gameObject.SetActive(false);
        ObjectCanvasSettings.SetActive(true);
    }
    private void ButtonActionBarrier()
    {
        GameObject Barrier = GameObject.Find("ObjectBarrierBack");
        if (BarrierCurved)
        {
            Barrier.transform.Find("BarrierBackCurved").gameObject.SetActive(false);
            Barrier.transform.Find("BarrierBackFlat").gameObject.SetActive(true);
            BarrierCurved = false;
        }
        else
        {
            Barrier.transform.Find("BarrierBackCurved").gameObject.SetActive(true);
            Barrier.transform.Find("BarrierBackFlat").gameObject.SetActive(false);
            BarrierCurved = true;
        }
    }
    private void SliderActionPaddleReflectLeft()
    {
        float value = (float) System.Math.Round(ObjectSliderPaddleReflectLeft.GetComponent<Slider>().value, 1);
        ObjectSliderPaddleReflectLeft.GetComponent<Slider>().value = value;
    }
    private bool BarrierCurved = true;
    public float OldBallSpeed, OldBallGravity, OldPaddleSpeedLeft, OldPaddleSpeedRight, OldPaddleReflectLeft, OldPaddleReflectRight;
    public float NewBallSpeed, NewBallGravity, NewPaddleSpeedLeft, NewPaddleSpeedRight, NewPaddleReflectLeft, NewPaddleReflectRight;
    private GameObject ObjectSliderPaddleReflectLeft;
    private GameObject ObjectButtonApply, ObjectButtonBarrier;
    private GameObject ObjectCanvasSettings;
    /*
     * 
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
        ball.GetComponent<ObjectBall>().HardReset();
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
    */
}
