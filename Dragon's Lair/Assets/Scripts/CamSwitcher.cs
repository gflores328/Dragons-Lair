using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamSwitcher : MonoBehaviour
{

    public CinemachineVirtualCamera activeCam; // The public variable that holds which active cam this script should be activating


    private void OnTriggerEnter(Collider other) 
    
    {
        if(other.CompareTag("Player")) // check if the collider is the player

        {
            activeCam.Priority = 1; // Set the cam to be active
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        if(other.CompareTag("Player")) // check if he collider is the player
        {
            activeCam.Priority = 0; // set the cam to no longer be active or have th epriority
        }
    }

}
