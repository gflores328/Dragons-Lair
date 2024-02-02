using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_PlayerMovement : MonoBehaviour
{
    public float horizontalInput;
    public float verticalInput;
    public float moveSpeed = 15;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        transform.Translate(Vector3.forward * Time.deltaTime * verticalInput * moveSpeed);
        transform.Translate(Vector3.right * Time.deltaTime * horizontalInput * moveSpeed);

    }
}
