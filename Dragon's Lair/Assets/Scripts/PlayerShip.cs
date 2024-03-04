/*
 * Created by Carlos Martinez
 * 
 * This script contains code for the Player Ship in the arcade
 * minigame, Mobile Fighter Axiom. - WIP
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public float speed = 5.0f; // Movement Speed
    
    [SerializeField] protected float health; // a float that can only be accessed by children of this class

    // Update is called once per frame
    private void Update()
    {
        // Player Movement (Left and Right)
        if (Input.GetKey(KeyCode.LeftArrow)) // Left
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.RightArrow)) // Right
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }
    }

    public virtual void TakeDamage(float dmgAmount) // The function that children of the class will be able to access and change
    {
        health -= dmgAmount; // Subtract Damage Amount from the Health
        Debug.Log($"Player took {dmgAmount} damage. Current health: {health}");
        if (health <= 0) // If Health Hits Zero
        {
            Die(); // Kill Player
        }
    }

    protected virtual void Die() // The function that will kill the enemy 

    {
        Debug.Log("Player Dead");
        ScoreSystem.scoreValue += 100;
        Destroy(gameObject); // Destroy enemy object
    }
}
