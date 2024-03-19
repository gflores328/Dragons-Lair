using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
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


    public void PlayerGoal()
    {
        playerScore++;
        playerScoreText.text = "Owen: " + playerScore.ToString();

        int newPlayerScore = Convert.ToInt32(playerScoreText);


        if ((newPlayerScore == 7) && (playerController.difficulty <= 1))
        { 
            gm.LoadLevelbyName("WinScene");
        }


        else if ((newPlayerScore == 5) && (playerController.difficulty == 3))
        {
            gm.LoadLevelbyName("WinScene");
        }


        else if ((newPlayerScore == 3) && (playerController.difficulty == 5))
        {
            gm.LoadLevelbyName("WinScreen");
        }
    }

    public void AIGoal()
    {
        aiScore++;
        aiScoreText.text = "Npc AI: : " + aiScore.ToString();
    }
}
