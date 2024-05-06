using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProximityAnime : MonoBehaviour
{

    public Animator randAnime1;


    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        randAnime1.SetBool("isNear", true);

    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        randAnime1.SetBool("isNear", false);
    }
}
