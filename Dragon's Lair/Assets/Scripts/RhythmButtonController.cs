/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 12, 2024 at 10:35 PM
 * 
 * TUTORIAL FOLLOWED: How To Make a Rhythm Game #1 - Hitting Notes https://www.youtube.com/watch?v=cZzf1FQQFA0
 * 
 * Makes the buttons at the bottom of the rhythm game react to player input by swapping the sprites
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmButtonController : MonoBehaviour
{
    [Tooltip("Sprite used for the default look of the button.")]
    public Sprite defaultImage;
    [Tooltip("Sprite used when the player has pressed the key associated with the button.")]
    public Sprite pressedImage;
    [Tooltip("Key that the button responds to.")]
    public KeyCode keyToPress;
    
    //Sprite renderer that is attached to the button object
    //This is grabbed from the button in the start function
    private SpriteRenderer sr;

    // Start is called before the first frame update
    void Start()
    {
        //Get the sprite renderer from the button object this script is attached to
        sr = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //Change the sprite of the button when the player presses the specified button
        if (Input.GetKeyDown(keyToPress))
        {
            sr.sprite = pressedImage;
        }

        //Reset the sprite of the button when the player releases the specified button
        if (Input.GetKeyUp(keyToPress))
        {
            sr.sprite = defaultImage;
        }
    }
}
