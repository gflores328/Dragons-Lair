using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

//Created by Aaron Torres
//Script Title: Chibi PlayerMovement
//Description: This script will be handling the player movement inside of the chibi game world

public class ChibiPlayerMovement : MonoBehaviour
{
    
    [Header("Player Movement Settings")]
    public float playerSpeedMultiplier;
    public float maxForce;
    public float jumpForce;
    public float gravityForce; // A float variable that will deteremine how fast the player is moving, The max force that can be applied to the movement, how powerful the jump is, how strong the gravity is.
    public static int jumpsCounter;
    public LayerMask groundLayer; // A layermask that holds the ground layer
    public float groundRayLength = 1f; // the length of the ray that will check if the player is grounded
    
    private PlayerInput playerInput; // A private variable that is meant to grab the PlayerInput component that is attached to the player.
    private InputAction walkAction; // A private variable that is meant to hold the move action so that its values can be accessed
    private InputAction jumpAction; // A private variable that is meant to hold the jump action 
    private InputAction pauseAction; // A private variable that holds the pause action

    private InputAction freeAimAction; // A private variable that holds the free aim action 

    private InputAction freeAimLeftAction; // A private variable meant to hold the free aim left action
    private InputAction freeAimRightAction; // A private variable meant to hold the free aim left action
    private GameObject gameManagerObj; // A gameObject variable that will hold the game manager game object
    private GameManager gameManager; // A GameManger object that will hold the instance of the script of GameManger
    
    private bool isFreeAiming = false;
    
    
   

    // Jumping variables
    
   
    
    [Header("Player Jump Settings")]
    public float initialJumpVelocity; // The inital velocity of the Jump
    public float maxJumpHeight =1.0f; // The max height that the jump will go
    public float maxJumpTime = 0.5f; // Maximum time the jump button can be held to reach maximum jump height
    private float coyoteTime = 0.2f; // How long the player will be able to jump after leaving the ground
    private float jumpTimer = 0f; // Timer to track how long the jump button has been held
    private float coyoteTimeCounter; // The counter that will hold the coyote time current time
    private bool isJumping; // a bool to know if the player is jumping currently
    private bool isJumpingPressed = false; // a bool to know if the player has pressed the jump button



    private PlayerOneWay playerOneWay; // Grabs the Player oneway script to access the isMOving down
    private Rigidbody playerRB; // A rigid body object which will hold the player's rigid body
    private playerState currentPlayerState; // the state that will hold the players current state by using the playerState enum created below
    private bool isGrounded; // A bool to know if the player is grounded

    private bool isFacingRight; // A bool to determine which way the character is facing right

  
    //PlayerUI and Information Section
    [Header("Player Health Settings")]
    public float maxHealth =  10f; // The float the determines how much health the player starts with
    public static float playerHealth = 10f; // A float that holds the players health
    public int numOfHearts; // an int that determines how many UI hearts are there

    public Image[] hearts; // The image array that will hold the 10 heart ui and access them
    public Sprite fullHeart; // The sprite for the full heart
    public Sprite emptyHeart; // The sprite for the empty heart

    public float invincibilityLength;
    private float invincibilityCounter;
    public Renderer playerRenderer;
    private float flashCounter;
    public float flashLength = 0.1f;
    
    public int maxExtraJumps = 1;

    private int currExtraJumps;
    

    
    public enum playerState // An enum that has a real life and chibi state to easily determine what state the character is in
    {
        RealLife,
        Chbi,
    }

    void Awake()

    {
        playerHealth = maxHealth;
        playerRB = GetComponent<Rigidbody>(); // Gets the rigid body of the player
        playerInput = GetComponent<PlayerInput>(); // Gets the playerinput component
        walkAction = playerInput.actions.FindAction("Walk"); // binds the walk action to the walk action that holds the inputs
        jumpAction = playerInput.actions.FindAction("Jump"); // binds the jump to the jump buttons
        jumpAction.started += OnJump; // When action is performed it is assigned to the onJump function
        jumpAction.canceled += OnJump;
        playerOneWay = GetComponent<PlayerOneWay>(); // Gets the player one way script from the player
        pauseAction = playerInput.actions.FindAction("Pause"); // Assigns the pause action to the pause action from the chibi movement
        pauseAction.performed += Pause; // Assigns the on performed pause action to the pause function 
        gameManager = FindObjectOfType<GameManager>(); // Finds the game manger in the scene
        freeAimAction = playerInput.actions.FindAction("FreeAim");
        freeAimLeftAction = playerInput.actions.FindAction("FreeAimLeft");
        freeAimRightAction = playerInput.actions.FindAction("FreeAimRight");


        freeAimAction.performed += OnFreeAimPerformed;
        freeAimAction.canceled += OnFreeAimCanceled;
        freeAimLeftAction.performed += OnFreeAimLeft;
        freeAimRightAction.performed += OnFreeAimRight;
        
        currExtraJumps = maxExtraJumps;
        //setupJumpVariables();

    }
   
    void Update()

    {
        if(playerHealth > numOfHearts)
        {
            playerHealth = numOfHearts;

        }

        for(int i = 0; i < hearts.Length; i++) {

            if( i < playerHealth) {
                hearts[i].sprite = fullHeart;
            }

            else 
            {
                hearts[i].sprite = emptyHeart;
            }

            if( i < maxHealth) 
            {
                hearts[i].enabled = true;
            }
            else 
            {
                hearts[i].enabled = false;
            }


        }
        if(invincibilityCounter > 0)
        {
            invincibilityCounter -= Time.deltaTime;

            flashCounter -= Time.deltaTime;
            if(flashCounter <= 0)

            {
                playerRenderer.enabled = !playerRenderer.enabled;
                flashCounter = flashLength;
            }

            if(invincibilityCounter <= 0)
            {
                playerRenderer.enabled = true;
            }      
        }
    }

    void FixedUpdate() 
        {
            if(!isFreeAiming)
            {
                ChibiMovePlayer(); // Calls the chibiMovePlayer function
            }
            isGrounded = IsGrounded(); // calls the isgrounded function which returns a bool to the isgrounded bool
            ApplyGravity(); // calls the apply gravity function so the player is affected by gravity
            UpdateJumpState(); // update the jump state for jump buffer and coyote time
            handleJump(); // The function that handles the jumping of the player
        }

    void ChibiMovePlayer() // Chibi move player function that moves and flips the player
    {

        Vector2 direction = walkAction.ReadValue<Vector2>(); // gets the value of the walk action and assigns it to the direction
        if (direction.x > 0) // Moving right

        {
            isFacingRight = true;
            transform.rotation = Quaternion.Euler(0, 0, 0);

        }

        else if (direction.x < 0) // Moving left

        {
            isFacingRight = false;
            transform.rotation = Quaternion.Euler(0, 180, 0); // Flipped scale along x-axis
            
        }
        Vector3 moveDirection = new Vector3(direction.x, 0, direction.y); // Move the player left or right depending on the movement of the move action assigns it to a vector 3
       

        if (direction.x < 0) // If moving left
        {
            // Flip the x component to make sure the player moves left
            moveDirection.x *= -1;
        }

        moveDirection = transform.TransformDirection(moveDirection); // the move direction is the transform direction that is needed to go the direction
        playerRB.MovePosition(transform.position + moveDirection * Time.deltaTime * playerSpeedMultiplier); // THis is the function that moves the player by using time move direction and the player current position
    }


    // void setupJumpVariables()
    // {
    //     float timeToApex = maxJumpTime / 2;
    //     gravityForce = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex,2);
    //     initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    // }

    void OnJump(InputAction.CallbackContext context) // THe on jump that is called when the jump button is pressed
    {
        isJumpingPressed = context.ReadValueAsButton();
    }

    
    void handleJump()
    {

        if(!isJumping && coyoteTimeCounter > 0 && isJumpingPressed && !playerOneWay.IsDownActionActive() && currExtraJumps > 0)
        {
            isJumping = true;
            playerRB.velocity = new Vector3(playerRB.velocity.x, initialJumpVelocity, 0);
        }
        else if( !isJumpingPressed && isJumping && isGrounded)
        {
            isJumping = false;
        }
    }

    void ApplyGravity() // Apply the gravity function which pushed the player down
    {

        bool isFalling = playerRB.velocity.y <= 0.0f || !isJumpingPressed;
        float fallMultiplier = 5.0f;

        if(isGrounded)
        {
            playerRB.AddForce(Vector3.down * gravityForce); // Push the player in the down direction by adding force down
            StopPlayerMovement();
        }

        else if(isFalling)
        {
            float previousYVelocity = playerRB.velocity.y;
            float newYVelocity = playerRB.velocity.y + (gravityForce * fallMultiplier * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            playerRB.velocity = new Vector3(playerRB.velocity.x, nextYVelocity, playerRB.velocity.z);
        }
        else 
        {
            float previousYVelocity = playerRB.velocity.y;
            float newYVelocity = playerRB.velocity.y + (gravityForce  * Time.deltaTime);
            float nextYVelocity = (previousYVelocity + newYVelocity) * .5f;
            playerRB.velocity = new Vector3(playerRB.velocity.x, nextYVelocity, playerRB.velocity.z);
        }
        
    }

    void UpdateJumpState()
    {
        isGrounded = IsGrounded(); // Check if the player is grounded

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime; // Reset coyote time counter if grounded
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // Decrement coyote time counter if not grounded
        }
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
        if(invincibilityCounter <= 0)
        {
            playerHealth -= dmgAmount; // Substract the damage amount from the players health

            if(playerHealth <= 0f)

            {

                    die();

            } 

            invincibilityCounter = invincibilityLength;

            playerRenderer.enabled = false;

            flashCounter = flashLength;
        }
        //Debug.Log($"I have been Hit! Health: {playerHealth}");
    }

    private void die() // Checks to see if the player needs to die
    {
        string currentSceneName = SceneManager.GetActiveScene().name;
        gameManager.LoadSceneAsync(currentSceneName);
        
    }

    public void AddHealth(float health)
    
    {
        float endHealth = health + playerHealth;
        if(endHealth > maxHealth)
        {
            playerHealth = maxHealth;
        }
        else{
            playerHealth = endHealth;
        }
    }
    
    private void OnFreeAimPerformed(InputAction.CallbackContext value) // The pause function that is called when the button is pressed
    {
        isFreeAiming = true;
        StopPlayerMovement();
    }

     private void OnFreeAimCanceled(InputAction.CallbackContext value) // The pause function that is called when the button is pressed
    {
        isFreeAiming = false;
    }
    

    private void OnFreeAimLeft(InputAction.CallbackContext value) // The function that is called when freeaiming and trying to face left
    {
        if(isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0); // Flip player to face left
            isFacingRight = false;
        }
    }

    private void OnFreeAimRight(InputAction.CallbackContext value) // The function that is called when free aiming and trying to face right

    {
        if(!isFacingRight)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0); // Rotates the character to face the right
            isFacingRight = true;
        }
    }

    private void StopPlayerMovement()
    {
        playerRB.velocity = Vector3.zero;
    }
}