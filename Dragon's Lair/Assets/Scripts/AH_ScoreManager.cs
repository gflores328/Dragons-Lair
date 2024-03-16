using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AH_ScoreManager : MonoBehaviour
{
    private int playerScore = 0;
    private int aiScore = 0;

    public Text playerScoreText;
    public Text aiScoreText;


    public void PlayerGoal()
    {
        playerScore++;
        playerScoreText.text = "Owen: " + playerScore.ToString();
    }

    public void AIGoal()
    {
        aiScore++;
        aiScoreText.text = "Npc AI: : " + aiScore.ToString();
    }
}
