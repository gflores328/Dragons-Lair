/*
 * Created by Carlos Martinez
 * 
 * This script contains the enemy spawner for Mobile Fighter Axiom.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private float spawnRate = 1f; // Spawn Rate: 1 (Default)
    [SerializeField] private GameObject[] enemyPrefabs; // Enemy Prefabs
    [SerializeField] private bool canSpawn = true; // Can Spawn

    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(Spawner()); // Activates Spawner
    }

    private IEnumerator Spawner()
    {
        WaitForSeconds wait = new WaitForSeconds(spawnRate); // Wait Time

        while (canSpawn)
        {
            yield return wait; // Pause for a Moment
            int rand = Random.Range(0, enemyPrefabs.Length); // Random Range
            GameObject enemyToSpawn = enemyPrefabs[rand]; // Selects Random Enemies from Enemy Prefabs

            Instantiate(enemyToSpawn, transform.position, Quaternion.identity); // Spawns Enemy
        }
    }
}
