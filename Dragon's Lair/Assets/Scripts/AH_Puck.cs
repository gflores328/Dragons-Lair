
using UnityEngine;


public class AH_Puck : MonoBehaviour
{
    public float speed;

    private Vector3 direction;

    private Rigidbody rb;


    // Start is called before the first frame update
    void Start()
    {
        this.rb = GetComponent<Rigidbody>();
        this.direction = new Vector3(0.5f, 0f, 0.5f);
        
    }

    // Update is called once per frame
    void Update()
    {
        //transform.position += direction * speed * Time.deltaTime;
    }

    void FixedUpdate()
    {
        this.rb.MovePosition(this.rb.position + speed * Time.fixedDeltaTime * direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AH_Wall"))
        {
            if (direction.z > 0)
            {
                direction.z = -direction.z;
            }
            else
            {
                direction.z = Mathf.Abs(direction.z); //if negative number it becomes positive
            }

            if (direction.z < 0)
            {
                direction.z = Mathf.Abs(direction.z); //if negative number it becomes positive
                
            }
            else
            {
                direction.z = -direction.z;
            }

            if (direction.y > 0)
            {
                direction.y = -direction.y;
            }
            else
            {
                direction.y = Mathf.Abs(direction.y); //if negative number it becomes positive
            }

            if (direction.y < 0)
            {
                direction.y = Mathf.Abs(direction.y); //if negative number it becomes positive
                
            }
            else
            {
                direction.y = -direction.y;
            }

        }

        if (other.CompareTag("AH_Player"))
        {

            direction.x = -direction.x;

        }

        if (other.CompareTag("AH_PlayerGoal"))
        {


        }

        if (other.CompareTag("AH_AIGoal"))
        {


        }
    }
}
