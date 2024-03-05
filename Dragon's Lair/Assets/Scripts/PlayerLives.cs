using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLives : MonoBehaviour
{
    public static int lives = 3;
    public static event Action OnPlayerDeath;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.tag == "Enemy")
        {
            Destroy(collision.collider.gameObject);
            lives -= 1;
            Destroy(gameObject);
            if(lives > 0)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            else if(lives <= 0)
            {
                OnPlayerDeath?.Invoke();
            }
        }
    }
}
