using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Choice
{
    [TextArea(2, 5)]
    public string choiceText;

    public Conversation conversation;
}
[CreateAssetMenu(fileName = "New Question", menuName = "Question")]
public class Question : ScriptableObject
{
    public Choice[] choices;
        
}
