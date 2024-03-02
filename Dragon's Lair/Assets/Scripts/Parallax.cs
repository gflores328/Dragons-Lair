using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public GameObject mainCamera; // The variable that holds the main camera game object

    public float parallaxEffect; // how much of the parallax effect we are gonna apply

    private float length, startPos; //The length and start position of the image

   
    
    void Start()
    {
        startPos = transform.position.x; // set the start postion to the position that it starts at
        length = GetComponent<SpriteRenderer>().bounds.size.x; // Get the length of the image
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (mainCamera.transform.position.x * ( 1 - parallaxEffect)); // the temp variable that holds the posiiton minus one to check wiwth later
        float distance = (mainCamera.transform.position.x * parallaxEffect); // The actual distance that we should move the image

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z); // Actually moves the image
        if(temp > startPos + length) // if temp is longer than the start position make the new start position be at the new position instead

        {
            startPos += length;


        }

        else if (temp < startPos - length) // if the temp is lower than the start pos minus lnegth
        
        {
            startPos-= length; // set the start position to be minus the length of the image
        }
    }
}
