/*
 * Created By: Gabriel Flores
 * 
 * This script will hold all of the game objects that wont be destroyed on load
 * This includes itself
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public List<GameObject> dontDestroy; // List of objects to not destroy

    // Start is called before the first frame update
    void Start()
    {
        // For each game object in the list dont destroy
        foreach(GameObject i in dontDestroy)
        {
            DontDestroyOnLoad(i);
        }
        // Dont destroy this game object
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
