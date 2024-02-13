using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextLevelTrigger : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;

    [SerializeField] private string levelToLoad;
    [SerializeField] private GameObject LoadingScreen;
    

    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            Time.timeScale = 0;
            gameManager.LoadSceneAsync(levelToLoad);
        }
        
    }


}
