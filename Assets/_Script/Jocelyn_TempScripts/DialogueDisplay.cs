using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Input System
using System;
//using System.CodeDom;

public class DialogueDisplay : MonoBehaviour
{
    //PlayerInput input; //Input System
    public GameObject nextButton;

    public Conversation conversaiton;
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

    void Start()
    {
        followupQuestion = questionManager.GetComponent<QuestionManager>();

        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();

        speakerUILeft.Speaker = conversaiton.speakerLeft;
        speakerUIRight.Speaker = conversaiton.speakerRight;
    }
    

    
    public void StartConversation() //CALL THIS FROM THE NPC (Observe)
    {        
        Debug.Log("StartConversation() executed");
        if (OnStartConversation != null)
        {
            OnStartConversation(this);
        }
        //activeLineIndex = 0;      
        
        nextButton.SetActive(true);      
        DisplayLine();
        activeLineIndex += 1;       
    }
    

    public void AdvanceConversation() //might be private?
    {
        Debug.Log("AdvanceConversation() executed");
        if (activeLineIndex < conversaiton.lines.Length)
        {
            DisplayLine();
            activeLineIndex += 1;
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
        activeLineIndex = 0;
        nextButton.SetActive(false);
        var ending = conversaiton.endingCon;
        Debug.Log(ending);

        //THE LINES FOR THE FOLLOW UP HANDLING        
        if (ending == Conversation.EndingType.question) //question follow-up
        {
            followupQuestion.questions = conversaiton.question;
            
            Debug.Log("QUESTION TIME! ");
            if (OnEndtoQuestion != null) 
            {
                OnEndtoQuestion(this);
            }            
        }

        if (ending == Conversation.EndingType.nextDialogue) //nextDialogue follow-up
        {
            //WE CAN ONLY PASS TO THE NEXT DIALOGUE BLOCK HERE, NOTHING ELSE
            conversaiton = conversaiton.nextConversation;
            Debug.Log("CONVERSATION CONTINUES! ");
            StartConversation();            
        }

        if (ending == Conversation.EndingType.noFollowup) //Back to movement
        {
            blockNumber = conversaiton.blockNumber;
            Debug.Log("CONVERSATION ENDS! ");
            if (conversaiton.isLeveling) //Announce to Level up the room if the conversation isLeveling true
            {
                if(OnEndLevelUp != null)
                {
                    OnEndLevelUp(this); 
                }
            }

            if (OnEndtoNothing != null)
            {
                OnEndtoNothing(this); //Annaounce NPC and Player input here
            }
        }
    }

    void DisplayLine()
    {
        Line line = conversaiton.lines[activeLineIndex];
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
}
