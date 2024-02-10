using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JMLTest_DisappearingLedges : MonoBehaviour
{
    [SerializeField] private GameObject obj;
    //[SerializeField] private int speed;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnTriggerEnter()
    {
        StartCoroutine(Vanish());
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Vanish());  //makes platform vanish on its own without player interaction
    }

    IEnumerator Vanish()
    {
        for (; ; )
        {
            obj.SetActive(true);
            yield return new WaitForSeconds(5);

            obj.SetActive(false);
            yield return new WaitForSeconds(2);

        }
    }
}