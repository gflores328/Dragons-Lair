
using System.Collections;
using UnityEngine;


public class AH_Puck : MonoBehaviour
{
    public AH_ScoreManager scoreManager;

    public float speed;

    private Vector3 direction;

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
        yield return new WaitForSeconds(3);

        if (playerStart == true)
        {
            this.direction = new Vector3(0.5f, 0f, 0.5f);
        }
        else
        {
            this.direction = new Vector3(-0.5f, 0f, -0.5f);
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
        this.rb.MovePosition(this.rb.position + speed * Time.fixedDeltaTime * direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AH_Wall"))
        {
            direction.x = -direction.x; 
        }

        if (other.CompareTag("AH_Wall2"))
        {
            direction.z = -direction.z;
        }

        if (other.CompareTag("AH_AI"))
        { 
            direction.z = -direction.z;
        }

        if (other.CompareTag("AH_Player"))
        {
            direction.z = -direction.z;
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
