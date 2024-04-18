/*
 * Created by: Carlos Martinez
 *
 * This script contains the movement of the claw in the crane game.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMovement : MonoBehaviour
{
    public bool clawsOpen;
    bool goUp, goDown, goLeft, goRight;
    Rigidbody2D lHook, rHook;

    // Define a delay time before going up
    public float delayBeforeGoingUp = 1f;
    
    // Define a delay time before the claw opens up
    public float delayBeforeClawOpens = 1f;

    // Define boundary coordinates
    float minX, maxX, minY, maxY;

    void Start()
    {
        rHook = GameObject.Find("Right_Hook").GetComponent<Rigidbody2D>();
        lHook = GameObject.Find("Left_Hook").GetComponent<Rigidbody2D>();
        clawsOpen = true;

        // Get boundary coordinates
        minX = -2f;
        maxX = 2.5f;
        minY = -2f;
        maxY = 2f;
    }

    void Update()
    {
        // Holding 'Space' = Descend
        if (Input.GetKeyDown(KeyCode.Space))
        {
            goDown = true;
            goUp = false;
        }

        // Release 'Space' = Ascend
        if (Input.GetKeyUp(KeyCode.Space))
        {
            goDown = false;
            clawsOpen = !clawsOpen; // Make the claws close
            StartCoroutine(WaitBeforeGoingUp()); // Start a coroutine to wait before the claw goes up
            StartCoroutine(WaitBeforeClawOpens()); // Start a coroutine to wait before the claw opens
        }

        // [<-] and [A] = Move Left
        if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A)) // Hold
        {
            goLeft = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A)) // Release
        {
            goLeft = false;
        }

        // [->] and [D] = Move Right
        if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D)) // Hold
        {
            goRight = true;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D)) // Release
        {
            goRight = false;
        }

        // Claw Movement
        if (goUp) // Up
        {
            gameObject.transform.Translate(0, 0.01f, 0);
        }

        if (goDown) // Down
        {
            gameObject.transform.Translate(0, -0.01f, 0);
        }

        if (goLeft) // Left
        {
            gameObject.transform.Translate(-0.015f, 0, 0);
        }

        if (goRight) // Right
        {
            gameObject.transform.Translate(0.015f, 0, 0);
        }
        
        // When the claws open, hooks rotate outward
        if (clawsOpen)
        {
            if (rHook.transform.eulerAngles.z < 61) // < 61
            {
                rHook.transform.Rotate(new Vector3(0, 0, 1f) * Time.deltaTime * 15);
            }

            if (lHook.transform.eulerAngles.z > 299) // > 299
            {
                Debug.Log(lHook.transform.eulerAngles.z > -61);
                lHook.transform.Rotate(new Vector3(0, 0, -1f) * Time.deltaTime * 15);
            }
        }

        // When the claws close, hooks rotate inward
        if (!clawsOpen)
        {
            if (rHook.transform.eulerAngles.z > 5) // > 5
            {
                rHook.transform.Rotate(new Vector3(0, 0, -1f) * Time.deltaTime* 15);
            }

            if (lHook.transform.eulerAngles.z < 355) // < 355
            {
                lHook.transform.Rotate(new Vector3(0, 0, 1f) * Time.deltaTime * 15);
            }
        }

        // Clamp position within boundaries
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );
        
        // Delay before the claw moves up
        IEnumerator WaitBeforeGoingUp()
        {
            yield return new WaitForSeconds(delayBeforeGoingUp);
            goUp = true;
        }

        // Delay before the claw opens
        IEnumerator WaitBeforeClawOpens()
        {
            yield return new WaitForSeconds(delayBeforeClawOpens);
            // Make the claws Open
            clawsOpen = !clawsOpen;
        }
    }

}
