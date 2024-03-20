/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Mar 16, 2024 at 11:28 PM
 * 
 * Manages a list of items by
 * - Adding items
 * - Removing items
 * - Retrieving a list of items
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //A list of items in the inventory
    [Tooltip("List of Items to represent the inventory")]
    private static List<Item> inventory;

    //GABE ADDED
    [Tooltip("Inventory UI Controller")]
    public InventoryUI inventoryUI;
    
    //Start is called before the first frame update
    //Initializes the inventory list as an empty list of Items
    void Start()
    {
        inventory = new List<Item>();
    }


    // Adds the given item and updates the Inventory UI
    public void AddItem(Item givenItem)
    {
        //Add the item
        inventory.Add(givenItem);
        //Update the Inventory UI
        inventoryUI.UpdateUI(GetInventory());

        //Debug.Log("Item added");
    }

    //Removes the last instance of a given item
    private bool RemoveItem(Item givenItem, bool removeAll)
    {
        //A variable to keep track of whether the item has been removed or not
        //This is returned at the end of the function
        bool wasItemRemoved = false;

        //Check to see if the inventory has the item that is going to be removed
        Item existingItem = FindItem(givenItem.GetName());

        //If the item doesn't already exist in the inventory, do nothing
        if (existingItem == null)
        {
            wasItemRemoved = false;
        }
        //If the item does exist, remove it
        else
        {
            if (removeAll)
            {
                //Itterate through the inventory
                for (int i = 0; i < inventory.Count; i++)
                {
                    //Remove every item with the same name
                    if (inventory[i].name.Equals(existingItem.name))
                    {
                        inventory.Remove(existingItem);
                    }
                }
            }
            else
            {
                inventory.Remove(existingItem);
            }

            //Mark that the item was removed
            wasItemRemoved = true;
        }

        //Update the inventory UI
        inventoryUI.UpdateUI(GetInventory());

        //Report whether the item was successfully removed or not
        return wasItemRemoved;
    }

    //Returns the inventory as an array of items
    public Item[] GetInventory()
    {
        inventory.Sort();
        //Return the inventory
        return inventory.ToArray();
    }

    //Returns the most recently added item with a name matching the given item name. Returns a null reference if no such item is found.
    //Accepts a string for the item name.
    private Item FindItem(string itemName)
    {
        //Search through the inventory
        for (int i = inventory.Count - 1; i >= 0; i--)
        {
            //Compare the current item's name to the given name
            if (inventory[i].GetName() == itemName)
            {
                //If the item name matches, return the reference to the item
                return inventory[i];
            }
        }

        //If no item with a matching name was found return a null reference
        return null;
    }

    //Returns true if there is at least one item with the given item's name
    public bool Contains(Item newItem)
    {
        //Tracks whether the desired item exists in the inventory
        bool containsItem = false;
        //A reference to the desired item
        Item item = FindItem(newItem.GetName());

        //First, check to make sure the item exists before trying to access its data
        if (item != null)
        {
            containsItem = true;
        }

        //Return the state of the desired item existing or not
        return containsItem;
    }
}
