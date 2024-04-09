using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberCheatCodes : MonoBehaviour
{
    public GameObject endOfLevelPoint; // A public gameObject to hold the spot where to teleport the player at the end of the level
    
    private ChibiPlayerMovement chibiPlayerMovement; // A variable that will grab the chibi player movement script
    private bool infiniteHealth = false; // A bool to activate infinite health

    public GameObject player; // a game object that holds the player 

    void Start() 
    {
        chibiPlayerMovement = FindObjectOfType<ChibiPlayerMovement>(); // Grab the players script
    }
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha8)) // if the number 1 is pressed
        {
            infiniteHealth = !infiniteHealth; // toggle infite health on or off
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha9)) // if the number 2 is pressed
        {
            if (endOfLevelPoint != null) // Check if endOfLevelPoint is not null
            {
                player.transform.position = endOfLevelPoint.transform.position; // put the player at the end point
            }
        }
        
        if(infiniteHealth) // If infinite health bool is true
        {
            ChibiPlayerMovement.playerHealth = chibiPlayerMovement.maxHealth; // Make the players health always maxed
        }
    }
}
