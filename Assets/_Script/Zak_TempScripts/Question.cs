using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
//public struct QuestionLine
//{
//    [TextArea(2, 5)]
//    public string Question;
//}

[CreateAssetMenu(fileName = "New Question", menuName = "NewQuestion")]
public class Question : ScriptableObject //this suppose to replace the Question.cs
{
    public int questionNumber; //same with Conversation blockNumber!
    [TextArea(2, 5)] public string fact;
    public string correctAnswer;
    public string wrongAnswer;
    public Conversation correctDestinationBlock;
    public Conversation wrongDestinationBlock;
}