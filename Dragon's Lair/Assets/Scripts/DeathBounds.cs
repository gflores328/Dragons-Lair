using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathBounds : MonoBehaviour
{

    [SerializeField] private string levelToLoad;

    [SerializeField] private GameManager gameManager;

    void OnTriggerEnter(Collider other)
    {

        if(other.tag == "Player")
        {
            gameManager.LoadLevelbyName(levelToLoad);
            
        }
        
    }
}
