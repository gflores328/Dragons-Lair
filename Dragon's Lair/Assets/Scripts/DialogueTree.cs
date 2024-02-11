/*
 * Created By: Gabriel Flores
 * 
 * This script will take multiple Dialogue objects and
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTree : MonoBehaviour
{
    [Header("Dialogue")]
    [Tooltip("The dialogue option that is run through at the start of interaction")]
    public Dialogue intro;
    [Tooltip("A list of dialogue options that represent dialogue branch points. Can only have a max 4 elements")]
    public List<Dialogue> dialogueBranches;
    [Tooltip("The dialogue manager")]
    public GameObject dialogueManager;

    [Header("UI")]
    [Tooltip("This will be the game object that holds the UI buttons for choosing dialogue")]
    public GameObject buttons;

    private Dialogue dialogueToDisplay; // This dialogue object will hold the dialogue that is to be displayed
    private bool introStarted; // This bool is to see if the intro text has started or not 
    private bool inDialogue; // A bool to see if the player is currently in dialogue
    private int dialogueLine; // This int holds the index number of the dialogue objects line that is to be displayed

    private void Start()
    {
        dialogueToDisplay = intro;
        introStarted = false;
        inDialogue = false;
        buttons.SetActive(false);
    }

    public void QuestionClicked(int questionNumber)
    {
        dialogueToDisplay = dialogueBranches[questionNumber - 1];
        dialogueManager.GetComponent<DialogueManager>().StartDialogue(dialogueToDisplay.dialogue[0]);
        buttons.SetActive(false);
    }

    public void CloseButton()
    {
        dialogueManager.GetComponent<DialogueManager>().EndDialogue();
        inDialogue = false;
        introStarted = false;
        buttons.SetActive(false);
        dialogueToDisplay = intro;
    }

    public void Interact()
    {
        if (!introStarted)
        {
            dialogueManager.GetComponent<DialogueManager>().TextChange(dialogueToDisplay.dialogue[dialogueLine]);
            inDialogue = true;
            introStarted = true;
        }

        else if (introStarted)
        {
            if (dialogueLine + 1 < dialogueToDisplay.dialogue.Length)
            {
                dialogueLine++;
                dialogueManager.GetComponent<DialogueManager>().TextChange(dialogueToDisplay.dialogue[dialogueLine]);
            }
            else
            {
                dialogueManager.GetComponent<DialogueManager>().EndDialogue();
                buttons.SetActive(true);
                dialogueLine = 0;

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When Player enters trigger the StartDialogue function is run to change the text
        if (other.tag == "Player")
        {
            dialogueManager.GetComponent<DialogueManager>().StartDialogue("Press E to interact");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When player exits the trigger EndDialogue is called, current line is set to, and inDialogue is set false;
        if (other.tag == "Player")
        {
            dialogueManager.GetComponent<DialogueManager>().EndDialogue();
            dialogueLine = 0;
            inDialogue = false;
            introStarted = false;
            dialogueToDisplay = intro;
        }
    }
}
