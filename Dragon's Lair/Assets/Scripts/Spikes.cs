using System.Collections;
using UnityEngine;

public class Spikes : MonoBehaviour
{
    //public float damage;

    public Animator anim; //animator variable

    public float waitTime = 0f; //wait time between actiations so you dont get hit over and over

    private IEnumerator coroutine; //couroutine is used to add wait time between actavation

    void Start()
    {
       
        StartCoroutine(StartAnimationWithDelay(waitTime));
       
    }

    

    //if player enters collider play animation then wait before playing again
    public void OnTriggerEnter(Collider other)
    {

        if (other.CompareTag("Player"))
        {
            
            ChibiPlayerMovement player = other.gameObject.GetComponent<ChibiPlayerMovement>(); 
            if (player != null)
            {
                // Call the player's takeDamage function and pass the damage amount
                player.takeDamage(1);

                
            }
            
        }
    }

    // //if player exits collider stop animation playback
    // public void OnTriggerExit(Collider other)
    // {
    //     if (other.CompareTag("Player"))
    //     {
    //         anim.StopPlayback();
    //     }
    //}

    private void OnCollisionEnter(Collision collision)
    {
        
            ChibiPlayerMovement player = collision.gameObject.GetComponent<ChibiPlayerMovement>(); 
            if (player != null)
            {
                // Call the player's takeDamage function and pass the damage amount
                player.takeDamage(1);

                
            }

        
    }

    private void OnCollisionStay(Collision collision)
    {
        
            ChibiPlayerMovement player = collision.gameObject.GetComponent<ChibiPlayerMovement>(); 
            if (player != null)
            {
                // Call the player's takeDamage function and pass the damage amount
                player.takeDamage(1);

                
            }

        
    }
    //wait time function
    private IEnumerator WaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
    }

    private IEnumerator StartAnimationWithDelay(float delay)
    {
        // Wait for the specified delay time
        yield return new WaitForSeconds(delay);

        // Trigger the animation after the delay
        anim.SetTrigger("onActive");
    }
}
