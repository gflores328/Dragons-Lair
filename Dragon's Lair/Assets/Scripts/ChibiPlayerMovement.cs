using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

//Created by Aaron Torres
//Script Title: Chibi PlayerMovement
//Description: This script will be handling the player movement inside of the chibi game world

public class ChibiPlayerMovement : MonoBehaviour
{
    [Header("Player Movement Settings")]
    public float playerSpeedMultiplier,maxForce,jumpForce,gravityForce; // A float variable that will deteremine how fast the player is moving, The max force that can be applied to the movement, how powerful the jump is, how strong the gravity is.
    
    public bool inRealLife = true; // A public bool to determine if the player is in real life or the arcade chibi world used to restrict movement
    public LayerMask groundLayer;
    public float groundRayLength = 1f;
    
    private PlayerInput playerInput; // A private variable that is meant to grab the PlayerInput component that is attached to the player.
    private InputAction walkAction; // A private variable that is meant to hold the move action so that its values can be accessed
    private InputAction jumpAction; 
    private InputAction pauseAction; // A private variable that holds the pause action
    private GameObject gameManagerObj;
    private GameManager gameManager;
    private Rigidbody playerRB; // A rigid body object which will hold the player's rigid body
    private playerState currentPlayerState; // the state that will hold the players current state by using the playerState enum created below
    private bool isGrounded;
    
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
        
        pauseAction = playerInput.actions.FindAction("Pause"); // Searches for the action and stores it inside of the  interact action variable
        pauseAction.performed += Pause;
        gameManagerObj =  GameObject.Find("GameManager");
        gameManager = gameManagerObj.GetComponent<GameManager>();
       
    }

    
    void FixedUpdate()
    {
        
        ChibiMovePlayer(); // Calls the ChibiMovePlayer function
        //CheckJump();


        isGrounded = IsGrounded();
        //Apply gravity if not on the ground
        if (!isGrounded)
        {
            
            playerRB.AddForce(Vector3.down * gravityForce);
        }

     
    }

    void SwitchActionMap(string actionMapName) // The function that switches the action map.
    {
        playerInput.SwitchCurrentActionMap(actionMapName);
        walkAction = playerInput.currentActionMap.FindAction("Walk");
    }

    

    //ChibiMovePlayer Function Description:
    //Designed to be the function that will actively move the player object in the game. but only in the x axis.
    void ChibiMovePlayer()
    {
        //Debug.Log(walkAction.ReadValue<Vector2>());
        //Vector3 currentVelocity = playerRB.velocity;
        Vector2 direction = walkAction.ReadValue<Vector2>();
        //Vector3 targetVelocity = new Vector3(direction.x,0,0);
        //targetVelocity *= playerSpeedMultiplier;
        //targetVelocity = transform.TransformDirection(targetVelocity);
        //Vector3 velocityChange = (targetVelocity + currentVelocity);
        //Vector3.ClampMagnitude(velocityChange,maxForce); // Creates a vector2 variable to assign and store the values of the walk action to be used to determine which way the player wants to move.
        transform.Translate(new Vector3(direction.x,0,direction.y) * Time.deltaTime * playerSpeedMultiplier); // Multiplies the values of the a new vector3 position time and the speed multiplier to make the player move.
        //playerRB.AddForce(velocityChange,ForceMode.VelocityChange);
    }
    
    //This is the function that is called when the jump button is pressed
  
   
    public void Jump(InputAction.CallbackContext value)
    {
        //Debug.Log("JumpButton has been pressed");
        OnJump();
        /*if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Debug.Log("Can Jump");
            OnJump();

        }
        */
    }
    
    //This is the function that is actually moving the player's rigid body
    void OnJump()
    {
        if (isGrounded)
        {
            // You can adjust the jump force according to your needs
            //Debug.Log("I am jumping");
            //float jumpForce = 10f;
            playerRB.AddForce(transform.up * jumpForce, ForceMode.Impulse);
            
        }
    }

   
    bool IsGrounded()
    {
        

        Vector3 rayOrigin = transform.position + Vector3.down * .5f;

        // Cast a ray downward to check if the player is on the ground
        bool isHit = Physics.Raycast(rayOrigin, Vector3.down, groundRayLength, groundLayer);

        Debug.DrawRay(rayOrigin, Vector3.down * groundRayLength, isHit ? Color.green : Color.red);

        Debug.Log(isGrounded); // Comment to see if grounded is true


        return isHit;
        
    }
    private void Pause(InputAction.CallbackContext value)
    {
        Debug.Log("Paused");
        gameManager.PauseGame();
    }
  
}
