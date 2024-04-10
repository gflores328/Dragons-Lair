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
using UnityEngine.EventSystems;

public class ItemDescription : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Item item;
    public TextMeshProUGUI descriptionBox;

    public void OnPointerEnter(PointerEventData pointerEventData)
    {
        descriptionBox.text = item.GetDescription();
        Debug.Log("Mouse in image");
    }

    public void OnPointerExit(PointerEventData pointerEventData)
    {
        descriptionBox.text = null;
    }

    public void SetItem(Item takenItem)
    {
        item = takenItem;
    }
}
