using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpPowerUp : Powerup
{

    public float initialJumpVelocitySetter = 15f;

    private ChibiPlayerMovement chibiPlayerMovement;

    void Awake()
    {
        base.Awake();
        chibiPlayerMovement = FindObjectOfType<ChibiPlayerMovement>();
    }

    protected override void OnTriggerEnter(Collider other)
    {
        base.OnTriggerEnter(other);
        if(other.gameObject.CompareTag("Player"))
        {
            chibiPlayerMovement.setInitalJump(initialJumpVelocitySetter);
        }
    }
}
