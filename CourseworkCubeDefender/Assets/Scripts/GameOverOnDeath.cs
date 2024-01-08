using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverOnDeath : MonoBehaviour
{
    private GuiController gui;

    public void TriggerGameOver()
    {
        //Debug.Log(gui.scoreAmount);
        SceneManager.LoadScene(2);
    }
}
