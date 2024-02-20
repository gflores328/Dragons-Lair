using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ArmRotation : MonoBehaviour
{
    public InputActionReference aimAction; // Input action for aiming
    public Transform armPivot; // The pivot point of the arm
    public float rotationSpeed = 5f; // Speed of arm rotation
    public float maxRotationAngle = 45f; // Maximum rotation angle in degrees
    public float targetYRotation = 90f; // Target Y rotation
    private Vector2 lastMousePosition; // Last mouse position in screen space
    private Camera mainCamera; // Canera that will hold the reference for the cursor 
    private Quaternion initialRotation; // Initial rotation of the arm

    private void Start()
    {
       
        initialRotation = armPivot.localRotation;  // Store the initial rotation of the arm
        mainCamera = Camera.main; // sets the mainCamera object to the game main camera object
    }

    private void OnEnable()
    {
        aimAction.action.Enable(); // Enable the aim action
        aimAction.action.performed += OnAimPerformed; // Subscribe to the performed event
    }

    private void OnDisable()
    {
        aimAction.action.Disable(); // Disable the aim action
        aimAction.action.performed -= OnAimPerformed; // Unsubscribe from the performed event
    }

    private void OnAimPerformed(InputAction.CallbackContext context)
    {
        // currentMousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Debug.Log(currentMousePosition);
        if (context.control.device is Gamepad)
        {
            Vector2 input = context.ReadValue<Vector2>(); // Read input value
            if(input.y <= 1.0f && input.y >= 0.9f) // If the joy stick is going to the top  these are the parameters
            {
                armPivot.localRotation = Quaternion.Euler(-90f, 90f, 0f); // rotate the gun all the way up

            }
            else if(input.y < 0.9f && input.y >= 0.5f) // If the joy stick is going to the top right or left
            {
                armPivot.localRotation = Quaternion.Euler(-45f, 90f, 0f); // rotate the gun to the top right or left 
            }
            
            else if(input.y >= -0.9f && input.y <= -0.5f) // if the joy stick is pointed to the bottom right or left 
            {
                armPivot.localRotation = Quaternion.Euler(45f, 90f, 0f); // rotate the gun to the bottom right and left
            }
            else
            {
                armPivot.localRotation = Quaternion.Euler(0f, 90f, 0f); // if not moving the joystick point it forward
            }
        }
        else if (context.control.device is Mouse) // If not using a controller it must use the cursor
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

    void RotateGun(Vector3 targetPosition)
    {
        // Calculate the direction to the target
        Vector3 lookDirection = targetPosition - armPivot.position;

        // Ignore the z-axis rotation
        lookDirection.z = 0f;

        if (lookDirection.magnitude > 0.001f)
        {
            // Calculate the angle to rotate the arm pivot around the X-axis
            float angleX = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;

            // Create the target rotation quaternion
            Quaternion targetRotation = Quaternion.Euler(-angleX, 90f, 0f);

            // Smoothly rotate the arm pivot to face the cursor position
            armPivot.rotation = Quaternion.Slerp(armPivot.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }
}