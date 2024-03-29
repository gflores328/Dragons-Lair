/*
 * Created by Carlos Martinez
 * 
 * This script contains the controller for the alien gun from the arcade minigame, Mobile Fighter Axiom.
 * Manipulates many variables such as number of shots fired at once,
 * fire rate, and accuracy.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienGunController : MonoBehaviour
{
    public int shots = 1; // Fires a number of bullets at once (Default = 1)
    public int shotsFired; // Counter for how many shots fired

    public AlienBulletController bullet; // Bullet Controller
    public AudioSource bulletAudio; // Audio Source

    public float fireRate; // Affects the firing rate of the gun
    public float fireRateCounter; // Counter for firing rate

    public float accuracy = 100; // Affects the accuracy of the gun (Default = 100)

    public float fireDelay = 0.5f; // A Delay Before the Alien Fires Again
    float cooldownTimer = 0; // Firing Cooldown Timer

    // Start is called before the first frame update
    void Start()
    {
        // Calls Bullet Controller
        if (!bullet) bullet = GetComponentInChildren<AlienBulletController>();
        if (bullet) bullet.plasma = this;
    }

    // Update is called once per frame
    void Update()
    {
        cooldownTimer -= Time.deltaTime;

        // Fires gun when the "Space" button is pressed
        if (cooldownTimer <= 0)
        {
            // Shoots if time passed is greater than or equal to the fire rate counter
            if (Time.time >= fireRateCounter)
            {
                bulletAudio.Play(); // Plays sound when fired

                bullet.Fire(shots); // Shoots a bullet

                fireRateCounter = Time.time + 1 / fireRate; // Affects fire rate counter
            }
        }
    }
}
