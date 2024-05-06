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
    public float gravityForce = -9.81f; // A float variable that will deteremine how fast the player is moving, The max force that can be applied to the movement, how powerful the jump is, how strong the gravity is.
    public static int jumpsCounter; // A int variable that will hold how many jumps the player currently has

    public LayerMask groundLayer; // A layermask that holds the ground layer
    public float groundRayLength = 1f; // the length of the ray that will check if the player is grounded
    public Animator animator;
    private PlayerInput playerInput; // A private variable that is meant to grab the PlayerInput component that is attached to the player.
    private InputAction walkAction; // A private variable that is meant to hold the move action so that its values can be accessed
    private InputAction jumpAction; // A private variable that is meant to hold the jump action 
    private InputAction pauseAction; // A private variable that holds the pause action

    private float velocity;
    private float verticalVelocity; // Declare vertical velocity as a float

    private Vector3 playerVelocity;
    [SerializeField] private float gravityMultiplier = 3.0f;

    private InputAction freeAimAction; // A private variable that holds the free aim action 

    private InputAction freeAimLeftAction; // A private variable meant to hold the free aim left action
    private InputAction freeAimRightAction; // A private variable meant to hold the free aim left action
    private GameObject gameManagerObj; // A gameObject variable that will hold the game manager game object
    private GameManager gameManager; // A GameManger object that will hold the instance of the script of GameManger
    
    private ArmRotation armRotation; 
    private bool isFreeAiming = false;
    
    private Vector2 moveInput;

    private Vector3 direction;

    //private Rigidbody playerRB;
    
    private CharacterController characterController;

   

    // Jumping variables
    
   
    
    [Header("Player Jump Settings")]
    public float maxFallSpeed = 0.5f;
    public float initialJumpVelocity; // The inital velocity of the Jump
    public float maxJumpHeight =1.0f; // The max height that the jump will go
    public float maxJumpTime = 0.5f; // Maximum time the jump button can be held to reach maximum jump height
    private float coyoteTime = 0.2f; // How long the player will be able to jump after leaving the ground
    //private float jumpTimer = 0f; // Timer to track how long the jump button has been held
    private float coyoteTimeCounter; // The counter that will hold the coyote time current time
    private float jumpTime;

    
    public float jumpStartTime;
    private bool isJumping; // a bool to know if the player is jumping currently
    private bool isJumpingPressed = false; // a bool to know if the player has pressed the jump button



    private PlayerOneWay playerOneWay; // Grabs the Player oneway script to access the isMOving down

    private CyberMouseHandler cyberMouseHandler; // Grabs the player cybermousehandler from the player
    //private Rigidbody //; // A rigid body object which will hold the player's rigid body
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

    public float invincibilityLength; // a float that will say how long invibility last
    private float invincibilityCounter; // The float that will act as the timer and decrease with time
    public Renderer playerRenderer; // Grabs the player renderer so we can make the character blink
    private float flashCounter; // How many flashes
    public float flashLength = 0.1f; // How long we should turn the renderer off for
    
    public int maxExtraJumps = 1; // The max amount times the player can jump after jumping from the ground

    private int currExtraJumps; // The counter to see if they have used up all of their max jumps
    
    private float aimingFloat = 0f;
    private float aimingDiagFloat = 0f;
    // [Header("Mouse Colliders")]
    // public LeftMouseCollisionHandler leftMouseCollider;
    // public RightMouseCollisionHandler rightMouseCollider;

    public enum playerState // An enum that has a real life and chibi state to easily determine what state the character is in
    {
        RealLife,
        Chbi,
    }

    void Awake()

    {
        
        //// = GetComponent<Rigidbody>(); // Gets the rigid body of the player
        characterController = GetComponent<CharacterController>();
        playerOneWay = GetComponent<PlayerOneWay>(); // Gets the player one way script from the player
        cyberMouseHandler = GetComponent<CyberMouseHandler>(); // Gets the cyber mouse handler 
        gameManager = FindObjectOfType<GameManager>(); // Finds the game manger in the scene
        playerInput = GetComponent<PlayerInput>(); // Gets the playerinput component
        armRotation = FindObjectOfType<ArmRotation>();
        playerHealth = maxHealth; // Set the player's health to the max health at the start
        walkAction = playerInput.actions.FindAction("Walk"); // binds the walk action to the walk action that holds the inputs
        jumpAction = playerInput.actions.FindAction("Jump"); // binds the jump to the jump buttons
        jumpAction.started += OnJump; // When action is performed it is assigned to the onJump function
        jumpAction.canceled += OnJump; // When action is stop it is assigned to the on jumpfunction

        
        pauseAction = playerInput.actions.FindAction("Pause"); // Assigns the pause action to the pause action from the chibi movement
        pauseAction.performed += Pause; // Assigns the on performed pause action to the pause function 
        
        freeAimAction = playerInput.actions.FindAction("FreeAim"); // Finds the freeaim action
        freeAimLeftAction = playerInput.actions.FindAction("FreeAimLeft"); // 
        freeAimRightAction = playerInput.actions.FindAction("FreeAimRight");


        freeAimAction.performed += OnFreeAimPerformed;
        freeAimAction.canceled += OnFreeAimCanceled;
        freeAimLeftAction.performed += OnFreeAimLeft;
        freeAimRightAction.performed += OnFreeAimRight;
        
        currExtraJumps = maxExtraJumps;
        //setupJumpVariables();
        Cursor.lockState = CursorLockMode.Confined;
    }
   
    void Update()
    {
        if (armRotation.gunRotationWithMouse)
        {
            if (cyberMouseHandler.isAimingRight)
            {
                transform.rotation = Quaternion.Euler(0, 0, 0); // Not flipped
            }
            else
            {
                transform.rotation = Quaternion.Euler(0, 180, 0); // Flipped scale along x-axis
            }
        }
        else
        {
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
            if (direction.x < 0) // If moving left
            {
                // Flip the x component to make sure the player moves left
                direction.x *= -1;
            }
        }
        transform.position = new Vector3(transform.position.x, transform.position.y, 0f);

        float playerVelocityMagnitude = characterController.velocity.magnitude;

        // Check if the player is moving by comparing the magnitude to a small threshold
        if (playerVelocityMagnitude > 0.1f)
        {
            animator.SetBool("isWalking", true); // Player is moving
        }
        else
        {
            animator.SetBool("isWalking", false); // Player is not moving
        }
        isGrounded = IsGrounded(); // calls the isgrounded function which returns a bool to the isgrounded bool
        //Debug.Log("" + isGrounded);
        UpdateJumpState(); // update the jump state for jump buffer and coyote time
        handleJump(); // The function that handles the jumping of the player
        
    }

    void FixedUpdate()
    {
       
        //ApplyGravity(); // calls the apply gravity function so the player is affected by gravity
        ApplyMovement();
        handleIFrames();
        

        animator.SetFloat("Aiming", aimingFloat);

        animator.SetFloat("AimingDiag", aimingDiagFloat);
    }
    private void handleIFrames()
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
    public void Walk(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
        direction = new Vector3(moveInput.x, 0.0f, moveInput.y);
        


        // Check if the player is not moving (velocity is zero)
        // Check if any movement input is being provided
        if (moveInput.magnitude > 0.1f)
        {
            animator.SetBool("isWalking", true); // Player is moving
            animator.SetFloat("Velocity", 1);
        }
        else
        {
            animator.SetBool("isWalking", false); // Player is not moving
            animator.SetFloat("Velocity", 0);
        }
        

        
    }


    private void ApplyMovement()
    {
        characterController.Move(direction * playerSpeedMultiplier * Time.deltaTime);
    }


    void setupJumpVariables()
    {
        float timeToApex = maxJumpTime / 2;
        gravityForce = (-2 * maxJumpHeight) / Mathf.Pow(timeToApex,2);
        initialJumpVelocity = (2 * maxJumpHeight) / timeToApex;
    }

    private void OnJump(InputAction.CallbackContext context)
    {
        if (context.started) // When jump button is pressed
        {
            isJumpingPressed = true;
            jumpStartTime = Time.time; // Record the time when the jump started
        }
        else if (context.canceled) // When jump button is released
        {
            isJumpingPressed = false;
        }
    }

    
    private void handleJump()
    {
        if (isGrounded)
        {
            playerVelocity.y = 0.0f;
            animator.SetBool("isJumping", false);
        }

        if(!isGrounded)
        {
            animator.SetBool("isJumping",true);
        }
        if (isJumpingPressed && (Time.time - jumpStartTime) < maxJumpTime && isGrounded) // While jump button is held and within max jump time
        {
            // Calculate the jump progress based on how long the jump button has been held
            float jumpProgress = Mathf.Clamp01((Time.time - jumpStartTime) / maxJumpTime);

            // Apply the calculated jump velocity
            float jumpVelocity = Mathf.Lerp(initialJumpVelocity, 0f, jumpProgress);
            playerVelocity.y = jumpVelocity;
            animator.SetBool("isJumping", true);
        }
        else if (!isJumpingPressed && playerVelocity.y > 0) // If jump button is released, and player is still going up
        {
            // Reduce the upward velocity to simulate a shorter jump
            playerVelocity.y *= 0.8f; // Adjust this factor as needed
            animator.SetBool("isJumping", false);
        }

        playerVelocity.y += gravityForce * Time.deltaTime;
        characterController.Move(playerVelocity * Time.deltaTime);
    }






    void ApplyGravity()
    {
        bool isFalling = verticalVelocity <= 0.0f || !isJumpingPressed;
        float fallMultiplier = 5.0f;

        if (isGrounded)
        {
            // Reset extra jumps when grounded
            currExtraJumps = maxExtraJumps;
        }

        if (!isGrounded)
        {
            if (isFalling)
            {
                // Apply gravity multiplied by fallMultiplier when falling
                verticalVelocity += gravityForce * fallMultiplier * Time.deltaTime;
            }
            else
            {
                // Apply regular gravity when not falling
                verticalVelocity += gravityForce * Time.deltaTime;
            }

            // Limit vertical velocity to prevent excessive falling speed
            verticalVelocity = Mathf.Max(verticalVelocity, -maxFallSpeed);

            // Move the player using CharacterController's Move function
            characterController.Move(Vector3.up * verticalVelocity * Time.deltaTime); // Adjust this line if necessary
        }
    }




    void UpdateJumpState()
    {
        isGrounded = IsGrounded(); // Check if the player is grounded

        if (isGrounded)
        {
            coyoteTimeCounter = coyoteTime; // Reset coyote time counter if grounded
            currExtraJumps = maxExtraJumps; // Reset extra jumps when grounded
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime; // Decrement coyote time counter if not grounded
        }
    }

    bool IsGrounded() // A function that returns a bool if the raycast hits the ground
    {
    
        
        //return characterController.isGrounded;
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
        animator.SetFloat("Aiming", 0f);
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
        //playerRB.velocity = Vector3.zero;
    }

    public void setInitalJump(float jumpSetter)
    {
        initialJumpVelocity = jumpSetter;
    }

    
    public void setAimingFloat(float newUp)
    {
        aimingFloat = newUp;
    }

    public float getAimingFloat()
    {
        return aimingFloat;

    }
}