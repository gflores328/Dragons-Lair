/*
 * Created By: Gabriel Flores
 * 
 * This script will act the same as an interact but wont have a trigger and will instead be called from somewhere else
 * It will have a function that takes a DialogueWithName asset and outputs the dialogue for it.
 * 
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectiveThoughts : MonoBehaviour
{
    public DialogueManager dialogueManager; // A reference to the dialogue manager
    private DialogueWithName currentDialogue;
    [HideInInspector]
    public bool lostInThought = false; // A bool to check if the player is in a thought dialogue or not
    private int currentLine = 0; // Represents the current index of the dialogue array
    public List<DialogueWithName> thoughtList; // A list to hold all of the detectives thought dialogues
    private GameObject gameState; // A reference to the game state

    private void Start()
    {
        
        gameState = GameObject.Find("GameState");
        if (gameState.GetComponent<GameState>().storyState == GameState.state.newGame)
        {
            StartCoroutine(ThoughtDelay(thoughtList[0]));
        }

        if (gameState.GetComponent<GameState>().storyState == GameState.state.Level1Complete)
        {
            StartCoroutine(ThoughtDelay(thoughtList[1]));
        }
    }

    // A function that starts the dialogue when called
    public void StartThinking(DialogueWithName dialogue)
    {
        currentDialogue = dialogue;
        dialogueManager.GetComponent<DialogueManager>().StartDialogue(currentDialogue.dialogueArray[currentLine]);
        lostInThought = true;
        currentLine++;
        Time.timeScale = 0;
        
    }

    public void Interact()
    {
        if (currentLine < currentDialogue.dialogueArray.Length)
        {
            // Time scale is set to 0 when interact is run
            Time.timeScale = 0;

            dialogueManager.GetComponent<DialogueManager>().TextChange(currentDialogue.dialogueArray[currentLine]);
            currentLine++;
        }
        // If currentLine is not less than then EndDialogue is run and timeScale is set back to 1;
        else
        {
            dialogueManager.GetComponent<DialogueManager>().EndDialogue();
            currentLine = 0;
            Time.timeScale = 1;
            lostInThought = false;

        }
    }

    IEnumerator ThoughtDelay(DialogueWithName dialogue)
    {
        yield return new WaitForSeconds(0.1f);
        StartThinking(dialogue);
    }
}
