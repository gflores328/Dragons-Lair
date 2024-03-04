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
    
    //[SerializeField] protected float health; // a float that can only be accessed by children of this class

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
    }
}
