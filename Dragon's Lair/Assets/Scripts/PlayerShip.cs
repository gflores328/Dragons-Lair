/*
 * Created by Carlos Martinez
 * 
 * This script contains code for the Player Ship in the arcade
 * minigame, Mobile Fighter Axiom. - WIP
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShip : MonoBehaviour
{
    public float speed = 5.0f; // Movement Speed

    // Update is called once per frame
    private void Update()
    {
        // Player Movement (Left and Right)
        if (Input.GetKey(KeyCode.LeftArrow)) // Left
        {
            this.transform.position += Vector3.left * this.speed * Time.deltaTime;
        }

        else if (Input.GetKey(KeyCode.RightArrow)) // Right
        {
            this.transform.position += Vector3.right * this.speed * Time.deltaTime;
        }

        // Player Shoots When Space is Pressed
        if (Input.GetKeyDown(KeyCode.Space))
        {
            //Shoot();
        }
    }

    // Player's Attack
    private void Shoot()
    {
        
    }
}
