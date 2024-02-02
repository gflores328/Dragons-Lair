/*
Created By: Gabriel Flores

This script takes a number of lines of a text box and will advance through each line of text until all are read

WHAT IT STILL NEEDS:
Needs a trigger so the textbox will only popup when player is in an appropriate location
Needs some sort of pause so that when diolouge is beig read the world stops
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Dialouge : MonoBehaviour
{
    [TextArea]
    public string[] textLines; // An array that holds the lines of text to be displayed

    public TextMeshProUGUI textBox; // UI for the text
    public GameObject background; // The background for the text. Set asleep by default

    private bool inDialogue; // Bool for if in dialouge
    private int currentLine; // Integer to hold the element of the current diolouge being displayed

    // Start is called before the first frame update
    void Start()
    {
        inDialogue = false;
        currentLine = 0;
    }

    // Update is called once per frame
    void Update()
    {
        // When E is pressed the Interact function is run
        if (Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void Interact()
    {
        // If inDialogue is false then StartDiolougue is run and inDiolouge is set to true
        if (!inDialogue)
        {
            StartDiolouge();
        }
        // If inDiolouge is true then the next line of text is displayed if there is one
        // If there is no nect line then EndDiolouge is run
        else if (inDialogue)
        {
            if (currentLine < textLines.Length)
            {
                currentLine++;
                if (!(currentLine >= textLines.Length))
                {
                    textBox.text = textLines[currentLine];
                }
                else
                {
                    EndDiolouge();
                }
            }
            
        }
    }

    // The text background is set active and the first line of text is set to the text UI
    private void StartDiolouge()
    {
        background.SetActive(true);
        textBox.text = textLines[currentLine];
        inDialogue = true;
    }

    // The background is set asleep, text UI is null, and current line is reset to 0
    private void EndDiolouge()
    {
        background.SetActive(false);
        textBox.text = null;
        currentLine = 0;
        inDialogue = false;
    }

}
