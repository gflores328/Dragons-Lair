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
using UnityEngine.UI;

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
    [Tooltip("The UI for the objective")]
    public TextMeshProUGUI objectiveText;
    public GameObject objectiveBox;
    [Tooltip("The image that holds the character bust of who is talking on the left")]
    public GameObject bustUILeft;
    [Tooltip("The image that holds the character bust of who is talking on the right")]
    public GameObject bustUIRight;




    private GameObject gameState;
    private void Start()
    {
        gameState = GameObject.Find("GameState");
        objectiveText.text = gameState.GetComponent<GameState>().objective;
        Debug.Log(objectiveText.text);
    }

    // This function will take a string variable and start the dialogue by showing the UI
    public void StartDialogue(DialogueAndName dialogueAndName)
    {
        // textBox is set active and dialogueText is set to the string value that was passed
        textBox.SetActive(true);
        dialogueText.text = dialogueAndName.dialogue;
        nameBox.SetActive(true);
        nameText.text = dialogueAndName.name;
        
        

        if (dialogueAndName.bustSide == side.left)
        {
            bustUILeft.SetActive(true);
            bustUILeft.GetComponent<Image>().sprite = dialogueAndName.bust;
        }
        else if (dialogueAndName.bustSide == side.right)
        {
            bustUIRight.SetActive(true);
            bustUIRight.GetComponent<Image>().sprite = dialogueAndName.bust;
        }

        
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
        bustUILeft.SetActive(false);
        bustUILeft.GetComponent<Image>().sprite = null;
        bustUIRight.SetActive(false);
        bustUIRight.GetComponent<Image>().sprite = null;


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

        if (!bustUILeft.activeInHierarchy && dialogueAndName.bustSide == side.left)
        {
            bustUILeft.SetActive(true);
            bustUIRight.SetActive(false);
        }
        else if (!bustUIRight.activeInHierarchy && dialogueAndName.bustSide == side.right)
        {
            bustUIRight.SetActive(true);
            bustUILeft.SetActive(false);
        }
        else if (dialogueAndName.bustSide == side.none)
        {
            bustUIRight.SetActive(false);
            bustUILeft.SetActive(false);
        }

        dialogueText.text = dialogueAndName.dialogue;
        nameText.text = dialogueAndName.name;

        if (dialogueAndName.bustSide == side.left)
        {
            bustUILeft.GetComponent<Image>().sprite = dialogueAndName.bust;
        }
        else if (dialogueAndName.bustSide == side.right)
        {
            bustUIRight.GetComponent<Image>().sprite = dialogueAndName.bust;
        }

       
    }

    /*
    public void QuestionClicked(DialogueWithName dialogue)
    {
        Interact.dialogueToDisplay = dialogue;
        StartDialogue(dialogue.dialogueArray[0]);
    }
    */

    // This is a function that will change the objective text to the inputted string
    public void ObjectiveChange(string objective)
    {
        objectiveText.text = objective;
        gameState.GetComponent<GameState>().UpdateObjective(objective);
    }

    public void ObjectiveActive()
    {
        objectiveBox.SetActive(true);
        objectiveText.gameObject.SetActive(true);
    }

    public void ObjectiveDeactive()
    {
        objectiveBox.SetActive(false);
        objectiveText.gameObject.SetActive(false);
    }
}
