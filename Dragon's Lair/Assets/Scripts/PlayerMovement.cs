using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Created by Aaron Torres
//Script Title: PlayerMovement
//Description: This script will be handling the player movement.

public class PlayerMovement : MonoBehaviour
{

    public float playerSpeedMultiplier,maxForce,jumpForce,gravityForce; // A float variable that will deteremine how fast the player is moving, The max force that can be applied to the movement, how powerful the jump is, how strong the gravity is.
    
    public bool inRealLife = true; // A public bool to determine if the player is in real life or the arcade chibi world used to restrict movement
    public LayerMask groundLayer;
    public float groundRayLength = 0.6f;
    
    private PlayerInput playerInput; // A private variable that is meant to grab the PlayerInput component that is attached to the player.
    private InputAction walkAction; // A private variable that is meant to hold the move action so that its values can be accessed
    private InputAction jumpAction;
    
    private Rigidbody playerRB; // A rigid body object which will hold the player's rigid body
    private playerState currentPlayerState; // the state that will hold the players current state by using the playerState enum created below
    
    public enum playerState // An enum that has a real life and chibi state to easily determine what state the character is in
    {
        RealLife,
        Chbi,
    }
    void Start()
    {
        playerRB = GetComponent<Rigidbody>(); // assigns the rigid body
        playerRB.freezeRotation = true; // freeze rotation so player doesn't rotate
        playerInput = GetComponent<PlayerInput>(); // Grabs the Player Input compoent from the player and assigns it to the playerInput that was initalized above
        walkAction = playerInput.actions.FindAction("Walk"); // Searches for the action and stores it inside of the  walk action variable
        jumpAction = playerInput.actions.FindAction("Jump"); // Assigns the jump action to the jump action input
        jumpAction.performed += Jump; // assigns when the jumpaction is performed then jump function will be called
        
       
    }

    
    void FixedUpdate()
    {
        if (inRealLife && playerInput.currentActionMap.name != "RealLifeMovement") // Checks to see if the player is in real life and that the current action map is not already real life movement

        {

            SwitchActionMap("RealLifeMovement"); //Calls the switch action map function and switches it to real life
            currentPlayerState = playerState.RealLife; // Sets the current player state to reallife
           

        }

        else if (!inRealLife && playerInput.currentActionMap.name != "ChibiMovement") // Checks to see if the player is in real life and that the current action map is not already in chibi movement

        {

            SwitchActionMap("ChibiMovement"); //Calls the switch action map function and switches it to ChibiMovement
            currentPlayerState = playerState.Chbi; // sets the current player state to chibi
            

        }

        if(currentPlayerState == playerState.RealLife)

        {
            RealLifeMovePlayer(); // Calls the RealLifeMovePlayer function
        }
        
        
        if(currentPlayerState == playerState.Chbi)

        {

            ChibiMovePlayer(); // Calls the ChibiMovePlayer function
            //CheckJump();

        }
        bool isGrounded = IsGrounded();
    
        //Apply gravity if not on the ground
        if (!isGrounded)
        {
            // You can adjust the gravity force according to your needs
            //float gravityForce = 20f;
            playerRB.AddForce(Vector3.down * gravityForce);
        }

     
    }

    void SwitchActionMap(string actionMapName) // The function that switches the action map.
    {
        playerInput.SwitchCurrentActionMap(actionMapName);
        walkAction = playerInput.currentActionMap.FindAction("Walk");
    }

    //MovePlayer Function Description:
    //Designed to be the function that will actively move the player object in the game. allowing movement in all directions 
    void RealLifeMovePlayer()
    {
        Vector3 currentVelocity = playerRB.velocity;
        Vector2 direction = walkAction.ReadValue<Vector2>();
        Vector3 targetVelocity = new Vector3(direction.x, 0, direction.y);
        
        // Get the camera's forward direction
        Vector3 cameraForward = Camera.main.transform.forward;
        cameraForward.y = 0f; // Ensure no vertical component
        
        // Transform the movement direction based on the camera's forward direction
        targetVelocity = Quaternion.LookRotation(cameraForward) * targetVelocity;
        
        targetVelocity *= playerSpeedMultiplier;
        
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        Vector3.ClampMagnitude(velocityChange, maxForce);

        playerRB.AddForce(velocityChange, ForceMode.VelocityChange);
        
    }

    //ChibiMovePlayer Function Description:
    //Designed to be the function that will actively move the player object in the game. but only in the x axis.
    void ChibiMovePlayer()
    {
        //Debug.Log(walkAction.ReadValue<Vector2>());
        Vector3 currentVelocity = playerRB.velocity;
        Vector2 direction = walkAction.ReadValue<Vector2>();
        Vector3 targetVelocity = new Vector3(direction.x,0,0);
        targetVelocity *= playerSpeedMultiplier;
        //targetVelocity = transform.TransformDirection(targetVelocity);
        Vector3 velocityChange = (targetVelocity - currentVelocity);
        Vector3.ClampMagnitude(velocityChange,maxForce); // Creates a vector2 variable to assign and store the values of the walk action to be used to determine which way the player wants to move.
        //transform.position += new Vector3(direction.x,0,direction.y) * Time.deltaTime * playerSpeedMultiplier; // Multiplies the values of the a new vector3 position time and the speed multiplier to make the player move.
        playerRB.AddForce(velocityChange,ForceMode.VelocityChange);
    }
    
    //This is the function that is called when the jump button is pressed
    public void Jump(InputAction.CallbackContext value)
    {

        if (value.phase == InputActionPhase.Started && IsGrounded())

        {

            OnJump();

        }
       
    }

    //This is the function that is actually moving the player's rigid body
    void OnJump()
    {
        // You can adjust the jump force according to your needs
        Debug.Log("I am jumping");
        //float jumpForce = 10f;
        playerRB.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        
    }

    bool IsGrounded()
    {
        

        Vector3 rayOrigin = transform.position;

        // Cast a ray downward to check if the player is on the ground
        bool isHit = Physics.Raycast(rayOrigin, Vector3.down, out RaycastHit hitInfo, groundRayLength, groundLayer);

        Debug.Log("Is Grounded: " + isHit); // Comment to see if grounded is true


        return isHit;
        
    }
    
}
