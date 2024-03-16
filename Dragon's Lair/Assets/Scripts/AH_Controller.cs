using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class AH_PlayerController : MonoBehaviour
{
    // You must set the cursor in the inspector.
    public Sprite CustomCursor;
    public Sprite ClickCursor;

    public Rigidbody rb;

    public float speed = 5;

    public LayerMask layers;


    [SerializeField] private Camera mainCamera;

    private Vector2 center;

    public void SetCursor(Sprite sprite, Vector2 center)
    {
        Cursor.SetCursor(CustomCursor.texture, center, CursorMode.Auto);
    }


    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector2 center = default;
        SetCursor(CustomCursor, center); //starts as open hand cursor
    }

    void FixedUpdate()
    {
        // Paddle will only move if we hold down the mouse button
        Ray paddleGrabed = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(paddleGrabed, out RaycastHit raycastHit, float.MaxValue, layers))
        {
            //change cursor to closed hand
            Cursor.SetCursor(ClickCursor.texture, center, CursorMode.Auto);

            transform.position = raycastHit.point; //moves puck where mouse is
          
        }
        else
        {
            //change cursor to open hand
            Cursor.SetCursor(CustomCursor.texture, center, CursorMode.Auto);

            rb.velocity = Vector3.zero;
        }
    }


}
