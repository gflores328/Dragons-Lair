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
    }
}
