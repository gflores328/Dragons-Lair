using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AaronGameManager : MonoBehaviour
{
    private bool ButtonClicked =  false;
    //private bool quitButtonClicked = false;
    //private bool settingButtonClicked = false;
    private bool settingsOn = false;
    private bool howToOn = false;
    private bool creditsOn = false;
    private bool pauseOn = false;
    private string sceneToLoad;

    private bool gameIsPaused;
    
    public GameObject mainMenu;
    public GameObject HowToMenu;
    public GameObject CreditsMenu;

    public GameObject pauseMenu;
    

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape)) // If Escaped preseed pause game.
        {
            //gameIsPaused = !gameIsPaused;

            PauseGame();
        }
        if(!pauseOn)
        {

        }
        if(ButtonClicked) // Checks to see if the button has been pressed 
        {
            StartCoroutine(AsyncLoadScene()); // runs IEnumerator AsyncLoad
            ButtonClicked = false; // sets button to false to say it isn't being pressed
        }

    }

    IEnumerator AsyncLoadScene() // Load the sceneToload asynchronously
    {
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneToLoad);
        
        while(!asyncLoad.isDone) // Wait until the scene is fully loaded
        {
            yield return null;
        }
    }

    public void setSceneToLoad(string sceneName)
    {
        
        sceneToLoad = sceneName; // sets the bariable scene to load with the name of the scene name
        ButtonClicked = true;

    }
    
    public void quitGame()
    {
        Application.Quit();
    }
    // public void controlSettingsMenu()
    // {
    //     settingsOn = !settingsOn;
    //     settingsMenu.SetActive(settingsOn);
    //     mainMenu.SetActive(!settingsOn);

    // }
    public void controlHowToMenu()
    {
        howToOn = !howToOn;
        HowToMenu.SetActive(howToOn);
        mainMenu.SetActive(!howToOn);

    }
    public void controlCredits()
    {
        creditsOn = !creditsOn;
        CreditsMenu.SetActive(creditsOn);
        mainMenu.SetActive(!creditsOn);   
    }
    public void loadChosenScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void PauseGame()
    {
        gameIsPaused = !gameIsPaused;
        pauseMenu.SetActive(gameIsPaused); //display or undisplay pause menu
        if(gameIsPaused)
        {
            Time.timeScale = 0f; // freezes time 
            
           
        }
        else {
            Time.timeScale = 1; // sets time back to normal scale
            
        }
        

    }
}
