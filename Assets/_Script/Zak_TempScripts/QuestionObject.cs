using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct QuestionLine
{
    [TextArea(2,5)]
    public string Question
}

[CreateAssetMenu(filename = "New Question", menuName = "NewQuestion")]
public class QuestionObject
