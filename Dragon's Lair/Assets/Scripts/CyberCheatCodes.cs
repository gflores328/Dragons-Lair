using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CyberCheatCodes : MonoBehaviour
{
    public GameObject endOfLevelPoint;
    
    private ChibiPlayerMovement chibiPlayerMovement;
    private bool infiniteHealth = false;

    public GameObject player;

    void Start() 
    {
        chibiPlayerMovement = FindObjectOfType<ChibiPlayerMovement>();
    }
    
    
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1)) 
        {
            infiniteHealth = !infiniteHealth;
        }
        
        if(Input.GetKeyDown(KeyCode.Alpha2)) 
        {
            if (endOfLevelPoint != null) // Check if endOfLevelPoint is not null
            {
                player.transform.position = endOfLevelPoint.transform.position; // 
            }
        }
        
        if(infiniteHealth)
        {
            ChibiPlayerMovement.playerHealth = chibiPlayerMovement.maxHealth;
        }
    }
}
