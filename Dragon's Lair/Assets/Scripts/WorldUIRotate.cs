/*
 * Created By: Gabriel Flores
 * 
 * This is a script that will rotate the object to always be facing towards the camera
 * It will take a world space UI image and will make that object rotate towards the camera
 * 
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldUIRotate : MonoBehaviour
{
    public Camera cameraReference; // camera that the object will look towards
    public Transform objectToRotate; // The object to rotate owards the camera

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // Sets the objects rotation value to the direction of the camera
        objectToRotate.rotation = Quaternion.Slerp(objectToRotate.rotation, cameraReference.transform.rotation, 3f * Time.deltaTime);
    }
}
