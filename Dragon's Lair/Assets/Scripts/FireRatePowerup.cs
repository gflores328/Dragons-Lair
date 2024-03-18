using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerup : MonoBehaviour
{
    public float fireRateSetter; // float for what rate the powerup will set the fire rate to
    //public GameObject powerUpDesc; // A game object variable that will hold the words that appear above the players head and tells it what the powerup does

    private GunController gunController; // A private gun controller variable to grab the player's gun controller script

    void Awake()
    {
        gunController = FindObjectOfType<GunController>(); // On awake it finds the gun controller ready to access it
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            //powerUpDesc.SetActive(true);
            gunController.setFireRate(fireRateSetter); // calls the setfire rate function from the gun controller script
            Destroy(gameObject); // Destroys the powerup on pick up
        }
    }

    private void OnTriggerExit(Collider other)

    {
        if (other.gameObject.CompareTag("Player"))
        {
            //powerUpDesc.SetActive(false);
            Debug.Log("I have exited");
        }
    }
}
