using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingDebris : MonoBehaviour
{
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
        if (collision.gameObject.tag == "Player")
        {
            collision.gameObject.GetComponent<ChibiPlayerMovement>().takeDamage(1);

            Destroy(gameObject);
        }
        if (collision.gameObject.tag == "Enemy")
        {

            Destroy(gameObject);
        }

        if (collision.gameObject.layer == 7)
        {
            // Play crumble animation
            //Wait for animation finish 

            Destroy(gameObject);
        }
    }
}
