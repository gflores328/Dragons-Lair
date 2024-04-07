using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
using TMPro;

public class AH_ScoreManager : MonoBehaviour
{
    public AH_PlayerController playerController;

    public AH_SFX sfx;

    public GameManager gameManager;

    public GameObject WinScreen;

    public GameObject LossScreen;

    public GameObject PauseMenu;

    public GameObject StartMenu;

    public Button easy;

    public Button intm;

    public Button hard;

    private int playerScore = 0;
    private int aiScore = 0;

    public TextMeshProUGUI playerScoreText;
    public TextMeshProUGUI aiScoreText;

    private GameObject gameState;

    private void Start()
    {
        gameState = GameObject.Find("GameState");
    }
    public void PlayerGoal()
    {
        playerScore++;
        playerScoreText.text = playerScore.ToString();


        if ((playerScore == 7) && (playerController.mode == "easy"))
        {
            sfx.PlayYouWin();
            WinScreen.SetActive(true);

            if (gameState != null && gameState.GetComponent<GameState>().storyState < GameState.state.BeatSarah)
            {
                gameState.GetComponent<GameState>().storyState = GameState.state.FindMicheal;
                gameState.GetComponent<GameState>().objective = "Ask Michael if the machine is fixed";
            }
        }


        if ((playerScore == 5) && (playerController.mode == "intermidate"))
        {
            sfx.PlayYouWin();
            WinScreen.SetActive(true);
        }


        if ((playerScore == 3) && (playerController.mode == "hard"))
        {
            sfx.PlayYouWin();
            WinScreen.SetActive(true);
        }
    }

    public void AIGoal()
    {
        aiScore++;
        aiScoreText.text = aiScore.ToString();

        

        if ((aiScore == 7) && (playerController.mode == "easy"))
        {
            sfx.PlayYouLose();
            LossScreen.SetActive(true);
        }


        if ((aiScore == 5) && (playerController.mode == "intermidate"))
        {
            sfx.PlayYouLose();
            LossScreen.SetActive(true);
        }


        if ((aiScore == 3) && (playerController.mode == "hard"))
        {
            sfx.PlayYouLose();
            LossScreen.SetActive(true);
        }
    }
}
