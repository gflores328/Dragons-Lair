/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 12, 2024 at 10:59 PM
 * 
 * TUTORIAL FOLLOWED: How To Make a Rhythm Game #1 - Hitting Notes https://www.youtube.com/watch?v=cZzf1FQQFA0
 *                    How To Make a Rhythm Game #2 - Playing Music & Missing Notes https://www.youtube.com/watch?v=PMfhS-kEvc0
 * 
 * Keeps track of if the note can be hit by the player.
 * Reports to the game manager if the note has been hit or missed.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmNoteObject : MonoBehaviour
{
    [Tooltip("Determines if the note is able to be hit.")]
    public bool canBePressed;
    [Tooltip("Determines if the note has already been hit by the player.")]
    public bool obtained = false;
    [Tooltip("The key that needs to be pressed in order for the note to be hit.")]
    public KeyCode keyToPress;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //If the specified key has been pressed and the note is able to be hit...
        if (Input.GetKeyDown(keyToPress))
        {
            if (canBePressed)
            {
                //Tell the game manager that the note has been hit
                RhythmGameManager.instance.NoteHit();
                //Mark the note has having been hit
                obtained = true;
                //Deactivate the note
                gameObject.SetActive(false);
            }
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
            //For some reason trigger exits work differently in this build of Unity so this extra workaround is necessary
            if (!obtained)
            {
                RhythmGameManager.instance.NoteMissed();
            }
        }
    }
}
