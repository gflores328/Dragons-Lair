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

    private Quaternion initialRotation; // Initial rotation of the arm

    private void Start()
    {
        // Store the initial rotation of the arm
        initialRotation = armPivot.localRotation;
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
        
        Debug.Log(input);
        // Calculate the target rotation angle based on input
        // float targetAngle = input.x * maxRotationAngle;

        // // Apply the target rotation to the arm while keeping the Y rotation unchanged
        // armPivot.localRotation = initialRotation * Quaternion.Euler(targetAngle, 0f, 0f);
        // armPivot.localEulerAngles = new Vector3(armPivot.localEulerAngles.x, targetYRotation, armPivot.localEulerAngles.z);
    }
}