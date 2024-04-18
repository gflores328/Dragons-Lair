/*
 * Created by: Carlos Martinez
 *
 * This script contains the movement of the claw in the crane game (WIP).
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawMovement : MonoBehaviour
{
    public bool clawsOpen;
    bool goUp, goDown, goLeft, goRight;
    Rigidbody2D lHook, rHook;

    // Define boundary coordinates
    float minX, maxX, minY, maxY;

    void Start()
    {
        rHook = GameObject.Find("Right_Hook").GetComponent<Rigidbody2D>();
        lHook = GameObject.Find("Left_Hook").GetComponent<Rigidbody2D>();
        clawsOpen = true;

        // Get boundary coordinates
        minX = -2f; // Example values, adjust according to your box size
        maxX = 2.5f;
        minY = -2f;
        maxY = 2f;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            goDown = true;
            goUp = false;
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            goDown = false;
            // Make the claws close
            // Wait before the claw goes up
            goUp = true;
        }

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            goLeft = true;
        }

        if (Input.GetKeyUp(KeyCode.LeftArrow))
        {
            goLeft = false;
        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            goRight = true;
        }

        if (Input.GetKeyUp(KeyCode.RightArrow))
        {
            goRight = false;
        }

        if (goUp)
        {
            gameObject.transform.Translate(0, 0.01f, 0);
        }

        if (goDown)
        {
            gameObject.transform.Translate(0, -0.01f, 0);
        }

        if (goLeft)
        {
            gameObject.transform.Translate(-0.015f, 0, 0);
        }

        if (goRight)
        {
            gameObject.transform.Translate(0.015f, 0, 0);
        }
        
        if (clawsOpen)
        {
            Debug.Log("lHook" + lHook.transform.eulerAngles);
            Debug.Log("rHook" + rHook.transform.eulerAngles);
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

        if (!clawsOpen)
        {
            Debug.Log("lHook" + lHook.transform.eulerAngles);
            Debug.Log("rHook" + rHook.transform.eulerAngles);
            if (rHook.transform.eulerAngles.z > 5) // > 5
            {
                rHook.transform.Rotate(new Vector3(0, 0, -1f) * Time.deltaTime* 15); // -1f
            }

            if (lHook.transform.eulerAngles.z < 355) // < 355
            {
                lHook.transform.Rotate(new Vector3(0, 0, 1f) * Time.deltaTime * 15); // 1f
            }
        }

        // Clamp position within boundaries
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("Trigger touching");
    }
}
