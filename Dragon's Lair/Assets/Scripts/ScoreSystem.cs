/*
 * Created by Carlos Martinez
 * 
 * This script contains the score system for Mobile Fighter Axiom.
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    public static int scoreValue = 0; // Score is set to 0
    public TextMeshProUGUI score;

    // Start is called before the first frame update
    void Start()
    {
        score = GetComponent<TextMeshProUGUI> ();
    }

    // Update is called once per frame
    void Update()
    {
        score.text = "Score: " + scoreValue; // Displays Score Text
    }
}
