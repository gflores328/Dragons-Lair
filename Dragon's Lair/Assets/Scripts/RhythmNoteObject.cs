/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Mar 4, 2024 at 4:08 PM
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
    [Tooltip("The direction the note is facing.")]
    public RhythmGameManager.Direction direction;

    [Tooltip("Effects to be played for various note hit/miss conditions")]
    public GameObject hitEffect, goodEffect, perfectEffect, missEffect;

    //Input action from the attached player input component
    //This is grabbed from the note in the start function
    private InputAction playerInput;

    void Start()
    {
        //Set the player input to the specified direction
        //Set the rotation to match the direction
        switch (direction)
        {
            case RhythmGameManager.Direction.Up:
                playerInput = GetComponent<PlayerInput>().actions.FindAction("Up Arrow");
                transform.rotation = Quaternion.Euler(0, 0, 90);
                break;
            case RhythmGameManager.Direction.Down:
                playerInput = GetComponent<PlayerInput>().actions.FindAction("Down Arrow");
                transform.rotation = Quaternion.Euler(0, 0, 270);
                break;
            case RhythmGameManager.Direction.Left:
                playerInput = GetComponent<PlayerInput>().actions.FindAction("Left Arrow");
                transform.rotation = Quaternion.Euler(0, 0, 180);
                break;
            case RhythmGameManager.Direction.Right:
                playerInput = GetComponent<PlayerInput>().actions.FindAction("Right Arrow");
                transform.rotation = Quaternion.Euler(0, 0, 0);
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
                if (Mathf.Abs(transform.position.y) > 0.25f + RhythmGameManager.instance.buttonCenter)
                {
                    //Debug.Log("Hit");
                    RhythmGameManager.instance.NormalHit();
                    Instantiate(hitEffect, transform.position, hitEffect.transform.rotation);
                }
                else if (Mathf.Abs(transform.position.y) > 0.05f + RhythmGameManager.instance.buttonCenter)
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
