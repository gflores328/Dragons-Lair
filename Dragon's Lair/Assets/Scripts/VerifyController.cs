using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VerifyController : MonoBehaviour
{
    //this script verifies that the controller is connected and prints out details of it to the user

    private bool connected = false;

    public GameManager gm;

    IEnumerator CheckForController()
    {
        while (true)
        {
            var controllers = Input.GetJoystickNames();

            if ((!connected) && (controllers.Length > 0))
            {
                connected = true; //controller is connected
                Debug.Log("Controlled connected successfully.");
                Debug.Log(controllers);
                gm.controlsPanel.activeSelf.Equals(true);
                gm.controlsKeyboard.activeSelf.Equals(true);
                gm.controlsGamepad.activeSelf.Equals(false);

            }
            else if ((connected) && (controllers.Length == 0))
            {
                connected = false; //controller is not connected
                Debug.Log("Controller disconnected.");
                Debug.Log(controllers);

                gm.controlsPanel.activeSelf.Equals(true);
                gm.controlsKeyboard.activeSelf.Equals(false);
                gm.controlsGamepad.activeSelf.Equals(true);

            }

            yield return new WaitForSeconds(1f);
        }
    }

    void Awake()
    {
        StartCoroutine(CheckForController());
    }
}