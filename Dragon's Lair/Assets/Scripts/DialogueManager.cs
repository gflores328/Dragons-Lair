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

    

    // This function will take a string variable and start the dialogue by showing the UI
    public void StartDialogue(string text)
    {
        // textBox is set active and dialogueText is set to the string value that was passed
        textBox.SetActive(true);
        dialogueText.text = text;
    }

    // This function will be used to switch the UI off
    public void EndDialogue()
    {
        textBox.SetActive(false);
        dialogueText.text = null;
    }

    // This function will be used to change the text using a string that was passed through
    public void TextChange(string text)
    {
        dialogueText.text = text;
    }
}
