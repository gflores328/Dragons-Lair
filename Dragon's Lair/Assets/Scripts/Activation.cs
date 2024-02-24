﻿using System.Collections;
using UnityEngine;

public class Activation : MonoBehaviour
{
    //public float damage;

    private Animator anim; //animator variable

    public float waitTime; //wait time between actiations so you dont get hit over and over

    private IEnumerator coroutine; //couroutine is used to add wait time between actavation

    void Start()
    {
        //initalization of animator and setup of couroutine with wait time
        anim = GetComponent<Animator>(); 
        coroutine = WaitTime(waitTime);
        StartCoroutine(coroutine);
    }

    void Update()
    {
        
    }

    //if player enters collider play animation then wait before playing again
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            
            anim.SetTrigger("onActive");
            coroutine = WaitTime(waitTime);
            StartCoroutine(coroutine);

        }
    }

    //if player exits collider stop animation playback
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            anim.StopPlayback();
        }
    }

    //wait time function
    private IEnumerator WaitTime(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        
    }
}
