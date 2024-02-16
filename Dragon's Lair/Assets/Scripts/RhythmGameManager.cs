/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 16, 2024 at 10:17 AM
 * 
 * TUTORIAL FOLLOWED: How To Make a Rhythm Game #2 - Playing Music & Missing Notes https://www.youtube.com/watch?v=PMfhS-kEvc0
 *                    How To Make a Rhythm Game #3 - Score and Multipliers https://www.youtube.com/watch?v=dV9rdTlMHxs
 *                    How To Make a Rhythm Game #4 - Timing Hits For Better Score https://www.youtube.com/watch?v=Oi0tT7QnFhs
 * 
 * Keeps track of if the note can be hit by the player.
 * Reports to the game manager if the note has been hit or missed.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;   //Used to access text objects

public class RhythmGameManager : MonoBehaviour
{
    [Tooltip("Keeps track of if the game has started or not.")]
    public bool startPlaying;

    [Header("Score Variables")]

    [Tooltip("The current score of the player.")]
    public int currentScore;
    [Tooltip("The score of each note.")]
    public int scorePerNote;
    public int scorePerGoodNote = 125;
    public int scorePerPerfectNote = 150;

    [Header("Multiplier Variables")]

    [Tooltip("The current score multiplier.")]
    public int currentMultiplier;
    [Tooltip("Keeps track of how many notes in a row have been hit.")]
    public int multiplierTracker;
    [Tooltip("Keeps track of the amount of consecutive notes that need to be hit in order to increase the score multiplier.")]
    public int[] multiplierThresholds;

    [Header("Game Objects")]

    [Tooltip("The music that plays along with the game.")]
    public AudioSource music;
    [Tooltip("Text box that displays the score.")]
    public Text scoreText;
    [Tooltip("Text box for displaying the score multiplier.")]
    public Text multiplierText;
    [Tooltip("Object that controlls how fast the notes move.")]    
    public RhythmBeatScroller beatScroller;

    //The instance of the Rhythm Game Manager
    public static RhythmGameManager instance;

    
    // Start is called before the first frame update
    void Start()
    {
        //Set the instance of the Rhythm Game Manager to itself
        //This, along with its static typing, ensures that only one Rhythm Game Manager exists
        instance = this;

        //Initialize score and multiplier values
        currentScore = 0;
        scorePerNote = 100;
        scoreText.text = "Score: " + currentScore;
        currentMultiplier = 1;
        multiplierText.text = "Multiplier: x" + currentMultiplier;

        //Initialize multiplier thresholds
        multiplierThresholds = new int[] { 4, 8, 12 };
    }

    // Update is called once per frame
    void Update()
    {
        //If the game has not started begin the game once any key has been pressed
        if (!startPlaying)
        {
            if (Input.anyKeyDown)
            {
                startPlaying = true;
                //Begin moving the notes
                beatScroller.hasStarted = true;
                //Play the music
                music.Play();
            }
        }
    }

    //Adjusts multiplier values and text boxes when a note is hit
    public void NoteHit()
    {
        //Debug.Log("Hit on time");

        //If there are still multiplier thresholds to hit...
        if (currentMultiplier - 1 < multiplierThresholds.Length)
        {
            //Increase the number of consecutive hit notes
            multiplierTracker++;

            //If the current multiplier threshold has been reached, reset the number of consecutive hit notes and increase the multiplier
            if (multiplierThresholds[currentMultiplier - 1] <= multiplierTracker)
            {
                multiplierTracker = 0;
                currentMultiplier++;
                //Update the multiplier text
                multiplierText.text = "Multiplier: x" + currentMultiplier;

            }
        }
        
        //Update the score text
        scoreText.text = "Score: " + currentScore;
    }

    //Increases the score based on a normal hit
    public void NormalHit()
    {
        //Increase the score according to the current multiplier
        currentScore += scorePerNote * currentMultiplier;
        NoteHit();
    }

    //Increases the score based on a good hit
    public void GoodHit()
    {
        //Increase the score according to the current multiplier
        currentScore += scorePerGoodNote * currentMultiplier;
        NoteHit();
    }

    //Increases the score based on a perfect hit
    public void PerfectHit()
    {
        //Increase the score according to the current multiplier
        currentScore += scorePerPerfectNote * currentMultiplier;
        NoteHit();
    }

    //Resets multiplier values when a note is missed
    public void NoteMissed()
    {
        Debug.Log("Missed Note");

        //Reset the multiplier back to one
        currentMultiplier = 1;
        //Reset the number of consecutive notes hit
        multiplierTracker = 0;
        //Reset the multiplier text
        multiplierText.text = "Multiplier: x" + currentMultiplier;
    }
}
