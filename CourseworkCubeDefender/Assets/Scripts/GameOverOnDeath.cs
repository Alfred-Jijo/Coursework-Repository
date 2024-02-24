using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameOverOnDeath : MonoBehaviour
{
    public TextMeshProUGUI FinalScore;
    public GuiController gui;
    public float score;
    public Text FinalScoreAmountText;


    public void TriggerGameOver()
    {
        GameObject FinalScoreAmount = GameObject.Find("FinalScoreAmount");
        // score = gui.scoreAmount;
        SceneManager.LoadScene(2);
        // FinalScore.text = "Final Score: " + score.ToString();
        //FinalScore.text = "Final " + gui.score.text;
        if (FinalScoreAmount)
        {
            FinalScoreAmountText = FinalScoreAmount.GetComponent<Text>();
        }
        // FinalScore.text = "Final Score:" + (int)gui.finalScore;

        UpdateFinalScoreAmount();
    }

    public void UpdateFinalScoreAmount()
    {
        string scoreProp = $" Final Score: {gui.scoreAmount}";
        FinalScoreAmountText.text = scoreProp;
    }
}
