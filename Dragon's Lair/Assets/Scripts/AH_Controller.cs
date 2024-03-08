using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class AH_PlayerController : MonoBehaviour
{

    //this script controls the movement for the players in the Air Hockey Game
    BoxCollider coll; //collider of table
    Rigidbody puck; //rigidbody of puck
    GameObject ps; //player pusher object

    public float playerSpeed; //set value from inspector
    public float puckSpeed; //speed that the pusher moves

    //AI ai; //uses AI script to control AI player


    void Awake()
    {
        puckSpeed = PlayerPrefs.GetFloat("Puck_Speed");
    }

    // Start is called before the first frame update
    void Start()
    {
        //ai = GameObject.Find("AH_AI").GetComponent<AI>();
        coll = GameObject.Find("AH_Table").GetComponent<BoxCollider>();
        puck = GameObject.FindGameObjectWithTag("AH_Puck").GetComponent<Rigidbody>();
        ps = GameObject.FindGameObjectWithTag("AH_Player");

    }

    // Update is called once per frame
    void Update()
    {
        //Input to move the player
        if (Input.GetKey("left"))
            transform.Translate(-playerSpeed * Time.deltaTime, 0f, 0f);

        if (Input.GetKey("right"))
            transform.Translate(playerSpeed * Time.deltaTime, 0f, 0f);

        if (Input.GetKey("up"))
            transform.Translate(0f, 0f, playerSpeed * Time.deltaTime);

        if (Input.GetKey("down"))
            transform.Translate(0f, 0f, -playerSpeed * Time.deltaTime);


        //Collision detection with edges, basically we are restricting player movement
        if (transform.position.x <= -4.74f)
            transform.position = new Vector3(-4.74f, transform.position.y, transform.position.z);

        if (transform.position.x >= 4.74f)
            transform.position = new Vector3(4.74f, transform.position.y, transform.position.z);

        if (transform.position.z >= -1f)
            transform.position = new Vector3(transform.position.x, transform.position.y, -1f);

        if (transform.position.z <= -8.4f)
            transform.position = new Vector3(transform.position.x, transform.position.y, -8.4f);


    }


    void OnCollisionEnter(Collision c)
    {
        if (c.gameObject.tag == "AH_Player")
        {
            //ai.counter = 0f; //see AI.cs for explanation

            

            //Controls to hit the puck
            if (Input.GetKey("space"))
            { //if you keep space pressed and up arrow key and then touch, puck is smashed

              //---Control Part---
                if (Input.GetKey("up"))
                {
                    if (Input.GetKey("right"))
                    {
                        puck.velocity = new Vector3(playerSpeed, puck.velocity.y, playerSpeed);
                    }
                    else
                    {
                        puck.velocity = new Vector3(-playerSpeed, puck.velocity.y, playerSpeed);
                    }
                }

            }

            else
            { //no space pressed and then touch then a gentle push is given

                if (Input.GetKey("right"))
                {
                    puck.velocity = new Vector3(playerSpeed * 0.5f, puck.velocity.y, playerSpeed* 0.60f);
                }
                else
                {
                    puck.velocity = new Vector3(playerSpeed * -0.5f, puck.velocity.y, playerSpeed * 0.60f);
                }
            }
        }

    }


}
