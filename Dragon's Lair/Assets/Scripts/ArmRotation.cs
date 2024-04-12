using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmRotation : MonoBehaviour
{
    

    public Transform armPivot; // The pivot point of the arm

    public Transform armLookTarget;

    public Transform armLookTargetUp;

    public Transform armLookTargetForward;

    public Transform armLookTargetForwardDiag;
    public float rotationSpeed = 5f; // Speed of arm rotation

    public float maxRotationAngle = 45f; // Maximum rotation angle in degrees

    public float targetYRotation = 90f; // Target Y rotation

    public bool gunRotationWithMouse = true; // A bool variable to determine if the player wants to use the mouse to aim or the keyboard


    private Vector2 lastMousePosition; // Last mouse position in screen space

    private Camera mainCamera; // Canera that will hold the reference for the cursor 

    private Quaternion initialRotation; // Initial rotation of the arm

    private GameManager gameManager; // grabs the game manager script from the game manager

    private ChibiPlayerMovement chibiPlayerMovement; // Grabs the chibi player script from the scene

    //Aiming input actions    
    private PlayerInput playerInput; // variable meant to grab the player's input system component
    private InputAction aimAction; // Input action for aiming
    private InputAction aimUpAction;  // Input action for aiming up
    
    private InputAction aimUpDiagAction;  // Input action for aiming up and diagonally

    private InputAction crouchAction; // Input action for crounching

    private bool isControllerConnected = false; // A bool to see if using keyboard or controller
    


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
        chibiPlayerMovement = FindObjectOfType<ChibiPlayerMovement>(); // find and store the reference to the chibi player movement

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
    
    private void Update()
    {
        if(gameManager.GetIsMouse())
        {
            gunRotationWithMouse = gameManager.GetusingMouseRotation();
        }
        //gunRotationWithMouse = gameManager.GetusingMouseRotation();
        if(!gameManager.GetIsMouse())
        {
            gunRotationWithMouse = false;
        }
        if(gunRotationWithMouse)
        {
            // Get the current mouse position
            Vector3 mousePosition = Input.mousePosition;

            // Convert the mouse position to a point in the game world
            Vector3 targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z - mainCamera.transform.position.z));

            // Set the Z-axis of the target position to the current Z position of the arm pivot
            targetPosition.z = armPivot.position.z;

            // Rotate the gun towards the cursor position
            RotateGun(targetPosition);
        }
       
        
    }

    

    private void OnAimPerformed(InputAction.CallbackContext context)
    {
        if (gameManager != null && gameManager.currentState == GameManager.pauseState.Unpaused)
        {
            // currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            // Debug.Log(currentMousePosition);
            if (context.control.device is Gamepad)
            {
                isControllerConnected = true; // set the is controller to true
                Vector2 input = context.ReadValue<Vector2>(); // Read input value
                if(input.y <= 1.0f && input.y >= 0.9f) // If the joy stick is going to the top  these are the parameters
                {
                    armPivot.localRotation = Quaternion.Euler(-90f, 90f, 0f); // rotate the gun all the way up
                    chibiPlayerMovement.setAimingFloat(1f);
                    armLookTarget.transform.position = armLookTargetUp.transform.position;
                    Debug.Log("ControllerAimingUP");

                }
                else if(input.y < 0.9f && input.y >= 0.5f) // If the joy stick is going to the top right or left
                { 
                    
                    armPivot.localRotation = Quaternion.Euler(-45f, 90f, 0f); // rotate the gun to the top right or left
                    chibiPlayerMovement.setAimingFloat(0.5f); 
                    armLookTarget.transform.position = armLookTargetForwardDiag.transform.position;
                }
                
                // else if(input.y >= -0.9f && input.y <= -0.5f) // if the joy stick is pointed to the bottom right or left 
                // {
                //     armPivot.localRotation = Quaternion.Euler(45f, 90f, 0f); // rotate the gun to the bottom right and left
                // }
                else
                {
                    
                    armPivot.localRotation = Quaternion.Euler(0f, 90f, 0f); // Set default rotation
                    chibiPlayerMovement.setAimingFloat(0f);
                    armLookTarget.transform.position = armLookTargetForward.transform.position;
                    
                }
            }


            else if (context.control.device is Mouse) // If not using a controller it must use the cursor
            {
                isControllerConnected = false;
                if( gunRotationWithMouse)
                {
                    // Get the current mouse position
                    Vector3 mousePosition = Input.mousePosition;

                    // Convert the mouse position to a point in the game world
                    Vector3 targetPosition = mainCamera.ScreenToWorldPoint(new Vector3(mousePosition.x, mousePosition.y, transform.position.z - mainCamera.transform.position.z));

                    // Set the Z-axis of the target position to the current Z position of the arm pivot
                    targetPosition.z = armPivot.position.z;

                    // Rotate the gun towards the cursor position
                    RotateGun(targetPosition);
                }
                
            
            }
        }
    }

    
    private void OnAimUpPerformed(InputAction.CallbackContext context)
    {
        if (gameManager != null && gameManager.currentState == GameManager.pauseState.Unpaused && !gunRotationWithMouse && !isControllerConnected)
        {
            armPivot.localRotation = Quaternion.Euler(-90f, 90f, 0f); // rotate the gun all the way up
            chibiPlayerMovement.setAimingFloat(1f);
            armLookTarget.transform.position = armLookTargetUp.transform.position;
        }
    }
    

    private void OnAimUpDiagPerformed(InputAction.CallbackContext context)

    {
        if (gameManager != null && gameManager.currentState == GameManager.pauseState.Unpaused && !gunRotationWithMouse && isControllerConnected)
        {
            armPivot.localRotation = Quaternion.Euler(-45f, 90f, 0f); // rotate the gun to the top right or left 
            chibiPlayerMovement.setAimingFloat(0.5f);
            armLookTarget.transform.position = armLookTargetForwardDiag.transform.position;
        }
    }

    private void OnAimUpCanceled(InputAction.CallbackContext context)

    {
        if (gameManager != null && gameManager.currentState == GameManager.pauseState.Unpaused && !gunRotationWithMouse && !isControllerConnected)
        {
           armPivot.localRotation = Quaternion.Euler(0f, 90f, 0f); // rotate the gun forward
           chibiPlayerMovement.setAimingFloat(0f);
           armLookTarget.transform.position = armLookTargetForward.transform.position;
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
    private void RotateGun(Vector3 targetPosition)
    {
        // Clamp the target position to be within the screen bounds
        Vector3 clampedPosition = targetPosition;
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, 0, Screen.width);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, 0, Screen.height);

        // Convert the clamped position back to world space
        Vector3 clampedWorldPosition = mainCamera.ScreenToWorldPoint(new Vector3(clampedPosition.x, clampedPosition.y, transform.position.z - mainCamera.transform.position.z));
        clampedWorldPosition.z = armPivot.position.z;

       // Calculate the direction to the target
        Vector3 lookDirection = targetPosition - armPivot.position;

        if (lookDirection.magnitude > 0.001f)
        {
            // Calculate the angle to rotate the arm pivot around the X-axis
            float angleX = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
            
            // Calculate the angle to rotate the arm pivot around the Z-axis
            float angleZ = Mathf.Atan2(lookDirection.z, lookDirection.x) * Mathf.Rad2Deg;

            // Create the target rotation quaternion
            Quaternion targetRotation = Quaternion.Euler(-angleX, 90f, -angleZ); // Adjust as necessary based on your arm's initial orientation

            // Smoothly rotate the arm pivot to face the cursor position
            armPivot.rotation = Quaternion.Slerp(armPivot.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    public void setGunRotationMouse(bool isMouse)
    {
        gunRotationWithMouse = isMouse;
    }

    public bool GetGunRoationMouse()
    {
        return gunRotationWithMouse;
    }
}