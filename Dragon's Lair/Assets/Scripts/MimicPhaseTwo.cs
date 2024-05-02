/*
 * Crated By: Gabriel Flores
 * 
 * This script will hold the functions and behavior for the second boss
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public enum Direction { right, left };

public class MimicPhaseTwo : Enemy
{
    
    public bool start = false; // A bool to determine wheter or not the object should be doing stuff
    private bool takingAction = false; // A bool to check if the object is currently in an action
    private int actionNumber = 1; // An int that represents the action the enemy should take 
    private bool startLunge = false;


    public GameObject player; // A reference to the player character
    public GameObject healthBar;

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
    public GameObject splashDebrisSpawn;
    
    private Direction directionFacing = Direction.left;

    public GameObject exit;

    [Header("Cameras")]
    public GameObject mainCamera;
    public GameObject bossCamera;
    // Start is called before the first frame update
    void Start()
    {
        healthBar.GetComponent<Slider>().maxValue = health;
        healthBar.GetComponent<Slider>().value = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (directionFacing == Direction.left)
        {
            transform.rotation = Quaternion.Euler(0f, 180f, 0f);
        }
        else if (directionFacing == Direction.right)
        {
            transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        }


        if (start && !takingAction)
        {
            switch (actionNumber)
            {
                case 1:
                    StartCoroutine(JumpAbovePlayer());
                    actionNumber++;
                    break;

                case 2:
                    if (directionFacing == Direction.right && !(transform.position.x + 10 > rightBorder.transform.position.x))
                    {
                        StartCoroutine(Lunge());
                    }
                    else if (directionFacing == Direction.left && !(transform.position.x - 10 < leftBorder.transform.position.x))
                    {
                        StartCoroutine(Lunge());
                    }
                    else
                    {
                        StartCoroutine(JumpAbovePlayer());
                    }
                    actionNumber++;
                    break;

                case 3:
                    StartCoroutine(JumpAbovePlayer());
                    actionNumber++;
                    break;

                case 4:
                    StartCoroutine(Meelee());
                    actionNumber++;
                    break;

                case 5:
                    StartCoroutine(JumpToCorner());
                    actionNumber++;
                    break;

                case 6:
                    StartCoroutine(Lunge());
                    actionNumber++;
                    break;

                case 7:

                    StartCoroutine(JumpAbovePlayer());
                    actionNumber++;
                    break;

                case 8:
                    StartCoroutine(Meelee());
                    actionNumber = 1;
                    break;
            }
        }

    }

    // This function when called will make this game object jump above the player and slam down on that spot
    IEnumerator JumpAbovePlayer()
    {
        // Taking action is set to true
        takingAction = true;

        if (player.transform.position.x > transform.position.x)
        {
            directionFacing = Direction.right;
        }
        else
        {
            directionFacing = Direction.left;
        }

        yield return StartCoroutine(Jump(player.transform.position.x));
        //FallingDebris();

        if (player.transform.position.x > transform.position.x)
        {
            directionFacing = Direction.right;
        }
        else
        {
            directionFacing = Direction.left;
        }

        // Waits for 1 second
        yield return new WaitForSeconds(1f);

        // takingAction is set to false
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
        GetComponentInChildren<Animator>().SetTrigger("jumpOnly");

        float yTarget = gameObject.transform.position.y + 5;

        // A vector 3 is created with the x and y targer
        Vector3 targetPosition = new Vector3(xTarget, yTarget, gameObject.transform.position.z);

        // Object Jumps
        // While the distance from the objcts current position and the targetPosition are greater than 0.01
        while (Vector3.Distance(gameObject.transform.position, targetPosition) > 0.01f)
        {
            // The object moves towards the target position at the speed of the jumpSpeed
            gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, targetPosition, jumpSpeed * Time.deltaTime);
            
            
            // Wait for next frame
            yield return null;

        }



        // Waits for .5 seconds before running the code to slam down
        yield return new WaitForSeconds(.5f);
        GetComponentInChildren<Animator>().SetTrigger("slamOnly");

        // A new target position is set
        yTarget = gameObject.transform.position.y - 5;
        targetPosition = new Vector3(xTarget, yTarget, transform.position.z);

        // Object Slams
        // While the distance from the objcts current position and the targetPosition are greater than 0.01
        while (Vector3.Distance(gameObject.transform.position, targetPosition) > 0.01f)
        {
            // The object moves towards the target position at the speed of the slamSpeed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, slamSpeed * Time.deltaTime);

            // Waits for next frame
            yield return null;
        }
        GetComponent<CinemachineImpulseSource>().GenerateImpulse(1f);
    }

    // This function when run will check for whichever border it is closest to and will jump to that position
    IEnumerator JumpToCorner()
    {
        takingAction = true;

        if (Vector3.Distance(transform.position, leftBorder.transform.position) >= Vector3.Distance(transform.position, rightBorder.transform.position))
        {
            directionFacing = Direction.left;
            yield return StartCoroutine(Jump(leftBorder.transform.position.x));
            directionFacing = Direction.right;
        }
        else
        {
            directionFacing = Direction.right;
            yield return StartCoroutine(Jump(rightBorder.transform.position.x));
            directionFacing = Direction.left;
        }

        SplashDebris();

        yield return new WaitForSeconds(1);
        takingAction = false;
    }

    IEnumerator Meelee()
    {
        takingAction = true;
        GetComponentInChildren<Animator>().SetTrigger("Attack");

        yield return new WaitForSeconds(3);
        takingAction = false;
    }

    IEnumerator Lunge()
    {
        takingAction = true;
        GetComponentInChildren<Animator>().SetTrigger("Lunge");
        float xTarget;
        Vector3 targetPosition;

        if (directionFacing == Direction.right)
        {
            xTarget = transform.position.x + 10;
            targetPosition = new Vector3(xTarget, transform.position.y, transform.position.z);
        }
        else
        {
            xTarget = transform.position.x - 10;
            targetPosition = new Vector3(xTarget, transform.position.y, transform.position.z);
        }
        

        //yield return new WaitUntil(() => startLunge);

        while (transform.position.x != xTarget)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }

        yield return new WaitForSeconds(2);


        startLunge = false;
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
        for (int i = 1; i <= 5; i++)
        {
            Vector3 spawn = new Vector3(splashDebrisSpawn.transform.position.x, splashDebrisSpawn.transform.position.y, transform.position.z);
            GameObject clone = Instantiate(splashDebris, spawn, Quaternion.identity);

            if (directionFacing == Direction.right)
            {
                clone.GetComponent<Rigidbody>().velocity = ((Vector3.right * (i * 3)) + (Vector3.up * 9));
            }
            else if (directionFacing == Direction.left)
            {
                clone.GetComponent<Rigidbody>().velocity = ((Vector3.left * (i * 3)) + (Vector3.up * 9));
            }
        }
    }

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
            other.GetComponent<ChibiPlayerMovement>().takeDamage(1);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<ChibiPlayerMovement>().takeDamage(1);
        }
    }

    public void StartLunge()
    {
        startLunge = true;
    }

    protected override void Die()
    {
        exit.SetActive(false);
        healthBar.SetActive(false);

        mainCamera.SetActive(true);
        bossCamera.SetActive(false);
        
        Destroy(gameObject);
    }

    public override void TakeDamage(float amnt)
    {
        base.TakeDamage(amnt);
        healthBar.GetComponent<Slider>().value = health;
    }

    IEnumerator StartDelay()
    {
        GetComponentInChildren<Animator>().SetTrigger("Roar");

        yield return new WaitForSeconds(3f);

        healthBar.SetActive(true);

        yield return new WaitForSeconds(1f);

        player.GetComponent<PlayerInput>().actions.Enable();
        start = true;

    }

    public void StartStartDelay()
    {
        StartCoroutine(StartDelay());
    }

    IEnumerator WalkToPlayer()
    {
        takingAction = true;
        // set animation

        Vector3 targetPosition = new Vector3(player.transform.position.x, transform.position.y, transform.position.z);

        if (player.transform.position.x > transform.position.x)
        {
            directionFacing = Direction.right;
        }
        else
        {
            directionFacing = Direction.left;
        }

        while (transform.position != targetPosition)
        {
            gameObject.transform.position = Vector3.MoveTowards(transform.position, targetPosition, movementSpeed * Time.deltaTime);
            yield return null;
        }

        takingAction = false;
        // end walking animation

    }

}

