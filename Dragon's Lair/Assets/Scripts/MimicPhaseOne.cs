/* Created By: Gabriel Flores
 * 
 * This script will hold the behavior for the mimic boss phase 1
 * The only thing that it will do is jump on the player
*/


using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class MimicPhaseOne : Enemy
{

    public bool start = false; // A bool to determine wheter or not the object should be doing stuff
    private bool takingAction = false; // A bool to check if the object is currently in an action

    public GameObject player;
    public GameObject exit;
    public GameObject healthBar;

    [Header("Speeds")]
    public float jumpSpeed; // The speed of the enemy jump
    public float slamSpeed; // The speed of the enemy slam

    [Header("Borders")]
    [Tooltip("The game objects that represent the borders that constrain this game object")]
    public GameObject leftBorder;
    public GameObject rightBorder;

    
    // Start is called before the first frame update
    void Start()
    {
        healthBar.GetComponent<Slider>().maxValue = health;
        healthBar.GetComponent<Slider>().value = health;
    }

    // Update is called once per frame
    void Update()
    {
        if (start)
        {
            if (!takingAction)
            {
                StartCoroutine(JumpAbovePlayer());
            }
        }
    }

    IEnumerator Jump(float xTarget)
    {
        float yTarget = transform.position.y + 5;

        // A vector 3 is created with the x and y targer
        Vector3 targetPosition = new Vector3(xTarget, yTarget, transform.position.z);


        // Object Jumps
        // While the distance from the objcts current position and the targetPosition are greater than 0.01
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // The object moves towards the target position at the speed of the jumpSpeed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, jumpSpeed * Time.deltaTime);

            // Wait for next frame
            yield return null;
        }

        // Waits for .5 seconds before running the code to slam down
        yield return new WaitForSeconds(.5f);

        // A new target position is set
        yTarget = transform.position.y - 5;
        targetPosition = new Vector3(xTarget, yTarget, transform.position.z);

        // Object Slams
        // While the distance from the objcts current position and the targetPosition are greater than 0.01
        while (Vector3.Distance(transform.position, targetPosition) > 0.01f)
        {
            // The object moves towards the target position at the speed of the slamSpeed
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, slamSpeed * Time.deltaTime);

            // Waits for next frame
            yield return null;
        }

        GetComponent<CinemachineImpulseSource>().GenerateImpulse(0.5f);

    }

    IEnumerator JumpAbovePlayer()
    {
        // Taking action is set to true
        takingAction = true;

        yield return StartCoroutine(Jump(player.transform.position.x));


        // Waits for 1 second
        yield return new WaitForSeconds(1f);

        // takingAction is set to false
        takingAction = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ChibiPlayerMovement>().takeDamage(1);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ChibiPlayerMovement>().takeDamage(1);
        }
    }

    protected override void Die()
    {
        exit.SetActive(false);
        healthBar.SetActive(false);
        base.Die();
    }

    public override void TakeDamage(float amnt)
    {
        base.TakeDamage(amnt);
        healthBar.GetComponent<Slider>().value = health;
    }
}
