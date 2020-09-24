using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class UiController : MonoBehaviour
{
    public DialogueManager dialogueManager;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void NextSentence()
    {
        Debug.Log("NextSentence() called");
        dialogueManager.DisplayNextSentence();
        
    }

    public void TestButtonPressed()
    {
        Debug.Log("Clicking the TEST BUTTON");

    }
}
