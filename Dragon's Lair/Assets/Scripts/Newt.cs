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

        float deathDuration = 1.5f;
        // Call the base Die method to handle destroying the game object
        Destroy(newtFirePoint);
        Destroy(gameObject, deathDuration);
    }

}
