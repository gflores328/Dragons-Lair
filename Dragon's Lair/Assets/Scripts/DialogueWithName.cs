using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}

[CreateAssetMenu]
public class DialogueWithName : ScriptableObject
{
    public DialogueAndName[] dialogueArray;
}
