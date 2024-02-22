/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 21, 2024 at 10:25 PM
 * 
 * TUTORIAL FOLLOWED: How To Make a Rhythm Game #2 - Playing Music & Missing Notes https://www.youtube.com/watch?v=PMfhS-kEvc0
 *                    How To Make a Rhythm Game #3 - Score and Multipliers https://www.youtube.com/watch?v=dV9rdTlMHxs
 *                    Rhythm Game Tutorial #4 - Timing Hits For Better Score https://www.youtube.com/watch?v=Oi0tT7QnFhs
 *                    Rhythm Game Tutorial #5 - Showing Reslts & Ranking https://www.youtube.com/watch?v=Usuh7WUAPbg
 * 
 * Manages the game in a variety of ways:
 * - Keeps track of score and multipliers
 * - Keeps track of the total number of notes hit or missed by the player
 * - Plays music
 * - Updates text boxes
 * - Moves the notes
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
    public int scorePerNote, scorePerGoodNote, scorePerPerfectNote;

    [Header("Multiplier Variables")]

    [Tooltip("The current score multiplier.")]
    public int currentMultiplier;
    [Tooltip("Keeps track of how many notes in a row have been hit.")]
    public int multiplierTracker;
    [Tooltip("Keeps track of the amount of consecutive notes that need to be hit in order to increase the score multiplier.")]
    public int[] multiplierThresholds;

    [Header("Total Note Counts")]

    [Tooltip("The total number of notes in the scene")]
    public int totalNotes;
    [Tooltip("Total amount of notes with a certain hit quality.")]
    public int normalHits, goodHits, perfectHits, missedHits;

    [Header("Game Objects")]

    [Tooltip("The music that plays along with the game.")]
    public AudioSource music;
    [Tooltip("Text box that displays the score.")]
    public Text scoreText;
    [Tooltip("Text box for displaying the score multiplier.")]
    public Text multiplierText;
    [Tooltip("Object that controlls how fast the notes move.")]    
    public RhythmBeatScroller beatScroller;
    [Tooltip("Introduction / Tutorial screen")]
    public GameObject introScreen;

    [Header("Results Screen Objects")]

    [Tooltip("Canvas containing results screen")]
    public GameObject resultsScreen;
    [Tooltip("Text boxes in results screen")]
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    //A list of directions recognized by the game
    public enum Direction { Up, Down, Left, Right };

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
        scorePerGoodNote = 125;
        scorePerPerfectNote = 150;
        scoreText.text = "Score: " + currentScore;
        currentMultiplier = 1;
        multiplierText.text = "Multiplier: x" + currentMultiplier;

        //Initialize the total number of notes in the scene
        totalNotes = FindObjectsOfType<RhythmNoteObject>().Length;

        //Initialize multiplier thresholds
        multiplierThresholds = new int[] { 4, 8, 12 };
    }

    // Update is called once per frame
    void Update()
    {
        //If the game has not started begin the game once any key has been pressed
        if (!startPlaying)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                startPlaying = true;
                //Remove the intro screen
                introScreen.SetActive(false);
                //Begin moving the notes
                beatScroller.hasStarted = true;
                //Play the music
                music.Play();
            }
        }
        //If the game has already started, the music has stopped playing, AND the results screen isn't active yet...
        //Update the text in the results screen and display the results screen
        else
        {
            if (!music.isPlaying && !resultsScreen.activeInHierarchy)
            {
                //Update the results text
                normalsText.text = normalHits.ToString();
                goodsText.text = goodHits.ToString();
                perfectsText.text = perfectHits.ToString();
                missesText.text = missedHits.ToString();

                //Calculate the percentage of notes hit
                float totalHit = totalNotes - missedHits;
                float percentHit = (totalHit / totalNotes) * 100f;
                //Update the results text
                percentHitText.text = percentHit.ToString("F1") + "%";

                //Calculate player rank and update rank text
                //Not a fan of how this looks but in terms of logic this seems to be the most "readable"
                rankText.text = "F";
                if (percentHit > 40)
                {
                    rankText.text = "D";
                    if (percentHit > 55)
                    {
                        rankText.text = "C";
                        if (percentHit > 70)
                        {
                            rankText.text = "B";
                            if (percentHit > 85)
                            {
                                rankText.text = "A";
                                if (percentHit > 95)
                                {
                                    rankText.text = "S";
                                }
                            }
                        }
                    }
                }

                //Update final score text
                finalScoreText.text = currentScore.ToString();

                //Display the results screen
                resultsScreen.SetActive(true);
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
        normalHits++;
        NoteHit();
    }

    //Increases the score based on a good hit
    public void GoodHit()
    {
        //Increase the score according to the current multiplier
        currentScore += scorePerGoodNote * currentMultiplier;
        goodHits++;
        NoteHit();
    }

    //Increases the score based on a perfect hit
    public void PerfectHit()
    {
        //Increase the score according to the current multiplier
        currentScore += scorePerPerfectNote * currentMultiplier;
        perfectHits++;
        NoteHit();
    }

    //Resets multiplier values when a note is missed
    public void NoteMissed()
    {
        //Debug.Log("Missed Note");

        missedHits++;

        //Reset the multiplier back to one
        currentMultiplier = 1;
        //Reset the number of consecutive notes hit
        multiplierTracker = 0;
        //Reset the multiplier text
        multiplierText.text = "Multiplier: x" + currentMultiplier;
    }
}
