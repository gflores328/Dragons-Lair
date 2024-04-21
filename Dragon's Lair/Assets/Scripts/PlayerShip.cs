/*
 * Created by Carlos Martinez
 * 
 * This script contains code for the Player Ship in the arcade
 * minigame, Mobile Fighter Axiom.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShip : MonoBehaviour
{
    public float speed = 5.0f; // Movement Speed

    private PlayerInput playerInput; // A private variable that is meant to grab the PlayerInput component that is attached to the player ship.
    private InputAction moveAction; // The variable of type input action that holds the move action
    
    void Awake ()
    {
        playerInput = GetComponent<PlayerInput>(); //  grabs the player input component from the player ship object
        moveAction = playerInput.actions.FindAction("Move"); // finds the move action in the player input mapping
    }
    
    private void OnEnable()
    {
        moveAction.Enable(); // when object is enabled enable the move action
    }

    private void OnDisable()
    {
        moveAction.Disable(); // when object is disabled disable the move action
    }

    private void Update()
    {
        MovePlayerShip(); // calls the move player ship script every frame
    }

    
    private void MovePlayerShip() // The function that moves the player
    {
        Vector2 direction = moveAction.ReadValue<Vector2>(); // the value that determines which button the player is pressing

        // Player Movement (Left and Right)
        Vector3 movement = new Vector3(direction.x, 0f, 0f); // Only using X-axis for movement
        transform.position += movement * speed * Time.deltaTime; // Moves the player by movement position + speed over time
    }
}

