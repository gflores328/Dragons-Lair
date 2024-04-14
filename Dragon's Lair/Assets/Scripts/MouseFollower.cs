using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseFollower : MonoBehaviour
{

    private ArmRotation armRotation;

    void Start()
    {
        armRotation = FindObjectOfType<ArmRotation>(); // Grabs the arm rotation script 
    }
    // Update is called once per frame
    void Update()
    {
        if(armRotation.GetGunRoationMouse())
        {
            // Create a ray from the main camera through the mouse position
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Calculate the distance from the camera to the object's current position
            float distance = Vector3.Dot(transform.position - Camera.main.transform.position, Camera.main.transform.forward);

            // Calculate the position along the ray at that distance
            Vector3 worldPosition = ray.origin + ray.direction * distance;

            // Ensure that the Z-coordinate of the world position matches the current position of the GameObject which should be 0
            worldPosition.z = transform.position.z;

            // Update the position of the gameobject to the mouse pointing
            transform.position = worldPosition;
        }
        
    }
}
