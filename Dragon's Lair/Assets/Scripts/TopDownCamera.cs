using UnityEngine;
using Cinemachine;

public class ThirdPersonCameraController : MonoBehaviour
{
    [Header("References")]
    public CinemachineFreeLook freeLookCamera;
    public Transform playerTransform;

    [Header("Settings")]
    public float rotationSpeed = 2f;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        // Rotate the player based on mouse X movement
        float mouseX = Input.GetAxis("Mouse X") * rotationSpeed;
        playerTransform.Rotate(Vector3.up * mouseX);

        // Rotate the camera based on mouse X movement only when right-clicking
        if (Input.GetMouseButton(1))
        {
            freeLookCamera.m_XAxis.m_InputAxisValue += mouseX * Time.deltaTime * rotationSpeed;
        }
        else
        {
            // Reset camera input if right mouse button is not pressed
            freeLookCamera.m_XAxis.m_InputAxisValue = 0f;
        }
    }
}
