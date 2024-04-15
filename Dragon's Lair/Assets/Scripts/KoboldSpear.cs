using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KoboldSpear : MonoBehaviour
{
    
    public KoboldController koboldController;
    private bool successfulHit = false;

    
    
    private void Update()
    {
        
        koboldController.SetHitPlayer(successfulHit);
        
    
    }
   
    private void OnTriggerEnter(Collider collider)
    {
        if(collider.CompareTag("Player") && !successfulHit)
        {
            successfulHit = true;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        if(collider.CompareTag("Player"))
        {
            successfulHit = false;
        }
    }
}
