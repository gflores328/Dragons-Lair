/*
 * Created by Carlos Martinez
 * 
 * This script contains code to display the prize remaining counter.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PrizeRemaining : MonoBehaviour
{
    public int prizeValue; // Prize Counter
    public TextMeshProUGUI prizeCounter; // Prize Counter Text
    public GameObject clawMovement;

    // Start is called before the first frame update
    void Start()
    {
        prizeCounter = GetComponent<TextMeshProUGUI>(); // Calls Text Component
    }

    // Update is called once per frame
    void Update()
    {
        prizeValue = clawMovement.GetComponent<ClawMovement>().prizeRemaining;
        prizeCounter.text = prizeValue.ToString("0"); // Converts Prize Remaining into a String Displayed as a Whole Number
    }
}
