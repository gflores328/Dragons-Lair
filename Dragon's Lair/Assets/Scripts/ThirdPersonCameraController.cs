using UnityEngine;
using Cinemachine;
using UnityEngine.InputSystem;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")]
    public CinemachineFreeLook freeLookCamera;
    public Transform playerTransform;

    [Header("Settings")]
    public float rotationSpeed = 2f;

    public InputActionReference lookAction; // Input action for camera rotation

    private Vector2 previousMousePosition; // Store the previous mouse position
    
    private float currentInputValue = 0;
    private void FixedUpdate()
    {
        freeLookCamera.m_XAxis.m_InputAxisValue += currentInputValue * rotationSpeed * Time.deltaTime;
    }
    private void OnEnable()
    {
        lookAction.action.Enable(); // Enable the look action
        lookAction.action.performed += OnLookPerformed; // Subscribe to the performed event

        // Initialize the previous mouse position
        previousMousePosition = Mouse.current.position.ReadValue();
    }

    private void OnDisable()
    {
        lookAction.action.Disable(); // Disable the look action
        lookAction.action.performed -= OnLookPerformed; // Unsubscribe from the performed event
    }

    private void OnLookPerformed(InputAction.CallbackContext context)
    {
        Vector2 input = context.ReadValue<Vector2>(); // Read input value

        currentInputValue = input.x;
        
        
    }
}
