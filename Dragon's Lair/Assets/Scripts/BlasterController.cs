/*
 * Created by Carlos Martinez
 * 
 * This script contains the controller for the blaster from the arcade minigame, Mobile Fighter Axiom.
 * Manipulates many variables such as number of shots fired at once,
 * fire rate, and accuracy.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class BlasterController : MonoBehaviour
{
    public int shots = 1; // Fires a number of bullets at once (Default = 1)
    public int shotsFired; // Counter for how many shots fired

    public SolarBulletController bullet; // Bullet Controller
    public AudioSource bulletAudio; // Audio Source

    public float fireRate; // Affects the firing rate of the gun
    private float fireRateCounter; // Counter for firing rate

    public float accuracy = 100; // Affects the accuracy of the gun (Default = 100)

   
    public InputActionReference fireAction; // Input action reference for firing

    // Start is called before the first frame update
    void Start()
    {
        // Calls Bullet Controller
        if (!bullet) bullet = GetComponentInChildren<SolarBulletController>();
        if (bullet) bullet.blast = this;
           
        
    }

    private void OnEnable()
    {
        
        fireAction.action.performed += OnFirePerformed;
        
    }

    private void OnDisable()
    {
       fireAction.action.performed -= OnFirePerformed;
    }

    private void OnFirePerformed(InputAction.CallbackContext context)
    {
        Shoot();
    }

    private void Shoot()
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

