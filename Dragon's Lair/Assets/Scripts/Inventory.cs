/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 16, 2024 at 10:01 AM
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


    /*
     * Adds the given item
     * If an item with the same name already exists in the inventory, add the given item's quantity to the existing item
     * If an item with the same name does NOT already exist in the inventory, add the new item
     */
    public void AddItem(Item givenItem)
    {
        //The quantity of the item being added must be a positive integer
        if (givenItem.GetQuantity() > 0)
        {
            //Check to see if the item to be added already exists in the inventory
            Item existingItem = FindItem(givenItem.GetName());

            //If the item isn't already in the inventory accept the given item
            if (existingItem == null)
            {
                inventory.Add(givenItem);
            }
            //If the item does aready exist simply increase the quantity of the existing item
            else
            {
                existingItem.IncreaseQuantity(givenItem.GetQuantity());
            }

            //Update the Inventory UI
            inventoryUI.UpdateUI(GetInventory());
        }
    }

    //Removes a specific amount or all of a given item
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
        //If the item does exist...
        else
        {
            //Calculate the new quantity of the item
            int newQuantity = existingItem.GetQuantity() - givenItem.GetQuantity();

            //If the new quantity is less than one or if removeAll is true remove the item from the inventory completely
            if (removeAll || newQuantity < 1)
            {
                inventory.Remove(existingItem);
            }
            //If the new quantity results in a postive integer simply decrease the quantity of the existing item
            else
            {
                existingItem.DecreaseQuantity(givenItem.GetQuantity());
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
        //Return the inventory
        return inventory.ToArray();
    }

    //Returns an item with a name matching the given item name. Returns a null reference if no such item is found.
    //Accepts a string for the item name.
    private Item FindItem(string itemName)
    {
        //Search through the inventory
        foreach (Item i in inventory)
        {
            //Compare the current item's name to the given name
            if (i.GetName().Equals(itemName))
            {
                //If the item name matches, return the reference to the item
                return i;
            }
        }

        //If no item with a matching name was found return a null reference
        return null;
    }

    //Returns true if there is at least a specified quantity of an item in an inventory
    //e.g. a quantity of 3 would return true if the item's quantity is 3 or above and return false for 2 and below
    public bool Contains(Item newItem)
    {
        //Tracks whether the desired item exists in the inventory
        bool containsItem = false;
        //A reference to the desired item
        Item item = FindItem(newItem.GetName());

        //First, check to make sure the item exists before trying to access its data
        if (item != null)
        {
            //If the given item's quantity is greater than zero AND the found item's quantity is greater than the given item's quantity, mark the item as existing in the inventory
            if (newItem.GetQuantity() > 0 && item.GetQuantity() >= newItem.GetQuantity())
            {
                containsItem = true;
            }
        }

        //Return the state of the desired item existing or not
        return containsItem;
    }
}
