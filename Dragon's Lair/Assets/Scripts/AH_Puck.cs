
using UnityEngine;


public class AH_Puck : MonoBehaviour
{
    public Rigidbody myRB;
    public float speed = 1.0f;

    // Start is called before the first frame update
    void Start()
    {
        myRB = GetComponentInChildren<Rigidbody>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        myRB.AddForce(speed, 0, 0, ForceMode.Acceleration);
    }
}
