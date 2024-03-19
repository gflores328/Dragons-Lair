using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class AH_ScoreManager : MonoBehaviour
{
    public AH_PlayerController playerController;

    public GameManager gameManager;

    private int playerScore = 0;
    private int aiScore = 0;

    public Text playerScoreText;
    public Text aiScoreText;

    private void Start()
    {
        
    }


    public void PlayerGoal()
    {
        playerScore++;
        playerScoreText.text = "Owen: " + playerScore.ToString();


        if ((playerScore == 7) && (playerController.mode == "easy"))
        { 
            gameManager.LoadLevelbyName("WinScene");
        }


        else if ((playerScore == 5) && (playerController.mode == "intermidate"))
        {
            gameManager.LoadLevelbyName("WinScene");
        }


        else if ((playerScore == 3) && (playerController.mode == "hard"))
        {
            gameManager.LoadLevelbyName("WinScreen");
        }
    }

    public void AIGoal()
    {
        aiScore++;
        aiScoreText.text = "Npc AI: : " + aiScore.ToString();

        int newAiScore = Convert.ToInt32(playerScoreText);

        if ((newAiScore == 7) && (playerController.difficulty == 0.5))
        {
            gameManager.LoadLevelbyName("LoseScene");
        }


        else if ((newAiScore == 5) && (playerController.difficulty == 0.7))
        {
            gameManager.LoadLevelbyName("Losecene");
        }


        else if ((newAiScore == 3) && (playerController.difficulty == 1))
        {
            gameManager.LoadLevelbyName("LoseScreen");
        }
    }
}
