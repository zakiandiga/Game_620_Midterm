﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Input System
using System;

public class DialogueDisplay : MonoBehaviour
{
    //PlayerInput input; //Input System
    public GameObject nextButton;
    public GameObject dialogue;

    public Conversation conversation;
    public int blockNumber; //get this number from Conversation

    public GameObject speakerLeft;
    public GameObject speakerRight;
    public GameObject questionManager;

    QuestionManager followupQuestion;

    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;

    private int activeLineIndex = 0;

    public static event Action<DialogueDisplay> OnEndtoQuestion;    
    public static event Action<DialogueDisplay> OnEndtoNothing;
    public static event Action<DialogueDisplay> OnStartConversation;
    public static event Action<DialogueDisplay> OnEndLevelUp;
    public static event Action<DialogueDisplay> OnAdvanceConvo;
    public static event Action<DialogueDisplay> OnQuestStart;
    public static event Action<DialogueDisplay> OnQuestCheck;
    public static event Action<DialogueDisplay> OnOffendedCheck;
    public static event Action<DialogueDisplay> OnGoodEnding;
    public static event Action<DialogueDisplay> OnBadEnding;

    public static event Action<bool> QuestStatus; //Added by Drew

    //public ButtonControl buttonControl;

    void Start()
    {
        followupQuestion = questionManager.GetComponent<QuestionManager>();

        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();        
    }
    

    
    public void StartConversation() //CALL THIS FROM THE NPC (Observe)
    {   
        if (OnStartConversation != null)
        {
            OnStartConversation(this);
        }
        activeLineIndex = 0; //Set the current/assigned dialogue block index to 0
        //SetDialogue();

        speakerUILeft.Speaker = conversation.speakerLeft; //Set the speaker UI
        speakerUIRight.Speaker = conversation.speakerRight; //Set the speaker UI


        nextButton.SetActive(true);
        if (OnAdvanceConvo != null)
        {
            OnAdvanceConvo(this);
        }
        DisplayLine();
        activeLineIndex += 1;       
    }
    

    public void AdvanceConversation()
    {
        if (activeLineIndex < conversation.lines.Length)
        {
            DisplayLine();
            activeLineIndex += 1;
            if(OnAdvanceConvo != null)
            {
                OnAdvanceConvo(this);
            }

        }
        else
        {            
            EndConversation();
        }
    }
    


    void EndConversation()
    {
        speakerLeft.SetActive(false);
        speakerRight.SetActive(false);        
        nextButton.SetActive(false);
        var ending = conversation.endingCon;
        //Debug.Log(ending);

        //THE LINES FOR THE FOLLOW UP HANDLING        
        if (ending == Conversation.EndingType.question) //question follow-up
        {
            followupQuestion.questions = conversation.question;
            
            //Debug.Log("QUESTION TIME! ");
            if (OnEndtoQuestion != null) 
            {
                OnEndtoQuestion(this);
            }            
        }

        if (ending == Conversation.EndingType.nextDialogue) //nextDialogue follow-up
        {
            //WE CAN ONLY PASS TO THE NEXT DIALOGUE BLOCK HERE, NOTHING ELSE
            conversation = conversation.nextConversation;
            //Debug.Log("CONVERSATION CONTINUES! ");
            StartConversation();            
        }

        if (ending == Conversation.EndingType.noFollowup) //Back to movement
        {
            blockNumber = conversation.blockNumber;
            //Debug.Log("CONVERSATION ENDS! ");

            if (conversation.isQuestCheck)
            {
                if (OnQuestCheck != null)
                {
                    OnQuestCheck(this);
                }

                if (conversation.isOffending)
                {
                    if (OnOffendedCheck != null)
                    {
                        OnOffendedCheck(this);
                    }
                }
            }

            

            if (conversation.isLeveling) //Announce to Level up the room if the conversation isLeveling true
            {
                if(conversation.isQuest)
                {
                    if (OnQuestStart != null)
                    {
                        OnQuestStart(this);
                    }
                }

                

                if(OnEndLevelUp != null)
                {
                    OnEndLevelUp(this); 
                }
            }

            if (OnEndtoNothing != null)
            {
                OnEndtoNothing(this); //Annaounce NPC and Player input here
            }

            if (conversation.isGoodEnding)
            {
                if (OnGoodEnding != null)
                {
                    OnGoodEnding(this);
                }
                    
            }
            if(conversation.isBadEnding)
            {
                if(OnBadEnding != null)
                {
                    OnBadEnding(this);
                }
            }
        }
    }

    void DisplayLine()
    {
        Line line = conversation.lines[activeLineIndex];
        Character character = line.character;

        if (speakerUILeft.SpeakerIs(character))
        {
            SetDialogue(speakerUILeft, speakerUIRight, line.text);
        }
        if (speakerUIRight.SpeakerIs(character))
        {
            SetDialogue(speakerUIRight, speakerUILeft, line.text);
        }
    }

    void SetDialogue(SpeakerUI activeSpeakerUI, 
                        SpeakerUI inactiveSpeakerUI,
                        string text)
    {
        activeSpeakerUI.Dialogue = text;
        activeSpeakerUI.Show();
        inactiveSpeakerUI.Hide();
    }

    private void UpdateQuestStatus() //Added by Drew
    {
        if (conversation.isQuest == true && QuestStatus != null)
        {
            QuestStatus(conversation.isQuest);
        }
    }
}
