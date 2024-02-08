using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[CreateAssetMenu]
public class Dialogue : ScriptableObject
{
    [Header("Dialouge")]
    [Tooltip("The number of lines of dialogue and the text that is in them")]
    [TextArea]
    public string[] dialogue;
}

