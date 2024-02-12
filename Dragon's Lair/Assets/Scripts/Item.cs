/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 4, 2024 at 6:40 pm
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
    private string description; //The description of the item   Currently, this value isn't used
    [SerializeField]
    private int quantity;       //The quantity of the ite
    
    public Sprite image;       //The sprite image of the item
    private int slotNumber; // The slot number the item takes in the inventory UI

    //Stores the descriptions for each item
    //This will have to be updated manually with each item that is added
    private static readonly Dictionary<string, string> descriptions = new Dictionary<string, string>()
    {
        //Example item/description pair
        //Item names should be in lowercase
        { "banana", "long yellow things" },
    };

    //Constructor for an item that accepts an item name as a string and an item quantity as an int
    public Item(string itemName, int quantity)
    {
        setName(itemName);
        setDescription(itemName);
        increaseQuantity(quantity);
    }

    //Constructor for an item that accepts an item name as a string and assumes a quantity of one
    public Item(string itemName)
    {
        setName(itemName);
        setDescription(itemName);
        increaseQuantity(1);
    }

    //Sets the name of the item. Item name is given as a string.
    //This function only needs to be used by the Item class itself. The item name shouldn've have to be changed once created.
    private void setName(string itemName)
    {
        this.itemName = itemName;
    }

    //Sets the description of the item based on the given item name
    private void setDescription(string itemName)
    {
        string description; //Stores the description
        itemName = itemName.ToLower();  //Convert the item name to lowercase to match the keys in the descriptions dictionary

        //If the item has a matching description, add the description to the item
        if (descriptions.TryGetValue(itemName, out description))
        {
            this.description = description;
        }
        //If the item doesn't have a matching descripion, give it a default value
        else
        {
            this.description = "[No description provided]";
        }
    }

    //Returns the name of the item as a string
    public string getName()
    {
        return itemName;
    }

    //Increases the quantity of the item by a specified amount
    //The provided amount must be greater than 0 (it wouldn't make sense to add 0 additional items or add a negative amount of items).
    public void increaseQuantity(int amount)
    {
        if (amount > 0)
        {
            quantity += amount;
        }
    }

    //Increases the quantity of the item by one
    public void increaseQuantity()
    {
        increaseQuantity(1);
    }

    //Decrease the quantity of the item by the specified amount (represented by a POSITIVE integer)
    //If the item count drops below zero, set it back to zero
    public void decreaseQuantity(int amount)
    {
        if (amount > 0)
        {
            quantity -= amount;

            if (quantity > 0)
            {
                quantity = 0;
            }
        }
    }

    //Decrease the quantity of the item by one
    public void decreaseQuantity()
    {
        decreaseQuantity(1);
    }

    //Return the quantity of the item as an int
    public int getQuantity()
    {
        return quantity;
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
