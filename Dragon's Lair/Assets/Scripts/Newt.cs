using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newt : Enemy
{
    public GameObject newtFirePoint; // A public variable meant to grab the newt fire point so when the newt dies the fire point goes away with it

    public Material dissolveMaterial; // Reference to the dissolving material
    public Renderer renderer;

    protected override void Die()
    {
        // Check if the dissolve material is assigned
        if (dissolveMaterial != null)
        {
            // Apply the dissolve material to the renderer of the game object
            
            if (renderer != null)
            {
                renderer.material = dissolveMaterial;
            }
            else
            {
                Debug.LogWarning("Renderer not found on Newt object.");
            }
        }
        else
        {
            Debug.LogWarning("Dissolve material not assigned to Newt.");
        }

        // Find the collider component attached to the Newt enemy
        Collider newtCollider = GetComponent<Collider>();
        if (newtCollider != null)
        {
            // Find the player GameObject
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            if (player != null)
            {
                // Find the collider component attached to the player
                Collider playerCollider = player.GetComponent<Collider>();
                if (playerCollider != null)
                {
                    // Ignore collision between the Newt and the player
                    Physics.IgnoreCollision(newtCollider, playerCollider);
                }
                else
                {
                    Debug.LogWarning("Collider not found on Player object.");
                }
            }
            else
            {
                Debug.LogWarning("Player object not found.");
            }
        }
        else
        {
            Debug.LogWarning("Collider not found on Newt object.");
        }

        float deathDuration = 1.5f;
        // Call the base Die method to handle destroying the game object
        Destroy(newtFirePoint);
        Destroy(gameObject, deathDuration);
    }

}
