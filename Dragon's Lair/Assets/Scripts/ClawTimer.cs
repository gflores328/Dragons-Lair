/*
 * Created by Carlos Martinez
 * 
 * This script contains the code of a timer for the claw game.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class ClawTimer : MonoBehaviour
{
    private float currentTime = 0f; // Time Displayed on Screen
    public float startingTime = 30f; // Starting Time (Default: 30)

    // Adjust this factor to control the speed of the countdown
    // 1 = Normal Speed
    // Less = Slower
    // More = Faster
    public float countdownSpeed = 1.0f;

    public GameObject loseScreen;
    public AudioClip loseSound;
    public AudioSource loseAudioSource;
    public AudioSource bgmAudioSource;
    private bool hasPlayedloseSound = false;
    public TextMeshProUGUI CountdownText; // TextMeshPro Required to Show Text
    
    // Start is called before the first frame update
    void Start()
    {
        bgmAudioSource.volume = 1f; // Unmuted at the Start
        currentTime = startingTime; // Timer Starts of at Starting Time
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= countdownSpeed * Time.deltaTime; // Timer Counting Down
        CountdownText.text = currentTime.ToString("0"); // Converts Current Time into a String Displayed as a Whole Number

        // Change text color if currentTime is 10 or less
        if (currentTime <= 10)
        {
            CountdownText.color = Color.red; // Change text color to red
        }

        // If the Timer Reaches 0
        if (currentTime <= 0 && !hasPlayedloseSound)
        {
            currentTime = 0; // Timer Stops Counting Down After Hitting 0
            Time.timeScale = 0; // Everything is Paused
            loseScreen.SetActive(true); // Trigger Game Over Menu
            
            if (loseAudioSource != null && loseSound != null) // Plays Game Over Audio
            {
                Debug.Log("Playing lose sound.");
                loseAudioSource.clip = loseSound;
                loseAudioSource.Play();
            }

            // Mute the Background Music
            if (bgmAudioSource != null)
            {
                bgmAudioSource.volume = 0f; // Mute the Volume
            }

            hasPlayedloseSound = true;

        }
    }
}
