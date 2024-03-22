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

    private void OnCollisionEnter(Collision collision)
    {
        // If Player Makes Contact with the Enemy
        if (collision.collider.gameObject.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject); // Alien is Destroyed
            playerDeathAudio.Play();
            lives -= 1; // Remove 1 Life
            Destroy(gameObject); // Player is Destroyed
            
            if (lives > 0) // If Player has Extra Lives
            {
                Debug.Log("Level Reset");
                SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Level Reset
            }

            else if(lives <= 0) // If Player Runs Out of Lives
            {
                OnPlayerDeath?.Invoke(); // Trigger Game Over
                lives = 3;
            }
        }
    }

}
