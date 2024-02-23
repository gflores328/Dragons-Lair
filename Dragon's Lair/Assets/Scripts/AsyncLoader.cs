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
    private Slider sliderBar; //slider for progress
    public GameObject loadingScreen;
    // Start is called before the first frame update

    private GameObject gameState; // The game object that will hold the GameState object
    private Scene scene; // A variable to hold the current scene
    void Start()
    {
        
        spaceText.enabled = false; //hide the spacebar text 

        scene = SceneManager.GetActiveScene(); // scene is set to the active scene
        gameState = GameObject.Find("GameState"); // gameState is set to find GameState
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void runAsync()
    {
        if (!sceneLoaded)
        {
            loadingScreen.SetActive(true);
            sceneLoaded = true;
            StartCoroutine(LoadNewScene(sceneToLoad));

            // If the current scene is LevelDesignRealLife
            if (scene.name == "LevelDesignRealLife")
            {
                // First time load is set to false and the position of Player is stored
                gameState.GetComponent<GameState>().SetFirstTimeLoad(false);
                gameState.GetComponent<GameState>().SetPlayerPosition(GameObject.Find("Player").transform.position);
            }
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
                spaceText.enabled = true; //enable space text
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
