using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeamPowerUp : Powerup
{
   private GunController gunController;
   
   protected override void Awake()
   {
        base.Awake(); 
        gunController = FindObjectOfType<GunController>();

   }

   protected override void OnTriggerEnter(Collider other)
   {
        base.OnTriggerEnter(other);
        if(other.gameObject.CompareTag("Player"))
        {
            gunController.setGunToLaser();
        }
   }
}
