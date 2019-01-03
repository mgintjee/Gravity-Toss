using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderPaddleReflectScript : MonoBehaviour
{

    public GameObject Paddle;
    public bool FairGame = true;
    public Slider SliderObject;
    public Slider OtherSlider;
    public Text ValueText;

    // Update is called once per frame
    void Start()
    {
        float value = Paddle.GetComponent<PaddleAI>().reflect;
        SliderObject.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        ValueText.text = value.ToString();
        SliderObject.value = value;
    }

    public void ValueChangeCheck()
    {
        float value = (float) System.Math.Round(SliderObject.value, 1);
        Debug.Log("Changing Paddle Reflect to " + value);
        Paddle.GetComponent<PaddleAI>().reflect = value;
        ValueText.text = value.ToString();

        if (FairGame)
        {
            OtherSlider.value = value;
        }
    }

    public void ToggleFairGame()
    {
        FairGame = !FairGame;
    }
}
