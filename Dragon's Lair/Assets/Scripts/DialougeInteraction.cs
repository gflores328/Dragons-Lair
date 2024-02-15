/*
Combination of Dialouge and Interactor scripts created by: Gabriel Flores and Carlos Martinez

This script will take a dialogue and dialogue manager object. 
The text in the dialogue script will be output into the dialogue manager each time the Interact function is run

WHAT IT STILL NEEDS:
Needs some sort of pause so that when diolouge is being read the player cant move
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class DialougeInteraction : MonoBehaviour
{
    [Tooltip("The dialogue asset that the script will read through")]
    public DialogueWithName dialogue; // This is the dialogue object that holds all the text

    [Tooltip("The object that holds the DialogueManger script")]
    public GameObject dialogueManger; // This is the object that holds the DialogueManager script
    
    
    private bool inDialouge; // bool to see if player is in dialouge
    private int currentLine; // the index of the dialouge array that is currently being shown

    
    // Start is called before the first frame update
    void Start()
    {
        inDialouge = false;
        currentLine = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        // When Player enters trigger the StartDialogue function is run to change the text
        if (other.tag == "Player")
        {
            dialogueManger.GetComponent<DialogueManager>().StartDialogue("Press E to interact");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When player exits the trigger EndDialogue is called, current line is set to, and inDialogue is set false;
        if (other.tag == "Player")
        {
            dialogueManger.GetComponent<DialogueManager>().EndDialogue();
            currentLine = 0;
            inDialouge = false;
        }
    }

    // This function will run differently depending on if inDialogue is true or false
    public void Interact()
    {
        // If inDialogue is false then StartDiolougue is run and inDiolouge is set to true
        if (!inDialouge)
        {
            dialogueManger.GetComponent<DialogueManager>().StartDialogue(dialogue.dialogueArray[currentLine]);
            inDialouge = true;
        }

        // if inDialogue is true
        else if (inDialouge)
        {
            // Checks to see if current line is less than the dialogue objects array legnth
            if (currentLine + 1 < dialogue.dialogueArray.Length)
            {
                // 1 is added to currentLine and TextChange is called with the string from the dialogue arrays current line element
                currentLine++;
                dialogueManger.GetComponent<DialogueManager>().TextChange(dialogue.dialogueArray[currentLine]);
            }
            // If current line + 1 isnt less than legnth then EndDialogue is called, current line is set to 0, and inDialogue is set to false
            else
            {
                dialogueManger.GetComponent<DialogueManager>().EndDialogue();
                currentLine = 0;
                inDialouge = false;
            }

        }
    }
}
