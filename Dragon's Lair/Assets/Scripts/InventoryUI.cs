/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 13, 2024 at 11:38 PM
 * 
 * Updates the Inventory UI to match the information in the inventory script. 
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryUI : MonoBehaviour
{
    [Tooltip("The number of slots in the Inventory UI.\nThis is important for making sure items aren't being displayed in a slot that doesn't exist.")]
    public int numberOfSlots;
    [SerializeField]
    private Item[] inventory;

    // Updates the information in the Inventory UI
    // Gets an array of items to represent the inventory
    public void UpdateUI(Item[] inventory)
    {
        // Get the new inventory information
        this.inventory = inventory;

        // Itterate through the inventory until we've used all the slots in the UI or until we've reached the end of the inventory, whichever comes first
        for (int slotNumber = 0; slotNumber < numberOfSlots &&  slotNumber < inventory.Length; slotNumber++)
        {
            Debug.Log(slotNumber);
            // Get the next item from the inventory
            Item item = inventory[slotNumber];

            // Store the item's image in the appropriate slot
            transform.GetChild(0).GetChild(slotNumber).GetChild(0).GetComponent<Image>().sprite = item.image;
            transform.GetChild(0).GetChild(slotNumber).GetChild(0).GetComponent<Image>().preserveAspect = true;
            transform.GetChild(0).GetChild(slotNumber).GetChild(0).gameObject.SetActive(true);

            // Store the item's name in the appropriate slot
            
              
            
            transform.GetChild(0).GetChild(slotNumber).GetChild(1).GetComponent<TextMeshProUGUI>().text = item.GetName() + " x" + item.GetQuantity();
        }

    }
}
