/*
 * Created by Carlos Martinez
 * 
 * This script contains the life system for Mobile Fighter Axiom.
 */

using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    public static int lives = 3;
    //public int lives = 3; // Default: 3 Lives
    public static event Action OnPlayerDeath;

    public AudioSource playerDeathAudio; // Audio Source

    [SerializeField] protected float health = 50; // a float that can only be accessed by children of this class

    public virtual void TakeDamage(float dmgAmount) // The function that children of the class will be able to access and change
    {
        health -= dmgAmount; // Subtract damage amount from the health
        Debug.Log($"Player took {dmgAmount} damage. Current health: {health}");
        if (health <= 0) // if health hits zero
        {
            Die();

        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // If Player Makes Contact with the Enemy
        if (collision.collider.gameObject.tag == "Enemy")
        {
            Die();
        }
    }

    protected virtual void Die() // The function that will kill the enemy 

    {
        Debug.Log("Player Dead");
        playerDeathAudio.Play();
        lives -= 1; // Remove 1 Life
        //Destroy(gameObject); // Destroy player object

        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(GameObject i in Enemies)
        {
            Destroy(i.transform.parent.gameObject);
        }


        if (lives > 0) // If Player has Extra Lives
        {
            Debug.Log("Level Reset");
            //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Level Reset
        }

        else if (lives <= 0) // If Player Runs Out of Lives
        {
            Destroy(gameObject);
            OnPlayerDeath?.Invoke(); // Trigger Game Over
            lives = 3;
        }
    }

}
