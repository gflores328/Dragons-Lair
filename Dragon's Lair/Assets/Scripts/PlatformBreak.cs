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
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }
}
