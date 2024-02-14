/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 13, 2024 at 11:45 PM
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
    private static List<Item> inventory;

    //GABE ADDED
    public InventoryUI inventoryUI;
    
    //Start is called before the first frame update
    //Initializes the inventory list as an empty list of strings
    void Start()
    {
        inventory = new List<Item>();
    }

    //Adds one or more of the given item based on the item name
    public void AddItem(Item newItem)
    {
        //The quantity of item being added must be a positive integer
        if (newItem.GetQuantity() > 0)
        {
            //Check to see if the item to be added already exists in the inventory
            Item item = FindItem(newItem.GetName());

            //If the item isn't already in the inventory accept the given item
            if (item == null)
            {
                inventory.Add(newItem);
            }
            //If the item does aready exist simply increase the quantity of the existing item
            else
            {
                item.IncreaseQuantity(newItem.GetQuantity());
            }

            //Update the Inventory UI
            inventoryUI.UpdateUI(GetInventory());
        }
    }

    //Removes a specific amount or all of a given item
    private bool RemoveItem(Item newItem, bool removeAll)
    {
        //A variable to keep track of whether the item has been removed or not
        //This is returned at the end of the function
        bool wasItemRemoved = false;

        //Check to see if the inventory has the item that is going to be removed
        Item item = FindItem(newItem.GetName());

        //If the item doesn't already exist in the inventory, do nothing
        if (item == null)
        {
            wasItemRemoved = false;
        }
        //If the item does exist...
        else
        {
            //Calculate the new quantity of the item
            int newQuantity = item.GetQuantity() - newItem.GetQuantity();

            //If the new quantity is less than one or if removeAll is true remove the item from the inventory completely
            if (removeAll || newQuantity < 1)
            {
                inventory.Remove(item);
            }
            //If the new quantity results in a postive integer simply decrease the quantity of the item
            else
            {
                item.DecreaseQuantity(newItem.GetQuantity());
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
