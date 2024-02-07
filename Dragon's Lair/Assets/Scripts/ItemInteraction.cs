using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemInteraction : MonoBehaviour
{
    [Header("Dialouge")]
    [Tooltip("The number of lines in a dialouge interaction and what they say")]
    [TextArea]
    public string[] dialougeLines; // The lines of dialouge will be shown

    [Tooltip("The UI objects that the dialouge will use")]
    [Header("UI")]
    public GameObject textBox; // The texbox UI
    public TextMeshProUGUI dialougeText; // The text UI

    [Header("Item Pick Up")]
    [Tooltip("The game object that holds the inventory")]
    public GameObject inventory; // This will be the object that hold the inventory script so the script can be referenced
    [Tooltip("The item that is picked up after the interaction")]
    public Item item; // This is a Item scriptable object thats data will be obtained
    [Tooltip("The game object that represents the item in the world")]
    public GameObject worldItem; // This game object is the object in the scene that represents the item


    private bool inDialouge; // bool to see if player is in dialouge
    private int currentLine; // the index of the dialouge array that is being shown
    private bool inTrigger;

    // Start is called before the first frame update
    void Start()
    {
        inDialouge = false;
        currentLine = 0;
        inTrigger = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            Interact();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // When Player enters trigger the UI prompt will be shown
        if (other.tag == "Player")
        {
            textBox.SetActive(true);
            dialougeText.text = "Press E to interact";
            inTrigger = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // When player exits the trigger then the UI will dissapear
        if (other.tag == "Player")
        {
            currentLine = 0;
            textBox.SetActive(false);
            dialougeText.text = null;
            inDialouge = false;
            inTrigger = false;
        }
    }

    /* private void OnTriggerStay(Collider other)
     {
         if (other.tag == "Player")
         {
             Debug.Log("player");
         }
         else
         {
             Debug.Log("NO player");
         }


         // When player is in trigger and E is pressed the Interact function is run
         if (other.tag == "Player" && Input.GetKeyDown(KeyCode.E))
         {
             Interact();
         }
     }
    */

    // The text background is set active and the first line of text is set to the text UI
    private void StartDiolouge()
    {
        textBox.SetActive(true);
        dialougeText.text = dialougeLines[currentLine];
        inDialouge = true;
    }

    // The background is set asleep, text UI is null, and current line is reset to 0
    private void EndDiolouge()
    {
        textBox.SetActive(false);
        dialougeText.text = null;
        currentLine = 0;
        inDialouge = false;
        PickUp();
    }

    private void Interact()
    {
        // If inDialogue is false then StartDiolougue is run and inDiolouge is set to true
        if (!inDialouge)
        {
            StartDiolouge();
        }
        // If inDiolouge is true then the next line of text is displayed if there is one
        // If there is no nect line then EndDiolouge is run
        else if (inDialouge)
        {
            if (currentLine < dialougeLines.Length)
            {
                currentLine++;
                if (!(currentLine >= dialougeLines.Length))
                {
                    dialougeText.text = dialougeLines[currentLine];
                }
                else
                {
                    EndDiolouge();
                }
            }

        }
    }

    //This function will pick up the item 
    private void PickUp()
    {
        Destroy(worldItem);
        inventory.GetComponent<Inventory>().AddItem(item);
    }
}
