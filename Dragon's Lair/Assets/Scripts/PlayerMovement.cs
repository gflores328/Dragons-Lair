using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Created by Aaron Torres
//Script Title: PlayerMovement
//Description: This script will be handling the player movement.

public class PlayerMovement : MonoBehaviour
{

    public float playerSpeedMultiplier = 3.0f; // A float variable that will deteremine how fast the player is moving.
    public bool inRealLife = true; // A public bool to determine if the player is in real life or the arcade chibi world used to restrict movement

    private PlayerInput playerInput; // A private variable that is meant to grab the PlayerInput component that is attached to the player.
    private InputAction walkAction; // A private variable that is meant to hold the move action so that its values can be accessed

    
    void Start()
    {
        playerInput = GetComponent<PlayerInput>(); // Grabs the Player Input compoent from the player and assigns it to the playerInput that was initalized above
        walkAction = playerInput.actions.FindAction("Walk"); // Searches for the action and stores it inside of the  walk action variable
        
       
    }

    
    void Update()
    {
        if (inRealLife && playerInput.currentActionMap.name != "RealLifeMovement") // Checks to see if the player is in real life and that the current action map is not already real life movement

        {

            SwitchActionMap("RealLifeMovement"); //Calls the switch action map function and switches it to real life

        }

        else if (!inRealLife && playerInput.currentActionMap.name != "ChibiMovement") // Checks to see if the player is in real life and that the current action map is not already in chibi movement

        {

            SwitchActionMap("ChibiMovement"); //Calls the switch action map function and switches it to ChibiMovement

        }

        RealLifeMovePlayer(); // Calls the RealLifeMovePlayer function
        ChibiMovePlayer(); // Calls the ChibiMovePlayer function
     
    }

    void SwitchActionMap(string actionMapName)
    {
        playerInput.SwitchCurrentActionMap(actionMapName);
        walkAction = playerInput.currentActionMap.FindAction("Walk");
    }

    //MovePlayer Function Description:
    //Designed to be the function that will actively move the player object in the game. allowing movement in all directions 
    void RealLifeMovePlayer()
    {
        Vector2 direction = walkAction.ReadValue<Vector2>(); // Creates a vector2 variable to assign and store the values of the walk action to be used to determine which way the player wants to move.
        transform.position += new Vector3(direction.x,0,direction.y) * Time.deltaTime * playerSpeedMultiplier; // Multiplies the values of the a new vector3 position time and the speed multiplier to make the player move.

    }

    //ChibiMovePlayer Function Description:
    //Designed to be the function that will actively move the player object in the game. but only in the x axis.
    void ChibiMovePlayer()
    {
        Vector2 direction = walkAction.ReadValue<Vector2>(); // Creates a vector2 variable to assign and store the values of the walk action to be used to determine which way the player wants to move.
        transform.position += new Vector3(direction.x,0,0) * Time.deltaTime * playerSpeedMultiplier; // Multiplies the values of the a new vector3 position time and the speed multiplier to make the player move.

    }
}
