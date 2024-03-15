/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Mar 14, 2024 at 9:24 PM
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

    private Transform player;

    //If the player is within range notify the pixie that the player has been found
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            pixie.playerToChase = other.gameObject;
            player = other.transform;
        }
    }

    //Determine if the player is on the left or the right of the pixie
    //Offset the range so the bullet doesn't collide with the pixie when it is spawned
    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (player.position.x > pixie.transform.position.x)
            {
                transform.localPosition = new Vector3(1.5f, 0, 0);
            }
            else
            {
                transform.localPosition = new Vector3(-1.5f, 0, 0);
            }
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
