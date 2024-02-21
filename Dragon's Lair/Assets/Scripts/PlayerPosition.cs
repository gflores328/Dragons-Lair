/*
 * Created By: Gabriel Flores
 * 
 * This script is responsible for setting the position of the player
 * It will be attached to the irl player object
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosition : MonoBehaviour
{
    public GameObject gameState; // A variable to hold the game state object

    // Start is called before the first frame update
    void Start()
    {
        // gameState is set to find GameState
        gameState = GameObject.Find("GameState");

        // If firstTimeLoad is false
        if (!gameState.GetComponent<GameState>().GetFirstTimeLoad())
        {
            // This game objects position is set to the position stored in GameState
            gameObject.GetComponent<CharacterController>().enabled = false;
            gameObject.transform.position = gameState.GetComponent<GameState>().GetPlayerPosition();
            gameObject.GetComponent<CharacterController>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
