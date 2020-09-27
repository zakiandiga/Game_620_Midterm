using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Line
{
    public Character character;
    //public Question _question;
    //public Conversation nextConversation;

    [TextArea(2, 5)]

    public string text;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    //Zak added these lines
    public int blockNumber;
    public string endingType;
    public bool isQuest;
    //public EndingType endingType;
    //public enum EndingType { question, nextDialogue, noFollowup }
    public Question question;
    public Conversation nextConversation;
    //End line

    public Character speakerLeft;
    public Character speakerRight;
    public Line[] lines;

    
}
