/*
 * Created By: Gabriel Flores
 * 
 * This script will hold the behavior for when the bullet prefab collides with another object
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletHit : MonoBehaviour
{
    public float damageAmount = 10f; // Amount of damage the bullet does to the player

    private void OnCollisionEnter(Collision collision)
    { 
        // Check if the collided object has a ChibiPlayerMovement component (assuming the player has this component)
        ChibiPlayerMovement player = collision.gameObject.GetComponent<ChibiPlayerMovement>();

        // If the collided object is the player and we found the ChibiPlayerMovement component
        if (player != null)
        {
            // Call the player's takeDamage function and pass the damage amount
            player.takeDamage(damageAmount);

            
        }
        // Destroy the bullet gameObject
        Destroy(gameObject);
    }
}
