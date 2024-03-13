/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Mar 13, 2024 at 3:30 PM
 * 
 * Determines if the player is close enough to the pixie to begin chasing the player
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixieRange : MonoBehaviour
{
    [Tooltip("The pixie that the range is attached to.")]
    public PixieController pixie;

    //If the player is within range notify the pixie that the player has been found
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pixie.playerToChase = other.gameObject;
        }
    }

    //If the player leaves the range tell the pixie to stop chasing the player
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pixie.playerToChase = null;
        }
    }
}
