using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using System;

public class ButtonControl : MonoBehaviour
{
    //public GameObject dialogueBox;
    public GameObject continueButton;
    PlayerInput input;
    Button button;


    void Start()
    {
        button = continueButton.GetComponent<Button>();
        input = GetComponent<PlayerInput>();
    //    NpcBehavior.OnTalkStart += EnableButton;
        //button.onClick.AddListener(DisplayNext);
    }

    //void EnableButton(NpcBehavior npc)
    //{
    //    Debug.Log("continue Button input enabled");
    //    input.enabled = false;
    //}

    //void DisableButton()
    
    // Update is called once per frame
    void DisplayNext()
    {
        Debug.Log(continueButton + " is clicked");
        button.onClick.Invoke();
        
    }

    //when InteractInput is pressed, display next sentence
    public void InputNext(InputAction.CallbackContext con)
    {
        DisplayNext();
                
    }


}
