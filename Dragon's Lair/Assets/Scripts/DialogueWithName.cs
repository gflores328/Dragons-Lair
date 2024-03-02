using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum side { left, right, none};

[System.Serializable]
public struct DialogueAndName
{
    [Tooltip("The dialogue line")]
    [TextArea]
    public string dialogue;
    [Tooltip("The name of who or what said the dialogue line")]
    public string name;
    [Tooltip("The bust for the charachter speaking")]
    public Sprite bust;
    [Tooltip("The side of the textbox that the bust will be on")]
    public side bustSide;
}

[CreateAssetMenu]
public class DialogueWithName : ScriptableObject
{
    public DialogueAndName[] dialogueArray;
}
