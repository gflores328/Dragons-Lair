/*
 * Crated By: Gabriel Flores
 * 
 * This script will hold the start trigger for the second boss to start
 */




using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicPhaseTwoTrigger : MonoBehaviour
{
    public GameObject entrance;
    public GameObject mimic;
    public GameObject healthBar;

    private void OnTriggerEnter(Collider other)
    {
        entrance.SetActive(false);
        if (other.tag == "Player")
        {
            mimic.GetComponent<MimicPhaseTwo>().start = true;
            healthBar.SetActive(true);

            Destroy(gameObject);
        }
    }
}
