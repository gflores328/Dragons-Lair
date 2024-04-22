/*
 * Created by Carlos Martinez
 * 
 * This script contains a controller for the Kobold-Type enemies.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class KoboldController : Enemy
{
    // Variables
    public NavMeshAgent navAgent; // Nav Mesh Agent
    public float startWaitTime = 4; // Wait Time
    public float timeToRotate = 2; // Time to Rotate
    public float speedWalk = 6; // Walking Speed
    public float speedRun = 9; // Running Speed

    public float viewRadius = 15; // Enemy View Radius
    public float viewAngle = 90; // Enemy View Angle
    public LayerMask playerMask; // Layer Mask for Player
    public LayerMask obstacleMask; // Layer Mask for Obstacle
    public float meshResolution = 1f;
    public int edgeIterations = 4;
    public float edgeDistance = 0.5f;

    public float attackRange = 2f; // Range within which the enemy can attack
    public float attackDamage = 10f; // Damage inflicted by the enemy's attack
    public float attackCooldown = 2f; // Cooldown between attacks

    private bool isAttacking = false; // Flag to check if the enemy is currently attacking
    private float nextAttackTime = 0f; // Time when the enemy can perform the next attack

    public Material hitMaterial;
    public Material dissolveMaterial; // Reference to the dissolving material
    public Renderer renderer;

    public float dissolveSpeed = 0.7f;

    private bool isDead = false;

    public Animator animator;

    public Transform[] waypoints; // Waypoints
    int m_CurrentWaypointIndex;

    Vector3 playerLastPosition = Vector3.zero;
    Vector3 m_PlayerPosition;

    float m_WaitTime; // Wait Time
    float m_TimeToRotate; // Time to Rotate
    bool m_PlayerInRange; // Player is in Range
    bool m_PlayerNear; // Player is Near
    bool m_IsPatrol; // Patrol Mode
    bool m_CaughtPlayer; // Caught Player

    public bool hitPlayer; // was player hit
    public KoboldData kData; // KoboldData - Unused

    // Start is called before the first frame update
    void Start()
    {
        // Variables are initialized
        m_PlayerPosition = Vector3.zero;
        m_IsPatrol = true;
        m_CaughtPlayer = false;
        m_WaitTime = startWaitTime;
        m_TimeToRotate = timeToRotate;

        m_CurrentWaypointIndex = 0;
        navAgent = GetComponent<NavMeshAgent>();

        navAgent.isStopped = false; // Kobold is not stopped
        navAgent.speed = speedWalk; // Kobold starts moving in walking speed
        navAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!isDead)

        {

            EnvironmentView(); // The Kobold's line of sight

            if (!m_IsPatrol) // If the kobold is not patrolling

            {

                Chasing();  // The kobold chases the player

            }

            else

            {

                Patrolling(); // The kobold patrols when the player is not in range

            }
        }
    }

    // Chase Mode
    void Chasing()
    {
        m_PlayerNear = false;
        playerLastPosition = Vector3.zero;

        if (!m_CaughtPlayer) // If the player is spotted
        {
            Move(speedRun); // Kobold now runs
            navAgent.SetDestination(m_PlayerPosition); // Follows the player

            // Check if the player is within attack range
            if (Vector3.Distance(transform.position, m_PlayerPosition) <= attackRange)
            {
                // Attack the player
                Debug.Log("Player in attack range");

                Stop();
                Attack();
                Invoke("ResetAttack", animator.GetCurrentAnimatorStateInfo(0).length);
            }
           
        }
        if (navAgent.remainingDistance <= navAgent.stoppingDistance) // If 
        {
            // If the player is not near, return to patrol mode
            if (m_WaitTime <= 0 && !m_CaughtPlayer && Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 6f)
            {
                m_IsPatrol = true; // Kobold is set in patrol mode
                m_PlayerNear = false; // Player is out of range
                Move(speedWalk); // Kobold is moving in walking speed
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position); // Kobold moves to its next waypoint
            }
            else // The kobold halts its movement
            {
                if (Vector3.Distance(transform.position, GameObject.FindGameObjectWithTag("Player").transform.position) >= 2.5f)
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    // Patrol Mode
    private void Patrolling()
    {
        
        if (m_PlayerNear) // If the kobold is near the player
        {
            if (m_TimeToRotate <= 0) // After turning around...
            {
                Move(speedWalk); // Movement speed is set to walking speed
                LookingPlayer(playerLastPosition); // the kobold walks to the player's last position
            }
            else // The kobold's movement is halted
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else // When the player is out of sight, the kobold moves to the next waypoint in the index
        {
            m_PlayerNear = false; // Player is out of range
            playerLastPosition = Vector3.zero;
            navAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position); // The kobold follows the waypoints
            if (navAgent.remainingDistance <= navAgent.stoppingDistance) // If the kobold reaches a waypoint
            {
                if (m_WaitTime <= 0) // If wait time is less than or equal to 0
                {
                    NextPoint(); // The kobold moves to the next waypoint
                    Move(speedWalk); // Movement speed is set to walking speed
                    m_WaitTime = startWaitTime;
                }
                else // The kobold's movement has been halted
                {
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }

    // Sets the kobold's movement speed
    void Move(float speed)
    {
        animator.SetBool("Walking_Kobold", true);
        navAgent.isStopped = false;
        navAgent.speed = speed;
    }

    // Stops the kobold's movement
    void Stop()
    {
        animator.SetBool("Walking_Kobold",false);
        // The kobold stops moving
        navAgent.isStopped = true;
        navAgent.speed = 0;
    }

    // The kobold moves to the next waypoint in the index
    public void NextPoint()
    {
        m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
        navAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
    }

    // When the kobold spots the player
    void CaughtPlayer()
    {
        m_CaughtPlayer = true; // The kobold has spotted the player
    }

    // The kobold moves to the player's last position then returns to patrol mode
    void LookingPlayer(Vector3 player)
    {
        navAgent.SetDestination(player); // Kobold moves to where the player was at
        if (Vector3.Distance(transform.position, player) <= 0.3)
        {
            if (m_WaitTime <= 0) // Kobold transitions to patrol mode
            {
                m_PlayerNear = false;
                Move(speedWalk);
                navAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
                m_WaitTime = startWaitTime;
                m_TimeToRotate = timeToRotate;
            }
            else // Kobold halts its movement
            {
                Stop();
                m_WaitTime -= Time.deltaTime;
            }
        }

    }

    // The kobold's line of sight
    void EnvironmentView()
    {
        Collider[] playerInRange = Physics.OverlapSphere(transform.position, viewRadius, playerMask); // Creates a sphere to detect the player within range

        for (int i = 0; i < playerInRange.Length; i++) // When the player is in range 
        {
            Transform player = playerInRange[i].transform; // Saves player's position
            Vector3 dirToPlayer = (player.position - transform.position).normalized; // Calculates the direction to the player
            if (Vector3.Angle(transform.position, dirToPlayer) < viewAngle / 2) // Calculates the angle the kobold can see the player
            {
                float dstToPlayer = Vector3.Distance(transform.position, player.position); // Calculates distance to the player
                if (!Physics.Raycast(transform.position, dirToPlayer, dstToPlayer, obstacleMask)) // If the player is NOT hiding behind obstacles
                {
                    m_PlayerInRange = true;
                    m_IsPatrol = false;
                }
                else // If the player is hiding behind obstacles
                {
                    m_PlayerInRange = false;
                }
            }
            if (Vector3.Distance(transform.position, player.position) > viewRadius) // If the player is out of sight
            {
                m_PlayerInRange = false; // The player is out of range
            }
            
            if (m_PlayerInRange) // If the player is in sight
            {
                m_PlayerPosition = player.transform.position; // Saves player position
            }
        }
    }

    protected override void Die()

    {
        Stop();
        isDead = true;
        animator.SetTrigger("IS_Dead");
        if (dissolveMaterial != null)
        {
            if (renderer != null)
            {
                renderer.material = dissolveMaterial;
                // Gradually increase the dissolve amount
                Debug.Log("Coroutine Started");
                StartCoroutine(DissolveOverTime());
            }
            else
            {
                Debug.LogWarning("Renderer not found on Newt object.");
            }

            
        }
        else
        {
            Debug.LogWarning("Dissolve material not assigned to Newt.");
        }


        // Disable the Capsule Collider
        CapsuleCollider collider = GetComponent<CapsuleCollider>();
        if (collider != null)
        {
            collider.enabled = false;
        }
        

        float deathDuration = 2f;

        Destroy(gameObject, deathDuration);



    }

    IEnumerator DissolveOverTime()
    {
        float dissolveAmount = 0f;
        while (dissolveAmount <= 1f) // Change the loop condition to dissolveAmount < 1f
        {
            dissolveAmount += dissolveSpeed * Time.deltaTime;
            Debug.Log("" + dissolveAmount);
            dissolveAmount = Mathf.Clamp01(dissolveAmount);

            renderer.material.SetFloat("_Dissolve", dissolveAmount);

            yield return null;
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(HitDelay());
    }

    IEnumerator HitDelay()
    {
        if (health != 0)
        {
            Material original = renderer.material;
            renderer.material = hitMaterial;
            yield return new WaitForSeconds(.1f);
            renderer.material = original;
        }
    }

    void Attack()
    {
        
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            ChibiPlayerMovement playerMovement = player.GetComponent<ChibiPlayerMovement>();
            if (playerMovement != null)
            {
                isAttacking = true; // Set attacking flag to true
                Stop();
               
                Debug.Log("Kobold is Attacking");
                
                animator.SetBool("Attack_Kobold", true); // Trigger attack animation

                // Calculate the next attack time based on the attack cooldown
                nextAttackTime = Time.time + attackCooldown;
                if(hitPlayer)
                {
                    playerMovement.takeDamage(attackDamage);
                }
                

                // Reset the attacking flag after the attack animation duration
                //Invoke("ResetAttack", animator.GetCurrentAnimatorStateInfo(0).length);
            }
        }
        else
        {
            Debug.LogError("Player GameObject not found.");
        }
    }

    void ResetAttack()
    {
        isAttacking = false;
        animator.SetBool("Attack_Kobold", false);
    }

    bool IsPlayerInRange(float range)
    {
        // Check if the player is within the specified range
        // For example, you can use a collider attached to the player to detect proximity
        Collider[] colliders = Physics.OverlapSphere(transform.position, range);
        foreach (Collider collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                return true;
            }
        }
        return false;
    }

    public void SetHitPlayer(bool success)
    {
        hitPlayer = success;
    }


} 
