using System.Collections;
using System.Collections.Generic;
using UnityEngine;

 // This Script does when you press WASD key it make the footstep sound and when you stop it stoped the sound.
public class footsteps : MonoBehaviour
{
    public AudioSource footstepsSound;

    void Update()
    {
        if(Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.D)){ // When press WASD key it played the footstep sound effect.
            footstepsSound.enabled = true;
    }
    else // When stop pressing WASD key it stop the sound.
    {
        footstepsSound.enabled = false;
    }
    }

}
