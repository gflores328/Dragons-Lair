/*
 * Created By: Gabriel Flores
 * 
 * This script will hold all of the behaviors and variables for the Mimic enemy
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicBehavior : Enemy
{
    public bool start = true;
    private bool takingAction = false;

    public GameObject player;
    public float movementSpeed;
    public float jumpSpeed;
    public float slamSpeed;

    [Header("Borders")]
    [Tooltip("The game objects that represent the borders that constrain this game object")]
    public GameObject leftBorder;
    public GameObject rightBorder;

    

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if(start && !takingAction)
        {
            StartCoroutine(JumpAbovePlayer());
            
        }
    }

    // This function will take a vector3 variable
    // When this function is called the game object will jump up and land on the vector 3 spot
    IEnumerator Jump(Vector3 landingSpot)
    {

        takingAction = true;

        

        yield return new WaitForSeconds(1f);
        takingAction = false;
    }

    // This function when called will make this game object jump
    IEnumerator JumpAbovePlayer()
    {
        takingAction = true;
        float yTarget = transform.position.y + 10;

        while (transform.position.y < yTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, new Vector3(player.transform.position.x, player.transform.position.y + 10f, transform.position.z), jumpSpeed * Time.deltaTime);
        }
        yield return new WaitForSeconds(.5f);

        yTarget = transform.position.y - 10;
        while (transform.position.y > yTarget)
        {
            transform.position += Vector3.down * slamSpeed * Time.deltaTime;
        }

        yield return new WaitForSeconds(1f);

        takingAction = false;
    }


}
