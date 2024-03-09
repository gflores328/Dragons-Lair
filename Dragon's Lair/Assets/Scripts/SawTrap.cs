using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawTrap : MonoBehaviour
{
   private void OnTriggerEnter(Collider collision)
    {
        
            ChibiPlayerMovement player = collision.gameObject.GetComponent<ChibiPlayerMovement>(); 
            if (player != null)
            {
                // Call the player's takeDamage function and pass the damage amount
                player.takeDamage(1);

                
            }

        
    }
}
