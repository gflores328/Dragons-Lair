using System.Collections;
using UnityEngine;

public class FireRatePowerup : Powerup
{
    public float fireRateSetter;

    private GunController gunController;

    protected override void Awake()
    {
        base.Awake(); // Call the base class Awake method
        gunController = FindObjectOfType<GunController>(); // Find the GunController in the scene
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other); // Call the base class OnTriggerEnter method
        if (other.gameObject.CompareTag("Player"))
        {
            gunController?.setFireRate(fireRateSetter); // Set the fire rate of the gun the "?" is a C# key and will check if the object if not null before attempting to attach and will skip 
        }
    }
}
