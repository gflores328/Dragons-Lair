using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberCheckPoints : MonoBehaviour
{

    public bool checkPointReached = false;
    private DeathBoundsTeleport deathBounds;
    // Start is called before the first frame update
    void Start()
    {
        deathBounds = FindObjectOfType<DeathBoundsTeleport>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !checkPointReached)
        {
            // Increment the current index in the DeathBounds script
            deathBounds.IncrementIndex();
            checkPointReached = true;
        }
    }
}
