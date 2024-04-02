using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MovingLedgesTest : MonoBehaviour
{
    //initialize variable for speed and waypoints
    [SerializeField] private int speed;
    [SerializeField] private GameObject[] waypoints;
    private int currentWaypointIndex;

    public bool waitForPlayer = false;

    private bool playerHasArrived = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Update()
    {
        //if the platform reaches waypoint A it will start moving towards waypoint b
        // and vice versa to set the movement path
        if(!waitForPlayer)
        {
            if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
            {
                currentWaypointIndex++;
                if(currentWaypointIndex >= waypoints.Length)
                {
                    currentWaypointIndex = 0;
                }
            }

            transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
        }
        if(waitForPlayer)
        {
            if(playerHasArrived)
            {
                if (Vector2.Distance(waypoints[currentWaypointIndex].transform.position, transform.position) < 0.1f)
                {
                    currentWaypointIndex++;
                    if(currentWaypointIndex >= waypoints.Length)
                    {
                        currentWaypointIndex = 0;
                    }
                }

                transform.position = Vector2.MoveTowards(transform.position, waypoints[currentWaypointIndex].transform.position, Time.deltaTime * speed);
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerHasArrived = true;
        }
    }
}
