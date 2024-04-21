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
    public TextMeshProUGUI descriptionText;

    // Updates the information in the Inventory UI
    // Gets an array of items to represent the inventory
    public void UpdateUI(Item[] inventory)
    {
        // Get the new inventory information
        this.inventory = inventory;
        int offset = 0;
        int lastSlot = 0;

        //Debug.Log("Length: " + inventory.Length);

        // Itterate through the inventory until we've used all the slots in the UI or until we've reached the end of the inventory, whichever comes first
        for (int slotNumber = 0; slotNumber < numberOfSlots && slotNumber + offset < inventory.Length; slotNumber++)
        {
            int quantity = 1;
            //Debug.Log(slotNumber);
            // Get the next item from the inventory
            Item item = inventory[slotNumber + offset];

            // Get the quantity of items and set the offset
            // While the next inventory slot has the same item as the current slot...
            while (slotNumber + offset < inventory.Length - 1 && inventory[slotNumber + offset + 1].name.Equals(inventory[slotNumber + offset].name))
            {
                // Increase the quantity of the current item
                quantity++;
                // Move the offset for the inventory index
                offset++;

                Debug.Log("Q" + quantity + " O" + offset);
            }

            // Store the item's image in the appropriate slot
            transform.GetChild(0).GetChild(slotNumber).GetChild(1).GetComponent<Image>().sprite = item.image;
            transform.GetChild(0).GetChild(slotNumber).GetChild(1).GetComponent<Image>().preserveAspect = true;
            transform.GetChild(0).GetChild(slotNumber).GetChild(1).gameObject.SetActive(true);

            // Store the item's name in the appropriate slot
            transform.GetChild(0).GetChild(slotNumber).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.GetName() + " x" + quantity;

            // Stores the item in it's Item Description script
            transform.GetChild(0).GetChild(slotNumber).GetChild(1).GetComponent<ItemDescription>().SetItem(item);

            lastSlot++;
        }


        // A for loop that clears the remaining inventory slots
        for (int i = lastSlot; i < numberOfSlots; i++)
        {
            transform.GetChild(0).GetChild(i).GetChild(1).gameObject.SetActive(false);
            transform.GetChild(0).GetChild(i).GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
        }

        // Resets the description box
        descriptionText = null;

    }
}
