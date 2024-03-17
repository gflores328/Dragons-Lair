using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AH_AI : MonoBehaviour
{
    public float speed;
    public GameObject pusher;
    public Rigidbody pusherRB;
    float forceDir;
    float counter;
    public float pusherSpeed;
    Vector3 basePoint;
    public float difficulty;

    private void Awake()
    {
        difficulty = PlayerPrefs.GetFloat("Difficulty");
    }


    // Start is called before the first frame update
    void Start()
    {
        pusher = GameObject.FindGameObjectWithTag("AH_AI");
        pusherRB = GameObject.FindGameObjectWithTag("AH_AI").GetComponent<Rigidbody>();

        //sets difficulty (will be done later via UI choices rather than haard coded)

        //If easy selected on UI choice do the following
        if(difficulty < 0.45f)
        {
            basePoint = new Vector3(0.88f, transform.position.y, 0.85f);
            counter = 0.2f;
        }

        //If intermediate selected on UI choice do the following
        if ((difficulty >= 0.45f) && (difficulty < 1f))
        {
            basePoint = new Vector3(0.88f, transform.position.y, 3.3f);
            counter = 0.7f;
        }

        //If hard selected on UI choice do the following
        if (difficulty == 1f)
        {
            basePoint = new Vector3(0.88f, transform.position.y, 6.65f);
            counter = 0.2f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        counter = 1f * Time.deltaTime; //acts like a timer

        if (pusher.transform.position.z >= -0.5f)
        { //if striker is in its half
            if (counter > 1f)
            { //wait for one second to see if the stiker comes to you or stops, if it does not come then :
                Vector3 newPos = new Vector3(pusher.transform.position.x - 0.5f, pusher.transform.position.y, pusher.transform.position.z + 0.5f);

                //move towards the striker to its position
                transform.position = Vector3.MoveTowards(transform.position, newPos, 4f * Time.deltaTime);
                //4f * Time.deltaTime states time to transit the two positions

            }
            else //if less than 1 second change your x-position based on difficulty, i.e. try to move closer to striker
                transform.position = new Vector3(pusher.transform.position.x * difficulty, transform.position.y, transform.position.z);
        }
        else //if in other half then move towards base position
            transform.position = Vector3.MoveTowards(transform.position, basePoint, 2f * Time.deltaTime);
    }

    
}