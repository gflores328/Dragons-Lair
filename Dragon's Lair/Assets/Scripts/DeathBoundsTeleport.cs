using UnityEngine;

public class DeathBoundsTeleport : MonoBehaviour
{
    [SerializeField] private Transform[] respawnPoints; // Array of respawn points
    private int currentIndex = 0; // Current index in the respawn points array
    [SerializeField] private float damageAmount = 2f; // Damage amount to apply to the player
    private ChibiPlayerMovement playerMovement; // Reference to the player movement script
    

    void Start()
    {
        playerMovement = FindObjectOfType<ChibiPlayerMovement>();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Teleport the player to the current respawn point
            TeleportPlayer();

            // Apply damage to the player
            playerMovement.takeDamage(damageAmount);
        }
    }

    private void TeleportPlayer()
    {
        // Check if there are any respawn points
        if (respawnPoints.Length == 0)
        {
            Debug.LogWarning("No respawn points assigned to DeathBounds script!");
            return;
        }

        // Ensure currentIndex is within bounds of the array
        currentIndex = Mathf.Clamp(currentIndex, 0, respawnPoints.Length - 1);

        // Teleport the player to the current respawn point
        playerMovement.transform.position = respawnPoints[currentIndex].position;
    }

    public void IncrementIndex()
    {
        currentIndex++; // Increment the current index

        // If index exceeds array length, loop back to the end
        if (currentIndex >= respawnPoints.Length)
        {
            currentIndex = respawnPoints.Length - 1;
        }
    }
}
