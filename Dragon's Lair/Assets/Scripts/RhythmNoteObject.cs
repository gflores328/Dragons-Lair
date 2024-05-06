/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Mar 8, 2024 at 10:13 AM
 * 
 * TUTORIAL FOLLOWED: How To Make a Rhythm Game #1 - Hitting Notes https://www.youtube.com/watch?v=cZzf1FQQFA0
 *                    How To Make a Rhythm Game #2 - Playing Music & Missing Notes https://www.youtube.com/watch?v=PMfhS-kEvc0
 *                    How To Make a Rhythm Game #4 - Timing Hits For Better Score https://www.youtube.com/watch?v=Oi0tT7QnFhs
 * 
 * Keeps track of if the note can be hit by the player.
 * Reports to the game manager if the note has been hit or missed.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  //Used to get player input

public class RhythmNoteObject : MonoBehaviour
{
    [Tooltip("Determines if the note is able to be hit.")]
    public bool canBePressed;
    [Tooltip("Determines if the note has already been hit by the player.")]
    public bool obtained = false;
    [Tooltip("The direction the note is facing. This is overwritten by a randomizer when the game starts and is only left here for compatibility purposes.")]
    public RhythmGameManager.Direction direction;

    [Space]

    [Tooltip("The sprite for each note direction")]
    public Sprite arrowUp;
    public Sprite arrowDown;
    public Sprite arrowLeft;
    public Sprite arrowRight;

    [Space]

    [Tooltip("Effects to be played for various note hit/miss conditions")]
    public GameObject hitEffect;
    public GameObject goodEffect;
    public GameObject perfectEffect;
    public GameObject missEffect;

    //Input action from the attached player input component
    //This is grabbed from the note in the start function
    private InputAction playerInput;
    //Sprite renderer from the attached object
    //This is grabbed from the note in the start function
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        /*
         * Do some magic to get the number of directions for the random number generator
         * While hardcoding the number of directions would be easier (it's not like there will ever
         * be more than 4 directions for this game), I'd rather make it scaleable just because I can
         */
        int numberOfDirections = System.Enum.GetValues(typeof(RhythmGameManager.Direction)).Length;
        //Get a random direction (numberOfDirections is exclusive)
        direction = (RhythmGameManager.Direction)Random.Range(0, numberOfDirections);

        //Get the sprite render from the object
        spriteRenderer = GetComponent<SpriteRenderer>();

        //Set the player input to the specified direction
        //Set the sprite to match the direction
        //Set the position to match the direction
        switch (direction)
        {
            case RhythmGameManager.Direction.Up:
                playerInput = GetComponent<PlayerInput>().actions.FindAction("Up Arrow");
                spriteRenderer.sprite = arrowUp;
                transform.position = new Vector3(-0.5f, transform.position.y, 0f);
                break;
            case RhythmGameManager.Direction.Down:
                playerInput = GetComponent<PlayerInput>().actions.FindAction("Down Arrow");
                spriteRenderer.sprite = arrowDown;
                transform.position = new Vector3(0.5f, transform.position.y, 0f);
                break;
            case RhythmGameManager.Direction.Left:
                playerInput = GetComponent<PlayerInput>().actions.FindAction("Left Arrow");
                spriteRenderer.sprite = arrowLeft;
                transform.position = new Vector3(-1.5f, transform.position.y, 0f);
                break;
            case RhythmGameManager.Direction.Right:
                playerInput = GetComponent<PlayerInput>().actions.FindAction("Right Arrow");
                spriteRenderer.sprite = arrowRight;
                transform.position = new Vector3(1.5f, transform.position.y, 0f);
                break;
            default: break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //If the specified key has been pressed and the note is able to be hit...
        if (playerInput.IsPressed())
        {
            if (canBePressed)
            {
                //Check the position of the note to determine the quality of the hit
                //Tell the game manager that the note has been hit
                //Instantiate the appropriate particle effect
                if (Mathf.Abs(transform.position.y - RhythmGameManager.instance.buttonCenter) > 0.25f)
                {
                    //Debug.Log("Hit");
                    RhythmGameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y - RhythmGameManager.instance.buttonCenter) > 0.05f)
                {
                    //Debug.Log("Good Hit");
                    RhythmGameManager.instance.GoodHit();
                    Instantiate(goodEffect, transform.position, goodEffect.transform.rotation);
                }
                else
                {
                    //Debug.Log("Perfect Hit");
                    RhythmGameManager.instance.PerfectHit();
                    Instantiate(perfectEffect, transform.position, perfectEffect.transform.rotation);
                }
                
                //Mark the note has having been hit
                obtained = true;
                //Deactivate the note
                gameObject.SetActive(false);
            }
        }

        //Deactivate the note if it has passed below the buttons
        if (transform.position.y < -2f)
        {
            gameObject.SetActive(false);
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //If the note is overlapping a button mark the note as being able to be hit
        if (other.tag == "Activator")
        {
            canBePressed = true;
        }
    }

    public void OnTriggerExit2D(Collider2D other)
    {
        //If the note passes a button...
        if (other.tag == "Activator")
        {
            //The note can no longer be hit
            canBePressed = false;

            //If the note passes a button and has not been hit by the player tell the game manager the note was missed
            //For some reason trigger exits works differently in this build of Unity so this extra workaround is necessary
            if (!obtained)
            {
                //Tell the game manager the note has been missed
                RhythmGameManager.instance.NoteMissed();
                //Instantiate the appropriate particle effect
                Instantiate(missEffect, transform.position, missEffect.transform.rotation);
            }
        }
    }
}
