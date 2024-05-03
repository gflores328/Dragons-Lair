/*
 * Created by Carlos Martinez
 * 
 * This script contains a controller for the Kobold-Type enemies.
 * 
 */

using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.AI;
#if UNITY_EDITOR

using UnityEditor;
#endif
[RequireComponent(typeof(NavMeshAgent))]
public class KoboldController : Enemy
{
    // Variables
    public NavMeshAgent navAgent; // Nav Mesh Agent
    public float startWaitTime = 4; // Wait Time
    public float timeToRotate = 2; // Time to Rotate
    public float speedWalk = 6; // Walking Speed
    public float speedRun = 9; // Running Speed

    [SerializeField]
    private float viewRadius = 15; // Enemy View Radius

    [SerializeField]
    private float viewAngle = 90; // Enemy View Angle

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

    // Kobold audio
    public AudioSource Kobold_Attack, Kobold_Hurt, Kobold_Death, Kobold_Aggro;

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
        if (!isDead)

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
        // Move towards player's position
        Move(speedRun);
        Vector3 targetPosition = new Vector3(m_PlayerPosition.x, m_PlayerPosition.y, transform.position.z);
        navAgent.SetDestination(targetPosition);

        // Check if player is in attack range
        if (m_PlayerInRange && Vector3.Distance(transform.position, m_PlayerPosition) <= attackRange)
        {
            Move(speedRun); // Kobold now runs
            navAgent.SetDestination(m_PlayerPosition); // Follows the player

            // Check if the player is within view angle and attack range
            if (m_PlayerInRange && Vector3.Distance(transform.position, m_PlayerPosition) <= attackRange)
            {
                // Attack the player
                Debug.Log("Player in attack range");
                Stop();
                Attack();
                // audio kobold attack
                Kobold_Attack.Play();
                Invoke("ResetAttack", animator.GetCurrentAnimatorStateInfo(0).length);
            }
        }

        // Handle transition back to patrol mode
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            if (m_WaitTime <= 0 && !m_CaughtPlayer && !m_PlayerInRange)
            {
                // Transition back to patrol mode
                m_IsPatrol = true;
                m_PlayerNear = false;
                Move(speedWalk);
                m_TimeToRotate = timeToRotate;
                m_WaitTime = startWaitTime;
                navAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
            }
            else
            {
                if (!m_PlayerInRange)
                {
                    // Halts Kobold's movement
                    Stop();
                    m_WaitTime -= Time.deltaTime;
                }
            }
        }
    }


    // Patrol Mode
    void Patrolling()
    {
        // Check if Kobold is near the player
        if (m_PlayerNear)
        {
            if (m_TimeToRotate <= 0)
            {
                Move(speedWalk);
                LookingPlayer(playerLastPosition);
            }
            else
            {
                Stop();
                m_TimeToRotate -= Time.deltaTime;
            }
        }
        else
        {
            m_PlayerNear = false;
            playerLastPosition = Vector3.zero;
            Vector3 targetPosition = new Vector3(waypoints[m_CurrentWaypointIndex].position.x, waypoints[m_CurrentWaypointIndex].position.y, transform.position.z);
            navAgent.SetDestination(targetPosition);

            // Check if Kobold reaches a waypoint
            if (navAgent.remainingDistance <= navAgent.stoppingDistance)
            {
                if (m_WaitTime <= 0)
                {
                    // Move to the next waypoint
                    NextPoint();
                    Move(speedWalk);
                    m_WaitTime = startWaitTime;
                }
                else
                {
                    // Halts Kobold's movement
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
        animator.SetBool("Walking_Kobold", false);
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
        Kobold_Aggro.Play();
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
        // Cast rays in a cone to detect the player
        RaycastHit hit;
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        foreach (GameObject player in players)
        {
            Vector3 dirToPlayer = (player.transform.position - transform.position).normalized;
            if (Vector3.Angle(transform.forward, dirToPlayer) < viewAngle / 2)
            {
                if (Physics.Raycast(transform.position, dirToPlayer, out hit, viewRadius, playerMask))
                {
                    if (hit.collider.CompareTag("Player"))
                    {
                        m_PlayerInRange = true;
                        m_IsPatrol = false;
                        m_PlayerPosition = hit.transform.position;
                        return;
                    }
                }
            }
        }

        // If no player is detected, set flags accordingly
        m_PlayerInRange = false;
        m_IsPatrol = true;
    }

    protected override void Die()

    {
        Stop();
        isDead = true;

        // audio Kobold Death
        Kobold_Death.Play();

        animator.SetTrigger("IS_Dead");
        if (dissolveMaterial != null)
        {
            if (renderer != null)
            {
                renderer.material = dissolveMaterial;
                // Gradually increase the dissolve amount
                //Debug.Log("Coroutine Started");
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
            //Debug.Log("" + dissolveAmount);
            dissolveAmount = Mathf.Clamp01(dissolveAmount);

            renderer.material.SetFloat("_Dissolve", dissolveAmount);

            yield return null;
        }
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        StartCoroutine(HitDelay());

        //audio kobold hurt
        Kobold_Hurt.Play();

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
                if (hitPlayer)
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

    #if UNITY_EDITOR
        private void OnDrawGizmosSelected()
        {
            // Draw the view angle only when the KoboldController is selected
            Handles.color = Color.green;
            Handles.DrawWireArc(transform.position, Vector3.up, Quaternion.Euler(0, -viewAngle / 2, 0) * transform.forward, viewAngle, viewRadius);

            // Draw the view radius only when the KoboldController is selected
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, viewRadius);
        }
    #endif
}