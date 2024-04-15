/*
 * Created By: Gabriel Flores
 * 
 * This sscript will be attached to the laser object and will check if the player is colliding with it so the player takes damage
 * 
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserDetect : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player in");

            other.GetComponent<ChibiPlayerMovement>().takeDamage(2);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("Player in");

            other.GetComponent<ChibiPlayerMovement>().takeDamage(2);
        }
    }
}
