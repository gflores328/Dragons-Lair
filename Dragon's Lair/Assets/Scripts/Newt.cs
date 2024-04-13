using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newt : Enemy
{
    public GameObject newtFirePoint; // A public variable meant to grab the newt fire point so when the newt dies the fire point goes away with it

    public Material dissolveMaterial; // Reference to the dissolving material
    public Renderer renderer;

    private Collider collider;

    public float dissolveSpeed = 1.5f; // Speed at which the material dissolves
    private void Start()
    {
        
        collider = GetComponent<Collider>();

    }

    protected override void Die()
    {
        collider.enabled = false;

        if (dissolveMaterial != null)
        {
            if (renderer != null)
            {
                renderer.material = dissolveMaterial;
            }
            else
            {
                Debug.LogWarning("Renderer not found on Newt object.");
            }

            // Gradually increase the dissolve amount
            StartCoroutine(DissolveOverTime());
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

    IEnumerator DissolveOverTime()
    {
        float dissolveAmount = 0f;
        while (dissolveAmount < 30f)
        {
            dissolveAmount += dissolveSpeed * Time.deltaTime;
            dissolveAmount = Mathf.Clamp01(dissolveAmount);

            renderer.material.SetFloat("_DissolveAmount", dissolveAmount);

            yield return null;
        }
    }
}
