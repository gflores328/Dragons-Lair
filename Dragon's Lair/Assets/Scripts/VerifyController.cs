using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class VerifyController : MonoBehaviour
{
    //this script verifies that the controller is connected and prints out details of it to the user

    private bool connected = false;

    public GameManager gm;

    public void OnJoin(InputAction.CallbackContext ctx)
    {
        if (ctx.control.device is Keyboard)
        {
            // Do for keyboard...
            gm.controlsPanel.SetActive(true);
            gm.controlsKeyboard.SetActive(true);
            gm.controlsGamepad.SetActive(false);

        }
        else if (ctx.control.device is Gamepad)
        {
            // Do for gamepad...
            gm.controlsPanel.SetActive(true);
            gm.controlsKeyboard.SetActive(false);
            gm.controlsGamepad.SetActive(true);
        }
    }

}