using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOverOnDeath : MonoBehaviour
{
    public TextMeshProUGUI finalScoreText;

    private float finalScore;

    public void UpdateFinalScore(float score)
    {
        finalScore = score;
    }


    public void TriggerGameOver()
    {
        //finalScoreText.text = "Final Score: " + Mathf.RoundToInt(finalScore).ToString();
        SceneManager.LoadScene(2);
    }
}
