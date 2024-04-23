using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpaceManager : MonoBehaviour
{
    public TextMeshProUGUI score;
    public GameObject winScreen;
    public Item prize;
    public AudioClip victorySound;
    public AudioSource victoryAudioSource;

    private GameObject gameState;
    private bool hasPlayedVictorySound = false;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.Find("GameState");
    }

    // Update is called once per frame
    void Update()
    {
        if (score.text == "Score: 3000" && !hasPlayedVictorySound)
        {
            Time.timeScale = 0;
            winScreen.SetActive(true);

            if (victoryAudioSource != null && victorySound != null)
            {
                Debug.Log("Playing victory sound.");
                victoryAudioSource.clip = victorySound;
                victoryAudioSource.Play();
            }

            if (gameState != null)
            {
                if (((int)gameState.GetComponent<GameState>().storyState) < ((int)GameState.state.SpaceGameDone))
                {
                    gameState.GetComponent<GameState>().storyState = GameState.state.SpaceGameDone;
                    gameState.GetComponent<GameState>().objective = "Turn in tickets for the prize";

                    GameObject.Find("Inventory").GetComponent<Inventory>().AddItem(prize);
                }
            }

            hasPlayedVictorySound = true;
        }
    }
}
