using System.Collections;
using UnityEngine;

public class Activation : MonoBehaviour
{
    //public float damage;

    private Animator anim;

    public float waitTime;

    private IEnumerator coroutine;

    void Start()
    {

        anim = GetComponent<Animator>();
        coroutine = WaitTime(waitTime);
        StartCoroutine(coroutine);
    }

    void Update()
    {
        
    }


    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            anim.SetTrigger("onActive");
            coroutine = WaitTime(waitTime);
            StartCoroutine(coroutine);

        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.StopPlayback();
        }
    }

    private IEnumerator WaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
    }
}
