/*
 * Created By: Gabriel Flores
 * 
 * This script will contain all of the variables that will affect the state of the game
 * This script will be put on an object that wont be destroyed on load
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameState : MonoBehaviour
{
    private Vector3 playerPosition; // A variable to hold the position of the player
    private bool firstTimeLoad = true; // A bool to determine if the irl scene has been loaded before or not
    public enum state {newGame, Level1Complete, Level2Complete} // An enem that holds values that represent the current state of the game
    public List<string> nonRespawnable;
    public state storyState;


    // Start is called before the first frame update
    void Start()
    {
        storyState = state.newGame;
    }

    // Update is called once per frame
    void Update()
    {

    }

    // Set function for playerPosition
    public void SetPlayerPosition(Vector3 position)
    {
        playerPosition = position;
    }

    // Get function for playerPosition
    public Vector3 GetPlayerPosition()
    {
        return playerPosition;
    }

    // Get function for firstTimeLoad
    public bool GetFirstTimeLoad()
    {
        return firstTimeLoad;
    }

    // Set function for firstTimeLoad
    public void SetFirstTimeLoad(bool i)
    {
        firstTimeLoad = i;
    }

    // A function to add to the non respawnable list
    public void AddNonRespawnable(string dontRespawn)
    {
        nonRespawnable.Add(dontRespawn);
    }
}
