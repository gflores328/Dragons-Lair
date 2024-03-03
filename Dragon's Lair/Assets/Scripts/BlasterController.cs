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

public class BlasterController : MonoBehaviour
{
    public int shots = 1; // Fires a number of bullets at once (Default = 1)
    public int shotsFired; // Counter for how many shots fired

    public SolarBulletController bullet; // Bullet Controller

    public float fireRate; // Affects the firing rate of the gun
    public float fireRateCounter; // Counter for firing rate

    public float accuracy = 100; // Affects the accuracy of the gun (Default = 100)

    // Start is called before the first frame update
    void Start()
    {
        // Calls Bullet Controller
        if (!bullet) bullet = GetComponentInChildren<SolarBulletController>();
        if (bullet) bullet.blast = this;
    }

    // Update is called once per frame
    void Update()
    {
        // Fires gun when the "Space" button is pressed
        if (Input.GetKey(KeyCode.Space))
        {
            // Shoots if time passed is greater than or equal to the fire rate counter
            if (Time.time >= fireRateCounter)
            {
                bullet.Fire(shots); // Shoots a bullet

                fireRateCounter = Time.time + 1 / fireRate; // Affects fire rate counter
            }
        }
    }
}