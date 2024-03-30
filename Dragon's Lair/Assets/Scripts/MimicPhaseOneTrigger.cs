/*
 * Created By: Gabriel Flores
 * 
 * This script will hold an on trigger enter that will start the boss and lock the player into the boss room
 */


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MimicPhaseOneTrigger : MonoBehaviour
{
    public GameObject entrance;
    public GameObject mimic;

    private void OnTriggerEnter(Collider other)
    {
        entrance.SetActive(true);
        mimic.GetComponent<MimicPhaseOne>().start = true;

        Destroy(gameObject);
    }
}
