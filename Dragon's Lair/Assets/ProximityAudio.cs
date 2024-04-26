using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityAudio : MonoBehaviour
{

    public AudioSource BuzzSaw;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        BuzzSaw.Play();

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        BuzzSaw.Stop();
    }
}
