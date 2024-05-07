using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;


// GameManager 
// Created by Jo Marie and Aaron Torres
// Controles the pause function and the level switching by using
public class GameManager : MonoBehaviour
{
    #region Fields
    [Header("What game is this instance of Game Manager in")]

    public bool inRealLife = false;
    public bool inCyber = false;
    

    

    [Header("UI Components Needed")]
    
    public GameObject pauseMenu; // The pause menu used to turn it active and not active

    public GameObject resumeButton; // Grabs the resume button so when paused the event system knows to select it first for the controller;

    //public GameObject controlsPanel;

    public GameObject controlsKeyboard;

    public GameObject controlsGamepad;

    private AsyncLoader asyncLoader;
    
    public bool mainMenu = false;

    private bool isMouse = true; // A bool variable to see if the mouse is connected or the controller

    private bool usingMouseRotation = true; // A bool variable that is changed in the game manager that informs the chibi arm movement if the player is using keyboard or mouse so you can toggle it on and off in settings
    [HideInInspector] public pauseState currentState; // A variable that is type of the enum created pause state
    

    [Header("Mouse Images")]

    public Texture2D targetTexture;
    public Texture2D magGlassTexture;
    public enum pauseState // pauseState Enum that has paused or unpaused states
    {
        Paused,
        Unpaused,
    }
    
    

    #endregion

    #region Engine Events



    private void Awake()
    {
       
       
        asyncLoader = GetComponent<AsyncLoader>();
        Time.timeScale = 1;


    }

    void Start()
    {
        currentState = pauseState.Unpaused; // The Game should not start paused when loaded in

        if(inRealLife && inCyber)
        {
            Debug.Log("inRealLife and InCyber cannot both be true fix it");
        }

        if(inRealLife)
        {
            Cursor.SetCursor(magGlassTexture, Vector2.zero, CursorMode.Auto);

        }
        else if(inCyber)
        {
            Cursor.SetCursor(targetTexture, Vector2.zero, CursorMode.Auto);
            Cursor.visible = false;
        }

    
    }

    void Update()
    {
        // Check if any of the devices are Gamepads
        bool isControllerConnected = Gamepad.current != null;
        
        if(!isControllerConnected)
        {
            controlsKeyboard.SetActive(true);
            controlsGamepad.SetActive(false);
        }
        // If a controller is connected and it was previously using mouse, switch to controller
        if (isControllerConnected && isMouse)
        {
            var gamepad = Gamepad.current;
            isMouse = false; // Switch to controller
            Cursor.visible = false; // Hide the cursor
            //controlsPanel.SetActive(true);
            controlsKeyboard.SetActive(false);
            controlsGamepad.SetActive(true);

            if(mainMenu)
            {
                SetButtonForController(resumeButton);
                Cursor.visible = true;
            }
        }
        // If no controller is connected and it was previously using controller, switch to mouse
        else if (!isControllerConnected && !isMouse)
        {
            var keyboard = Keyboard.current;
            var mouse = Mouse.current;
            isMouse = true; // switch to mouse
            Cursor.visible = true; // Hide the cursor
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null);

            //controlsPanel.SetActive(true);
            controlsKeyboard.SetActive(true);
            controlsGamepad.SetActive(false);

        }
        else if(inCyber)
        {
            if(currentState == pauseState.Paused)
            {
                Cursor.visible = true;
            }
            else if(currentState == pauseState.Unpaused)
            {
                Cursor.visible = false;
            }
            
        }

        
    }



    #endregion

    #region Methods


    public void hidePanel(GameObject currentPanel) //hides a panel from an active state
    {
        currentPanel.SetActive(false); 
    }

    public void showPanel(GameObject currentPanel) //Shows the panel by making an active state
    {
        currentPanel.SetActive(true);
        
    }

    
    #endregion

    #region UI Callbacks

    

    

    public void PauseGame() // Pause game function that switches between pause and unpause
    {
        if(currentState == pauseState.Unpaused) // If the game is unpaused flip it and pause the game.
        {
            //Cursor.lockState = CursorLockMode.None;
            showPanel(pauseMenu); //Show the pause menu
            Time.timeScale = 0; // Pause the game by setting time to 0
            currentState = pauseState.Paused; // Change the current state to paused
            if(!isMouse)
            {
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(resumeButton); // Select the top button when paused if a controller is connected
            }
            
        }
        else if(currentState == pauseState.Paused)
        {
            //Cursor.lockState = CursorLockMode.Locked;
            hidePanel(pauseMenu); // Hide the pause menu
            Time.timeScale = 1; // Resume the game time
            currentState = pauseState.Unpaused; // Set the current state to unpaused
        }
        
    }

    

    public void Quit() //Function to call to quit the game
    {
        Application.OpenURL("https://forms.gle/oxiWQY4zmdjgEtv89");
        Application.Quit();
        
        
    }

    

    public void LoadLevelbyName(string levelName) //A function that allows the loading of the level by the name of the level usinge normal load scene
    {
        SceneManager.LoadScene(levelName);
    }

    public void LoadSceneAsync(string sceneName) // A function that allows a loading screen before loading the level
    {
        if (asyncLoader != null) // check there is an asyncLoader 
        {
            // Set the scene to load in the asyncLoader
            asyncLoader.sceneToLoad = sceneName;
            // Enable the asyncLoader to start loading the scene
            asyncLoader.enabled = true;
            asyncLoader.runAsync(); // calls the runAsync function from the asycnLoaderScript
        }
        else
        {
            Debug.LogError("AsyncLoader reference not set in GameManager!"); // Deploy Error message To he console
        }
    }

    public void SetButtonForController(GameObject gameObject)
    
    {
        if(!isMouse)

        {
            UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(gameObject);
        }
    }
    
    public void setMainMenuBool(bool newBool)
    {
        mainMenu = newBool;
    }
    #endregion

    public bool GetIsMouse()
    {
        return isMouse;
    }

    public bool GetusingMouseRotation()
    {
        return usingMouseRotation;
    }

    public void SetusingMouseRotation(bool isUsing)
    {
        usingMouseRotation = isUsing;
    }
}