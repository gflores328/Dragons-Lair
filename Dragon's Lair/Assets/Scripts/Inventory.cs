/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 4, 2024 at 4:27 PM
 * 
 * Manages a list of items by
 * - Adding items
 * - Removing items
 * - Retrieving a list of items
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    //A list of items in the inventory
    private List<Item> inventory;

    //GABE ADDED
    public GameObject inventoryUI;
    private int slotNumber;
    
    //Start is called before the first frame update
    //Initializes the inventory list as an empty list of strings
    void Start()
    {
        inventory = new List<Item>();

        //GABE ADDED
        slotNumber = 0;
    }

    //Returns an item with a name matching the given item name. Accepts a string for the item name.
    private Item FindItem(string itemName)
    {
        //Search through the inventory
        foreach (Item i in inventory)
        {
            //Compare the current item's name to the given name
            if (i.getName().Equals(itemName))
            {
                //If the item name matches, return the reference to the item
                return i;
            }
        }

        //If no item with a matching name was found return a null reference
        return null;
    }

    //Adds one or more of the given item based on the item name
    public void AddItem(string itemName, int quantity)
    {
        //The quantity of items being added must be a positive integer
        if (quantity > 0)
        {
            //Check to see if the item to be added already exists in the inventory
            Item item = FindItem(itemName);

            //If the item isn't already in the inventory create a new entry for the item
            if (item == null)
            {
                inventory.Add(new Item(itemName, quantity));
            }
            //If the item does aready exist simply increase the quantity
            else
            {
                item.increaseQuantity(quantity);
            }
        }
    }

    //Adds one of the given item represented by a string
    public void AddItem(string itemName)
    {
        //Call the other AddItem function, specifing that only one item is to be added
        AddItem(itemName, 1);
    }

    //Removes a specific amount or all of a given item
    private bool RemoveItem(string itemName, int quantity, bool removeAll)
    {
        //A variable to keep track of whether the item has been removed or not
        //This is returned at the end of the function
        bool wasItemRemoved = false;

        //Check to see if the inventory has the item that is going to be removed
        Item item = FindItem(itemName);

        if (item == null)
        {
            wasItemRemoved = false;
        }
        else
        {
            //Calculate the new quantity of the item
            int newQuantity = item.getQuantity() - quantity;

            //If the new quantity is less than one or if removeAll is true remove the item from the inventory completely
            if (removeAll || newQuantity < 1)
            {
                inventory.Remove(item);
            }
            //If the new quantity results in a postive integer simply decrease the quantity of the item
            else
            {
                item.decreaseQuantity(quantity);
            }

            //Mark that the item was removed
            wasItemRemoved = true;
        }

        //Report whether the item was successfully removed or not
        return wasItemRemoved;
    }

    //Removes a specific amount of a given item
    public bool RemoveItem(string itemName, int quantity)
    {
        return RemoveItem(itemName, quantity, false);
    }

    //Removes all of a given item
    public bool RemoveItem(string itemName, bool removeAll)
    {
        return RemoveItem(itemName, 0, removeAll);
    }

    //Removes one of a given item
    public bool RemoveItem(string itemName)
    {
        return RemoveItem(itemName, 1, false);
    }

    //Returns the inventory as an array of strings
    public string[] GetInventory()
    {
        //Create a list of strings to store the item information
        List<string> items = new List<string>();

        //Convert each item into a string containing the name and the quantity  e.g. "Paper x3" or "Coins x56"
        foreach (Item i in inventory)
        {
            items.Add( i.getName() + " x" + i.getQuantity() );
        }

        //Return the list of strings as an array
        return items.ToArray();
    }

    //Returns if there is at least a specified quantity of an item in an inventory
    //e.g. a quantity of 3 would return true if the item's quantity is 3 or above and return false for 2 and below
    public bool Contains(string itemName, int quantity)
    {
        //Tracks whether the desired item exists in the inventory
        bool containsItem = false;
        //A reference to the desired item
        Item item = FindItem(itemName);

        //First, check to make sure the item exists before trying to access its data
        if (item != null)
        {
            //If the given quantity is greater than zero and the found item's quantity is greater than the given quantity marks the level as contained
            if (quantity > 0 && item.getQuantity() >= quantity)
            {
                containsItem = true;
            }
        }

        //Return the state of the desired item existing or not
        return containsItem;
    }

    //Returns if there is at least one of an item in the inventory
    public bool Contains(string itemName)
    {
        return Contains(itemName, 1);
    }

    // GABE ADDED
    public void AddItem(Item item)
    {
        foreach (Item i in inventory)
        {
            if (item.name == i.name)
            {
                i.increaseQuantity(item.getQuantity());
                inventoryUI.transform.GetChild(item.GetSlotNumber()).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.getName() + "X" + item.getQuantity();
                return;
            }
        }

        Debug.Log(item + " added to inventory");
        inventory.Add(item);

        inventoryUI.transform.GetChild(slotNumber).GetComponent<Image>().sprite = item.image;
        inventoryUI.transform.GetChild(slotNumber).GetChild(0).GetComponent<TextMeshProUGUI>().text = item.getName() + "X" + item.getQuantity();
        item.SetSlotNumber(slotNumber);

        slotNumber++;
    }
}
