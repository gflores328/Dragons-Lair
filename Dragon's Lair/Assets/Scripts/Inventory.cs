/*
 * CREATED BY: Trevor Minarik
 * 
 * Manages a list of strings representing items
 */
 
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    //A list of strings representing items
    public List<string> inventory;
    
    //Start is called before the first frame update
    //Initializes the inventory list as an empty list of strings
    void Start()
    {
        inventory = new List<string>();
    }

    //Adds one or more of the given item represented by a string
    public void AddItem(string item, int quantity)
    {
        //The quantity of items being added must be a positive integer
        if (quantity > 0)
        {
            for (int i = 0; i < quantity; i++)
            {
                inventory.Add(item);
            }
        }
    }

    //Adds one of the given item represented by a string
    public void AddItem(string item)
    {
        //Call the other AddItem function, specifing that only one item is to be added
        AddItem(item, 1);
    }

    //Removes one of the given item represented by a string
    public bool RemoveItem(string item)
    {
        //A variable to keep track of whether the item has been removed or not
        //This is returned at the end of the function
        bool itemRemoved = false;

        //Check to see if the inventory has the item that is going to be removed
        if (inventory.Contains(item))
        {
            inventory.Remove(item);
            //Mark that the item was removed
            itemRemoved = true;
        }

        //Report whether the item was successfully removed or not
        return itemRemoved;
    }

    //Returns the inventory as an array of strings
    public string[] GetInventory()
    {
        string[] items = inventory.ToArray();
        return items;
    }
}
