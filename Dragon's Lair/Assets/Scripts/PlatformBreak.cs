/*
 *  Created By: Gabriel Flores
 *  
 *  This script will cause a game object to break when an enemy collider touches it
 */



using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformBreak : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(WaitAndDestroy());
    }


    IEnumerator WaitAndDestroy()
    {
        yield return new WaitForSeconds(8.5f);
        Destroy(gameObject);
    }
}
