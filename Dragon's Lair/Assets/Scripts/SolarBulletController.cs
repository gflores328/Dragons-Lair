/*
 * Created by Carlos Martinez
 * 
 * This script contains the controller for the solar energy bullet in the arcade minigame, Mobile Fighter Axiom.
 * It can manipulate the speed of the bullet and call the Blaster Controller.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SolarBulletController : MonoBehaviour
{
    public float speed = 15; // Movement speed of the bullet (Default = 15)
    public float damageAmount = 50; // Attack Power

    private ParticleSystem part; // Particle System
    public AudioSource deathAudio; // Audio Source
    public GameObject score;
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>(); // For particle collision

    public BlasterController blast; // Blaster Controller

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
            Alien enemy = other.GetComponent<Alien>();

            // Check if the enemy component exists
            if (enemy != null)
            {
                // Call the TakeDamage function of the enemy with the specified damage amount
                deathAudio.Play();
                score.GetComponent<ScoreSystem>().scoreValue += 100;
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
        blast.shotsFired++; // Raises counter for shots fired
        part.Emit(shots); // Fires bullet

        // Accuracy
        var partS = part.shape;
        partS.angle = (100 - blast.accuracy);

        // Bullet speed 
        var partM = part.main;
        partM.startSpeed = speed;
    }
}
