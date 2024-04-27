/*
 * Created by Carlos Martinez
 * 
 * This script contains the code of a timer for the claw game.
 * 
 */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClawTimer : MonoBehaviour
{
    float currentTime = 0f;
    float startingTime = 30f;

    public TextMeshProUGUI CountdownText;
    
    // Start is called before the first frame update
    void Start()
    {
        currentTime = startingTime;
    }

    // Update is called once per frame
    void Update()
    {
        currentTime -= 1 * Time.deltaTime;
        CountdownText.text = currentTime.ToString("0");

        if (currentTime <= 0)
        {
            currentTime = 0;
        }
    }
}
