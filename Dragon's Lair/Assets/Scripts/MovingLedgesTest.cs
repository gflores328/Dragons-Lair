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

    private Vector3 velocity;
    private bool moving;


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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHasArrived = true;
            moving = true;
            collision.collider.transform.SetParent(transform);
            transform.position += (velocity * Time.deltaTime);
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHasArrived = false;
            moving = false;
            collision.collider.transform.SetParent(null);
        }
    }

    private void FixedUpdate()
    {

        if (moving)
        {
            transform.position += (velocity * Time.deltaTime);
        }
    }
}
