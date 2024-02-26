/*
 * Created By: Gabriel Flores
 * 
 * This is a script that will update / change the Interact date of the David game object depending on the game state
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class David2State : MonoBehaviour
{
    private GameObject gameState; // A reference to the game state object

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.Find("GameState");

        if (gameState.GetComponent<GameState>().storyState == GameState.state.DDRComplete)
        {
            gameObject.SetActive(true);
        }
        else
        {
            gameObject.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}