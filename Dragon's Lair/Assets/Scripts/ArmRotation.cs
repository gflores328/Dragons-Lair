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
    //private Vector2 currentMousePosition;

    private Vector2 lastMousePosition; // Last mouse position in screen space
    private Camera mainCamera;
    private Quaternion initialRotation; // Initial rotation of the arm

    private void Start()
    {
        // Store the initial rotation of the arm
        initialRotation = armPivot.localRotation;
        mainCamera = Camera.main;
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
            if(input.y <= 1.0f && input.y >= 0.9f)
            {
                armPivot.localRotation = Quaternion.Euler(-90f, 90f, 0f);

            }
            else if(input.y < 0.9f && input.y >= 0.5f) 
            {
                armPivot.localRotation = Quaternion.Euler(-45f, 90f, 0f);
            }
            else if(input.y >= -1.0f && input.y <= -0.9f)
            {
                armPivot.localRotation = Quaternion.Euler(90f, 90f, 0f);

            }
            else if(input.y >= -0.9f && input.y <= -0.5f) 
            {
                armPivot.localRotation = Quaternion.Euler(45f, 90f, 0f);
            }
            else
            {
                armPivot.localRotation = Quaternion.Euler(0f, 90f, 0f);
            }
        }
        else if (context.control.device is Mouse)
        {
            Vector2 currentMousePosition = Mouse.current.position.ReadValue();

            // Calculate the mouse delta
            Vector2 mouseDelta = currentMousePosition - lastMousePosition;

            // Convert mouse delta to world space
            Vector3 mouseDeltaWorld = mainCamera.ScreenToWorldPoint(new Vector3(mouseDelta.x, mouseDelta.y, mainCamera.nearClipPlane));
            Vector3 lastMousePositionWorld = mainCamera.ScreenToWorldPoint(new Vector3(lastMousePosition.x, lastMousePosition.y, mainCamera.nearClipPlane));

            // Calculate the direction vector from the arm pivot to the mouse position
            Vector3 direction = mouseDeltaWorld - lastMousePositionWorld;

            // Calculate the rotation angle around the X-axis based on mouse movement
            float angleX = Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;

            // Clamp the rotation angle to a certain range
            angleX = Mathf.Clamp(angleX, -maxRotationAngle, maxRotationAngle);

            // Create the rotation quaternion
            Quaternion targetRotation = Quaternion.Euler(angleX, targetYRotation, 0f);

            // Apply smooth rotation
            armPivot.rotation = Quaternion.Slerp(armPivot.rotation, targetRotation, rotationSpeed * Time.deltaTime);

            // Update the last mouse position
            lastMousePosition = currentMousePosition;

        }
        //Debug.Log(input);
        // Calculate the target rotation angle based on input
        // float targetAngle = input.x * maxRotationAngle;

        // // Apply the target rotation to the arm while keeping the Y rotation unchanged
        // armPivot.localRotation = initialRotation * Quaternion.Euler(targetAngle, 0f, 0f);
        // armPivot.localEulerAngles = new Vector3(armPivot.localEulerAngles.x, targetYRotation, armPivot.localEulerAngles.z);
    }
}