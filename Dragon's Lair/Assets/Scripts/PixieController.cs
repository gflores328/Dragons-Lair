/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Mar 14, 2024 at 9:24 PM
 * 
 * Controls the Pixie's movment, speed, and ability to attack the player
 * Player detection can be found in the 'PixieRange' script
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PixieController : Enemy
{
    [Tooltip("How fast the pixie moves towards the player")]
    public float speed;
    [Tooltip("A reference to the player. Used as a target for the pixie to move towards.")]
    public GameObject playerToChase;

    // Start is called before the first frame update
    void Start()
    {
        //Set the player reference to null so the pixie has nothing to track
        playerToChase = null;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        //If there is a player to track, begin moving towards the player
        if (playerToChase != null)
        {
            //Stay a certain distance away from the player and begin firing projectiles
            if (Vector3.Distance(transform.position, playerToChase.transform.position) > 3f)
            {
                transform.position = Vector3.MoveTowards(transform.position, playerToChase.transform.position, speed * Time.deltaTime);
            }
        }
    }

    //When the pixie reaches the player, deal damage and stop moving
    //This prevents the pixie from pushing the player around which would be really annoying
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            playerToChase.GetComponent<ChibiPlayerMovement>().takeDamage(1);
        }
    }
}
