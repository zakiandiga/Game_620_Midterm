using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable]
public struct Line
{
    public Character character;

    [TextArea(2, 5)]

    public string text;
}

[CreateAssetMenu(fileName = "New Conversation", menuName = "Conversation")]
public class Conversation : ScriptableObject
{
    //Zak added these lines
    public int blockNumber;
    public string endingType;
    //public EndingType endingType;
    //public enum EndingType { question, quest, noFollowup }
    //End line

    public Character speakerLeft;
    public Character speakerRight;
    public Line[] lines;
}
