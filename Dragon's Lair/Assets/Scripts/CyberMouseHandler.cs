using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberMouseHandler : MonoBehaviour
{
    private string leftMouseCollider = "MouseColliderLeft"; // Name of the first collider
    private string rightMouseCollider = "MouseColliderRight"; // Name of the second collider

    public bool isAimingRight = true; // A bool variable to track aiming direction

    void Update()
    {
        // Cast a ray from the mouse position
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            // Check if the collider hit has the name of the left mouse collider
            if (hit.collider.gameObject.name == leftMouseCollider)
            {
                // Do something specific when mouse is over the left collider
                isAimingRight = false;
                Debug.Log("Mouse entered left collider");
            }
            // Check if the collider hit has the name of the right mouse collider
            else if (hit.collider.gameObject.name == rightMouseCollider)
            {
                // Do something specific when mouse is over the right collider
                isAimingRight = true;
                Debug.Log("Mouse entered right collider");
            }
        }
    }
}
