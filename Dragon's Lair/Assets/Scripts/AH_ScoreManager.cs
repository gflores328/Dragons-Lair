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


        if ((playerScore == 5) && (playerController.mode == "easy"))
        {
            Cursor.visible = true;
            sfx.PlayYouWin();
            WinScreen.SetActive(true);
            

            if (gameState != null && gameState.GetComponent<GameState>().storyState < GameState.state.BeatSarah)
            {
                gameState.GetComponent<GameState>().storyState = GameState.state.BeatSarah;
                gameState.GetComponent<GameState>().objective = "Get VR Coin from Sarah";
            }
        }


        if ((playerScore == 7) && (playerController.mode == "intermidate"))
        {
            Cursor.visible = true;
            sfx.PlayYouWin();
            WinScreen.SetActive(true);
            

            if (gameState != null && gameState.GetComponent<GameState>().storyState < GameState.state.BeatSarah)
            {
                gameState.GetComponent<GameState>().storyState = GameState.state.BeatSarah;
                gameState.GetComponent<GameState>().objective = "Get VR Coin from Sarah";
            }
        }


        if ((playerScore == 3) && (playerController.mode == "hard"))
        {
            Cursor.visible = true;
            sfx.PlayYouWin();
            WinScreen.SetActive(true);
            

            if (gameState != null && gameState.GetComponent<GameState>().storyState < GameState.state.BeatSarah)
            {
                gameState.GetComponent<GameState>().storyState = GameState.state.BeatSarah;
                gameState.GetComponent<GameState>().objective = "Get VR Coin from Sarah";
            }
        }
    }

    public void AIGoal()
    {
        aiScore++;
        aiScoreText.text = aiScore.ToString();

        

        if ((aiScore == 7) && (playerController.mode == "easy"))
        {
            Cursor.visible = true;
            sfx.PlayYouLose();
            LossScreen.SetActive(true);

        }


        if ((aiScore == 5) && (playerController.mode == "intermidate"))
        {
            Cursor.visible = true;
            sfx.PlayYouLose();
            LossScreen.SetActive(true);
        }


        if ((aiScore == 3) && (playerController.mode == "hard"))
        {
            Cursor.visible = true;
            sfx.PlayYouLose();
            LossScreen.SetActive(true);
        }
    }
}
