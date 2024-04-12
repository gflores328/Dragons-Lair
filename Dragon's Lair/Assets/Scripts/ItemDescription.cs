/*
 * Created by: Carlos Martinez and Gabriel Flores
 *
 * Displays Item Description in the Inventory Menu When the Cursor is Pointing at an Item. 
 */
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public TextMeshProUGUI descriptionBox;

    // Displays Text from the Item's Description when the Mouse Cursor Points at it
    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        descriptionBox.text = item.GetDescription();
        Debug.Log("Mouse in image");
    }

    // Clears Text when the Mouse Cursor No Longer Points at the Item
    public void OnPointerExit(PointerEventData pointerEventData)
    {
        descriptionBox.text = null;
    }

    // Sets an Item
    public void SetItem(Item takenItem)
    {
        item = takenItem;
    }
}
