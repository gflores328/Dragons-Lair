/*
 * Created by Carlos Martinez
 * 
 * This script contains the controller for the bullet.
 * It can manipulate the speed of the bullet and call the Gun Controller.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 15; // Movement speed of the bullet (Default = 15)
    public float damageAmount = 50;

    private ParticleSystem part; // Particle System
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>(); // For particle collision

    public GunController gun; // Gun Controller
    
    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>(); // Calls particle system
    }
    
    // For when particles collide
    private void OnParticleCollision(GameObject other)
    {
        // If particle hits an enemy
        if (other.CompareTag("Enemy"))
        {
            // Get the Enemy component from the collided GameObject
            Enemy enemy = other.GetComponent<Enemy>();

            // Check if the enemy component exists
            if (enemy != null)
            {
                // Call the TakeDamage function of the enemy with the specified damage amount
                enemy.TakeDamage(damageAmount);
            }
            else
            {
                Debug.LogError("Enemy component not found on " + other.gameObject.name);
            }
        }
    }

    // Affects the firing of the bullet
    public void Fire(int shots)
    {
        gun.shotsFired++; // Raises counter for shots fired
        part.Emit(shots); // Fires bullet

        // Accuracy
        var partS = part.shape;
        partS.angle = (100 - gun.accuracy);
        
        // Bullet speed 
        var partM = part.main;
        partM.startSpeed = speed;
    }
}
