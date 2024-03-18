/*
 * Created by Carlos Martinez
 * 
 * This script contains the controller for the gun.
 * Manipulates many variables such as number of shots fired at once,
 * fire rate, and accuracy.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    public int shots = 1; // Fires a number of bullets at once (Default = 1)
    public int shotsFired; // Counter for how many shots fired
    public InputActionReference fireAction; // Input action for shooting
    public BulletController bullet; // Bullet Controller

    public float fireRate; // Affects the firing rate of the gun
    public float fireRateCounter; // Counter for firing rate

    public float accuracy = 100; // Affects the accuracy of the gun (Default = 100)
    
    private bool isFiring; // Flag to track if firing action is in progress

    // Start is called before the first frame update
    void Start()
    {
        // Calls Bullet Controller
        if (!bullet) bullet = GetComponentInChildren<BulletController>(); 
        if (bullet) bullet.gun = this;

        // Initialize isFiring to false
        isFiring = false;
    }

    private void OnEnable()
    {
        fireAction.action.Enable(); // Enable the aim action
        fireAction.action.started += OnFireStarted; // Subscribe to the started event
        fireAction.action.canceled += OnFireCanceled; // Subscribe to the canceled event
    }

    private void OnDisable()
    {
        fireAction.action.Disable(); // Disable the aim action
        fireAction.action.started -= OnFireStarted; // Unsubscribe from the started event
        fireAction.action.canceled -= OnFireCanceled; // Unsubscribe from the canceled event
    }

    private void OnFireStarted(InputAction.CallbackContext context)
    {
        // Start firing when the fire action is started
        isFiring = true;
        StartCoroutine(FireRoutine());
    }

    private void OnFireCanceled(InputAction.CallbackContext context)
    {
        // Stop firing when the fire action is canceled
        isFiring = false;
    }

    private IEnumerator FireRoutine()
    {
        while (isFiring)
        {
            // Shoots if time passed is greater than or equal to the fire rate counter
            if (Time.time >= fireRateCounter)
            {
                bullet.Fire(shots); // Shoots a bullet
                fireRateCounter = Time.time + 1 / fireRate; // Affects fire rate counter
            }

            // Wait for the next frame
            yield return null;
        }
    }

    public void setFireRate(float newFireRate)

    {
        fireRate = newFireRate;
    }
}
