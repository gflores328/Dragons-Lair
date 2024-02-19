using UnityEngine;

public class Activation : MonoBehaviour
{
    //public float damage;

    void Start()
    {
       

    }

    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            transform.GetComponent<Animator>().Play("Spike");

            //start damage functions here
        }
    }
}
