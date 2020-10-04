using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using System;

public class ButtonControl : MonoBehaviour
{
    //public GameObject dialogueBox;
    public GameObject questionArea;//, dialogue;
    public GameObject nextLine, correct, wrong;
    Button buttonNextLine, buttonCorrect, buttonWrong;

    PlayerInput input;


    void Start()
    {
        DialogueDisplay.OnAdvanceConvo += OnDialogueStart;
        //DialogueDisplay.OnEndtoQuestion += OnDialogueEnd;
        DialogueDisplay.OnEndtoNothing += OnDialogueEnd;
        QuestionManager.OnQuestionStart += OnQuestionStart;
        QuestionManager.OnAnswerSelected += OnQuestionEnd;
        input = GetComponent<PlayerInput>();
        buttonNextLine = nextLine.GetComponent<Button>();
        buttonCorrect = correct.GetComponent<Button>();
        buttonWrong = wrong.GetComponent<Button>();

        //    NpcBehavior.OnTalkStart += EnableButton;
        //button.onClick.AddListener(DisplayNext);
    }

    private void OnDialogueStart(DialogueDisplay d)
    {
        //Debug.Log("OnDialogueStart() called");
        input.enabled = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(nextLine);
    }

    private void OnDialogueEnd(DialogueDisplay d)
    {
        //Debug.Log("OnDialogueEnd() called");
        input.enabled = false;
    }

    private void OnQuestionStart(QuestionManager q)
    {
        Invoke("QuestionStart", 0.8f);

    }

    private void QuestionStart()
    {
        input.enabled = true;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(correct);
        //Debug.Log("OnQuestionStart() called");
    }

    private void OnQuestionEnd(QuestionManager q)
    {
        //input.enabled = false;
    }
    
    //public void OnNavigate(InputAction.CallbackContext con)
    //{

    //}

    void Update()
    {

            

    }


}
