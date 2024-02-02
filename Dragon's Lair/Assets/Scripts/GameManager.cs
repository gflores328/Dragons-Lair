using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.AI;
using System;
using System.Text;
using TMPro;

public class GameManager : MonoBehaviour
{
	#region Fields

    [Header("Connections")]

    [Header("Buttons")]
    public Button restartGame;
    public Button resumeGame;
    public Button mainMenu;
    public Button quitGame;

    [Header("Meters")]

    public int Number;

    public GameManager gameManager;

    // statics
    public static int state = 0; // 0 = main menu, 1 = in game, 2 = game over

    static GameManager gm;

    int counter;

    public GameObject Panel;

    public GameObject ChoicePanel;

    public GameObject VictoryPanel;

    public GameObject DeathPanel;

    public GameObject canvasObject; // drag your canvas object to this variable in the editor
                                    // make your canvas active from a disables state by calling this method
    public void MakeActive()
    {
        canvasObject.SetActive(true);
    }

    #endregion

    #region Engine Events



    private void Awake()
    {
        // init

        if (restartGame != null) { restartGame.gameObject.SetActive(false); }
        if (resumeGame != null) { resumeGame.gameObject.SetActive(false); }
        if (quitGame != null) { quitGame.gameObject.SetActive(false); }
        if (mainMenu != null) { mainMenu.gameObject.SetActive(true); }

        gm = this;
        Time.timeScale = 1;

        if (restartGame == null)
        {
            RestartGame();
        }

    }

    void Start()
    {


    }

    private void Update()
    {
        if (state == 1)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (resumeGame != null)
                {
                    MakeActive();
                    resumeGame.gameObject.SetActive(true);
                    Time.timeScale = 0;
                }

                if (restartGame != null)
                {
                    restartGame.gameObject.SetActive(true);
                }

                if (resumeGame != null)
                {
                    resumeGame.gameObject.SetActive(true);
                }

                if (quitGame != null)
                {
                    quitGame.gameObject.SetActive(true);
                }

            }
        }
    }

    #endregion

    #region Methods

    public void hidePanel()
    {
        Panel.gameObject.SetActive(false);
    }

    public void showhidePanel()
    {
        
        counter++;
        if (counter % 2 == 1) // if odd number
        {
            Panel.gameObject.SetActive(false);
        }
        else
        {
            Panel.gameObject.SetActive(true);
        }
        
    }

    public void Choice()
    {
        Panel.gameObject.SetActive(true);
    }

    #endregion

    #region UI Callbacks

    public void RestartGame()
    {
        state = 1;
        if (restartGame != null)
        {
            SceneManager.LoadScene("Level1");
        }
    }

    public void ResumeGame()
    {

        canvasObject.SetActive(false);
        Time.timeScale = 1;

    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("Level1");
    }
    public void LoadLevelbyName(string levelName)
    {
        SceneManager.LoadScene(levelName);
    }

    #endregion
}
