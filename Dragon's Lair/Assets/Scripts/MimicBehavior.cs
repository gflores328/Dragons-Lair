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
    public bool start = true; // A bool to determine wheter or not the object should be doing stuff
    private bool takingAction = false; // A bool to check if the object is currently in an action
    private int actionNumber = 1; // An int that represents the action the enemy should take 

    public GameObject player; // A reference to the player character

    [Header("Speeds")]
    public float movementSpeed; // The speed that the enemy moves
    public float jumpSpeed; // The speed of the enemy jump
    public float slamSpeed; // The speed of the enemy slam

    [Header("Borders")]
    [Tooltip("The game objects that represent the borders that constrain this game object")]
    public GameObject leftBorder;
    public GameObject rightBorder;

    [Header("Debris")]
    public GameObject fallingDebris;
    public GameObject fallingDebrisHeight;
    public GameObject splashDebris;

    
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(actionNumber);

        if (start)
        {
            if (actionNumber > 2)
            {
                actionNumber = 1;
            }

            switch (actionNumber)
            {
                case 1:
                    if (!takingAction)
                    {
                        Debug.Log("Starting jump on player");
                        StartCoroutine(JumpAbovePlayer());
                    }
                    break;

                case 2:
                    if (!takingAction)
                    {
                        Debug.Log("Starting move to corner");
                        StartCoroutine(MoveToCorner());
                    }
                    break;

            }
        }
    }

    // This function when called will make this game object jump above the player and slam down on that spot
    IEnumerator JumpAbovePlayer()
    {
        // Taking action is set to true
        takingAction = true;

        yield return StartCoroutine(Jump(player.transform.position.x));

        FallingDebris();

        // Waits for 1 second
        yield return new WaitForSeconds(1f);

        // takingAction is set to false
        actionNumber++;
        takingAction = false;
    }

    // This function will move the object to whichever border it is closest to
    IEnumerator MoveToCorner()
    {
        takingAction = true;
        bool goLeft;

        // If the object is farther from the left border than goLeft is set to true and if not it is false 
        if(Vector3.Distance(transform.position, leftBorder.transform.position) >= Vector3.Distance(transform.position, rightBorder.transform.position))
        {
            goLeft = true;
        }
        else
        {
            goLeft = false;
        }
        
        // While the objects position is not at either borders position
        while ((transform.position.x != leftBorder.transform.position.x) && (transform.position.x != rightBorder.transform.position.x))
        {
            
            // If go left is true then the object moves towards the left border
            if (goLeft)
            {
                Vector3 targetPosition = new Vector3(leftBorder.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
                yield return null;
            }
            // else it moves towards the right border
            else
            {
                Vector3 targetPosition = new Vector3(rightBorder.transform.position.x, transform.position.y, transform.position.z);
                transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
                yield return null;
            }
        }

        yield return new WaitForSeconds(1);
        actionNumber++;
        takingAction = false;
       
    }

    // This function contains the action for the object to jump
    // It takes a float and the value will be the x position that the object will jump to
    IEnumerator Jump (float xTarget)
    {
        float yTarget = transform.position.y + 5;

        // A vector 3 is created with the x and y targer
        Vector3 targetPosition = new Vector3(xTarget, yTarget, transform.position.z);


        // Object Jumps
        // While the distance from the objcts current position and the targetPosition are greater than 0.01
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // The object moves towards the target position at the speed of the jumpSpeed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, jumpSpeed * Time.deltaTime);

            // Wait for next frame
            yield return null;
        }

        // Waits for .5 seconds before running the code to slam down
        yield return new WaitForSeconds(.5f);

        // A new target position is set
        yTarget = transform.position.y - 5;
        targetPosition = new Vector3(xTarget, yTarget, transform.position.z);

        // Object Slams
        // While the distance from the objcts current position and the targetPosition are greater than 0.01
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // The object moves towards the target position at the speed of the slamSpeed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, slamSpeed * Time.deltaTime);

            // Waits for next frame
            yield return null;
        }

    }

    // This function when run will check for whichever border it is closest to and will jump to that position
    IEnumerator JumpToCorner()
    {
        takingAction = true;

        if (Vector3.Distance(transform.position, leftBorder.transform.position) >= Vector3.Distance(transform.position, rightBorder.transform.position))
        {
            yield return StartCoroutine(Jump(leftBorder.transform.position.x));
        }
        else
        {
            yield return StartCoroutine(Jump(rightBorder.transform.position.x));
        }

        yield return new WaitForSeconds(1);
        takingAction = false;
    }

    private void FallingDebris()
    {

        for(int i = 0; i <= 4; i++)
        {
            float xSpawn = Random.Range(leftBorder.transform.position.x, rightBorder.transform.position.x);
            GameObject clone = Instantiate(fallingDebris, new Vector3(xSpawn, fallingDebrisHeight.transform.position.y, gameObject.transform.position.z), Quaternion.identity);
        }

    }

    private void SplashDebris()
    {

    }
}
