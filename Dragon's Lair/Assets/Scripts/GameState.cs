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
    [HideInInspector]
    // An enem that holds values that represent the current state of the game
    public enum state 
    {
        newGame,
        talkToDavid,
        PlayDDR,
        DDRComplete,
        getIntoBasement,
        unlockDoor,
        PlayVR,
        Level1Complete,
        talkToMicheal,
        playSpaceGame,
        SpaceGameDone,
        givePrize,
        playLevel2,
        Level2Complete
    } 


    [HideInInspector]
    public List<string> nonRespawnable; // A list of object names that wont load when scene is loaded once they are added
    [HideInInspector]
    public state storyState; // An instance of the state enum
    [HideInInspector]
    public string objective; // a string that reresents the text for the objective UI


    // Start is called before the first frame update
    void Start()
    {
        objective = "Talk to the Manager";
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

    public void UpdateObjective(string newObjective)
    {
        objective = newObjective;

        if (objective == "Talk to David")
        {
            
            storyState = state.talkToDavid;
        }

        if (objective == "Score over 20,000 points in BBB")
        {
            storyState = state.PlayDDR;
        }

        if (objective == "Get into the basement")
        {
            storyState = state.getIntoBasement;
        }

        if (objective == "Unlock basement door")
        {
            storyState = state.unlockDoor;
        }

        if (objective == "Play VR Game")
        {
            storyState = state.PlayVR;
        }

        if (objective == "Talk to Michael")
        {
            storyState = state.talkToMicheal;
        }

        if (objective == "Earn tickets in Mobile Fighter Axiom")
        {
            storyState = state.playSpaceGame;
        }

        if (objective == "Give the prize to Micheal")
        {
            storyState = state.givePrize;
        }

        Debug.Log(storyState);
    }
}
