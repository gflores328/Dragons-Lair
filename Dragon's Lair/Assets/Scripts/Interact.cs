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
    public Item itemToPickup;
    [HideInInspector, SerializeField]
    private GameObject inventory;

    // Variables for dialogue type interact
    //[HideInInspector, SerializeField]
    //private List<DialogueWithName> dialogueBranches;
    [HideInInspector, SerializeField]
    private GameObject questionUI;
    [SerializeField, HideInInspector] 
    GameObject firstButton;

    // Variables for checking if an item is needed to interact
    [HideInInspector, SerializeField]
    private Item itemNeeded;
    [HideInInspector, SerializeField]
    private DialogueWithName itemNotObtained;

    // Variables for if a story trigger is needed to interact
    public bool storyStateNeeded;
    [HideInInspector, SerializeField]
    private GameState.state state;
    [HideInInspector, SerializeField]
    private DialogueWithName cantInteractYetDialogue;
    private GameObject gameState;
    private bool correctTrigger = false;

    // Variables for if an interact updates the objective list
    public bool updateObjective;
    [HideInInspector, SerializeField]
    string newObjective;

    private void Start()
    {
        dialogueToDisplay = interactDialogue;
        inventory = GameObject.Find("Inventory");
        gameState = GameObject.Find("GameState");
    }

    // When player steps onto the trigger then the prompt to interact is shown
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            dialogueManager.GetComponent<DialogueManager>().StartDialogue("Press Interact Button");

            // If the interact does not need a story event or an item
            if (!storyStateNeeded && !needItem)
            {
                dialogueToDisplay = interactDialogue;
                correctTrigger = true;
                hasItemNeeded = true;
                
            }
            // If the interact needs a story event and an item
            else if(storyStateNeeded && needItem)
            {
                // if the current story state is not the same as the one needed then the dialog is set to cant interact yet
                if (!((int)state <= ((int)gameState.GetComponent<GameState>().storyState)))
                {
                    dialogueToDisplay = cantInteractYetDialogue;
                    correctTrigger = false;
                }
                // if the state is correct but the item is not obtained then the dialogue is set to item not obtained
                else if(((int)state) <= ((int)gameState.GetComponent<GameState>().storyState) && !inventory.GetComponent<Inventory>().Contains(itemNeeded))
                {
                    dialogueToDisplay = itemNotObtained;
                    hasItemNeeded = false;
                    correctTrigger = true;
                }
                // else the dialogue is set to interact
                else
                {
                    correctTrigger = true;
                    hasItemNeeded = true;
                    dialogueToDisplay = interactDialogue;
                }
            }
            // if only a story event is needed
            else if(storyStateNeeded)
            {
                
                // if the state needed is the current state of the game
                if (!(((int)state) <= ((int)gameState.GetComponent<GameState>().storyState)))
                {
                    correctTrigger = false;
                    dialogueToDisplay = cantInteractYetDialogue;

                }
                // else the dialogue is set to the normal interact
                else
                {
                    hasItemNeeded = true; 
                    correctTrigger = true;
                    dialogueToDisplay = interactDialogue;
                }
            }
            // if only an item is needed
            else if (needItem)
            {
                
                // if the item is not in the inventory then dialogue is set to the no item dialogue
                if (!inventory.GetComponent<Inventory>().Contains(itemNeeded))
                {
                    dialogueToDisplay = itemNotObtained;
                    hasItemNeeded = false;
                    
                }
                else
                {
                    correctTrigger = true;
                    hasItemNeeded = true;
                    dialogueToDisplay = interactDialogue;
                    
                }
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
        //Cursor.lockState = CursorLockMode.None;
        // This if is multi purpose and will run if interaction type is inspect or if hasItemNeeded is false 
        if (interactionType == InteractionType.inspect || !hasItemNeeded || !correctTrigger)
        {
            // While the currentLine is less than the dialogueToDisplays array legnth then the text is changed to the currentLine of the array
            if (currentLine < dialogueToDisplay.dialogueArray.Length)
            {
                // Time scale is set to 0 when interact is run
                Time.timeScale = 0;

                dialogueManager.GetComponent<DialogueManager>().TextChange(dialogueToDisplay.dialogueArray[currentLine]);
                currentLine++;
            }
            // If currentLine is not less than then EndDialogue is run and timeScale is set back to 1;
            else
            { 
                dialogueManager.GetComponent<DialogueManager>().EndDialogue();
                currentLine = 0;
                Time.timeScale = 1;

                if (updateObjective && hasItemNeeded && correctTrigger)
                {
                    dialogueManager.GetComponent<DialogueManager>().ObjectiveChange(newObjective);
                }
                          
            }
        }

        // If interaction type is menu and hasItemNeeded is true
        if (interactionType == InteractionType.menu && !menuOpen && hasItemNeeded && correctTrigger)
        {
            // While the currentLine is less than the dialogueToDisplays array legnth then the text is changed to the currentLine of the array
            if (currentLine < dialogueToDisplay.dialogueArray.Length)
            { 
                // Time scale is set to 0 when interact is run
                Time.timeScale = 0;

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
                Cursor.lockState = CursorLockMode.None;
            }
        }

        // If interaction type is item and hasItemNeeded is true
        if (interactionType == InteractionType.item && hasItemNeeded && correctTrigger)
        {
            // While the currentLine is less than the dialogueToDisplays array legnth then the text is changed to the currentLine of the array
            if (currentLine < dialogueToDisplay.dialogueArray.Length)
            {
                // Time scale is set to 0 when interact is run
                Time.timeScale = 0;

                dialogueManager.GetComponent<DialogueManager>().TextChange(dialogueToDisplay.dialogueArray[currentLine]);
                currentLine++;
            }
            // When dilogue is over then the item set will be added to the inventory and the game object is destroyed
            else
            { 
            
                dialogueManager.GetComponent<DialogueManager>().EndDialogue();
                inventory.GetComponent<Inventory>().AddItem(itemToPickup);
                Time.timeScale = 1;
                GameObject.Find("GameState").GetComponent<GameState>().AddNonRespawnable(gameObject.name);
               
                if (updateObjective)
                {
                    dialogueManager.GetComponent<DialogueManager>().ObjectiveChange(newObjective);
                }

                if (!gameObject.TryGetComponent(out InteractionState interactionState))
                {
                    Destroy(gameObject);
                }
            }
        }

        // If interaction type is dialogue and hasItemNeeded is true
        if (interactionType == InteractionType.dialogue && !menuOpen && hasItemNeeded && correctTrigger)
        {
            // While the currentLine is less than the dialogueToDisplays array legnth then the text is changed to the currentLine of the array
            if (currentLine < dialogueToDisplay.dialogueArray.Length)
            {
                // Time scale is set to 0 when interact is run
                Time.timeScale = 0;

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
                Cursor.lockState = CursorLockMode.None;
                 
            }
        }
    }

    // This function is for a button click event
    public void QuestionSelected(DialogueWithName dialogue)
    {
        // It takes and int and will set dialogueToDisplay to the index of it in dialogueBranches - 1
        dialogueToDisplay = dialogue;
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
        //
        
    }

    // For on on click event that will close the UI that the menu type interact pops up
    public void MenuClose()
    {
        Time.timeScale = 1;
        menuOpen = false;
        menuUI.SetActive(false);
        //Cursor.lockState = CursorLockMode.Locked;
    }

    #region Editor
#if UNITY_EDITOR

    [CustomEditor(typeof(Interact))]
    public class InteractEditor : Editor
    {
        // This function overrides the inspector for this gameobject
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI(); // The base inspector values
            Interact interact = (Interact)target; // A reference to this game object

            // If the interaction type is menu then these fields are shown in the editor
            if (interact.interactionType == InteractionType.menu)
            {
                // ClearPanels();

                // A header called Menu Interaction
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Menu Interaction", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();

                // A game object field for the Menu UI
                EditorGUILayout.LabelField("Menu UI", GUILayout.MaxWidth(126));
                interact.menuUI = EditorGUILayout.ObjectField(interact.menuUI, typeof(GameObject),true, GUILayout.MaxWidth(220)) as GameObject;
                EditorGUILayout.EndHorizontal();

                // A game object field for the first button 
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("First Button", GUILayout.MaxWidth(126));
                interact.firstButton = EditorGUILayout.ObjectField(interact.firstButton, typeof(GameObject), true, GUILayout.MaxWidth(220)) as GameObject;
                EditorGUILayout.EndHorizontal();


            }

            // If interaction type is set to item then these fields will show on the editor
            if(interact.interactionType == InteractionType.item)
            {
              
                // A header named Item Interaction
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Item Interaction", EditorStyles.boldLabel);

                // A Item field fot the Item thats picked up on interaction
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Item", GUILayout.MaxWidth(126));
                interact.itemToPickup = EditorGUILayout.ObjectField(interact.itemToPickup, typeof(Item), true, GUILayout.MaxWidth(220)) as Item;
                EditorGUILayout.EndHorizontal();

                /*
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Inventory", GUILayout.MaxWidth(126));
                interact.inventory = EditorGUILayout.ObjectField(interact.inventory, typeof(GameObject), true, GUILayout.MaxWidth(220)) as GameObject;
                EditorGUILayout.EndHorizontal();
                */
            }
           
            // If interaction type is dialogue then these fields will be shown in the inspector
            if(interact.interactionType == InteractionType.dialogue)
            {
                // A header named Dialogue Interaction
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Dialogue Interaction", EditorStyles.boldLabel);

                // A game object field for the questions UI
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Questions UI", GUILayout.MaxWidth(126));
                interact.questionUI = EditorGUILayout.ObjectField(interact.questionUI, typeof(GameObject), true, GUILayout.MaxWidth(220)) as GameObject;
                EditorGUILayout.EndHorizontal();

                // A game object field for the first button
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("First Button", GUILayout.MaxWidth(126));
                interact.firstButton= EditorGUILayout.ObjectField(interact.firstButton, typeof(GameObject), true, GUILayout.MaxWidth(220)) as GameObject;
                EditorGUILayout.EndHorizontal();

                EditorGUILayout.Space();

               /* // An integer field fot the number of questions
                List<DialogueWithName> list = interact.dialogueBranches;
                int size = Mathf.Max(0, EditorGUILayout.IntField("Number of Questions", list.Count));

                // Checks the int in the int field and shows that many DialogueWithName fields in the inspector
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
               */
            }

            // If need item is set to true then these fields will show in the inspector
            if (interact.needItem)
            {
                // A header named Need Item
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Need Item", EditorStyles.boldLabel);

                /*
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Inventory", GUILayout.MaxWidth(126));
                interact.inventory = EditorGUILayout.ObjectField(interact.inventory, typeof(GameObject), true, GUILayout.MaxWidth(220)) as GameObject;
                EditorGUILayout.EndHorizontal();
                */

                // A Item field for the item needed
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Item Needed", GUILayout.MaxWidth(126));
                interact.itemNeeded = EditorGUILayout.ObjectField(interact.itemNeeded, typeof(Item), true, GUILayout.MaxWidth(220)) as Item;
                EditorGUILayout.EndHorizontal();

                // A DialogueWithName field for the no item dialogue
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("No Item Dialogue", GUILayout.MaxWidth(126));
                interact.itemNotObtained = EditorGUILayout.ObjectField(interact.itemNotObtained, typeof(DialogueWithName), false, GUILayout.MaxWidth(220)) as DialogueWithName;
                EditorGUILayout.EndHorizontal();

            }

            // If need a need a story trigger is true
            if (interact.storyStateNeeded)
            {
                // A header called Need Story State
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Need Story State", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();

                // An enum field that represents the game state
                EditorGUILayout.LabelField("Story State", GUILayout.MaxWidth(126));
                interact.state = (GameState.state)EditorGUILayout.EnumPopup(interact.state, GUILayout.MaxWidth(220));
                EditorGUILayout.EndHorizontal();

                // A DialogueWithName field for incorrect state dialogue
                EditorGUILayout.BeginHorizontal();
                EditorGUILayout.LabelField("Not State Dialogue", GUILayout.MaxWidth(126));
                interact.cantInteractYetDialogue = EditorGUILayout.ObjectField(interact.cantInteractYetDialogue, typeof(DialogueWithName), false, GUILayout.MaxWidth(220)) as DialogueWithName;
                EditorGUILayout.EndHorizontal();
            }

            // If the objective should be updated after the interaction
            if (interact.updateObjective)
            {
                // A header called Update Objective
                EditorGUILayout.Space();
                EditorGUILayout.LabelField("Update Objective", EditorStyles.boldLabel);
                EditorGUILayout.BeginHorizontal();

                // A string field that represents the new objective
                EditorGUILayout.LabelField("New Objective", GUILayout.MaxWidth(126));
                interact.newObjective = EditorGUILayout.TextField(interact.newObjective, GUILayout.MaxWidth(220));
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

        // A function that will clear the panels upon enum switch
        // Currecntly not working
        public void ClearPanels()
        {
            //Cursor.lockState = CursorLockMode.Locked;
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
