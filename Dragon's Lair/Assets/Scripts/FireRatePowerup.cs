using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRatePowerup : MonoBehaviour
{
    public float fireRateSetter; 
    public GameObject powerUpDesc; 
    
    private GunController gunController; 
    private bool showDesc = false;
    private float waitTime = 5f;
    private IEnumerator coroutine;

    void Awake()
    {
        gunController = FindObjectOfType<GunController>(); 
        coroutine = WaitAndDisable(waitTime);
    }

    void Update()
    {
        if (showDesc)
        {
            powerUpDesc.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {   
            showDesc = true;
            gunController.setFireRate(fireRateSetter);
            StartCoroutine(coroutine);
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

