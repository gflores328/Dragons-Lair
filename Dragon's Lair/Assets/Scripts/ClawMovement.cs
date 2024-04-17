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

    void Start()
    {
        rHook = GameObject.Find("Right_Hook").GetComponent<Rigidbody2D>();
        lHook = GameObject.Find("Left_Hook").GetComponent<Rigidbody2D>();
        clawsOpen = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            goDown = true;
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            goDown = false;
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            goUp = true;
        }

        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            goUp = false;
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
            if (lHook.transform.eulerAngles.z > 5) // > 5
            {
                lHook.transform.Rotate(0, 0, 1f);
            }

            if (rHook.transform.eulerAngles.z < 355) // < 355
            {
                rHook.transform.Rotate(0, 0, -1f);
            }
        }

        if (!clawsOpen)
        {
            Debug.Log("lHook" + lHook.transform.eulerAngles);
            Debug.Log("rHook" + rHook.transform.eulerAngles);
            if (lHook.transform.eulerAngles.z < 299) // < 299
            {
                lHook.transform.Rotate(0, 0, -1f); // -1f
            }

            if (rHook.transform.eulerAngles.z > 61) // > 61
            {
                rHook.transform.Rotate(0, 0, 1f); // 1f
            }
        }
    }
}
