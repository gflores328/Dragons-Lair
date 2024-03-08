/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Mar 8, 2024 at 9:14 AM
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
using UnityEngine.InputSystem;  //Used to get the input to start the game

public class RhythmGameManager : MonoBehaviour
{
    [Tooltip("Keeps track of if the game has started or not.")]
    public bool startPlaying;
    [Tooltip("Keeps track of the center of the buttons. This affects note position and the quality of note hits. It is grabbed from the button holder in the start function.")]
    public float buttonCenter;
    [Tooltip("Keep track of when the game is paused")]
    public bool isPaused;

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
    [Tooltip("Object that holds the beatScroller")]
    public GameObject beatScrollerObject;

    [Tooltip("Object that holds the Chibi Poses ")]
    public GameObject chibiHolder;

    [Tooltip("Object that holds the arrows Poses ")]
    public GameObject buttonHolder;


    [Tooltip("Introduction / Tutorial screen")]
    public GameObject introScreen;
    [Tooltip("A reference to the game manager")]
    public GameManager gameManager;

    [Header("Results Screen Objects")]

    [Tooltip("Canvas containing results screen")]
    public GameObject resultsScreen;
    [Tooltip("Text boxes in results screen")]
    public Text percentHitText, normalsText, goodsText, perfectsText, missesText, rankText, finalScoreText;

    //A list of directions recognized by the game
    public enum Direction { Up, Down, Left, Right };

    //The instance of the Rhythm Game Manager
    public static RhythmGameManager instance;

    //Input action from the attached player input component
    //This is grabbed from the manager in the start function
    private InputAction startInput;
    private InputAction pauseInput;

    // GABE ADDED
    private GameObject gameState;
    
    // Start is called before the first frame update
    void Start()
    {
        //Set the instance of the Rhythm Game Manager to itself
        //This, along with its static typing, ensures that only one Rhythm Game Manager exists
        instance = this;

        //Align the notes with the center of the buttons. This helps the notes stay on beat.
        buttonCenter = buttonHolder.transform.position.y;
        beatScrollerObject.transform.position = new Vector3(0, buttonCenter, 0);

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

        //Get the input action that will start and pause the game
        startInput = GetComponent<PlayerInput>().actions.FindAction("Start");
        pauseInput = GetComponent<PlayerInput>().actions.FindAction("Pause");
        pauseInput.performed += Pause;
        isPaused = false;

        //Set text in intro screen
        if (Gamepad.current == null && Joystick.current == null)
        {
            introScreen.transform.GetChild(0).GetComponent<Text>().text =
                "Use [WASD] or [Arrow Keys] to hit notes\n\n" +
                "Hit the notes closer to the center of the button to get more points\n\n" +
                "Press [Space] to play";
        }
        else
        {
            introScreen.transform.GetChild(0).GetComponent<Text>().text =
                "Use [Left Stick] or [D-Pad] to hit notes\n\n" +
                "Hit the notes closer to the center of the button to get more points\n\n" +
                "Press [A] to play";
        }

        //Enable the intro screen
        introScreen.SetActive(true);
        //disable the notes until the start button is pressed and also chibi holder
        beatScrollerObject.SetActive(false);
        chibiHolder.SetActive(false);
        //GABE ADDED
        gameState = GameObject.Find("GameState");
    }

    // Update is called once per frame
    void Update()
    {
        //If the game has not started begin the game once any key has been pressed
        if (!startPlaying)
        {
            if (startInput.IsPressed())
            {
                chibiHolder.SetActive(true);
                beatScrollerObject.SetActive(true);
                startPlaying = true;
                //Remove the intro screen
                introScreen.SetActive(false);
                //Begin moving the notes
                beatScroller.hasStarted = true;
                //Play the music
                music.Play();
            }
        }
        //If the game has already started, the music has stopped playing (but isn't paused), AND the results screen isn't active yet...
        //Update the text in the results screen and display the results screen
        else
        {
            if (!isPaused && !music.isPlaying && !resultsScreen.activeInHierarchy)
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
                chibiHolder.SetActive(false);
                buttonHolder.SetActive(false);

                // GABE ADDED
                if (currentScore > 20000)
                {
                    gameState.GetComponent<GameState>().storyState = GameState.state.DDRComplete;
                    gameState.GetComponent<GameState>().objective = "Get information from David";
                }
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

    //Tell the game manager to pause the game
    public void Pause(InputAction.CallbackContext value)
    {
        PauseMusic();
        gameManager.PauseGame();
    }

    //Pause and unpause the music player
    public void PauseMusic()
    {
        if (isPaused)
        {
            isPaused = false;
            music.Play();
        }
        else
        {
            isPaused = true;
            music.Pause();
        }
    }
}
