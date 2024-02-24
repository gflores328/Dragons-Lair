/*
 * Created By: Gabriel Flores
 * 
 * This script will be attached to an object and will be used to see if it should respawn or not by checking the Game State object
 * and seeing if the item has been deleted or not
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontRespawn : MonoBehaviour
{
    private GameObject gameState;

    // Start is called before the first frame update
    void Start()
    {
        // Finds the game state object
        gameState = GameObject.Find("GameState");

        // Foreach string in the game states non respawnable list
        foreach (string dontRespawn in gameState.GetComponent<GameState>().nonRespawnable)
        {
            // if the string is the name of the game obect then it destroys itself
            if (dontRespawn == gameObject.name)
            {
                Destroy(gameObject);
            }
        }
    }

}
