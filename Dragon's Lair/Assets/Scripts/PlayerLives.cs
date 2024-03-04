using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLives : MonoBehaviour
{
    public int lives = 3;
    
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
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            lives -= 1;
            if (lives <= 0) { 
                Destroy(gameObject);
            }
            // If Player has Lives over 0
                // Insert Code for Level Reset Here
            // Else
                // Trigger Game Over Screen
        }
    }
}
