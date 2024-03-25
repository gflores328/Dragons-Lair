using UnityEngine;
using UnityEngine.AI;
using System.Collections;
using System;

public class AH_PlayerController : MonoBehaviour
{
    public GameManager gameManager;

    public AH_ScoreManager scoreManager;

    [SerializeField] private Camera mainCamera; //main camera variable

    // You must set the cursor in the inspector.
    public Sprite CustomCursor;
    public Sprite ClickCursor;

    // Direction variables
    private Vector2 center;
    private float minDir;
    Vector3 basePoint;
    public Vector3 direction;

    public float difficulty; //level difficulty 

    public float speed; //puck speed

    public float pusherSpeed; //ai & player speed

    public string mode;

    public AH_Puck puckC; // puck script to control puck movements

    private Transform puck; //puck object

    public  Transform pusherR; // ai object

    public GameObject StartMenu;

    public Rigidbody rb; //player rb

    public Rigidbody rbR; // ai rb

    public bool isPlayer = true; //used to determine if it is player or ai

    public LayerMask layers; // layer mask for table


    private void Awake()
    {
        difficulty = PlayerPrefs.GetFloat("Difficulty");
    }

    public void SetCursor(Sprite sprite, Vector2 center)
    {
        Cursor.SetCursor(CustomCursor.texture, center, CursorMode.Auto);
    }

    void Start()
    {
        rb = GameObject.FindGameObjectWithTag("AH_Player").GetComponent<Rigidbody>();
        rbR = GameObject.FindGameObjectWithTag("AH_AI").GetComponent<Rigidbody>();
        puck = GameObject.FindGameObjectWithTag("AH_Puck").transform;
        pusherR = GameObject.FindGameObjectWithTag("AH_AI").transform;
        Vector2 center = default;
        SetCursor(CustomCursor, center); //starts as open hand cursor


        //sets difficulty (will be done later via UI choices rather than haard coded)

        //If easy selected on UI choice do the following
        if (scoreManager.easy)
        {
            basePoint = new Vector3(0.6f, transform.position.y, 0.6f);
            difficulty = 0.05f;
            speed = 0.5f;
            pusherSpeed = 0.5f;
            mode = "easy";
        }

        //If intermediate selected on UI choice do the following
        if (scoreManager.intm)
        {
            basePoint = new Vector3(0.6f, transform.position.y, 3f);
            difficulty = 0.07f;
            speed = 1.5f;
            pusherSpeed = 1.5f;
            mode = "intermidate";
        }

        //If hard selected on UI choice do the following
        if (scoreManager.hard)
        {
            basePoint = new Vector3(0.6f, transform.position.y, 6f);
            difficulty = 1f;
            speed = 3f;
            pusherSpeed = 3f;
            mode = "hard";
        }

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

        Vector3 newPos = transform.position;
        newPos.x = Mathf.Lerp(transform.position.x, puck.position.x, difficulty);
        //newPos.z = Mathf.Lerp(transform.position.z, puck.position.z, difficulty);

        transform.position = newPos;

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
