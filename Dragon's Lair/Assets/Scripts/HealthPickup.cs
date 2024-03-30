using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : Powerup
{
    public float healthToGive;

    public Renderer spriteRenderer;
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
