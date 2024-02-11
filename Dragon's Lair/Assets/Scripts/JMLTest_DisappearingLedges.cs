using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JMLTest_DisappearingLedges : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    //[SerializeField] private int speed;
    [SerializeField] private float timeToDisappear;
    [SerializeField] private float timeToReappear;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            StartCoroutine(Vanish());
            Debug.Log("player on");
        }
    }
        // Update is called once per frame
        void Update()
    {
        //StartCoroutine(Vanish());  //makes platform vanish on its own without player interaction
    }

    IEnumerator Vanish()
    {
        yield return new WaitForSeconds(timeToDisappear);
        obj.SetActive(false);
        gameObject.GetComponent<BoxCollider>().enabled = false;

        yield return new WaitForSeconds(timeToReappear);
        obj.SetActive(true);
        gameObject.GetComponent<BoxCollider>().enabled = true;
    }

}