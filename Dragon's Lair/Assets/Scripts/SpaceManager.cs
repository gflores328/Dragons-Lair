using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpaceManager : MonoBehaviour
{

    public TextMeshProUGUI score;
    public GameObject winScreen;

    private GameObject gameState;


    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.Find("GameState");    
    }

    // Update is called once per frame
    void Update()
    {
        if (score.text == "Score: 5000")
        {
            gameState.GetComponent<GameState>().storyState = GameState.state.SpaceGameDone;
            gameState.GetComponent<GameState>().objective = "Turn in tickets for the prize";
            winScreen.SetActive(true);
            Time.timeScale = 0;
        }
    }
}
