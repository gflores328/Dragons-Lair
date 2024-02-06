/*
Combination of Dialouge and Interactor scripts created by: Gabriel Flores and Carlos Martinez

This script will be for a trigger so that when the player walks into it thay can interact and bring up
lines of dialouge

WHAT IT STILL NEEDS:
Needs some sort of pause so that when diolouge is being read the world stops
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialougeInteraction : MonoBehaviour
{
    [Header("Dialouge")]
    [Tooltip("The number of lines in a dialouge interaction and what they say")]
    [TextArea]
    public string[] dialougeLines; // The lines of dialouge will be shown

    [Tooltip("The UI objects that the dialouge will use")]
    [Header("UI")]
    public GameObject textBox; // The texbox UI
    public TextMeshProUGUI dialougeText; // The text UI

    private bool inDialouge; // bool to see if player is in dialouge
    private int currentLine; // the index of the dialouge array that is being shown
    private bool inTrigger;

    // Start is called before the first frame update
    void Start()
    {
        inDialouge = false;
        currentLine = 0;
        inTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When Player enters trigger the UI prompt will be shown
        if (other.tag == "Player")
        {
            textBox.SetActive(true);
            dialougeText.text = "Press E to interact";
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When player exits the trigger then the UI will dissapear
        if (other.tag == "Player")
        {
            currentLine = 0;
            textBox.SetActive(false);
            dialougeText.text = null;
            inDialouge = false;
            inTrigger = false;
        }
    }

   /* private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            Debug.Log("player");
        }
        else
        {
            Debug.Log("NO player");
        }


        // When player is in trigger and E is pressed the Interact function is run
        if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }
   */

    // The text background is set active and the first line of text is set to the text UI
    private void StartDiolouge()
    {
        textBox.SetActive(true);
        dialougeText.text = dialougeLines[currentLine];
        inDialouge = true;
    }

    // The background is set asleep, text UI is null, and current line is reset to 0
    private void EndDiolouge()
    {
        textBox.SetActive(false);
        dialougeText.text = null;
        currentLine = 0;
        inDialouge = false;
    }

    private void Interact()
    {
        // If inDialogue is false then StartDiolougue is run and inDiolouge is set to true
        if (!inDialouge)
        {
            StartDiolouge();
        }
        // If inDiolouge is true then the next line of text is displayed if there is one
        // If there is no nect line then EndDiolouge is run
        else if (inDialouge)
        {
            if (currentLine < dialougeLines.Length)
            {
                currentLine++;
                if (!(currentLine >= dialougeLines.Length))
                {
                    dialougeText.text = dialougeLines[currentLine];
                }
                else
                {
                    EndDiolouge();
                }
            }

        }
    }
}
