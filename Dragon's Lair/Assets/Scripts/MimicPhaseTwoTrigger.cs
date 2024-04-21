/*
 * Crated By: Gabriel Flores
 * 
 * This script will hold the start trigger for the second boss to start
 */




using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MimicPhaseTwoTrigger : MonoBehaviour
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
            entrance.SetActive(false);
            mimic.GetComponent<MimicPhaseTwo>().StartStartDelay();

            other.GetComponent<PlayerInput>().actions.Disable();

            // Camera switch
            mainCamera.SetActive(false);
            bossCamera.SetActive(true);

            Destroy(gameObject);
        }
    }
}
