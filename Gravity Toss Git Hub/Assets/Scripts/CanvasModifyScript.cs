using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasModifyScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ButtonListeners();
	}

    void ButtonListeners()
    {
        ApplyButtonListener();
        ResetButtonListener();
    }

    void ApplyButtonListener()
    {
        string aName = "ApplyButton";
        applyButton = modCanvas.transform.Find("Panel").Find(aName);
        Button aBtn = applyButton.GetComponent<Button>();
        aBtn.onClick.AddListener(OnApplyClickAction);
    }

    void OnApplyClickAction()
    {
        settingCanvas.GetComponent<CanvasSettingScript>().EnableCanvas();
        DisableCanvas();
    }

    void ResetButtonListener()
    {
        string rName = "ResetButton";
        resetButton = modCanvas.transform.Find("Panel").Find(rName);
        Button rBtn = resetButton.GetComponent<Button>();
        rBtn.onClick.AddListener(OnResetClickAction);
    }

    void OnResetClickAction()
    {
        sliderLeftPaddleSpeed.value = PaddleSpeed;
        sliderRightPaddleSpeed.value = PaddleSpeed;
        sliderLeftPaddleReflect.value = PaddleReflect;
        sliderRightPaddleReflect.value = PaddleReflect;
        sliderBallMaxSpeed.value = ballMaxSpeed;
        sliderBallGravityScale.value = ballGravityScale;
    }

    public void DisableCanvas()
    {
        modCanvas.SetActive(false);
    }

    public void EnableCanvas()
    {
        modCanvas.SetActive(true);
    }
    public GameObject settingCanvas, modCanvas;
    public Slider sliderLeftPaddleSpeed, sliderLeftPaddleReflect, sliderRightPaddleSpeed, sliderRightPaddleReflect, sliderBallMaxSpeed, sliderBallGravityScale;
    public int PaddleSpeed, ballMaxSpeed;
    public float ballGravityScale, PaddleReflect;
    private Transform applyButton;
    private Transform resetButton;
}
