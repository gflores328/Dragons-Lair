using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public float healthToGive;
    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            ChibiPlayerMovement player = other.gameObject.GetComponent<ChibiPlayerMovement>(); 
            if (player != null)
            {
                // Call the player's takeDamage function and pass the damage amount
                player.AddHealth(healthToGive);

                Destroy(gameObject);
            }
            
        }
    }

    
}
