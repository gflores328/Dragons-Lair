using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;

public class AH_ScoreManager : MonoBehaviour
{
    public AH_PlayerController playerController;

    public GameManager gameManager;

    public GameObject WinScreen;

    public GameObject LossScreen;

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

        

        if ((aiScore == 7) && (playerController.mode == "easy"))
        {
            LossScreen.SetActive(true);
        }


        else if ((aiScore == 5) && (playerController.mode == "intermidate"))
        {
            LossScreen.SetActive(true);
        }


        else if ((aiScore == 3) && (playerController.mode == "hard"))
        {
            LossScreen.SetActive(true);
        }
    }
}
