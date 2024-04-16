using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class VerifyController : MonoBehaviour
{
    //this script verifies that the controller is connected and prints out details of it to the user

    //private bool connected = false;

    public GameManager gm;


    public void OnJoinKeyboard()
    {
        // This works for other types of devices, too.


        if (gm.GetIsMouse())
        {
            // Do for keyboard...
           // gm.controlsPanel.SetActive(true);
            gm.controlsKeyboard.SetActive(true);
            gm.controlsGamepad.SetActive(false);
        }

    }

    public void OnJoinGamePad()
    {


        if (!gm.GetIsMouse())
        {
            // Do for gamepad...
            //gm.controlsPanel.SetActive(true);
            gm.controlsKeyboard.SetActive(false);
            gm.controlsGamepad.SetActive(true);
        }
    }
}