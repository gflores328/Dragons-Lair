/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 13, 2024 at 11:26 PM
 * 
 * A container for storing and retrieving information about an item including:
 * - The item's name
 * - The item's description
 * - The quantity of the item
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Item : ScriptableObject
{
    [SerializeField]
    private string itemName;    //The name of the item
    [SerializeField]
    private string description; //The description of the item
    
    public Sprite image;       //The sprite image of the item
    private int slotNumber; // The slot number the item takes in the inventory UI

    //Sets the name of the item. Item name is given as a string.
    //This function only needs to be used by the Item class itself. The item name shouldn't have to be changed once created.
    public void SetName(string itemName)
    {
        this.itemName = itemName;
    }

    //Returns the name of the item as a string
    public string GetName()
    {
        return itemName;
    }

    //Sets the description of the item.
    public void SetDescription(string description)
    {
        this.description = description;
    }

    //Returns the description of the item as a string
    public string GetDescription()
    {
        return description;
    }

    //GABE ADDED

    public void SetSlotNumber(int number)
    {
        slotNumber = number;
    }

    public int GetSlotNumber()
    {
        return slotNumber;
    }
}
