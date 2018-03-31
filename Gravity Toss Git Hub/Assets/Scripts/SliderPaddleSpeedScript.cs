using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPaddleSpeedScript : MonoBehaviour
{

    public GameObject Paddle;
    public bool FairGame = true;
    public Slider SliderObject;
    public Slider OtherSlider;
    public Text ValueText;

    // Update is called once per frame
    void Start()
    {
        int value = Paddle.GetComponent<PaddleAI>().speed;
        SliderObject.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        ValueText.text = value.ToString();
        SliderObject.value = value;
    }

    public void ValueChangeCheck()
    {
        int value = (int)SliderObject.value;
        Debug.Log("Changing Paddle Speed to " + value);
        Paddle.GetComponent<PaddleAI>().speed = value;
        ValueText.text = value.ToString();

        if ( FairGame)
        {
            OtherSlider.value = value;
        }
    }

    public void ToggleFairGame()
    {
        FairGame = !FairGame;
    }
}
