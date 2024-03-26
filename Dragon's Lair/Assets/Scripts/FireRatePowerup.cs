using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerup : MonoBehaviour
{
    public float fireRateSetter; 
    public GameObject powerUpDesc; 
    public Renderer powerUpRenderer; // Reference to the renderer component

    private GunController gunController; 
    private bool showDesc = false;
    private float waitTime = 5f;
    private Coroutine coroutine;

    void Awake()
    {
        gunController = FindObjectOfType<GunController>(); 
        powerUpRenderer = GetComponent<Renderer>(); // Grab the renderer component
    }

    void Update()
    {
        if (showDesc && coroutine == null)
        {
            powerUpDesc.SetActive(true);
            coroutine = StartCoroutine(WaitAndDisable(waitTime));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            showDesc = true;
            gunController.setFireRate(fireRateSetter);
            powerUpRenderer.enabled = false; // Turn off the renderer when picked up
        }
    }

    private IEnumerator WaitAndDisable(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        showDesc = false;
        powerUpDesc.SetActive(false);
        Destroy(gameObject);
    }
}
