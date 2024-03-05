using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SocialPlatforms.Impl;

public class LivesCounter : MonoBehaviour
{
    public TextMeshProUGUI livesText;
    //public PlayerLives player;

    // Start is called before the first frame update
    void Start()
    {
        livesText = GetComponent<TextMeshProUGUI>();
    }

    // Update is called once per frame
    void Update()
    {
        livesText.text = "Lives: " + PlayerLives.lives; // Displays Life Counter
    }
}
