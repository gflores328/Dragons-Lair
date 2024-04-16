/*
 * Created By: Gabriel Flores
 * 
 * This script will check to see if an interaction point has been interacted with before and will then change its world icon color
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrayOut : MonoBehaviour
{
    public Sprite gray; // This will hold the grayed out icon image

    private GameObject gameState; // a reference to the game state

    // Start is called before the first frame update
    void Start()
    {
        gameState = GameObject.Find("GameState");

        if (gameState != null)
        {
            foreach (string i in gameState.GetComponent<GameState>().interactedWith)
            {
                if (transform.parent.parent.gameObject.name == i)
                {
                    gameObject.GetComponent<Image>().sprite = gray;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Change()
    {
        gameObject.GetComponent<Image>().sprite = gray;
    }


}
