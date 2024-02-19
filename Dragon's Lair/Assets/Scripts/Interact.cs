/*
 * Created By: Gabriel Flores
 * 
 * This script will be an interact script that combines the other interact scripts(dialogue interact, item interact, menu interect, and dialogue tree)
 * There will be a custom editor so that when the type of interact seleted by the enem is chosen the appropriate fields to fill out will be shown
 * The script will run a bit diffrently depending on which enem is run so that it can reflect which interact type is selected in the enum
 * 
 * Tutorial used: https://www.youtube.com/watch?v=RImM7XYdeAc
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR

using UnityEditor;

#endif

public class Interact : MonoBehaviour
{
    // Variables for all uses
    enum InteractionType {inspect, menu, item, dialogue} // An enem to determine the interaction type

    [Tooltip("The type of interaction that the script will be")]
    [SerializeField]
    InteractionType interactionType;
    [Tooltip("The dialogue that will pop up when the object is interacted with")]
    public DialogueWithName interactDialogue;
    [Tooltip("A reference to the dialogue manager")]
    public GameObject dialogueManager;
    [Tooltip("A bool that is used to set whether or not the interaction needs a certain Item")]
    public bool needItem;

    private DialogueWithName dialogueToDisplay; // A dialogueWitName object to hold the dialogue that needs to be displayed
    private int currentLine = 0; // An into to see which line of the array in dialogueToDisplay is being shown
    //private bool inDialogue = false; // A bool to check if whether or not the player is in dialogue or not
    private bool hasItemNeeded = true; // A bool to check if the player has the item needed to interact


    // Variables for menu type interact
    [HideInInspector, SerializeField]
    private GameObject menuUI;

    private bool menuOpen; // A bool that checks to see if the menu is open

    // Variables for item type interact
    [HideInInspector, SerializeField]
    private Item itemToPickup;
    [HideInInspector, SerializeField]
    private GameObject inventory;

    // Variables for dialogue type interact
    [HideInInspector, SerializeField]
    private List<DialogueWithName> dialogueBranches;
    [HideInInspector, SerializeField]
    private GameObject questionUI;
    [SerializeField] GameObject firstButton;

    // Variables for checking if an item is needed to interact
    [HideInInspector, SerializeField]
    private Item itemNeeded;
    [HideInInspector, SerializeField]
    private DialogueWithName itemNotObtained;

    private void Start()
    {
        dialogueToDisplay = interactDialogue;
    }

    // When player steps onto the trigger then the prompt to interact is shown
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dialogueManager.GetComponent<DialogueManager>().StartDialogue("Press Interact Button");

            // If the player needs an item to interact then Inventory.Contains is called to check if it it in the inventory
            if (needItem)
            {
                // If it is then dialogueToDisplay is set to interactDialogue and hasItemNeeded is set to true
                if (inventory.GetComponent<Inventory>().Contains(itemNeeded))
                {
                    dialogueToDisplay = interactDialogue;
                    hasItemNeeded = true;
                }
                // If it is not there then the dialogueToDisplay is set ti the itemNotObtained dialogue and hasItemNeeded is set to false
                else
                {
                    dialogueToDisplay = itemNotObtained;
                    hasItemNeeded = false;
                }
            }
            // If the player does not need an item then dialogueToDisplay is set to interactDialogue
            else
            {
                dialogueToDisplay = interactDialogue;
            }
        }
    }

    // When player exits the trigger multiple variables are reset and EndDialogue is run
    private void OnTriggerExit(Collider other)
    {
        currentLine = 0;
        //inDialogue = false;
        menuOpen = false;
        dialogueManager.GetComponent<DialogueManager>().EndDialogue();

        // If the interactionType is menu then the menuUI that belongs to that interaction type will also be set to false
        if (interactionType == InteractionType.menu)
        {
            menuUI.SetActive(false);
        }
    }

    // Everytime this script is called it checks which enem type is selected and will run differently according to that
    public void Interacted()
    {
        // Time scale is set to 0 when interact is run
        Time.timeScale = 0;

        // This if is multi purpose and will run if interaction type is inspect or if hasItemNeeded is false 
        if (interactionType == InteractionType.inspect || !hasItemNeeded)
        {
            // While the currentLine is less than the dialogueToDisplays array legnth then the text is changed to the currentLine of the array
            if (currentLine < dialogueToDisplay.dialogueArray.Length)
            {
                dialogueManager.GetComponent<DialogueManager>().TextChange(dialogueToDisplay.dialogueArray[currentLine]);
                currentLine++;
            }
            // If currentLine is not less than then EndDialogue is run and timeScale is set back to 1;
            else
            { 
                dialogueManager.GetComponent<DialogueManager>().EndDialogue();
                currentLine = 0;
                Time.timeScale = 1;            
            }
        }

        // If interaction type is menu and hasItemNeeded is true
        if (interactionType == InteractionType.menu && !menuOpen && hasItemNeeded)
        {
            // While the currentLine is less than the dialogueToDisplays array legnth then the text is changed to the currentLine of the array
            if (currentLine < dialogueToDisplay.dialogueArray.Length)
            {
                dialogueManager.GetComponent<DialogueManager>().TextChange(dialogueToDisplay.dialogueArray[currentLine]);
                currentLine++;
            }
            // When dialogue is over then the menuUI will be set active
            else
            {
                currentLine = 0;
                dialogueManager.GetComponent<DialogueManager>().EndDialogue();
                menuOpen = true;
                menuUI.SetActive(true);
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstButton);
                Time.timeScale = 1;
            }
        }

        // If interaction type is item and hasItemNeeded is true
        if (interactionType == InteractionType.item && hasItemNeeded)
        {
            // While the currentLine is less than the dialogueToDisplays array legnth then the text is changed to the currentLine of the array
            if (currentLine < dialogueToDisplay.dialogueArray.Length)
            {
                dialogueManager.GetComponent<DialogueManager>().TextChange(dialogueToDisplay.dialogueArray[currentLine]);
                currentLine++;
            }
            // When dilogue is over then the item set will be added to the inventory and the game object is destroyed
            else
            { 
            
                dialogueManager.GetComponent<DialogueManager>().EndDialogue();
                inventory.GetComponent<Inventory>().AddItem(itemToPickup);
                Time.timeScale = 1;
                Destroy(gameObject);
            }
        }

        // If interaction type is dialogue and hasItemNeeded is true
        if (interactionType == InteractionType.dialogue && !menuOpen && hasItemNeeded)
        {
            // While the currentLine is less than the dialogueToDisplays array legnth then the text is changed to the currentLine of the array
            if (currentLine < dialogueToDisplay.dialogueArray.Length)
            {
                dialogueManager.GetComponent<DialogueManager>().TextChange(dialogueToDisplay.dialogueArray[currentLine]);
                currentLine++;
            }
            // When dialogue is done then the questionsUI is set active
            else
            {
                dialogueManager.GetComponent<DialogueManager>().EndDialogue();
                currentLine = 0;
                questionUI.SetActive(true);
                UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(firstButton);
                menuOpen = true;
                 
            }
        }
    }

    // This function is for a button click event
    public void QuestionSelected(int questionNumber)
    {
        // It takes and int and will set dialogueToDisplay to the index of it in dialogueBranches - 1
        dialogueToDisplay =  dialogueBranches[questionNumber - 1];
        dialogueManager.GetComponent<DialogueManager>().StartDialogue(dialogueToDisplay.dialogueArray[currentLine]);
        currentLine++;
        questionUI.SetActive(false);
        menuOpen = false;
    }

    // For an on click event that will close menus and end dialogue also sets timeScale back to 1
    public void CloseButton()
    {
        Time.timeScale = 1;
        currentLine = 0;
        dialogueToDisplay = interactDialogue;
        //inDialogue = false;
        menuOpen = false;
        questionUI.SetActive(false);
    }

    #region Editor
#if UNITY_EDITOR

    [CustomEditor(typeof(Interact))]
    public class InteractEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            Interact interact = (Interact)target;

            if (interact.interactionType == InteractionType.menu)
            {
                // ClearPanels();

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Menu Interaction", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Menu UI", GUILayout.MaxWidth(126));
                interact.menuUI = EditorGUILayout.ObjectField(interact.menuUI, typeof(GameObject),true, GUILayout.MaxWidth(220)) as GameObject;
                EditorGUILayout.EndHorizontal();

                
            }

            if(interact.interactionType == InteractionType.item)
            {
              // ClearPanels();

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Item Interaction", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Item", GUILayout.MaxWidth(126));
                interact.itemToPickup = EditorGUILayout.ObjectField(interact.itemToPickup, typeof(Item), true, GUILayout.MaxWidth(220)) as Item;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Inventory", GUILayout.MaxWidth(126));
                interact.inventory = EditorGUILayout.ObjectField(interact.inventory, typeof(GameObject), true, GUILayout.MaxWidth(220)) as GameObject;
                EditorGUILayout.EndHorizontal();
            }
           
            if(interact.interactionType == InteractionType.dialogue)
            {
                //ClearPanels();

                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Dialogue Interaction", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Questions UI", GUILayout.MaxWidth(126));
                interact.questionUI = EditorGUILayout.ObjectField(interact.questionUI, typeof(GameObject), true, GUILayout.MaxWidth(220)) as GameObject;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

                List<DialogueWithName> list = interact.dialogueBranches;
                int size = Mathf.Max(0, EditorGUILayout.IntField("Number of Questions", list.Count));

                while (size > list.Count)
                {
                    list.Add(null);
                }

                while (size < list.Count)
                {
                    list.RemoveAt(list.Count - 1);
                }
                for (int i = 0; i < list.Count; i++)
                {
                    list[i] = EditorGUILayout.ObjectField("Question " + (i + 1), list[i], typeof(DialogueWithName), false) as DialogueWithName;
                }
            }

            if (interact.needItem)
            {
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Need Item", EditorStyles.boldLabel);

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Inventory", GUILayout.MaxWidth(126));
                interact.inventory = EditorGUILayout.ObjectField(interact.inventory, typeof(GameObject), true, GUILayout.MaxWidth(220)) as GameObject;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Item Needed", GUILayout.MaxWidth(126));
                interact.itemNeeded = EditorGUILayout.ObjectField(interact.itemNeeded, typeof(Item), true, GUILayout.MaxWidth(220)) as Item;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("No Item Dialogue", GUILayout.MaxWidth(126));
                interact.itemNotObtained = EditorGUILayout.ObjectField(interact.itemNotObtained, typeof(DialogueWithName), false, GUILayout.MaxWidth(220)) as DialogueWithName;
                EditorGUILayout.EndHorizontal();

            }
            /*
            if (!interact.needItem)
            {
                interact.itemNeeded = null;
                interact.itemNotObtained = null;
            }
            */
        }

        public void ClearPanels()
        {
            Interact interact = (Interact)target;

            interact.menuUI = null;
            interact.itemToPickup = null;
            interact.inventory = null;
            interact.questionUI = null;
            //foreach(DialogueWithName item in interact.dialogueBranches)
            //{
            //    item.dialogueArray = null;
            //}
        }
    }

#endif
    #endregion
}
