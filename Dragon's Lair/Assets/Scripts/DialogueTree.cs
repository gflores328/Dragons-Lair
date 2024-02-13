/*
 * Created By: Gabriel Flores
 * 
 * This script will take multiple Dialog objects. One will be used as the intro when interacted with.
 * After that a UI will pop up. The UI will hold buttons with a on click event on them that will then output the corrosponding dialogue
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
    public GameObject firstButton; // Button for controller ref

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

    // This function will be put on a buttons on click event and will take an int value that represents the question number
    public void QuestionClicked(int questionNumber)
    {
        // dialogueToDisplay is set to index number questionNumber - 1 of dialogueBranches since the index for the question would be 1 less
        dialogueToDisplay = dialogueBranches[questionNumber - 1];

        // StartDialogue is started with and the buttons are set inactive
        dialogueManager.GetComponent<DialogueManager>().StartDialogue(dialogueToDisplay.dialogue[dialogueLine]);
        buttons.SetActive(false);
    }

    // This function will be put on a buttons on click event and will close everything dialogue related
    public void CloseButton()
    {
        dialogueManager.GetComponent<DialogueManager>().EndDialogue();
        inDialogue = false;
        introStarted = false;
        buttons.SetActive(false);
        dialogueToDisplay = intro;
        Time.timeScale = 1;
    }

    // This function will run differently depending on what bools are set
    public void Interact()
    {
        // If Interact is run and introStarted is false
        Time.timeScale = 0;
        if (!introStarted)
        {
            // The intro  Dialogue is started and bools are set to true
            dialogueManager.GetComponent<DialogueManager>().TextChange(dialogueToDisplay.dialogue[dialogueLine]);
            inDialogue = true;
            introStarted = true;
        }
        // If introStarted is true
        else if (introStarted)
        {
            // If the dialogueLine integer + 1 is less than the dialogueToDiaplay legnth
            if (dialogueLine + 1 < dialogueToDisplay.dialogue.Length)
            {
                // 1 is added to dialogueLine and TextChange is called for the current dialogueToDisplay with the new dialogueLine as the index
                dialogueLine++;
                dialogueManager.GetComponent<DialogueManager>().TextChange(dialogueToDisplay.dialogue[dialogueLine]);
            }
            // If dialogueLine is not less than the dialogueToDisplaye legnth
            else
            {
                // End dialouge is called, the UI buttons pop up, and dialogeLine is set to 0
                dialogueManager.GetComponent<DialogueManager>().EndDialogue();
                buttons.SetActive(true);
                dialogueLine = 0;
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstButton);   

            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When Player enters trigger the StartDialogue function is run to change the text
        if (other.tag == "Player")
        {
            dialogueManager.GetComponent<DialogueManager>().StartDialogue("Press E or X Button to interact");
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
