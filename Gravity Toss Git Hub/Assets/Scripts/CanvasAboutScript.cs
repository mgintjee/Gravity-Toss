using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAboutScript : MonoBehaviour {

	// Use this for initialization
	void Start () {
        ReturnButtonListener();
    }

    void ReturnButtonListener()
    {
        string aName = "ReturnButton";
        returnButton = aboutCanvas.transform.Find("Panel").Find(aName);
        Button rBtn = returnButton.GetComponent<Button>();
        rBtn.onClick.AddListener(OnReturnClickAction);
    }

    void OnReturnClickAction()
    {
        settingCanvas.GetComponent<CanvasSettingScript>().EnableCanvas();
        DisableCanvas();
    }

    public void DisableCanvas()
    {
        aboutCanvas.SetActive(false);
    }

    public void EnableCanvas()
    {
        aboutCanvas.SetActive(true);
    }

    public GameObject settingCanvas;
    public GameObject aboutCanvas;
    private Transform returnButton;
}
