/*
 * Created By: Gabriel Flores
 * 
 * This script will hold the behavior for when the bullet prefab collides with another object
 * 
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBulletHit : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    { 
        // When this object hits anything it is destroyed
            Destroy(gameObject);   
    }
}
