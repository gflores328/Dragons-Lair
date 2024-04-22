using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Newt : Enemy
{
    public GameObject newtFirePoint; // A public variable meant to grab the newt fire point so when the newt dies the fire point goes away with it

    public Material hitMaterial; // a material that will show when the enemy has been shot
    public Material dissolveMaterial; // Reference to the dissolving material
    public Renderer renderer;

    private Collider collider;

    public float dissolveSpeed = 0.01f; // Speed at which the material dissolves
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
                // Gradually increase the dissolve amount
                Debug.Log("Coroutine Started");
                StartCoroutine(DissolveOverTime());
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

    IEnumerator DissolveOverTime()
    {
        float dissolveAmount = 0f;
        while (dissolveAmount <= 1f) // Change the loop condition to dissolveAmount < 1f
        {
            dissolveAmount += dissolveSpeed * Time.deltaTime;
            Debug.Log("" + dissolveAmount);
            dissolveAmount = Mathf.Clamp01(dissolveAmount);

            renderer.material.SetFloat("_Dissolve", dissolveAmount);

            yield return null;
        }
    }


    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(HitDelay());
    }

    IEnumerator HitDelay()
    {
        if (health != 0)
        {
            Material original = renderer.material;
            renderer.material = hitMaterial;
            yield return new WaitForSeconds(.1f);
            renderer.material = original;
        }
    }

}
