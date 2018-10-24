using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGravityScript : MonoBehaviour
{

    public GameObject Ball;
    public Slider SliderObject;
    public Text ValueText;
    
    void Start()
    {
        float value = Ball.GetComponent<Rigidbody2D>().gravityScale;
        SliderObject.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        ValueText.text = value.ToString();
        SliderObject.value = value;
    }

    public void ValueChangeCheck()
    {
        float value = (float)System.Math.Round(SliderObject.value, 1);
        Debug.Log("Changing Ball Gravity Scale to " + value);
        Ball.GetComponent<Rigidbody2D>().gravityScale = value;
        ValueText.text = value.ToString();
    }
}
