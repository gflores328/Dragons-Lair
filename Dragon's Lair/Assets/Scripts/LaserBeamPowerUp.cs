using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamPowerUp : Powerup
{
   private GunController gunController; // A private gun controller that will grab the gun controller from the player
   
   protected override void Awake()
   {
        base.Awake(); // calles the power up base awake function
        gunController = FindObjectOfType<GunController>(); //  find the guncontroller from player and assign it to this variable

   }

   protected override void OnTriggerEnter(Collider other) // override base ontrigger enter 
   {
        base.OnTriggerEnter(other); // call power up ontrigger enter
        if(other.gameObject.CompareTag("Player"))
        {
            gunController.setGunToLaser(); // Calls the gun controller set gun to laser function which gives the player a laserbeam
        }
   }
}
