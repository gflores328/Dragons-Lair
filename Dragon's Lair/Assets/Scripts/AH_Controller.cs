using UnityEngine;

public class AH_Controller : MonoBehaviour
{
    //this script controls the movement for the players in the Air Hockey Game

    public float speed = 15; //speed the object can move
    public float minDist = 0; //minimum distance
    public float maxDist = 10; //max distance
    public LayerMask layers;  //layerMask to have raycast go directly to ground
   
    
    [SerializeField] private Camera mainCamera;  //main camera object


    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void Awake()
    {
        

    }

    // Update is called once per frame
    void Update()
    {

        //casts a ray to the location of the mouse
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit,maxDist, layers))
        {
            transform.position = hit.point;
            

            
        }

    }

}
