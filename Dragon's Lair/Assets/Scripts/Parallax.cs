using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallax : MonoBehaviour
{

    public GameObject mainCamera;

    public float parallaxEffect;

    private float length, startPos;

   
    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float temp = (mainCamera.transform.position.x * ( 1 - parallaxEffect));
        float distance = (mainCamera.transform.position.x * parallaxEffect);

        transform.position = new Vector3(startPos + distance, transform.position.y, transform.position.z);
        if(temp > startPos + length)

        {
            startPos += length;


        }

        else if (temp < startPos - length)
        
        {
            startPos-= length;
        }
    }
}
