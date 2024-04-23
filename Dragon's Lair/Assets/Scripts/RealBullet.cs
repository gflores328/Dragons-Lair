using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RealBullet : MonoBehaviour
{
    // Public variables
    public float bulletSpeed = 10f; // Speed of the bullet

    [SerializeField] protected float damageAmount = 50;
    public float lifetime = 2f; // Time before the bullet is destroyed if it doesn't collide

    public ParticleSystem particle; // Particle system for impact

    // Private variables
    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to the bullet object
        rb = GetComponent<Rigidbody>();

        // Set the velocity of the bullet to move it forward
        rb.velocity = transform.forward * bulletSpeed;

        if (particle != null)
        {
            particle.Play();
        }
        // Destroy the bullet after 'lifetime' seconds if it hasn't collided with anything
        Destroy(gameObject, lifetime);
    }

    void OnCollisionEnter(Collision collision)
    {
        bool destroy = true;
        // If colliding with an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            // Get the Enemy component from the collided GameObject
            Enemy enemy = collision.gameObject.GetComponent<Enemy>();

            // Check if the enemy component exists
            if (enemy != null)
            {
                // Call the TakeDamage function of the enemy with the specified damage amount
                enemy.TakeDamage(damageAmount);
            }
            else
            {
                Debug.LogError("Enemy component not found on " + collision.gameObject.name);
            }

            // Destroy the bullet upon collision with an enemy
            Destroy(gameObject);
            
        }
        if(collision.gameObject.CompareTag("Player"))
        {
            destroy = false;
        }
        if(destroy)
        {
            Destroy(gameObject);
        }
    }
}
