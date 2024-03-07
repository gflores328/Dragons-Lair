
//JML - Helps to change cursor in a scene. Add to empty game object called cusor and put cusor image in the cursor of inspector.

using UnityEngine;

public class setCursor : MonoBehaviour
{
    // You must set the cursor in the inspector.
    public Texture2D CustomCursor;
    public Texture2D ClickCursor;

    void Start()
    {

        //set the cursor origin to its centre. (default is upper left corner)
        Vector2 cursorOffset = new Vector2(CustomCursor.width / 2, CustomCursor.height / 2);

        //Sets the cursor to the Crosshair sprite with given offset 
        //and automatic switching to hardware default if necessary
        Cursor.SetCursor(CustomCursor, cursorOffset, CursorMode.Auto);
    }

    void OnMouseOver()
    {
        Cursor.SetCursor(ClickCursor, Vector2.zero, CursorMode.Auto);
    }
}