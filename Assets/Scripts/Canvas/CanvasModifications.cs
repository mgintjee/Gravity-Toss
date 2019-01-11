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

    private void ApplyGameplayChanges()
    {
        if (OldBallSpeed != NewBallSpeed)
            UpdateBallSpeed();
        if (OldGravityScale != NewGravityScale)
            UpdateGravityScale();
        if (OldPaddleSpeedLeft != NewPaddleSpeedLeft)
            UpdatePaddleSpeedLeft();
        if (OldPaddleSpeedRight != NewPaddleSpeedRight)
            UpdatePaddleSpeedRight();
        UpdateBarrierBackType();
    }
    private void UpdateBallSpeed()
    {
        GameObject Ball = GameObject.Find("ObjectBall");
        Ball.GetComponent<ObjectBall>().MaxSpeed = (int) (BufferBallSpeed * NewBallSpeed);
    }
    private void UpdateGravityScale()
    {
        GameObject Ball = GameObject.Find("ObjectBall");
        Ball.GetComponent<ToolCustomGravity>().GravityScale = NewGravityScale;
    }
    private void UpdatePaddleSpeedLeft()
    {
        GameObject PaddleLeft = GameObject.Find("ObjectBarrierGoalLeft").transform.Find("ObjectPaddle").gameObject;
        PaddleLeft.GetComponent<ObjectPaddleAI>().Speed = (int)(NewPaddleSpeedLeft * BufferPaddleSpeed);
    }
    private void UpdatePaddleSpeedRight()
    {
        GameObject PaddleRight = GameObject.Find("ObjectBarrierGoalRight").transform.Find("ObjectPaddle").gameObject;
        PaddleRight.GetComponent<ObjectPaddleAI>().Speed = (int)(NewPaddleSpeedRight * BufferPaddleSpeed);
    }
    private void UpdateBarrierBackType()
    {
        GameObject Barrier = GameObject.Find("ObjectBarrierBack");
        Barrier.transform.Find("BarrierBackCurved").gameObject.SetActive(BarrierCurved);
        Barrier.transform.Find("BarrierBackFlat").gameObject.SetActive(!BarrierCurved);
    }
    // Gatherers
    private void GatherPreloadAttributes()
    {
        GatherButtons();
        GatherCanvases();
        GatherSliders();
        Gathered = true;
    }
    public void GatherGameAttributes()
    {
        GameObject Ball = GameObject.Find("ObjectBall");
        GameObject PaddleLeft = GameObject.Find("ObjectBarrierGoalLeft").transform.Find("ObjectPaddle").gameObject;
        GameObject PaddleRight = GameObject.Find("ObjectBarrierGoalRight").transform.Find("ObjectPaddle").gameObject;
        OldBallSpeed = Ball.GetComponent<ObjectBall>().MaxSpeed;
        OldGravityScale = Ball.GetComponent<ToolCustomGravity>().GravityScale;
        OldPaddleSpeedLeft = PaddleLeft.GetComponent<ObjectPaddleAI>().Speed;
        OldPaddleSpeedRight = PaddleRight.GetComponent<ObjectPaddleAI>().Speed;
        OldPaddleReflectLeft = PaddleLeft.GetComponent<ObjectPaddleAI>().Reflect;
        OldPaddleReflectRight = PaddleRight.GetComponent<ObjectPaddleAI>().Reflect;
        bool SamePaddleSpeed = OldPaddleSpeedLeft == OldPaddleSpeedRight;
        bool SamePaddleReflect = OldPaddleReflectLeft == OldPaddleReflectRight;

        if (!(SamePaddleSpeed && SamePaddleReflect))
        {
            GameFair = false;
        }
        else
        {
            GameFair = true;
        }
        if (GameObject.Find("ObjectBarrierBack").transform.Find("BarrierBackCurved").gameObject.activeSelf)
        {
            BarrierCurved = true;
        }
        else
        {
            BarrierCurved = false;
        }
        UpdateGameplaySlidersWithOldValues();
        UpdateGameplayButtonsWithOldValues();
    }

    private void UpdateGameplaySlidersWithOldValues()
    {
        ObjectSliderBallMaxSpeed.GetComponent<Slider>().value = OldBallSpeed/BufferBallSpeed;
        ObjectSliderBallMaxSpeed.GetComponent<Slider>().transform.Find("value").GetComponent<Text>().text = OldBallSpeed.ToString();
        ObjectSliderGravityScale.GetComponent<Slider>().value = OldGravityScale;
        ObjectSliderGravityScale.GetComponent<Slider>().transform.Find("value").GetComponent<Text>().text = OldGravityScale.ToString();

        ObjectSliderPaddleSpeedLeft.GetComponent<Slider>().value = OldPaddleSpeedLeft/BufferPaddleSpeed;
        ObjectSliderPaddleSpeedLeft.GetComponent<Slider>().transform.Find("value").GetComponent<Text>().text = OldPaddleSpeedLeft.ToString();
        ObjectSliderPaddleSpeedRight.GetComponent<Slider>().value = OldPaddleSpeedRight/ BufferPaddleSpeed;
        ObjectSliderPaddleSpeedRight.GetComponent<Slider>().transform.Find("value").GetComponent<Text>().text = OldPaddleSpeedRight.ToString();

        ObjectSliderPaddleReflectLeft.GetComponent<Slider>().value = OldPaddleReflectLeft/BufferPaddleReflect;
        ObjectSliderPaddleReflectLeft.GetComponent<Slider>().transform.Find("value").GetComponent<Text>().text = OldPaddleReflectLeft.ToString();
        ObjectSliderPaddleReflectRight.GetComponent<Slider>().value = OldPaddleReflectRight/BufferPaddleReflect;
        ObjectSliderPaddleReflectRight.GetComponent<Slider>().transform.Find("value").GetComponent<Text>().text = OldPaddleReflectRight.ToString();
    }

    private void UpdateGameplayButtonsWithOldValues()
    {
        if (GameFair)
            ObjectButtonGameFairness.transform.Find("Text").GetComponent<Text>().text = "Fair Game";
        else
            ObjectButtonGameFairness.transform.Find("Text").GetComponent<Text>().text = "Unfair Game";

        if (BarrierCurved)
            ObjectButtonBarrier.transform.Find("Text").GetComponent<Text>().text = "Curved Barrier";
        else
            ObjectButtonBarrier.transform.Find("Text").GetComponent<Text>().text = "Flat Barrier";
    }

    private void GatherButtons()
    {
        ObjectButtonApply = GameObject.Find("ButtonApply");
        ObjectButtonGameFairness = GameObject.Find("ButtonGameFairness");
        ObjectButtonBarrier = GameObject.Find("ButtonBarrierType");
    }
    private void GatherCanvases()
    {
        ObjectCanvasSettings = GameObject.Find("CanvasSettings");
    }
    private void GatherSliders()
    {
        ObjectSliderBallMaxSpeed = GameObject.Find("SliderBallMaxSpeed");
        ObjectSliderGravityScale = GameObject.Find("SliderGravityScale");
        ObjectSliderPaddleReflectLeft = GameObject.Find("SliderLeftPaddleReflect");
        ObjectSliderPaddleReflectRight = GameObject.Find("SliderRightPaddleReflect");
        ObjectSliderPaddleSpeedLeft = GameObject.Find("SliderLeftPaddleSpeed");
        ObjectSliderPaddleSpeedRight = GameObject.Find("SliderRightPaddleSpeed");
    }
    // Setting Listeners
    private void SetListeners()
    {
        ObjectButtonApply.transform.GetComponent<Button>().onClick.AddListener(ButtonActionApply);
        ObjectButtonBarrier.transform.GetComponent<Button>().onClick.AddListener(ButtonActionBarrier);
        ObjectButtonGameFairness.transform.GetComponent<Button>().onClick.AddListener(ButtonActionGameFairness);
        ObjectSliderBallMaxSpeed.GetComponent<Slider>().onValueChanged.AddListener(delegate { SliderActionBallMaxSpeed(); });
        ObjectSliderGravityScale.GetComponent<Slider>().onValueChanged.AddListener(delegate { SliderActionGravityScale(); });
        ObjectSliderPaddleSpeedLeft.GetComponent<Slider>().onValueChanged.AddListener(delegate { SliderActionPaddleSpeedLeft(); });
        ObjectSliderPaddleSpeedRight.GetComponent<Slider>().onValueChanged.AddListener(delegate { SliderActionPaddleSpeedRight(); });
        ObjectSliderPaddleReflectLeft.GetComponent<Slider>().onValueChanged.AddListener(delegate { SliderActionPaddleReflectLeft(); });
        ObjectSliderPaddleReflectRight.GetComponent<Slider>().onValueChanged.AddListener(delegate { SliderActionPaddleReflectRight(); });
    }

    // Listener Actions
    private void ButtonActionBarrier()
    {
        if (BarrierCurved)
        {
            ObjectButtonBarrier.transform.Find("Text").GetComponent<Text>().text = "Flat Barrier";
            BarrierCurved = false;
        }
        else
        {
            ObjectButtonBarrier.transform.Find("Text").GetComponent<Text>().text = "Curved Barrier";
            BarrierCurved = true;
        }
    }
    private void ButtonActionApply()
    {
        ApplyGameplayChanges();
        this.gameObject.SetActive(false);
        ObjectCanvasSettings.SetActive(true);
    }
    private void ButtonActionGameFairness()
    {
        if (GameFair)
        {
            GameFair = false;
            ObjectButtonGameFairness.transform.Find("Text").GetComponent<Text>().text = "Unfair Game";
        }
        else
        {
            GameFair = true;
            ObjectButtonGameFairness.transform.Find("Text").GetComponent<Text>().text = "Fair Game";
        }
    }
    private void SliderActionGravityScale()
    {
        NewGravityScale = ObjectSliderGravityScale.GetComponent<Slider>().value;
        ObjectSliderGravityScale.transform.Find("value").GetComponent<Text>().text = NewGravityScale.ToString();
    }
    private void SliderActionBallMaxSpeed()
    {
        NewBallSpeed = ObjectSliderBallMaxSpeed.GetComponent<Slider>().value;
        ObjectSliderBallMaxSpeed.transform.Find("value").GetComponent<Text>().text = (BufferBallSpeed * NewBallSpeed).ToString();
    }
    private void SliderActionPaddleSpeedLeft()
    {
        NewPaddleSpeedLeft = ObjectSliderPaddleSpeedLeft.GetComponent<Slider>().value;
        ObjectSliderPaddleSpeedLeft.transform.Find("value").GetComponent<Text>().text = (BufferPaddleSpeed * NewPaddleSpeedLeft).ToString();
        if (GameFair)
            UpdatePaddleSpeedRight(NewPaddleSpeedLeft);
    }
    private void SliderActionPaddleSpeedRight()
    {
        NewPaddleSpeedRight = ObjectSliderPaddleSpeedRight.GetComponent<Slider>().value;
        ObjectSliderPaddleSpeedRight.transform.Find("value").GetComponent<Text>().text = (BufferPaddleSpeed * NewPaddleSpeedRight).ToString();
        if (GameFair)
            UpdatePaddleSpeedLeft(NewPaddleSpeedRight);
    }
    private void SliderActionPaddleReflectLeft()
    {
        NewPaddleReflectLeft = ObjectSliderPaddleReflectLeft.GetComponent<Slider>().value;
        ObjectSliderPaddleReflectLeft.transform.Find("value").GetComponent<Text>().text = (BufferPaddleReflect * NewPaddleReflectLeft).ToString();
        if (GameFair)
            UpdatePaddleReflectRight(NewPaddleReflectLeft);
    }
    private void SliderActionPaddleReflectRight()
    {
        NewPaddleReflectRight = ObjectSliderPaddleReflectRight.GetComponent<Slider>().value;
        ObjectSliderPaddleReflectRight.transform.Find("value").GetComponent<Text>().text = (BufferPaddleReflect * NewPaddleReflectRight).ToString();
        if (GameFair)
            UpdatePaddleReflectLeft(NewPaddleReflectRight);
    }
    private void UpdatePaddleSpeedLeft(float NewValue)
    {
        NewPaddleSpeedLeft = NewValue;
        ObjectSliderPaddleSpeedLeft.GetComponent<Slider>().value = NewValue;
        ObjectSliderPaddleSpeedLeft.transform.Find("value").GetComponent<Text>().text = (BufferPaddleSpeed * NewPaddleSpeedLeft).ToString();
    }
    private void UpdatePaddleSpeedRight(float NewValue)
    {
        NewPaddleSpeedRight = NewValue;
        ObjectSliderPaddleSpeedRight.GetComponent<Slider>().value = NewValue;
        ObjectSliderPaddleSpeedRight.transform.Find("value").GetComponent<Text>().text = (BufferPaddleSpeed * NewPaddleSpeedRight).ToString();
    }
    private void UpdatePaddleReflectLeft(float NewValue)
    {
        NewPaddleReflectLeft = NewValue;
        ObjectSliderPaddleReflectLeft.GetComponent<Slider>().value = NewValue;
        ObjectSliderPaddleReflectLeft.transform.Find("value").GetComponent<Text>().text = (BufferPaddleReflect * NewPaddleReflectLeft).ToString();
    }
    private void UpdatePaddleReflectRight(float NewValue)
    {
        NewPaddleReflectRight = NewValue;
        ObjectSliderPaddleReflectRight.GetComponent<Slider>().value = NewValue;
        ObjectSliderPaddleReflectRight.transform.Find("value").GetComponent<Text>().text = (BufferPaddleReflect * NewPaddleReflectRight).ToString();
    }

    public bool BarrierCurved, GameFair;
    public float OldBallSpeed, OldGravityScale, OldPaddleSpeedLeft, OldPaddleSpeedRight, OldPaddleReflectLeft, OldPaddleReflectRight;
    public float NewBallSpeed, NewGravityScale, NewPaddleSpeedLeft, NewPaddleSpeedRight, NewPaddleReflectLeft, NewPaddleReflectRight;
    private GameObject ObjectSliderPaddleReflectLeft, ObjectSliderPaddleReflectRight, ObjectSliderPaddleSpeedLeft, ObjectSliderPaddleSpeedRight, ObjectSliderGravityScale, ObjectSliderBallMaxSpeed;
    private GameObject ObjectButtonApply, ObjectButtonBarrier, ObjectButtonGameFairness;
    private GameObject ObjectCanvasSettings;
    private int BufferPaddleSpeed = 3, BufferBallSpeed = 5;
    private float BufferPaddleReflect = 0.5f;
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
