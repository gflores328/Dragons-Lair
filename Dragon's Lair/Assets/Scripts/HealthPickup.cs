using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HealthPickup : Powerup
{
    public float healthToGive;

    public Renderer spriteRenderer;

    protected virtual void Update()
    {
        if (showDesc && coroutine == null) // if showDesc is true and the coroutine is not null 
        {
            powerUpDesc.SetActive(true); // Display the text

            // Access the TextMeshPro component and set its text to the power-up name
            TextMeshProUGUI textMeshPro = powerUpDesc.GetComponentInChildren<TextMeshProUGUI>(); 
            if (textMeshPro != null) // make sure there was a text mesh object was grabbed
            {
                textMeshPro.text = $"+{healthToGive}  Health!"; // set the text to display the power up description 
            }
            else
            {
                Debug.LogError("TextMeshProUGUI component not found in powerUpDesc GameObject."); // if fails display to console
            }

            coroutine = StartCoroutine(WaitAndDisable(waitTime)); // Run the wait and disable corutine
        }
    }
    
   protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if (other.CompareTag("Player"))
        {
            ChibiPlayerMovement player = other.gameObject.GetComponent<ChibiPlayerMovement>(); 
            if (player != null)
            {
                // Call the player's takeDamage function and pass the damage amount
                player.AddHealth(healthToGive);
                spriteRenderer.enabled = false;

                
            }
            
        }
    }

    
}
