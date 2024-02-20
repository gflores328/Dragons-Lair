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
    //public float jumpBufferDuration = 0.1f;
    //private float jumpBufferTimer = 0f;
    //private bool inRealLife = true; // A public bool to determine if the player is in real life or the arcade chibi world used to restrict movement
    public LayerMask groundLayer; // A layermask that holds the ground layer
    public float groundRayLength = 1f; // the length of the ray that will check if the player is grounded
    
    private PlayerInput playerInput; // A private variable that is meant to grab the PlayerInput component that is attached to the player.
    private InputAction walkAction; // A private variable that is meant to hold the move action so that its values can be accessed
    private InputAction jumpAction; // A private variable that is meant to hold the jump action 
    private InputAction pauseAction; // A private variable that holds the pause action
    private GameObject gameManagerObj; // A gameObject variable that will hold the game manager game object
    private GameManager gameManager; // A GameManger object that will hold the instance of the script of GameManger
    
    private PlayerOneWay playerOneWay; // Grabs the Player oneway script to access the isMOving down
    private Rigidbody playerRB; // A rigid body object which will hold the player's rigid body
    private playerState currentPlayerState; // the state that will hold the players current state by using the playerState enum created below
    private bool isGrounded; // A bool to know if the player is grounded
    static float playerHealth = 100f; // A float that holds the players health
    
    public enum playerState // An enum that has a real life and chibi state to easily determine what state the character is in
    {
        RealLife,
        Chbi,
    }
    void Start()
    {
        playerRB = GetComponent<Rigidbody>(); // Gets the rigid body of the player
        playerInput = GetComponent<PlayerInput>(); // Gets the playerinput component
        walkAction = playerInput.actions.FindAction("Walk"); // binds the walk action to the walk action that holds the inputs
        jumpAction = playerInput.actions.FindAction("Jump"); // binds the jump to the jump buttons
        jumpAction.performed += OnJump; // When action is performed it is assigned to the onJump function
        playerOneWay = GetComponent<PlayerOneWay>(); // Gets the player one way script from the player
        pauseAction = playerInput.actions.FindAction("Pause"); // Assigns the pause action to the pause action from the chibi movement
        pauseAction.performed += Pause; // Assigns the on performed pause action to the pause function 
        gameManager = FindObjectOfType<GameManager>(); // Finds the game manger in the scene
       
    }

    void FixedUpdate() 
    {
        ChibiMovePlayer(); // Calls the chibiMovePlayer function
        isGrounded = IsGrounded(); // calls the isgrounded function which returns a bool to the isgrounded bool

        if (!isGrounded) // if the player is not grounded
        {
            ApplyGravity(); // Run the apply Gravity function
        }
        // else
        // {
        //     if (jumpBufferTimer > 0f)
        //     {
        //         jumpBufferTimer -= Time.deltaTime;
        //     }
        // }
    }

    void ChibiMovePlayer() // Chibi move player function that moves and flips the player
    {

        Vector2 direction = walkAction.ReadValue<Vector2>(); // gets the value of the walk action and assigns it to the direction
        if (direction.x > 0) // Moving right

        {

            transform.localScale = new Vector3(1, 1, 1); // Normal scale

        }

        else if (direction.x < 0) // Moving left

        {

            transform.localScale = new Vector3(-1, 1, 1); // Flipped scale along x-axis
            
        }
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y); // Move the player left or right depending on the movement of the move action assigns it to a vector 3
        moveDirection = transform.TransformDirection(moveDirection); // the move direction is the transform direction that is needed to go the direction
        playerRB.MovePosition(transform.position + moveDirection * Time.deltaTime * playerSpeedMultiplier); // THis is the function that moves the player by using time move direction and the player current position
    }

    void OnJump(InputAction.CallbackContext context) // THe on jump that is called  when the jump button is pressed
    {
        if (context.performed && IsGrounded() && !playerOneWay.isMovingDown) // if it is performed and the player is grounded and it is not moving down then allow it to jump
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce); // Push the player up
        }
        
        // Debug.Log("started jumping");
        // if (context.canceled && playerRB.velocity.y > 0f)
        // {
        //    playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.5f);
        // }
    }

    

    void ApplyGravity() // Apply the gravity function which pushed the player down
    {
        playerRB.AddForce(Vector3.down * gravityForce); // Push the player in the down direction by adding force down
    }

    bool IsGrounded() // A function that returns a bool if the raycast hits the ground
    {
        

        Vector3 rayOrigin = transform.position + Vector3.down * .5f; // Shoots it from the players feet

        // Cast a ray downward to check if the player is on the ground
        bool isHit = Physics.Raycast(rayOrigin, Vector3.down, groundRayLength, groundLayer);

        Debug.DrawRay(rayOrigin, Vector3.down * groundRayLength, isHit ? Color.green : Color.red);

        //Debug.Log(isGrounded); // Comment to see if grounded is true


        return isHit; // return the result
        
    }
    private void Pause(InputAction.CallbackContext value) // The pause function that is called when the button is pressed
    {
        //Debug.Log("Paused");
        gameManager.PauseGame(); // Calls the pause function from the game manager script
    }
    public void takeDamage(float dmgAmount) // The amount put in here will be subtracted 
    {
        playerHealth -= dmgAmount; // Substract the damage amount from the players health
    }
    private void die() // Checks to see if the player needs to die
    {
        if(playerHealth <= 0f) // player health hits zero

           { 
                Destroy(this); // Destroy player
           }
        
    }
  
}
