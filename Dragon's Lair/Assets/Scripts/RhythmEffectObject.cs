/*
 * CREATED BY: Trevor Minarik
 * 
 * LAST MODIFIED BY: Trevor Minarik
 * LAST MODIFIED ON: Feb 16, 2024 at 10:12 AM
 * 
 * TUTORIAL FOLLOWED: How To Make a Rhythm Game #4 - Timing Hits For Better Score https://www.youtube.com/watch?v=Oi0tT7QnFhs
 * 
 * Manages the lifetime for particle effects
 */
 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RhythmEffectObject : MonoBehaviour
{
    [Tooltip("How long the effect will last. Default is one second.")]
    public float lifetime;

    // Start is called before the first frame update
    void Start()
    {
        //Set the lifetime to the default value
        lifetime = 1f;
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy the effect after the given lifetime
        Destroy(gameObject, lifetime);
    }
}
