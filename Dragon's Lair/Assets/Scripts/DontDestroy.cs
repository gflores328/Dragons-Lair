/*
 * Created By: Gabriel Flores
 * 
 * This script will check to see if it already exists in the current scene and if it does it destroys itself
 * If it doesnt then it is set to dont destroy on load
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
   
    public GameObject[] dontDestroy; // An array to hold other do not destry objects

    // Start is called before the first frame update
    void Start()
    {
        // Finds a game object with the same name as this game object
        GameObject duplicate = GameObject.Find(gameObject.name);

        // if duplicate is not this game object
        if (duplicate != gameObject)
        {
            // destroy everything in the dontDestroy array and this game object
            foreach(GameObject i in dontDestroy)
            {
                Destroy(i);
            }
            Destroy(gameObject);
        }
        // Else set all to dont destroy on load
        else
        {
            foreach (GameObject i in dontDestroy)
            {
                DontDestroyOnLoad(i);
            }
            DontDestroyOnLoad(gameObject);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
