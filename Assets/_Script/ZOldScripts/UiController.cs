using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using System;


public class UiController : MonoBehaviour
{
    public DialogueManager dialogueManager;
    public GameObject choiceMenu;

    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void OnChoices()
    {
        Debug.Log("We are on the OnChoices() function");
    }
    
    public void ButtonClick(InputAction.CallbackContext con)
    {
        EventSystem.current.SetSelectedGameObject(null);

    }

    public void NextSentence()
    {
        Debug.Log("NextSentence() called");
        dialogueManager.DisplayNextSentence();
        
    }

    public void TestButtonPressed()  //this should be called by the conversation that requires player to answer
    {
        
        if (!choiceMenu.activeInHierarchy)
        {
            Debug.Log("Clicking the TEST BUTTON");
            choiceMenu.SetActive(true);
            OnChoices();
        }
        else
        {
            choiceMenu.SetActive(false);
        }
    }

    public void ButtonClickDebugger()
    {
        Debug.Log("Yes, this button is clicked");
    }


}
