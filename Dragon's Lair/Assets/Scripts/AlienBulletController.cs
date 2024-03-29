/*
 * Created by Carlos Martinez
 * 
 * This script contains the controller for the alien bullet in the arcade minigame, Mobile Fighter Axiom.
 * It can manipulate the speed of the bullet and call the AlienGunController Controller.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.SceneManagement;
using System;
using UnityEngine.SocialPlatforms.Impl;

public class AlienBulletController : MonoBehaviour
{
    public float speed = 15; // Movement speed of the bullet (Default = 15)
    public float damageAmount = 50; // Attack Power

    private ParticleSystem part; // Particle System
    public AudioSource deathAudio; // Audio Source
    private List<ParticleCollisionEvent> collisionEvents = new List<ParticleCollisionEvent>(); // For particle collision
    public static event Action OnPlayerDeath;

    public AlienGunController plasma; // Alien Gun Controller

    // Start is called before the first frame update
    void Start()
    {
        part = GetComponent<ParticleSystem>(); // Calls particle system
    }

    // For when particles collide
    private void OnParticleCollision(GameObject other)
    {
        // If particle hits the player
        if (other.CompareTag("Player"))
        {
            // Get the player component from the collided GameObject
            PlayerLives player = other.GetComponent<PlayerLives>();

            // Check if the player component exists
            if (player != null)
            {
                // Call the TakeDamage function of the player with the specified damage amount
                deathAudio.Play();
                player.TakeDamage(damageAmount);
            }
            else
            {
                Debug.LogError("Player component not found on " + other.gameObject.name);
            }
        }
    }

    // Affects the firing of the bullet
    public void Fire(int shots)
    {
        plasma.shotsFired++; // Raises counter for shots fired
        part.Emit(shots); // Fires bullet

        // Accuracy
        var partS = part.shape;
        partS.angle = (100 - plasma.accuracy);

        // Bullet speed 
        var partM = part.main;
        partM.startSpeed = speed;
    }
}
