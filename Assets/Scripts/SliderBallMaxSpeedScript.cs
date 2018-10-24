using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderBallMaxSpeedScript : MonoBehaviour
{
    public GameObject Ball;
    public Slider SliderObject;
    public Text ValueText;
    
    void Start()
    {
        int value = Ball.GetComponent<BallScript>().maxSpeed;
        SliderObject.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        ValueText.text = value.ToString();
        SliderObject.value = value;
    }

    public void ValueChangeCheck()
    {
        int value = (int)SliderObject.value;
        Debug.Log("Changing Ball Max Speed to " + value);
        Ball.GetComponent<BallScript>().maxSpeed = value;
        ValueText.text = value.ToString();
    }
}
