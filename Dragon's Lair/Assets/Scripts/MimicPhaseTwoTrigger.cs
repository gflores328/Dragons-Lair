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

    private void OnTriggerEnter(Collider other)
    {
        //entrance.SetActive(true);
        mimic.GetComponent<MimicPhaseTwo>().start = true;

        Destroy(gameObject);
    }
}
