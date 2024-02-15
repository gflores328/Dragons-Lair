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
}

[CreateAssetMenu]
public class DialogueWithName : ScriptableObject
{
    public DialogueAndName[] dialogueArray;
}
