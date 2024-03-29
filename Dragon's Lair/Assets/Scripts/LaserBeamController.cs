using UnityEngine;
using UnityEngine.VFX;

public class LaserBeamController : BulletController
{
    [SerializeField] private float laserDamagePerSecond = 1f; // Damage per second of the laser beam
    [SerializeField] private float raycastLength = 10f; // Length of the raycast
    private bool isFiring; // Flag to track if the laser beam is firing
    private VisualEffect visualEffect; // Reference to the VisualEffect component

    private void Start()
    {
        isFiring = false; // Initialize isFiring to false
        visualEffect = GetComponent<VisualEffect>(); // Get the VisualEffect component
        if (visualEffect == null)
        {
            Debug.LogError("VisualEffect component not found on " + gameObject.name);
        }

         // Initially stop the visual effect from playing
        if (visualEffect != null)
        {
            visualEffect.Stop();
        }
    }

    private void Update()
    {
        // Check if the laser beam is firing
        if (isFiring)
        {
            // Draw the raycast in the scene view
            Debug.DrawRay(transform.position, transform.forward * raycastLength, Color.red);

            // Deal damage to enemies within the collider
            RaycastHit[] hits = Physics.RaycastAll(transform.position, transform.forward, raycastLength);
            foreach (RaycastHit hit in hits)
            {
                Enemy enemy = hit.collider.GetComponent<Enemy>();
                if (enemy != null)
                {
                    // Calculate damage based on the elapsed time since last frame
                    float damage = laserDamagePerSecond * Time.deltaTime;
                    enemy.TakeDamage(damage);
                }
            }
        }
    }

    // Method to start firing the laser beam
    public void StartFiring()
    {
        isFiring = true;
        if (visualEffect != null)
        {
            visualEffect.Play(); // Start the visual effect
        }
        else
        {
            Debug.LogError("VisualEffect component not found on " + gameObject.name);
        }
    }

    // Method to stop firing the laser beam
    public void StopFiring()
    {
        isFiring = false;
        if (visualEffect != null)
        {
            visualEffect.Stop(); // Stop the visual effect
        }
        else
        {
            Debug.LogError("VisualEffect component not found on " + gameObject.name);
        }
    }
}
