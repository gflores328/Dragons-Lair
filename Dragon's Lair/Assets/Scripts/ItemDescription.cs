/*
 * Created by: Carlos Martinez
 *
 * Displays Item Description in the Inventory Menu When the Cursor is Pointing at an Item. 
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemDescription : MonoBehaviour
{
    public Item item;
    public TextMeshProUGUI descriptionBox;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        descriptionBox.text = item.GetDescription();
    }

    private void OnMouseExit()
    {
        descriptionBox.text = null;
    }

    public void SetItem(Item takenItem)
    {
        item = takenItem;
    }
}
