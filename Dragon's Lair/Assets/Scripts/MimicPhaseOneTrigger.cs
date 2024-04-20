/*
 * Created By: Gabriel Flores
 * 
 * This script will hold an on trigger enter that will start the boss and lock the player into the boss room
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MimicPhaseOneTrigger : MonoBehaviour
{
    public GameObject entrance;
    public GameObject mimic;
    public GameObject healthBar;

    public GameObject mainCamera;
    public GameObject bossCamera;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            entrance.SetActive(true);
            mimic.GetComponent<MimicPhaseOne>().StartStartDelay();

            other.GetComponent<PlayerInput>().actions.Disable();

            // Camera switch
            //mainCamera.SetActive(false);
            //bossCamera.SetActive(true);

            Destroy(gameObject);
        }
    }
}
