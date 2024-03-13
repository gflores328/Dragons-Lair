/*
 * Created by Carlos Martinez
 * 
 * This script contains a despawner for Mobile Fighter Axiom.
 * Enemies disappear when making contact with the despawner
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Cinemachine.DocumentationSortingAttribute;
using UnityEngine.SceneManagement;

public class Despawner : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Enemy") // If Despawner Boundary Makes Contact with the Enemy
        {
            Destroy(collision.transform.parent.gameObject); // Enemy Disappears
        }
    }
}
