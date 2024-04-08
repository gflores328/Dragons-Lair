using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; //for scene changing
using TMPro; //for text mesh pro
using UnityEngine.UI; //for basic ui

public class AsyncLoader : MonoBehaviour
{
    private bool sceneLoaded = false; //check if you have asked for the scene to be loaded
    public string sceneToLoad; //name of scene to load
    [SerializeField]
    private TextMeshProUGUI loadingText; //progress text
    [SerializeField]
    private TextMeshProUGUI spaceText; //press spacebar text
    [SerializeField]
    private TextMeshProUGUI controllerText; //press spacebar text
    [SerializeField]
    private Slider sliderBar; //slider for progress
    public GameObject loadingScreen;
    // Start is called before the first frame update
    
    private GameObject gameState; // The game object that will hold the GameState object
    private Scene scene; // A variable to hold the current scene

    private GameManager gameManager;

    
    void Start()
    {
        
        spaceText.enabled = false; //hide the spacebar text 
        controllerText.enabled = false;
        scene = SceneManager.GetActiveScene(); // scene is set to the active scene
        gameManager = GetComponent<GameManager>(); // Gets the gamemanger script
        gameState = GameObject.Find("GameState"); // gameState is set to find GameState
    }

    


    public void runAsync()
    {
        if (!sceneLoaded)
        {
            loadingScreen.SetActive(true);
            sceneLoaded = true;

            if (gameState != null && gameState.GetComponent<GameState>().GetFirstTimeLoad()) 
            {
                sceneToLoad = "IntroCutscene";
                gameState.GetComponent<GameState>().SetFirstTimeLoad(false);
            }

            if (scene.name == "LevelDesignRealLife")
            {
                // First time load is set to false and the position of Player is stored
                gameState.GetComponent<GameState>().SetPlayerPosition(GameObject.Find("Player").transform.position);
            }

            if (scene.name == "ForestTwo" && sceneToLoad == "LevelDesignRealLife")
            {
                gameState.GetComponent<GameState>().storyState = GameState.state.Level1Complete;
                gameState.GetComponent<GameState>().objective = "Talk to the manager about the machine";
            }

            if (scene.name == "CaveTwo" && sceneToLoad == "LevelDesignRealLife")
            {
                gameState.GetComponent<GameState>().storyState = GameState.state.Level2Complete;
                gameState.GetComponent<GameState>().objective = "Talk to Michael";
            }

            

            if (sceneToLoad == "WinScreen")
            {
                gameState.GetComponent<GameState>().storyState = GameState.state.end;
                gameState.GetComponent<GameState>().objective = "End of Demo";
            }

            StartCoroutine(LoadNewScene(sceneToLoad));

            // If the current scene is LevelDesignRealLife
            
        }
    }
    IEnumerator LoadNewScene(string sceneName)
    {

        AsyncOperation async = SceneManager.LoadSceneAsync(sceneName); //begins async operation
        async.allowSceneActivation = false; //makes it so it doesn't auto transition to next scene

        while (!async.isDone) //while async < .9 ASYNC ONLY LOADS FROM 0- 0.9
        {
            float progress = Mathf.Clamp01(async.progress / 0.9f); //fancy math to get your progress in a nice number
            sliderBar.value = progress; //apply it to your slider
            loadingText.text = "Loading: " + (progress * 100f) + "%"; //print it to your text
            
            if (async.progress >= 0.9f) //if scene is "fully" loaded
            {
                if(!gameManager.GetIsMouse())
                {
                    controllerText.enabled = true;
                }
                else
                {
                    spaceText.enabled = true;
                }
                if (Input.GetButtonDown("Jump")) //if space is pressed
                {
                    async.allowSceneActivation = true; //allow scene transition
                    //loadingScreen.SetActive(false);
                }
            }
            yield return null;
        }
        
    }
}
