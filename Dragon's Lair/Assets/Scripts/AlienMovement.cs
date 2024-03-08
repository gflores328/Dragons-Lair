/*
 * Created by Carlos Martinez
 * 
 * This script contains movement for the aliens in Mobile Fighter Axiom.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlienMovement : MonoBehaviour
{
    public float moveSpeed = 5; // Movement Speed

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * moveSpeed * Time.deltaTime); // Movement
    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Boundary") // If Alien Hits a Boundary
        {
            // Alien Moves in Opposite Direction
            transform.position = new Vector3(transform.position.x, transform.position.y - 2, transform.position.z);
            moveSpeed *= -1;
        }
    }
}
