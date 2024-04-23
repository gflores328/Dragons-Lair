/*
 * Created by Carlos Martinez
 * 
 * This script contains the UI Manager for Mobile Fighter Axiom.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public GameObject gameOverMenu; // Game Over Menu
    public AudioSource gameOverAudioSource; // Reference to AudioSource component
    public AudioClip gameOverSound; // Sound to play when game over

    private void OnEnable()
    {
        PlayerLives.OnPlayerDeath += EnableGameOverMenu; // Turn On Game Over Menu
    }

    private void OnDisable()
    {
        PlayerLives.OnPlayerDeath -= EnableGameOverMenu; // Turn Off Game Over Menu
    }

    public void EnableGameOverMenu()
    {
        gameOverMenu.SetActive(true); // Menu is Set Active
        if (gameOverAudioSource != null && gameOverSound != null)
        {
            // Play the game over sound if AudioSource and AudioClip are set
            gameOverAudioSource.clip = gameOverSound;
            gameOverAudioSource.Play();
        }
    }
}
