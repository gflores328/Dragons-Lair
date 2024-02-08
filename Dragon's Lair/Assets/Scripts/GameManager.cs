using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Fields

    // statics
    public GameManager gameManager; //creates gamemanager object to hold values

    

    [Header("Panels")]
    [Tooltip("Panel GameObject in inspector pairs with a chosen UI panel to add make active/hide functionality")]
    public GameObject mainMenu;
    public GameObject HowToMenu;
    public GameObject CreditsMenu;
    

    // make your canvas active from a disables state by calling this method
    public void MakeActive(GameObject currentPanel)
    {
        currentPanel.gameObject.SetActive(true);
    }

    #endregion

    #region Engine Events



    private void Awake()
    {
        // initialize with only main menu active and all other panels hidden

    
        Time.timeScale = 1;


    }

    void Start()
    {
        

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

    public void showhidePanel(GameObject currentPanel)
    {

        
        // if (counter % 2 == 1) // if odd number
        // {
        //     currentPanel.SetActive(false); //hides panel if active
        //     counter--;

        // }
        // else
        // {
        //     currentPanel.SetActive(true); //sets active if hidden
        //     counter++;
        // }
        
    }

    
    #endregion

    #region UI Callbacks

    

    public void ResumeGame() //closes pause menu and resume game
    {
        
        Time.timeScale = 1;
    }

    

    public void Quit() //press Q to quit the game
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    

    public void LoadLevelbyName(string levelName) //allows for the selections of what level to load into on inspector
    {
        SceneManager.LoadScene(levelName);
    }

    #endregion
}
