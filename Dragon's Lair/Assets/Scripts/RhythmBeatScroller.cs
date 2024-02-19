/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 16, 2024 at 2:10 PM
 * 
 * TUTORIAL FOLLOWED: How To Make a Rhythm Game #1 - Hitting Notes https://www.youtube.com/watch?v=cZzf1FQQFA0
 * 
 * Moves the notes in accordance with the given tempo
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmBeatScroller : MonoBehaviour
{
    [Tooltip("The tempo of the song in beats per minute.")]
    public float beatTempo;
    [Tooltip("Keeps track of if the game has started or not.")]
    public bool hasStarted;

    // Start is called before the first frame update
    void Start()
    {
        //Convert tempo to beats per second
        beatTempo /= 60f;
    }

    // Update is called once per frame
    void Update()
    {
        //If the game has started begin moving the notes down
        if (hasStarted)
        {
            transform.position -= new Vector3(0f, beatTempo * Time.deltaTime, 0f);
        }
    }
}
