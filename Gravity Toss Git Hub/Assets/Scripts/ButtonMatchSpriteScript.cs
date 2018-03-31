using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonMatchSpriteScript : MonoBehaviour
{
    public Button button;
    public string Identifier;
    public SliderGameObjectSpriteScript Paddle;
    public SliderGameObjectSpriteScript Goal;

    // Use this for initialization
    void Start () {

        MatchListener();
    }

    void MatchListener()
    {
        Button Btn = button.GetComponent<Button>();
        ButtonText = Btn.GetComponent<Text>();
        Btn.onClick.AddListener(MatchClickAction);
    }
    void MatchClickAction()
    {
        Paddle.ToggleMatch();
        Goal.ToggleMatch();
        fair = !fair;
        button.transform.Find("Text").gameObject.GetComponent<Text>().text = FairStatus();
    }
    private string FairStatus()
    {
        return Identifier + ": " + ( (fair) ? "Same" : "Unique" );
    }
    private bool fair = true;
    private Text ButtonText;
}
