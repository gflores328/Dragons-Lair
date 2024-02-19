using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    #region Fields

    

    

    [Header("UI Components Needed")]
    [Tooltip("Panel GameObject in inspector pairs with a chosen UI panel to add make active/hide functionality")]
    public GameObject pauseMenu; // The pause menu used to turn it active and not active
    public GameObject resumeButton; // Grabs the resume button so when paused the event system knows to select it first for the controller;
    public AsyncLoader asyncLoader;
    

    private bool isMouse = true;

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

    void Update()
    {
        // Check if any of the devices are Gamepads
        bool isControllerConnected = Gamepad.current != null;

        // If a controller is connected and it was previously using mouse, switch to controller
        if (isControllerConnected && isMouse)
        {
            isMouse = false;
            // Call a method or perform actions specific to controller input
            // For example:
            // SwitchToControllerInput();
            Debug.Log("Switched to controller input");
        }
        // If no controller is connected and it was previously using controller, switch to mouse
        else if (!isControllerConnected && !isMouse)
        {
            isMouse = true;
            Debug.Log("Switched to mouse input");
        }
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

    public void LoadSceneAsync(string sceneName)
    {
        if (asyncLoader != null)
        {
            // Set the scene to load in the asyncLoader
            asyncLoader.sceneToLoad = sceneName;
            // Enable the asyncLoader to start loading the scene
            asyncLoader.enabled = true;
            asyncLoader.runAsync();
        }
        else
        {
            Debug.LogError("AsyncLoader reference not set in GameManager!");
        }
    }

    #endregion
}