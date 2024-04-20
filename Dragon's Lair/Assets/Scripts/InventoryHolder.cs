/*
 * Created by: Gabriel Flores
 * 
 * This script will copy the inventory object on start so that its functions can be called by the inspector
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryHolder : MonoBehaviour
{
    private GameObject inventory; // Referenct to the inventory
    public Item coin; // A reference to a coin

    // Start is called before the first frame update
    void Start()
    {
        inventory = GameObject.Find("Inventory");
    }

    // This function will be called when a game is started so a coin can be removed
    public void GameStart()
    {
        inventory.GetComponent<Inventory>().RemoveItem(coin, false);
    }
}
