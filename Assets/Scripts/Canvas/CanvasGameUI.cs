using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


public class CanvasGameUI : MonoBehaviour {
    public int Player = 0;

    void Start ()
    {
        GatherAttributeValues();
        SetListeners();
    }
    void Update()
    {
        HideGatheredCanvases();
    }

    public void UpdateButtonsForPlayer()
    {
        ObjectButtonLeft.GetComponent<CanvasGameUIButtonControl>().EnablePlayer(Player);
        ObjectButtonRight.GetComponent<CanvasGameUIButtonControl>().EnablePlayer(Player);
    }
    public void UpdateButtonsForAI()
    {
        Player = 0;
        ObjectButtonLeft.GetComponent<CanvasGameUIButtonControl>().DisablePlayer();
        ObjectButtonRight.GetComponent<CanvasGameUIButtonControl>().DisablePlayer();
    }

    private void GatherAttributeValues()
    {
        GatherButtons();
        GatherCanvases();
    }
    private void GatherButtons()
    {
        ObjectButtonLeft = GameObject.Find("ButtonLeft");
        ObjectButtonRight = GameObject.Find("ButtonRight");
        ObjectButtonSettings = GameObject.Find("ButtonSettings");
    }
    private void SetListeners()
    {
        ListenerButtonLeft();
        ListenerButtonRight();
        ListenerButtonSettings();
    }
    private void GatherCanvases()
    {
        ObjectCanvasSettings = GameObject.Find("CanvasSettings");
        ObjectCanvasModifications = GameObject.Find("CanvasModifications");
        ObjectCanvasAbout = GameObject.Find("CanvasAbout");
    }
    private void HideGatheredCanvases()
    {
        if (HideSettings)
        {
            bool GatheredCanvasesSettings = ObjectCanvasSettings.GetComponent<CanvasSettings>().Gathered;
            if (GatheredCanvasesSettings)
            {
                ObjectCanvasSettings.SetActive(false);
                HideSettings = false;
            }
        }
        if (HideModifications)
        {
            bool GatheredCanvasesModifications = ObjectCanvasModifications.GetComponent<CanvasModifications>().Gathered;
            if (GatheredCanvasesModifications)
            {
                ObjectCanvasModifications.SetActive(false);
                HideModifications = false;
            }
        }
        if (HideAbout)
        {
            bool GatheredCanvasesAbout = ObjectCanvasAbout.GetComponent<CanvasAbout>().Gathered;
            if (GatheredCanvasesAbout)
            {
                ObjectCanvasAbout.SetActive(false);
                HideAbout = false;
            }
        }
    }
    private void ListenerButtonLeft()
    {
        ObjectButtonLeft.transform.GetComponent<Button>().onClick.AddListener(ButtonActionLeft);
    }

    private void ListenerButtonRight()
    {
        ObjectButtonRight.transform.GetComponent<Button>().onClick.AddListener(ButtonActionRight);
    }

    private void ListenerButtonSettings()
    {
        ObjectButtonSettings.transform.GetComponent<Button>().onClick.AddListener(ButtonActionSettings);
    }

    private void ButtonActionLeft()
    {
        if(Player == 0)
        {
            Player = -1;
            UpdateButtonsForPlayer();
        }
    }
    private void ButtonActionRight()
    { 
        if (Player == 0)
        {
            Player = 1;
            UpdateButtonsForPlayer();
        }
    }
    private void ButtonActionSettings()
    {
        Time.timeScale = 0;
        ObjectCanvasSettings.SetActive(true);
    }
    private GameObject ObjectButtonLeft, ObjectButtonRight, ObjectButtonSettings;
    private GameObject ObjectCanvasSettings, ObjectCanvasModifications, ObjectCanvasAbout;
    private bool HideSettings = true, HideModifications = true, HideAbout = true;
}
