using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Fields

    // statics
    public GameManager gameManager; //creates gamemanager object to hold values

    public static int state = 0; // 0 = main menu, 1 = in game, 2 = game over

    static GameManager gm; //creates a static gamemanage that can hold values from one level to the next

    int counter = 2; //used in showhidePanel() to change to active state

    [Header("Buttons")]
    [Tooltip("restartGame button in inspector pairs with restartGame button on UI to add functionality")]
    public Button restartGame;
    [Tooltip("resumeGame button in inspector pairs with resumeGame button on UI to add functionality")]
    public Button resumeGame;
    [Tooltip("mainMenu button in inspector pairs with mainMenu button on UI to add functionality")]
    public Button mainMenu;
    [Tooltip("quitGame button in inspector pairs with quitGame button on UI to add functionality")]
    public Button quitGame;

    [Header("Panels")]
    [Tooltip("Panel GameObject in inspector pairs with a chosen UI panel to add make active/hide functionality")]
    public GameObject Panel;
    [Tooltip("VictoryPanel GameObject in inspector pairs with a chosen UI panel to add make active/hide functionality")]
    public GameObject VictoryPanel;
    [Tooltip("DeathPanel GameObject in inspector pairs with a chosen UI panel to add make active/hide functionality")]
    public GameObject DeathPanel;

    // make your canvas active from a disables state by calling this method
    public void MakeActive()
    {
        Panel.gameObject.SetActive(true);
    }

    #endregion

    #region Engine Events



    private void Awake()
    {
        // initialize with only main menu active and all other panels hidden

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
            if (Input.GetKeyDown(KeyCode.P)) //P brings up pause UI
            {
                if ((resumeGame != null)&& (Time.timeScale == 1))
                {
                    resumeGame.gameObject.SetActive(true);
                    mainMenu.gameObject.SetActive(true);
                    quitGame.gameObject.SetActive(true);
                    Time.timeScale = 0;
                }


                else if ((resumeGame != null) && (Time.timeScale == 0))  
                {
                    Input.GetKeyDown(KeyCode.P); //P hides pause UI

                    showhidePanel();

                    Time.timeScale = 1;

                }

                if (restartGame != null)
                {
                    restartGame.gameObject.SetActive(true);
                }
                if (quitGame != null)
                {
     
                    Quit();
                }

            }
        }
    }

    #endregion

    #region Methods

    public void hidePanel() //hides a panel from an active state
    {
        Panel.gameObject.SetActive(false);
    }

    public void showhidePanel()
    {

        
        if (counter % 2 == 1) // if odd number
        {
            Panel.gameObject.SetActive(false); //hides panel if active
            counter--;

        }
        else
        {
            Panel.gameObject.SetActive(true); //sets active if hidden
            counter++;
        }
        
    }

    public void Choice() //allows for a panel to be selected in inspector to be active or hidden
    {
        Panel.gameObject.SetActive(true);
    }

    #endregion

    #region UI Callbacks

    public void RestartGame() //reload the game at level 1
    {
        state = 1;
        if (restartGame != null)
        {
            SceneManager.LoadScene("Level1");
        }
    }

    public void ResumeGame() //closes pause menu and resume game
    {
         Panel.gameObject.SetActive(false);
         Time.timeScale = 1;
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu"); //go back to main menu screen
    }

    public void Quit() //press Q to quit the game
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadLevel1() //loads the first level of the game with all default values
    {
        SceneManager.LoadScene("Level1");
    }

    public void LoadLevelbyName(string levelName) //allows for the selections of what level to load into on inspector
    {
        SceneManager.LoadScene(levelName);
    }

    #endregion
}
