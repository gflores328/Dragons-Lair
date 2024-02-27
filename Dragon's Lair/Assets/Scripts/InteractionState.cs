/*
 * Created By: Gabriel Flores
 * 
 * This script when attached to on object will check the current game state and determine if the object should be active or not
 * It will take 2 enem state values and the object will exist while between those two values
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionState : MonoBehaviour
{
    public GameState.state spawn;
    public GameState.state despawn;
    public GameObject makeActive;
    private GameObject gameState;

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.Find("GameState");

        if (((int)gameState.GetComponent<GameState>().storyState) >= ((int)spawn) && ((int)gameState.GetComponent<GameState>().storyState) < ((int)despawn))
        {
            Debug.Log("Spawned " + gameObject);

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

        if (((int)gameState.GetComponent<GameState>().storyState) >= ((int)despawn))
        {
            Debug.Log("Destoryed " + gameObject);
            makeActive.SetActive(true);
            Destroy(gameObject);
        }
        
    }
}
