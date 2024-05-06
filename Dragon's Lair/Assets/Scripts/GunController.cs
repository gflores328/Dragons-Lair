using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class GunController : MonoBehaviour
{
    public bool isUsingBullet = true; // Indicates whether the gun is currently using bullets or not
    public int shots = 1; // Fires a number of bullets at once (Default = 1)
    public int shotsFired; // Counter for how many shots fired
    public InputActionReference fireAction; // Input action for shooting
    public BulletController bullet; // Bullet Controller

    public GameObject realBulletPrefab; // Prefab for the RealBullet
    //public LaserBeamController laserBeam; // Laser Beam Controller

    public float fireRate; // Affects the firing rate of the gun
    public float fireRateCounter; // Counter for firing rate

    public float accuracy = 100; // Affects the accuracy of the gun (Default = 100)
    
    private AudioSource bulletSound;
    
    private bool isFiring; // Flag to track if firing action is in progress

    // Start is called before the first frame update
    void Start()
    {
        // Calls Bullet Controller
        if (!bullet) bullet = GetComponentInChildren<BulletController>(); 
        if (bullet) bullet.gun = this;

        // Calls Laser Beam Controller
        //if (!laserBeam) laserBeam = GetComponentInChildren<LaserBeamController>();
        //if (laserBeam) laserBeam.gun = this;

        // Initialize isFiring to false
        isFiring = false;

        bulletSound = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        fireAction.action.Enable(); // Enable the aim action
        fireAction.action.started += OnFireStarted; // Subscribe to the started event
        fireAction.action.canceled += OnFireCanceled; // Subscribe to the canceled event
    }

    private void OnDisable()
    {
        fireAction.action.Disable(); // Disable the aim action
        fireAction.action.started -= OnFireStarted; // Unsubscribe from the started event
        fireAction.action.canceled -= OnFireCanceled; // Unsubscribe from the canceled event
    }

    private void OnFireStarted(InputAction.CallbackContext context)
    {
        // Start firing when the fire action is started
        isFiring = true;
        StartCoroutine(FireRoutine());
    }

    private void OnFireCanceled(InputAction.CallbackContext context)
    {
        // Stop firing when the fire action is canceled
        isFiring = false;
    }

    private IEnumerator FireRoutine()
    {
        while (isFiring)
        {
           if (Time.time >= fireRateCounter)
            {
                // Spawn a RealBullet
                FireRealBullet();

                if (bulletSound != null)
                {
                    bulletSound.Play();
                }
                    
                // Set the next allowed firing time based on the fire rate
                fireRateCounter = Time.time + 1 / fireRate;
            }

            // Wait for the next frame
            yield return null;
        
        }
        //laserBeam.StopFiring();
    }

    private void FireRealBullet()
    {
        // Instantiate the RealBullet prefab at the bullet spawn point
        GameObject newBulletObject = Instantiate(realBulletPrefab, transform.position, transform.rotation);

        // Ensure that the newBulletObject is not null
        if (newBulletObject != null)
        {
            // Get the RealBullet component from the instantiated object
            RealBullet newBullet = newBulletObject.GetComponent<RealBullet>();

            // Set the direction of the bullet to match the gun's forward direction
            if (newBullet != null)
            {
                newBullet.transform.forward = transform.forward;
                //newBullet.lifetime = 5f; // Set a lifetime for the bullet
            }
            else
            {
                Debug.LogError("RealBullet component not found on instantiated object.");
            }
        }
        else
        {
            Debug.LogError("Failed to instantiate new bullet.");
        }
    }

    public void setFireRate(float newFireRate)
    {
        fireRate = newFireRate;
    }

    public void setGunToLaser()
    {
        isUsingBullet = false;
    }
}
