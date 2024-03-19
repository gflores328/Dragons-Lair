using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class AH_ScoreManager : MonoBehaviour
{
    public AH_PlayerController playerController;

    public GameManager gm;

    private int playerScore = 0;
    private int aiScore = 0;

    public Text playerScoreText;
    public Text aiScoreText;

    private void Start()
    {
        gm = GetComponent<GameManager>();
    }


    public void PlayerGoal()
    {
        playerScore++;
        playerScoreText.text = "Owen: " + playerScore.ToString();

        int newPlayerScore = Convert.ToInt32(playerScoreText);


        if ((newPlayerScore == 7) && (playerController.difficulty == 0.5))
        { 
            gm.LoadLevelbyName("WinScene");
        }


        else if ((newPlayerScore == 5) && (playerController.difficulty == 0.7))
        {
            gm.LoadLevelbyName("WinScene");
        }


        else if ((newPlayerScore == 3) && (playerController.difficulty == 1))
        {
            gm.LoadLevelbyName("WinScreen");
        }
    }

    public void AIGoal()
    {
        aiScore++;
        aiScoreText.text = "Npc AI: : " + aiScore.ToString();

        int newAiScore = Convert.ToInt32(playerScoreText);

        if ((newAiScore == 7) && (playerController.difficulty == 0.5))
        {
            gm.LoadLevelbyName("LoseScene");
        }


        else if ((newAiScore == 5) && (playerController.difficulty == 0.7))
        {
            gm.LoadLevelbyName("Losecene");
        }


        else if ((newAiScore == 3) && (playerController.difficulty == 1))
        {
            gm.LoadLevelbyName("LoseScreen");
        }
    }
}
