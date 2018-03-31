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

    void Start()
    {
        float h, s, v;
        Color.RGBToHSV(Color.white, out h, out s, out v);
        s = 1f;
        v = 1f;
        ObjColor = Color.HSVToRGB(h, s, v);

        SliderObject.onValueChanged.AddListener(delegate { ValueChangeCheck(); });
        ValueText.text = h.ToString();
        SliderObject.value = h;
    }

    public void ValueChangeCheck()
    {
        float h, s, v;
        h = (float) Math.Round(SliderObject.value,2);
        s = 1f;
        v = 1f;
        ObjColor = Color.HSVToRGB(h, s, v);

        ValueText.text = h.ToString();
        Obj.GetComponent<SpriteRenderer>().color = ObjColor;

        if( OtherSlider != null)
        {
            if( Match)
            {
                OtherSlider.value = h;
            }
        }
    }

    public void ToggleMatch()
    {
        Match = !Match;
    }
}
