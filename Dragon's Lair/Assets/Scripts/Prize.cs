using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("In trigger");
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("in Collision");
    }
}

