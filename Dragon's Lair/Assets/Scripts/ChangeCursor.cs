
//JML - Helps to change cursor in a scene. Add to empty game object called cusor and put cusor image in the cursor of inspector.

using System;
using UnityEngine;

public class setCursor : MonoBehaviour
{
    // You must set the cursor in the inspector.
    public Sprite CustomCursor;
    public Sprite ClickCursor;


    public void Start()
    {

        Vector2 center = default;
        SetCursor(CustomCursor, center);
    }

    void Update()
    {
        

    }

    public void SetCursor(Sprite sprite, Vector2 center)
    {
        Cursor.SetCursor(CustomCursor.texture, center, CursorMode.Auto);
    }


    public void OnMouseOver()
    {
        //switches to alternate click cursor when mouse is clicked
        Cursor.SetCursor(ClickCursor.texture, Vector2.zero, CursorMode.Auto);
    }
}