
/*
Created by: Gabriel Flors

This script will be for a trigger so that when the player walks into it thay can interact and bring up lines of dialouge. 
Once dialouge is done the game object that thescript is attached to will be destroyed and the Item attached to the script will be added to the Inventory

WHAT IT STILL NEEDS:
Needs some sort of pause so that when diolouge is being read the player cant move
*/

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInteraction : MonoBehaviour
{
    [Tooltip("The dialogue asset that the script will read through")]
    public Dialogue dialogue; // This is the dialogue object that holds all the text

    [Tooltip("The object that holds the DialogueManger script")]
    public GameObject dialogueManger; // This is the object that holds the DialogueManager script

    [Header("Inventory")]
    [Tooltip("The game object that holds the Inventory script")]
    public GameObject inventoryManager;
    [Tooltip("The item that will be picked up once the dialogue is done")]
    public Item item;

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
            dialogueManger.GetComponent<DialogueManager>().StartDialogue(dialogue.dialogue[0]);
            inDialouge = true;
        }

        // if inDialogue is true
        else if (inDialouge)
        {
            // Checks to see if current line is less than the dialogue objects array legnth
            if (currentLine + 1 < dialogue.dialogue.Length)
            {
                // 1 is added to currentLine and TextChange is called with the string from the dialogue arrays current line element
                currentLine++;
                dialogueManger.GetComponent<DialogueManager>().TextChange(dialogue.dialogue[currentLine]);
            }
            // If current line + 1 isnt less than legnth then EndDialogue is called, current line is set to 0, and inDialogue is set to false
            else
            {
                dialogueManger.GetComponent<DialogueManager>().EndDialogue();
                currentLine = 0;
                inDialouge = false;
                PickUp();
            }

        }
    }

    //This function will pick up the item 
    private void PickUp()
    {
        inventoryManager.GetComponent<Inventory>().AddItem(item);
        Destroy(gameObject);
    }
}
