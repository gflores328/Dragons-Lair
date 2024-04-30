using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicTounge : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ChibiPlayerMovement>().takeDamage(1);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.GetComponent<ChibiPlayerMovement>().takeDamage(1);
        }
    }
}
