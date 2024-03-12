using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmRotation : MonoBehaviour
{
    

    public Transform armPivot; // The pivot point of the arm

    public float rotationSpeed = 5f; // Speed of arm rotation

    public float maxRotationAngle = 45f; // Maximum rotation angle in degrees

    public float targetYRotation = 90f; // Target Y rotation



    private Vector2 lastMousePosition; // Last mouse position in screen space

    private Camera mainCamera; // Canera that will hold the reference for the cursor 

    private Quaternion initialRotation; // Initial rotation of the arm

    private GameManager gameManager;



    //Aiming input actions    
    private PlayerInput playerInput; // variable meant to grab the player's input system component
    private InputAction aimAction; // Input action for aiming
    private InputAction aimUpAction;  // Input action for aiming up
    
    private InputAction aimUpDiagAction;  // Input action for aiming up and diagonally

    private InputAction crouchAction; // Input action for crounching

    


    private void Awake()
    {

        playerInput = GetComponent<PlayerInput>(); // Gets the playerinput component
        aimAction = playerInput.actions.FindAction("Aim");
        aimUpAction = playerInput.actions.FindAction("AimUp");
        aimUpDiagAction = playerInput.actions.FindAction("AimUpDiag");
        crouchAction = playerInput.actions.FindAction("Crouch");
        initialRotation = armPivot.localRotation;  // Store the initial rotation of the arm
        mainCamera = Camera.main; // sets the mainCamera object to the game main camera object
        gameManager = FindObjectOfType<GameManager>(); // Find and store a refernce to the GameManager
    }

    private void OnEnable()
    {
        aimAction.Enable(); // Enable the aim action
        aimAction.performed += OnAimPerformed; // Subscribe to the performed event
        aimUpAction.performed += OnAimUpPerformed; // Subscribe to the performed event
        aimUpDiagAction.performed += OnAimUpDiagPerformed; // Subscribe to the performed event
        aimUpAction.canceled += OnAimUpCanceled; // Subscribe to the performed event
        aimUpDiagAction.canceled += OnAimUpCanceled; // Subscribe to the performed event
        crouchAction.performed += OnCrouchPerformed; // Subscribe crouch action to crouch performed
        crouchAction.canceled += OnCrouchCanceled; // Subscribe crouch action to crouch canceled
        
    }

    private void OnDisable()
    {
        aimAction.Disable(); // Disable the aim action
        aimAction.performed -= OnAimPerformed; // Unsubscribe from the performed event
        aimUpAction.performed -= OnAimUpPerformed; // Unsubscribe from the performed event
        aimUpDiagAction.performed -= OnAimUpDiagPerformed; // Unsubscribe from the performed event
        aimUpAction.canceled -= OnAimUpCanceled; // Subscribe to the performed event
        aimUpDiagAction.canceled -= OnAimUpCanceled; // Subscribe to the performed event
        crouchAction.performed -= OnCrouchPerformed; // Subscribe crouch action to crouch performed
        crouchAction.canceled -= OnCrouchCanceled; // Subscribe crouch action to crouch canceled
        
    }

    private void OnAimPerformed(InputAction.CallbackContext context)
    {
        if (gameManager != null && gameManager.currentState == GameManager.pauseState.Unpaused)
        {
            // currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Debug.Log(currentMousePosition);
            
            Vector2 input = context.ReadValue<Vector2>(); // Read input value
            if(input.y <= 1.0f && input.y >= 0.9f) // If the joy stick is going to the top  these are the parameters
            {
                armPivot.localRotation = Quaternion.Euler(-90f, 90f, 0f); // rotate the gun all the way up

            }
            else if(input.y < 0.9f && input.y >= 0.5f) // If the joy stick is going to the top right or left
            { 
                armPivot.localRotation = Quaternion.Euler(-45f, 90f, 0f); // rotate the gun to the top right or left 
            }
            
            // else if(input.y >= -0.9f && input.y <= -0.5f) // if the joy stick is pointed to the bottom right or left 
            // {
            //     armPivot.localRotation = Quaternion.Euler(45f, 90f, 0f); // rotate the gun to the bottom right and left
            // }
            else
            {
                
                armPivot.localRotation = Quaternion.Euler(0f, 90f, 0f); // Set default rotation
                
            }
            
            
        }
    }

    private void OnAimUpPerformed(InputAction.CallbackContext context)
    {
        if (gameManager != null && gameManager.currentState == GameManager.pauseState.Unpaused)
        {
            armPivot.localRotation = Quaternion.Euler(-90f, 90f, 0f); // rotate the gun all the way up
        }
    }
    

    private void OnAimUpDiagPerformed(InputAction.CallbackContext context)

    {
        if (gameManager != null && gameManager.currentState == GameManager.pauseState.Unpaused)
        {
            armPivot.localRotation = Quaternion.Euler(-45f, 90f, 0f); // rotate the gun to the top right or left 
        }
    }

    private void OnAimUpCanceled(InputAction.CallbackContext context)

    {
        if (gameManager != null && gameManager.currentState == GameManager.pauseState.Unpaused)
        {
           armPivot.localRotation = Quaternion.Euler(0f, 90f, 0f);
        }
    }
    

    private void OnCrouchPerformed(InputAction.CallbackContext context)
    {
        // Do Crounching things
    }

    private void OnCrouchCanceled(InputAction.CallbackContext context)
    {
        // Do Crounching things
    }
    
}