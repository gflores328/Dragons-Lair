/*
 * Created by: Carlos Martinez
 *
 * This script contains the movement of the claw in the crane game.
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ClawMovement : MonoBehaviour
{
    public bool clawsOpen;
    bool goUp, goDown, goLeft, goRight;
    Rigidbody2D lHook, rHook;

    // Define a delay time before going up
    public float delayBeforeGoingUp = 1f;
    
    // Define a delay time before the claw opens up
    public float delayBeforeClawOpens = 1f;

    // Define boundary coordinates
    float minX, maxX, minY, maxY;

    public bool allowControls = true;
    public bool touchingPrize = false;

    private GameObject gameState;
    
    private GameManager gameManager;
    public Item prize;
    void Start()
    {
        rHook = GameObject.Find("Right_Hook").GetComponent<Rigidbody2D>();
        lHook = GameObject.Find("Left_Hook").GetComponent<Rigidbody2D>();
        gameManager =  GetComponent<GameManager>();
        clawsOpen = true;
        
        // Get boundary coordinates
        minX = -2f;
        maxX = 2.5f;
        minY = -2.5f;
        maxY = 2f;

        gameState = GameObject.Find("GameState");
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            gameManager.PauseGame();
        }
        // Holding 'Space' = Descend
        if (Input.GetKeyDown(KeyCode.Space) && allowControls)
        {
            goDown = true;
            goUp = false;
            allowControls = false;
        }

        if (goDown && !touchingPrize)
        {
            gameObject.transform.Translate(new Vector3(0, -1f, 0) * Time.deltaTime * 2);
        }

        if (touchingPrize || transform.position.y <= minY)
        {
            goDown = false;
            clawsOpen = false;
            touchingPrize = false;

            gameObject.GetComponent<BoxCollider2D>().enabled = true;

            StartCoroutine(WaitBeforeGoingUp());
        }

        // Check if the claw reaches the bottom of the machine
        if (transform.position.y <= minY)
        {
            goDown = false;
            clawsOpen = false;
            StartCoroutine(WaitBeforeGoingUp());
        }

        // [<-] and [A] = Move Left
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && allowControls) // Hold
        {
            gameObject.transform.Translate(new Vector3(-1f, 0, 0) * Time.deltaTime * 5);
        }

        // [->] and [D] = Move Right
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) & allowControls) // Hold
        {
            gameObject.transform.Translate(new Vector3(1f, 0, 0) * Time.deltaTime * 5);
        }

        // Claw Movement
        if (goUp && transform.position.y < maxY) // Up
        {
            gameObject.transform.Translate(new Vector3(0, 1f, 0) * Time.deltaTime * 2);
        }
     
        // When the claws open, hooks rotate outward
        if (clawsOpen)
        {
            if (rHook.transform.eulerAngles.z < 61) // < 61
            {
                rHook.transform.Rotate(new Vector3(0, 0, 1f) * Time.deltaTime * 20);
            }

            if (lHook.transform.eulerAngles.z > 299) // > 299
            {
                lHook.transform.Rotate(new Vector3(0, 0, -1f) * Time.deltaTime * 20);
            }
        }

        // When the claws close, hooks rotate inward
        if (!clawsOpen)
        {
            if (rHook.transform.eulerAngles.z > 5) // > 5
            {
                rHook.transform.Rotate(new Vector3(0, 0, -1f) * Time.deltaTime* 20);
            }

            if (lHook.transform.eulerAngles.z < 355) // < 355
            {
                lHook.transform.Rotate(new Vector3(0, 0, 1f) * Time.deltaTime * 20);
            }
        }

        // Clamp position within boundaries
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minX, maxX),
            Mathf.Clamp(transform.position.y, minY, maxY),
            transform.position.z
        );
        
        
    }

    // Delay before the claw moves up
    IEnumerator WaitBeforeGoingUp()
    {
        yield return new WaitForSeconds(delayBeforeGoingUp);
        goUp = true;
        StartCoroutine(WaitBeforeClawOpens());
    }

    // Delay before the claw opens
    IEnumerator WaitBeforeClawOpens()
    {
        yield return new WaitForSeconds(delayBeforeClawOpens);
        // Make the claws Open
        clawsOpen = true;
        CheckForPrize();
        allowControls = true;


        ClawTouch[] claws = GetComponentsInChildren<ClawTouch>();

        foreach (ClawTouch i in claws)
        {
            i.canTouch = true;
        }
    }

    // Checks for each object with Prize that is a child of the game object
    private void CheckForPrize()
    {

        Prize[] prizes = GetComponentsInChildren<Prize>();

        foreach (Prize i in prizes)
        {
            clawsOpen = true;
            // If the prize is a screw driver then the game is won
            if (i.gameObject.tag == "Screwdriver")
            {
                // WIn screen
                // go back to real life
                Debug.Log("You win");
                Time.timeScale = 0;

                if (gameState != null)
                {
                    if (gameState.GetComponent<GameState>().storyState < GameState.state.GiveScrewdriver)
                    {
                        gameState.GetComponent<GameState>().storyState = GameState.state.GiveScrewdriver;
                        gameState.GetComponent<GameState>().objective = "Give the screwdriver to Michael";
                        GameObject.Find("Inventory").GetComponent<Inventory>().AddItem(prize);
                    }
                }
                GetComponent<AsyncLoader>().runAsync();
            }
            Destroy(i.gameObject);
        }
        gameObject.GetComponent<BoxCollider2D>().enabled = false;
    }

    /*
    // If the triggger touches a prize then a bool is set to true
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent<Prize>(out Prize prize))
        {
            touchingPrize = true;
        }
    }
    */

}
