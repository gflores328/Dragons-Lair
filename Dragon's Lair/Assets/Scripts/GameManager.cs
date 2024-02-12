using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    #region Fields

    

    

    [Header("UI Components Needed")]
    [Tooltip("Panel GameObject in inspector pairs with a chosen UI panel to add make active/hide functionality")]
    public GameObject pauseMenu; // The pause menu used to turn it active and not active
    public GameObject resumeButton; // Grabs the resume button so when paused the event system knows to select it first for the controller;
    
    
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
       

    
        Time.timeScale = 1;


    }

    void Start()
    {
        currentState = pauseState.Unpaused;

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
            //Cursor.lockState = CursorLockMode.None;
            showPanel(pauseMenu);
            Time.timeScale = 0;
            currentState = pauseState.Paused;
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(resumeButton);
        }
        else if(currentState == pauseState.Paused)
        {
            //Cursor.lockState = CursorLockMode.Locked;
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