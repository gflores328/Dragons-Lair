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
   // public bool inactiveAfter; // A bool to set the object inactive after it is set to dont destroy
    public GameObject[] dontDestroy;

    // Start is called before the first frame update
    void Start()
    {
        GameObject duplicate = GameObject.Find(gameObject.name);

        if (duplicate != gameObject)
        {
            foreach(GameObject i in dontDestroy)
            {
                Destroy(i);
            }
            Destroy(gameObject);
        }
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
