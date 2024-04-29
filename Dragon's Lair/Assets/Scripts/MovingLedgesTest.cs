using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class MovingLedgesTest : MonoBehaviour
{
    //initialize variable for speed and waypoints
    [SerializeField] private float speed = 5f;
    [SerializeField] private GameObject[] waypoints;

    ChibiPlayerMovement controller;

    private int currentWaypointIndex;

    public bool waitForPlayer = false;

    private bool playerHasArrived = false;

    private Vector3 velocity;
    private bool moving = true;


    // Start is called before the first frame update
    void Start()
    {
        if (waypoints.Length <= 0) return;
        {
            currentWaypointIndex = 0;
            controller = GetComponent<ChibiPlayerMovement>();
        }
    }

    private void FixedUpdate()
    {

        //if the platform reaches waypoint A it will start moving towards waypoint b
        // and vice versa to set the movement path
        if (!waitForPlayer)
        {
            HandleMovement();

        }

        if(waitForPlayer)
        {
            if(playerHasArrived)
            {
                HandleMovement();
                
            }
        }

    }

    private void HandleMovement()
    {

        transform.position = Vector3.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position,
            (speed * Time.deltaTime));

        if (Vector3.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) <= 0)
        {
            currentWaypointIndex++;
        }

        if (currentWaypointIndex != waypoints.Length) return;
        waypoints.Reverse();
        currentWaypointIndex = 0;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.transform.parent = transform;
        playerHasArrived = true;

    }

    private void OnTriggerStay(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.transform.parent = transform;
        playerHasArrived = true;

    }
    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        other.transform.parent = null;
        playerHasArrived = false;
    }

  
}
