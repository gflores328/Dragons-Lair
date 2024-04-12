/*
 * Created by: Carlos Martinez
 *
 * This script contains the movement of the claw in the crane game (WIP).
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMovement : MonoBehaviour
{
    public float moveSpeed = 5f; // Adjust this value to control the speed of the claw
    public float verticalSpeed = 3f; // Adjust this value to control the vertical speed of the claw

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Get input for horizontal and vertical movement
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        // Calculate movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput) * moveSpeed * Time.deltaTime;

        // Apply movement to the Rigidbody
        rb.MovePosition(rb.position + movement);
    }
}
