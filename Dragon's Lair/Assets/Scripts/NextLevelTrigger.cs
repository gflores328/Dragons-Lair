using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private string levelToLoad;

    

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            gameManager.LoadLevelbyName(levelToLoad);
        }
        
    }


}
