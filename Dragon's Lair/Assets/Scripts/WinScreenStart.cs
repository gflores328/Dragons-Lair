using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinScreenStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        //Cursor.visible = true;
        GameObject.Find("GameState").GetComponent<GameState>().objective = "Free Play";
        GameObject.Find("GameState").GetComponent<GameState>().storyState = GameState.state.end;
    }
}
