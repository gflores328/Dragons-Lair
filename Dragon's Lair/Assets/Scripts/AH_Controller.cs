using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class AH_PlayerController : MonoBehaviour
{
    public AH_Puck puckC;

    private Transform puck;

    public AH_AI ai;

    // You must set the cursor in the inspector.
    public Sprite CustomCursor;
    public Sprite ClickCursor;

    public Rigidbody rb;

    public bool isPlayer = true;

    public float speed = 5;

    public LayerMask layers;

    public Vector3 direction;

    [SerializeField] private Camera mainCamera;

    private Vector2 center;
    private float minDir;

    public float offset = 0.6f;


    public void SetCursor(Sprite sprite, Vector2 center)
    {
        Cursor.SetCursor(CustomCursor.texture, center, CursorMode.Auto);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        puck = GameObject.FindGameObjectWithTag("AH_Puck").transform;
        Vector2 center = default;
        SetCursor(CustomCursor, center); //starts as open hand cursor
    }

    void FixedUpdate()
    {
        if (this.isPlayer)
        {
            MoveByPlayer();
        }
        else
        {
            MoveByComputer();
        }

    }


    public void MoveByPlayer()
    {
        // Paddle will only move if we hold down the mouse button
        Ray paddleGrabed = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(paddleGrabed, out RaycastHit raycastHit, float.MaxValue, layers))
        {
            //change cursor to closed hand
            Cursor.SetCursor(ClickCursor.texture, center, CursorMode.Auto);

            transform.position = raycastHit.point; //moves puck where mouse is

        }
        else
        {
            //change cursor to open hand
            Cursor.SetCursor(CustomCursor.texture, center, CursorMode.Auto);

            rb.velocity = Vector3.zero;
        }
    }

    public void MoveByComputer()
    {
        /*if (puck.position.z < transform.position.z + offset)
        {
            rb.velocity = Vector3.right * speed;

        }


        else if (puck.position.z > transform.position.z - offset)
        {
            rb.velocity = Vector3.left * speed;

        }
        else
        {
            rb.velocity = Vector3.zero;
        }*/
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AH_Wall"))
        {
            Vector3 newDirection = (transform.position - other.transform.position).normalized;

            newDirection.x = Mathf.Sign(newDirection.x) * Mathf.Max(Mathf.Abs(newDirection.x), this.minDir);
            newDirection.z = Mathf.Sign(newDirection.z) * Mathf.Max(Mathf.Abs(newDirection.z), this.minDir);

            direction = newDirection;
        }

        if (other.CompareTag("AH_Wall2"))
        {
            Vector3 newDirection = (transform.position - other.transform.position).normalized;

            newDirection.x = Mathf.Sign(newDirection.x) * Mathf.Max(Mathf.Abs(newDirection.x), this.minDir);
            newDirection.z = Mathf.Sign(newDirection.z) * Mathf.Max(Mathf.Abs(newDirection.z), this.minDir);

            direction = newDirection;
        }
    }

}
