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
    public LayerMask groundLayer;
    public float groundRayLength = 1f;
    
    private PlayerInput playerInput; // A private variable that is meant to grab the PlayerInput component that is attached to the player.
    private InputAction walkAction; // A private variable that is meant to hold the move action so that its values can be accessed
    private InputAction jumpAction; 
    private InputAction pauseAction; // A private variable that holds the pause action
    private GameObject gameManagerObj;
    private GameManager gameManager;
    
    private PlayerOneWay playerOneWay;
    private Rigidbody playerRB; // A rigid body object which will hold the player's rigid body
    private playerState currentPlayerState; // the state that will hold the players current state by using the playerState enum created below
    private bool isGrounded;
    static float playerHealth = 100f;
    
    public enum playerState // An enum that has a real life and chibi state to easily determine what state the character is in
    {
        RealLife,
        Chbi,
    }
    void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerInput = GetComponent<PlayerInput>();
        walkAction = playerInput.actions.FindAction("Walk");
        jumpAction = playerInput.actions.FindAction("Jump");
        jumpAction.performed += OnJump;
        playerOneWay = GetComponent<PlayerOneWay>();
        pauseAction = playerInput.actions.FindAction("Pause");
        gameManager = FindObjectOfType<GameManager>();
       
    }

    void FixedUpdate()
    {
        ChibiMovePlayer();
        isGrounded = IsGrounded();

        if (!isGrounded)
        {
            ApplyGravity();
        }
        // else
        // {
        //     if (jumpBufferTimer > 0f)
        //     {
        //         jumpBufferTimer -= Time.deltaTime;
        //     }
        // }
    }

    void ChibiMovePlayer()
    {

        Vector2 direction = walkAction.ReadValue<Vector2>();
        if (direction.x > 0) // Moving right

        {

            transform.localScale = new Vector3(1, 1, 1); // Normal scale

        }

        else if (direction.x < 0) // Moving left

        {

            transform.localScale = new Vector3(-1, 1, 1); // Flipped scale along x-axis
            
        }
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y);
        moveDirection = transform.TransformDirection(moveDirection);
        playerRB.MovePosition(transform.position + moveDirection * Time.deltaTime * playerSpeedMultiplier);
    }

    void OnJump(InputAction.CallbackContext context)
    {
        if (context.performed && IsGrounded() && !playerOneWay.isMovingDown)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);
        }
        
        // Debug.Log("started jumping");
        // if (context.canceled && playerRB.velocity.y > 0f)
        // {
        //    playerRB.velocity = new Vector2(playerRB.velocity.x, playerRB.velocity.y * 0.5f);
        // }
    }

    

    void ApplyGravity()
    {
        playerRB.AddForce(Vector3.down * gravityForce);
    }

    bool IsGrounded()
    {
        

        Vector3 rayOrigin = transform.position + Vector3.down * .5f;

        // Cast a ray downward to check if the player is on the ground
        bool isHit = Physics.Raycast(rayOrigin, Vector3.down, groundRayLength, groundLayer);

        Debug.DrawRay(rayOrigin, Vector3.down * groundRayLength, isHit ? Color.green : Color.red);

        //Debug.Log(isGrounded); // Comment to see if grounded is true


        return isHit;
        
    }
    private void Pause(InputAction.CallbackContext value)
    {
        //Debug.Log("Paused");
        gameManager.PauseGame();
    }
    public void takeDamage(float dmgAmount) // The amount put in here will be subtracted 
    {
        playerHealth -= dmgAmount;
    }
    private void die()
    {
        if(playerHealth <= 0f)

           { 
                Destroy(this);
           }
        
    }
  
}
