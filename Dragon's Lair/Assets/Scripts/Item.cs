/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 4, 2024
 * 
 * A container for storing and retrieving information about an item including:
 * - The item's name
 * - The item's description
 * - The quantity of the item
 */

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    private string itemName;    //The name of the item
    private string description; //The description of the item   Currently, this value isn't used
    private int quantity;       //The quantity of the item

    //Constructor for an item that accepts an item name as a string and an item quantity as an int
    public Item(string itemName, int quantity)
    {
        setName(itemName);
        increaseQuantity(quantity);
    }

    //Constructor for an item that accepts an item name as a string and assumes a quantity of one
    public Item(string itemName)
    {
        setName(itemName);
        increaseQuantity(quantity);
    }

    //Sets the name of the item. Item name is given as a string.
    //This function only needs to be used by the Item class itself. The item name shouldn've have to be changed once created.
    private void setName(string itemName)
    {
        this.itemName = itemName;
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
}
