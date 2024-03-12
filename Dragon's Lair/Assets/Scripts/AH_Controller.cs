using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class AH_PlayerController : MonoBehaviour
{

    public Rigidbody rb;

    public float speed = 5;
    public float minDist = 0;
    public float maxDist = 5;
    public LayerMask layers;

    [SerializeField] private Camera mainCamera;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Paddle will only move if we hold down the mouse button
        Ray paddleGrabed = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(paddleGrabed, out RaycastHit raycastHit, float.MaxValue, layers))
        {
            transform.position = raycastHit.point;
        }
    }

    void FixedUpdate()
    {


    }

}
