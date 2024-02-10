using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Fields

    // statics
    //public GameManager gameManager; //creates gamemanager object to hold values

    

    [Header("Panels")]
    [Tooltip("Panel GameObject in inspector pairs with a chosen UI panel to add make active/hide functionality")]
    public GameObject pauseMenu;
    // public GameObject HowToMenu;
    // public GameObject CreditsMenu;
    
    private pauseState currentState;
    public enum pauseState
    {
        Paused,
        Unpaused,
    }
    // make your canvas active from a disables state by calling this method
    

    #endregion

    #region Engine Events



    private void Awake()
    {
        // initialize with only main menu active and all other panels hidden

    
        Time.timeScale = 1;


    }

    void Start()
    {
        currentState = pauseState.Unpaused;

    }

    private void Update()
    {
        // if (state == 1)
        // {
        //     if (Input.GetKeyDown(KeyCode.P)) //P brings up pause UI
        //     {
        //         if ((resumeGame != null)&& (Time.timeScale == 1))
        //         {
        //             // resumeGame.SetActive(true);
        //             // mainMenu.SetActive(true);
        //             // quitGame.SetActive(true);
        //             Time.timeScale = 0;
        //         }


        //         else if ((resumeGame != null) && (Time.timeScale == 0))  
        //         {
        //             Input.GetKeyDown(KeyCode.P); //P hides pause UI

        //             showhidePanel();

        //             Time.timeScale = 1;

        //         }

        //         if (restartGame != null)
        //         {
        //             restartGame.SetActive(true);
        //         }
        //         if (quitGame != null)
        //         {
     
        //             Quit();
        //         }

        //     }
        // }
    }

    #endregion

    #region Methods
    

    public void hidePanel(GameObject currentPanel) //hides a panel from an active state
    {
        currentPanel.SetActive(false);
    }

    public void showPanel(GameObject currentPanel)
    {
        currentPanel.SetActive(true);
        
    }

    
    #endregion

    #region UI Callbacks

    

    

    public void PauseGame()
    {
        if(currentState == pauseState.Unpaused)
        {
            showPanel(pauseMenu);
            Time.timeScale = 0;
            currentState = pauseState.Paused;
        }
        else if(currentState == pauseState.Paused)
        {
            hidePanel(pauseMenu);
            Time.timeScale = 1;
            currentState = pauseState.Unpaused;
        }
        
    }

    

    public void Quit() //press Q to quit the game
    {
       
        Application.Quit();
        
    }

    

    public void LoadLevelbyName(string levelName) //allows for the selections of what level to load into on inspector
    {
        SceneManager.LoadScene(levelName);
    }

    #endregion
}