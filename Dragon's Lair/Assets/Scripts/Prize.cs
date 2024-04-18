using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Prize : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("In trigger");
        transform.parent = collision.gameObject.transform;
        GetComponent<Rigidbody2D>().gravityScale = 0;
    }

    private void OnTriggerStay(Collider other)
    {
        Debug.Log(other + ": staying");
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        transform.parent = null;
        GetComponent<Rigidbody2D>().gravityScale = 1;
    }
}

