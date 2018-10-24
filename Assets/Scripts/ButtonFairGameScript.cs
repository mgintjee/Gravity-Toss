using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonFairGameScript : MonoBehaviour {
    public Button button;
    public SliderPaddleSpeedScript leftSpeed;
    public SliderPaddleSpeedScript rightSpeed;
    public SliderPaddleReflectScript leftReflect;
    public SliderPaddleReflectScript rightReflect;

    // Use this for initialization
    void Start()
    {
        FairGameListener();
    }

    void FairGameListener()
    {
        Button Btn = button.GetComponent<Button>();
        ButtonText = Btn.GetComponent<Text>();
        Btn.onClick.AddListener(FairGameClickAction);
    }

    void FairGameClickAction()
    {
        leftSpeed.ToggleFairGame();
        rightSpeed.ToggleFairGame();
        leftReflect.ToggleFairGame();
        rightReflect.ToggleFairGame();
        fair = !fair;
        button.transform.Find("Text").gameObject.GetComponent<Text>().text = FairStatus();
    }
    private string FairStatus()
    {
        return (fair) ? "Fair Game" : "Unfair Game";
    }
    private bool fair = true;
    private Text ButtonText;
}
