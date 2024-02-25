using UnityEngine;

public class AH_Controller : MonoBehaviour
{
    //this script controls the movement for the players in the Air Hockey Game

    private Rigidbody rb;

    public float speed = 5;
    public float minDist = 0;
    public float maxDist = 5;
    public LayerMask layers;
    


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
