using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GuiController : MonoBehaviour
{
    private Text selectedBuildText = null;
    private Text creditsText = null;
    private RectTransform timerRect = null;
    public float scoreAmount;
    public float scoreIncrement;
    public TextMeshProUGUI score;
    private GameOverOnDeath gameOverScript;
    public float finalScore;

    // Start is called before the first frame update
    void Start()
    {
        scoreAmount = 0f;
        scoreIncrement = 1f;
        GameObject selectedBuild = GameObject.Find("Selected Build");

        if (selectedBuild)
        {
            selectedBuildText = selectedBuild.GetComponent<Text>();
        }

        GameObject credits = GameObject.Find("Credits");

        if (credits)
        {
            creditsText = credits.GetComponent<Text>();
        }

        GameObject timer = GameObject.Find("Timer");

        if (timer)
        {
            timerRect = timer.GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        scoreAmount += scoreIncrement * Time.deltaTime;
        score.text = " Score:" + (int)scoreAmount;
        finalScore = scoreAmount;
    }

    public void UpdateSelectedBuildText(string name, int price)
    {
        if (selectedBuildText)
        {
            string pricetag = price > 0 ? " [$" + price + "]" : "";
            selectedBuildText.text = "Selected Build: " + name + pricetag;
        }
    }

    public void UpdateCreditsText(int credits)
    {
        if (creditsText)
        {
            creditsText.text = "Credits: [$" + credits + "]";
        }
    }

    public void UpdateTimer(float timerProgress)
    {
        if (timerRect)
        {
            float val = Mathf.Clamp(timerProgress, 0, 1); 
            timerRect.localScale = new Vector3(val, timerRect.localScale.y, timerRect.localScale.z);
        }
    }
}
