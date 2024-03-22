using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class AH_ScoreManager : MonoBehaviour
{
    public AH_PlayerController playerController;

    public GameManager gameManager;

    public GameObject WinScreen;

    public GameObject LoseScreen;

    public GameObject PauseMenu;

    public GameObject StartMenu;

    private int playerScore = 0;
    private int aiScore = 0;

    public Text playerScoreText;
    public Text aiScoreText;


    public void PlayerGoal()
    {
        playerScore++;
        playerScoreText.text = "Owen: " + playerScore.ToString();


        if ((playerScore == 7) && (playerController.mode == "easy"))
        {
            WinScreen.SetActive(true);
        }


        else if ((playerScore == 5) && (playerController.mode == "intermidate"))
        {
            WinScreen.SetActive(true);
        }


        else if ((playerScore == 3) && (playerController.mode == "hard"))
        {
            WinScreen.SetActive(true);
        }
    }

    public void AIGoal()
    {
        aiScore++;
        aiScoreText.text = "Npc AI: " + aiScore.ToString();

        int newAiScore = Convert.ToInt32(playerScoreText);

        if ((newAiScore == 7) && (playerController.difficulty == 0.5))
        {
            LoseScreen.SetActive(true);
        }


        else if ((newAiScore == 5) && (playerController.difficulty == 0.7))
        {
            LoseScreen.SetActive(true);
        }


        else if ((newAiScore == 3) && (playerController.difficulty == 1))
        {
            LoseScreen.SetActive(true);
        }
    }
}
