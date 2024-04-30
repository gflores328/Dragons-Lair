/*
 * Created by Carlos Martinez
 * 
 * This script contains the code for the instruction panel for the claw
 * and space shooter game.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClawStart : MonoBehaviour
{
    public GameObject startMenu;
    public bool started = false;

    // Start is called before the first frame update
    void Start()
    {
        Time.timeScale = 0;
        startMenu.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!started)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                started = true;
                startMenu.SetActive(false);
                Time.timeScale = 1;

            }
        }
    }
}
