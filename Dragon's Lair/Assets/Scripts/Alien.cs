using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Base Script: Enemy
// Created by Aaron Torres
// The base class that all enemies will inherit from all enemies will have health and a take damage function

// Modified by Carlos Martinez
// Additions: Score Value
// The Alien script uses Aaron's Enemy script as a base and is tailored for the evil aliens in Mobile Fighter Axiom.
public class Alien : MonoBehaviour
{
    
    [SerializeField] protected float health; // a float that can only be accessed by children of this class

    public virtual void TakeDamage(float dmgAmount) // The function that children of the class will be able to access and change
    {
        health -= dmgAmount; // Subtract damage amount from the health
        Debug.Log($"Enemy took {dmgAmount} damage. Current health: {health}");
        if (health <= 0) // if health hits zero
        {
            Die(); // Kill enemy
        }
    }

    protected virtual void Die() // The function that will kill the enemy 

    {
        Debug.Log("Enemy Dead");
        Destroy(gameObject.transform.parent.gameObject); // Destroy enemy object
    }
}
