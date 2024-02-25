/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 25, 2024 at 5:49 PM
 * 
 * TUTORIAL FOLLOWED: How To Make a Rhythm Game #1 - Hitting Notes https://www.youtube.com/watch?v=cZzf1FQQFA0
 * 
 * Makes the buttons at the bottom of the rhythm game react to player input by swapping the sprites
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;  //Used to get player input

public class RhythmButtonController : MonoBehaviour
{
    [Tooltip("Sprite used for the default look of the button.")]
    public Sprite defaultImage;
    [Tooltip("Sprite used when the player has pressed the key associated with the button.")]
    public Sprite pressedImage;
    [Tooltip("Direction that the button responds to.")]
    public RhythmGameManager.Direction direction;

    //Sprite renderer that is attached to the button object
    //This is grabbed from the button in the start function
    private SpriteRenderer sr;
    //Input action from the attached player input component
    //This is grabbed from the button in the start function
    private InputAction playerInput;

    // Start is called before the first frame update
    void Start()
    {
        //Get the sprite renderer from the button object this script is attached to
        sr = GetComponent<SpriteRenderer>();

        //Set the player input action to the specified direction
        switch (direction)
        {
            case RhythmGameManager.Direction.Up:
                playerInput = GetComponent<PlayerInput>().actions.FindAction("Up Arrow");
                break;
            case RhythmGameManager.Direction.Down:
                playerInput = GetComponent<PlayerInput>().actions.FindAction("Down Arrow");
                break;
            case RhythmGameManager.Direction.Left:
                playerInput = GetComponent<PlayerInput>().actions.FindAction("Left Arrow");
                break;
            case RhythmGameManager.Direction.Right:
                playerInput = GetComponent<PlayerInput>().actions.FindAction("Right Arrow");
                break;
            default: break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Change the sprite of the button when the player presses the specified button
        if (playerInput.IsPressed())
        {
            sr.sprite = pressedImage;
        }
        //Reset the sprite of the button when the player releases the specified button
        else
        {
            sr.sprite = defaultImage;
        }
    }
}
