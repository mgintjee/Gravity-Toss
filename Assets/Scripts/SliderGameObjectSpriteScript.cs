using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderGameObjectSpriteScript : MonoBehaviour {

    public GameObject Obj;
    public string Identifier;
    public bool Match = true;
    public Slider SliderObject;
    public Slider OtherSlider;
    public Color ObjColor;
    public Text ValueText;
    private List<String> ListOfColorsNames = new List<string> { "White", "Red", "Orange", "Yellow", "Green", "Sea Green", "Cyan", "Blue", "Violet", "Purple", "Pink", "Black" };
    private List<float> ListOfColorsValues = new List<float> { 0, 0, .1f, .2f, .3f, .4f, .5f, 0.6f, 0.7f, 0.8f, 0.9f, 1};
    void Start()
    {
        float h, s, v;
        Color.RGBToHSV(Color.white, out h, out s, out v);
        s = 1f;
        v = 1f;
        ObjColor = Color.HSVToRGB(h, s, v);

        SliderObject.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        ValueText.text = ListOfColorsNames[0];
        SliderObject.value = h;
    }

    public void ValueChangeCheck()
    {
        float h, s, v;
        int index = (int) SliderObject.value;
        h = ListOfColorsValues[index];
        s = 1f;
        v = 1f;

        if (index == 11)
        {
            ObjColor = Color.black;
        }
        else if(index == 0)
        {
            ObjColor = Color.white;
        }
        else
        {
            ObjColor = Color.HSVToRGB(h, s, v);
        }

        ValueText.text = ListOfColorsNames[index];
        Obj.GetComponent<SpriteRenderer>().color = ObjColor;

        if( OtherSlider != null)
        {
            if(Match)
            {
                OtherSlider.value = index;
            }
        }
    }

    public void ToggleMatch()
    {
        Match = !Match;
    }
}
