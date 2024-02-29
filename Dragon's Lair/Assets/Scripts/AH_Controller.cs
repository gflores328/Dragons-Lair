using UnityEngine;

public class AH_Controller : MonoBehaviour
{
    //this script controls the movement for the players in the Air Hockey Game

    public float speed = 15;
    public float minDist = 0;
    public float maxDist = 10;
    public LayerMask layers;
    public Rigidbody myRigidBody;
    
    [SerializeField] private Camera mainCamera;


    // Start is called before the first frame update
    void Start()
    {
        myRigidBody.GetComponent<Rigidbody>();
    }

    private void Awake()
    {
        

    }

    // Update is called once per frame
    void Update()
    {


        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit,layers))
        {
            transform.position = hit.point;
            myRigidBody.AddRelativeForce(Vector3.up * speed * Time.deltaTime);

            
        }

    }

}
