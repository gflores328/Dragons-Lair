/*
 * Created by Carlos Martinez
 * 
 * This script contains the code for the instruction panel for the claw
 * and space shooter game.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawStart : MonoBehaviour
{
    public GameObject startMenu;
    public bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0; // Game is Paused
        startMenu.SetActive(true); // Instructions Panel is Active
    }

    // Update is called once per frame
    void Update()
    {
        // If Game has Not Started
        if (!started)
        {
            // Press 'Enter' Key
            if (Input.GetKeyDown(KeyCode.Return))
            {
                started = true; // Game Start
                startMenu.SetActive(false); // Panel Disappears
                Time.timeScale = 1; // Game Resumes

            }
        }
    }
}
