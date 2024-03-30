using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : Powerup
{

    public float initialJumpVelocitySetter = 15f; // what the new jump velocity should be set to

    private ChibiPlayerMovement chibiPlayerMovement; // Grab the players movement script

    protected override void Awake()
    {
        base.Awake(); // Call power up awake function 
        chibiPlayerMovement = FindObjectOfType<ChibiPlayerMovement>(); // Find the players movement script in the scene
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other); // call base ontrigger enter function
        if(other.gameObject.CompareTag("Player"))
        {
            chibiPlayerMovement.setInitalJump(initialJumpVelocitySetter); // Call the set jump function from player and set jump to the setter in this script
        }
    }
}
