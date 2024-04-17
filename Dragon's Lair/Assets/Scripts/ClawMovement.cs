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
            if (rHook.transform.eulerAngles.z < 61) // > 5
            {
                rHook.transform.Rotate(new Vector3(0, 0, 1f) * Time.deltaTime * 15);
            }

            if (lHook.transform.eulerAngles.z > 299) // < 355
            {
                Debug.Log(lHook.transform.eulerAngles.z > -61);
                lHook.transform.Rotate(new Vector3(0, 0, -1f) * Time.deltaTime * 15);
            }
        }

        if (!clawsOpen)
        {
            Debug.Log("lHook" + lHook.transform.eulerAngles);
            Debug.Log("rHook" + rHook.transform.eulerAngles);
            if (rHook.transform.eulerAngles.z > 5) // < 299
            {
                rHook.transform.Rotate(new Vector3(0, 0, -1f) * Time.deltaTime* 15); // -1f
            }

            if (lHook.transform.eulerAngles.z < 355) // > 61
            {
                lHook.transform.Rotate(new Vector3(0, 0, 1f) * Time.deltaTime * 15); // 1f
            }
        }
    }
}
