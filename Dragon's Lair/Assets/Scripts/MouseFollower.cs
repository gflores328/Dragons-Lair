using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{

    private ArmRotation armRotation;

    private SpriteRenderer spriteRenderer;



    void Start()
    {
        armRotation = FindObjectOfType<ArmRotation>(); // Grabs the arm rotation script 
        spriteRenderer = GetComponent<SpriteRenderer>(); // grans the sprite renderer from the game object
    }
    
    void Update()
    {
        if(armRotation.GetGunRoationMouse())
        {
            
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition); // Create a ray from the main camera through the mouse position

            
            float distance = Vector3.Dot(transform.position - Camera.main.transform.position, Camera.main.transform.forward);  // Calculate the distance from the camera to the object's current position

            // Calculate the position along the ray at that distance
            Vector3 worldPosition = ray.origin + ray.direction * distance;

            // Ensure that the Z of the world position matches the current position of the GameObject which should be 0
            worldPosition.z = transform.position.z;

            // Update the position of the gameobject to the mouse position
            transform.position = worldPosition;
        }
        
    }

     // Method to set the color of the cursor
    public void SetColor(Color color)
    {
        spriteRenderer.color = color;
    }
}
