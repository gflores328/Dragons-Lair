using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineofSight : MonoBehaviour
{
    public float TargetDistance; // Distace player is before enemy starts action ie walking or attacking

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit; //sends out a raycast from orgin in desired direction until it hits something

        if (Physics.Raycast (transform.position,transform.TransformDirection (Vector3.forward), out hit)) //raycast continues in the direction forward until it hits something
        {
            TargetDistance = hit.distance; //determines how far the ray can go.
        }
    }
}
