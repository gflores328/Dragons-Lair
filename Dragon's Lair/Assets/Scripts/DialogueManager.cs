/*  
 * Created By: Gabriel Flores
 * 
 * This will be a script that manages dialogue by holding all of the functions that they will use
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    [Header("UI")]
    [Tooltip("This will be the textbox that will show when dialogue is active")]
    public GameObject textBox; // The UI textbox that is shown when dialogue is active
    [Tooltip("The text UI that will be shown")]
    public TextMeshProUGUI dialogueText;
    [Tooltip("The UI for the name textbox")]
    public GameObject nameBox;
    [Tooltip("The text UI to show the name")]
    public TextMeshProUGUI nameText;
    

    // This function will take a string variable and start the dialogue by showing the UI
    public void StartDialogue(DialogueAndName dialogueAndName)
    {
        // textBox is set active and dialogueText is set to the string value that was passed
        textBox.SetActive(true);
        dialogueText.text = dialogueAndName.dialogue;
        nameBox.SetActive(true);
        nameText.text = dialogueAndName.name;
    }

    public void StartDialogue(string dialogue)
    {
        textBox.SetActive(true);
        dialogueText.text = dialogue;
        //Cursor.lockState = CursorLockMode.None;
    }

    // This function will be used to switch the UI off
    public void EndDialogue()
    {
        textBox.SetActive(false);
        dialogueText.text = null;
        nameBox.SetActive(false);
        nameText.text = null;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    // This function will be used to change the text using a string that was passed through
    public void TextChange(DialogueAndName dialogueAndName)
    {
        if (!nameBox.activeInHierarchy)
        {
            nameBox.SetActive(true);
        }
        if (!textBox.activeInHierarchy)
        {
            textBox.SetActive(true);
        }

        dialogueText.text = dialogueAndName.dialogue;
        nameText.text = dialogueAndName.name;
    }

    /*
    public void QuestionClicked(DialogueWithName dialogue)
    {
        Interact.dialogueToDisplay = dialogue;
        StartDialogue(dialogue.dialogueArray[0]);
    }
    */
}
