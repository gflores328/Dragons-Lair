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
using UnityEngine.InputSystem;

public class DetectiveThoughts : MonoBehaviour
{
    public DialogueManager dialogueManager; // A reference to the dialogue manager
    private DialogueWithName currentDialogue;
    [HideInInspector]
    public bool lostInThought = false; // A bool to check if the player is in a thought dialogue or not
    private int currentLine = 0; // Represents the current index of the dialogue array
    public List<DialogueWithName> thoughtList; // A list to hold all of the detectives thought dialogues
    private GameObject gameState; // A reference to the game state

    private PlayerInput playerInput;
    private void Start()
    {

        playerInput = GameObject.Find("Player").GetComponent<PlayerInput>();
        gameState = GameObject.Find("GameState");
        if (gameState != null)
        {
            if (gameState.GetComponent<GameState>().storyState == GameState.state.newGame)
            {

                StartCoroutine(ThoughtDelay(thoughtList[0]));
            }

            if (gameState.GetComponent<GameState>().storyState == GameState.state.DDRComplete)
            {

                StartCoroutine(ThoughtDelay(thoughtList[1]));
            }

            if (gameState.GetComponent<GameState>().storyState == GameState.state.Level1Complete)
            {

                StartCoroutine(ThoughtDelay(thoughtList[2]));
            }

            if (gameState.GetComponent<GameState>().storyState == GameState.state.SpaceGameDone)
            {

                StartCoroutine(ThoughtDelay(thoughtList[3]));
            }
            if (gameState.GetComponent<GameState>().storyState == GameState.state.Level2Complete)
            {

                StartCoroutine(ThoughtDelay(thoughtList[4]));
            }
            if (gameState.GetComponent<GameState>().storyState == GameState.state.GiveScrewdriver)
            {
                StartCoroutine(ThoughtDelay(thoughtList[5]));
            }

        }
    }

    // A function that starts the dialogue when called
    public void StartThinking(DialogueWithName dialogue)
    {
        AudioClip audioClip = dialogueManager.GetComponent<DialogueManager>().GetAudioSource().clip;
        dialogueManager.GetComponent<DialogueManager>().GetAudioSource().PlayOneShot(audioClip);

        currentDialogue = dialogue;
        dialogueManager.GetComponent<DialogueManager>().StartDialogue(currentDialogue.dialogueArray[currentLine]);
        lostInThought = true;
        currentLine++;
        
    }

    public void Interact()
    {
        dialogueManager.GetComponent<DialogueManager>().GetComponent<AudioSource>().Stop();
        AudioClip audioClip = dialogueManager.GetComponent<DialogueManager>().GetAudioSource().clip;
        dialogueManager.GetComponent<DialogueManager>().GetAudioSource().PlayOneShot(audioClip);

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
            playerInput.actions.FindAction("Pause").Enable();
            playerInput.actions.FindAction("Inventory").Enable();
            playerInput.actions.FindAction("Walk").Enable();

        }
    }

    IEnumerator ThoughtDelay(DialogueWithName dialogue)
    {
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<RealPlayerMovement>().currentInteractable = null;
        StartThinking(dialogue);
    }
}
