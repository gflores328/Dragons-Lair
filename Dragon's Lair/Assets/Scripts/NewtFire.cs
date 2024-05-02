/*
 * Created By: Gabriel Flores
 * 
 * This script contains the code that will allow the newt enemy to fire a bullet towards the player when it enters its trigger
 * 
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewtFire : MonoBehaviour
{
    public GameObject player; // The game object that represents the player
    public GameObject bulletPrefab; // The game object for the bullet prefab
    public float bulletSpeed; // The speed of the buller

    private Vector3 fireDirection; // The direction that the bullet will be shot in
    private bool firing = false; // A bool to check if the enemy is firing or not

    private bool firstTimeShot = true; // a bool to check if it is the first time a player is being shot at

    public AudioSource Newt_Attack;

    public Animator shooting;

    private void Update()
    {   
        // fireDirection is set to the direction of the player from the position of this game object
        fireDirection = (player.transform.position - transform.position).normalized;
    }

    
    private void OnTriggerStay(Collider other)
    {
        // While the player is on the trigger and firing is false
        if (other.tag == "Player" && !firing)
        {
            // ShootWithDelay is started
            StartCoroutine(ShootWithDelay());

        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            firstTimeShot = true;
        }
    }

    // This function will shoot a bullet at the player and then wait 2 seconds
    IEnumerator ShootWithDelay()
    {
        // firing is set to true
        firing = true;



        // If it is the players first time being shot at a small delay appears
        if (firstTimeShot)
        {
            yield return new WaitForSeconds(1.5f);




            firstTimeShot = false;
        }

        // The bullet prefab is cloned to a new object called clonedBullet
        GameObject clonedBullet = Instantiate(bulletPrefab, gameObject.transform.position, Quaternion.identity);

        // Force is applied to the clonedBullet in the direction of the player times the bullet speed
        Rigidbody rb = clonedBullet.GetComponent<Rigidbody>();
        rb.velocity = fireDirection * bulletSpeed;

        // animator for enemy shooting
        shooting.SetBool("Shooting", true);
        

        //audio of newt firing
        Newt_Attack.Play();



        // Wait for 2 seconds and then destroy the clonedBullet
        yield return new WaitForSeconds(2);

        // set bool to shoot to false
        shooting.SetBool("Shooting", false);

        Destroy(clonedBullet);

        // firing set to false to allow a new bullet to be shot
        firing = false;
    }

    
   
}
