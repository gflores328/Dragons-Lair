using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

// PlayerOneWay
// Created by Aaron Torres
// This the script that if a platform is tagged as a oneway platform you can jump from the bottom of it and you can jump down
public class PlayerOneWay : MonoBehaviour
{
    
    public InputActionReference downAction; // This gets the down action that will require the user to press down and Jump at the same time
    [SerializeField] private Collider playerCollider; // Grabs the player collider from the inspector

    [HideInInspector]
    public bool isMovingDown = false; // A bool that will be used to stop jumping if the player is pressing down 


    private GameObject currentOneWayPlatform; // A game object that will hold the platform that the player is standing on
    

    

    
    
    private void OnEnable() // When Enabled enables the downaction
    {
        downAction.action.Enable(); // Enables the downaction
        downAction.action.performed += OnDownPerformed; // Assigns the function to the action

    }
    private void OnDisable() // When disabled disables the down action

    {
        downAction.action.Disable(); // Disables the down action
        downAction.action.performed -= OnDownPerformed; // Designs the function to the action

    }
    private void OnCollisionEnter(Collision collision) // When the player enters a collision 
    {
        if ( collision.gameObject.CompareTag("OneWayPlatform")) // Checks if it is a one way platform
        {
            currentOneWayPlatform = collision.gameObject; // Assigns the platform to the currentOneway platform
            //Debug.Log("Grabbed this");
        }
    }
    private void OnCollisionExit(Collision collision) // WHen the player is no longer colliding 
    {
        if ( collision.gameObject.CompareTag("OneWayPlatform")) // if it is leaving a one way platform
        {
            currentOneWayPlatform = null; // Assign the current one way platform to null
        }
    }
    private void OnTriggerEnter(Collider other) // There is a trigger for checking under a platform at the top of the player's head
    {
        if(other.gameObject.CompareTag("OneWayPlatform")) // if the trigger hits a one way platform 

        {
            currentOneWayPlatform = other.gameObject; // Assigns the current platform to the one the player is trying to jump on
            StartCoroutine(DisableCollision()); // Disables the collision by running the coroutine
        }
    }

    private void OnTriggerExit(Collider other) // When the trigger exits 
    {
        if(other.gameObject.CompareTag("OneWayPlatform"))

        {
            currentOneWayPlatform = null; // Set the current platform to null
        }
    }

    private void OnDownPerformed(InputAction.CallbackContext context) // This is the action that happens when pressing down and jump

    {

        isMovingDown = true; // sets moving down to true to stop jump
        if(currentOneWayPlatform != null) // makes sure there is a platform to go through

        {
            StartCoroutine(DisableCollision()); // Runs the coroutine to disable collision and allow the player to fall through
        }
        
    }

    
    private IEnumerator DisableCollision() // The disable collision ienumerator that runs to disable collision
    {
        Collider platformCollider = currentOneWayPlatform.GetComponent<Collider>(); // A collider object created to get the collider of the platform

        Physics.IgnoreCollision(playerCollider, platformCollider); // ignores the collision between player and platform 
        yield return new WaitForSeconds(1.5f); // Waits a set amount of time
        Physics.IgnoreCollision(playerCollider, platformCollider,false); // Turn the ignore collision back on 
        isMovingDown = false; // Turns is moving down down
    }

    
    
}
