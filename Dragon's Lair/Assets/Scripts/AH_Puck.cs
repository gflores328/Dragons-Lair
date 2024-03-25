
using System.Collections;
using UnityEngine;


public class AH_Puck : MonoBehaviour
{
    public AH_ScoreManager scoreManager;
    public AH_PlayerController playerController;

    public float pSpeed;

    public float minDir = 0.5f;

    public Vector3 direction;

    private Rigidbody rb;

    public bool playerStart = true; 

    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.direction = new Vector3(0.5f, 0f, 0.5f);

        StartCoroutine(Launch());
        
    }

    public IEnumerator Launch()
    {
        Restart();
        yield return new WaitForSeconds(1);

        float signX = Mathf.Sign(Random.Range(-1f, 1f));
        float signZ = Mathf.Sign(Random.Range(-1f, 1f));

        if (playerStart == true)
        {
            this.direction = new Vector3(0.5f * signX, 0f, 0.5f);
        }
        else
        {
            this.direction = new Vector3(0.5f * signZ, 0f, -0.5f);
        }
    }

    private void Restart()
    {
        rb.velocity = new Vector3(0f, 0, 0);
        transform.position = new Vector3(0f, 0f, 0f);
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += direction * speed * Time.deltaTime; //alternate to movement in fixed update
    }

    // FixedUpdated is caled on every frame
    void FixedUpdate()
    {
        this.rb.MovePosition(this.rb.position + pSpeed * Time.fixedDeltaTime * direction);
    }

    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AH_Wall"))
        {
            direction.x = -direction.x; 
        }

        if (other.CompareTag("AH_Wall2"))
        {
            direction.z = -direction.z;
        }

        if (other.CompareTag("AH_Player"))
        {
            //direction.z = -direction.z;
            Vector3 newDirection = (transform.position - other.transform.position).normalized;

            newDirection.x = Mathf.Sign(newDirection.x) * Mathf.Max(Mathf.Abs(newDirection.x), this.minDir);
            newDirection.z = Mathf.Sign(newDirection.z) * Mathf.Max(Mathf.Abs(newDirection.z), this.minDir);

            direction = newDirection;
        }

        if (other.CompareTag("AH_AI"))
        {
            //direction.z = -direction.z;
            Vector3 newDirection = (transform.position - other.transform.position).normalized;

            newDirection.x = Mathf.Sign(newDirection.x) * Mathf.Max(Mathf.Abs(newDirection.x), this.minDir);
            newDirection.z = Mathf.Sign(newDirection.z) * Mathf.Max(Mathf.Abs(newDirection.z), this.minDir);

            direction = newDirection;
        }

        if (other.CompareTag("AH_PlayerGoal"))
        {
            scoreManager.PlayerGoal();
            
            playerStart = false;
            StartCoroutine(Launch());
        }

        if (other.CompareTag("AH_AIGoal"))
        {
            scoreManager.AIGoal();
            
            playerStart = true;

            StartCoroutine(Launch());
        }
    }

}
