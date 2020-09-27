using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Input System
using System;
//using System.CodeDom;

public class DialogueDisplay : MonoBehaviour
{
    PlayerInput input; //Input System
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
    public static event Action<DialogueDisplay> OnEndtoDialogue;
    public static event Action<DialogueDisplay> OnEndtoNothing;

    void Start()
    {
        input = GetComponent<PlayerInput>(); //Input System
        followupQuestion = questionManager.GetComponent<QuestionManager>();


        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();

        speakerUILeft.Speaker = conversaiton.speakerLeft;
        speakerUIRight.Speaker = conversaiton.speakerRight;
    }
    
    public void NextSentence(InputAction.CallbackContext con)
    {
        if(con.performed) //IMPORTANT TO MAKE SURE THAT WE SEND 1 INPUT EACH CLICK!!!
        {
            AdvanceConversation();
        }        
    }

    /*void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            
        }
    }*/

    public void StartConversation() //CALL THIS FROM THE NPC (Observe)
    {        
        //Zak add these line
        //set conversaiton based on valuse from npc
        input.enabled = true;
        //OnStartDialogue event announce here
        activeLineIndex = 0;
        DisplayLine();
        activeLineIndex += 1;
    }

    public void AdvanceConversation()
    {
        if (activeLineIndex < conversaiton.lines.Length)
        {
            DisplayLine();
            activeLineIndex += 1;
        }
        else
        {
            //Zak modify this
            EndConversation();

            /* Convo Looper
            speakerUILeft.Hide();
            speakerUIRight.Hide();
            activeLineIndex = 0;
            */
        }
    }
    


    void EndConversation()
    {
        Debug.Log("Conversation End");
        input.enabled = false;
        speakerLeft.SetActive(false);
        speakerRight.SetActive(false);

        //THE LINES FOR FOLLOW UP HANDLING
        
        if (conversaiton.endingType == "question") //How to check with enum instead of string???
        {
            //blockNumber = conversaiton.blockNumber;
            followupQuestion.questions = conversaiton.question;
            
            Debug.Log("We go to question! " + conversaiton.question);
            if (OnEndtoQuestion != null) //we might move this inside each ending type
            {
                OnEndtoQuestion(this);
            }
            //we actually have the follow-up question data here

            //make change to all global parameters that need to be changed before the question (activate quest X, change NPC state, etc)
        }

        if (conversaiton.endingType == "nextDialogue") //How to check with enum instead of string???
        {
            blockNumber = conversaiton.blockNumber;
            conversaiton = conversaiton.nextConversation;
            Debug.Log("We go to next dialog!");
            if (OnEndtoDialogue != null) //we might move this inside each ending type
            {
                OnEndtoDialogue(this);
            }
            //we actually have the follow-up dialogue block data here also
            StartConversation();


            //make change to all global parameters (activate quest X, change NPC state, etc)
        }

        if (conversaiton.endingType == "noFollowup") //How to check with enum instead of string???
        {
            blockNumber = conversaiton.blockNumber;
            Debug.Log("Nothing happen on the foreground!");
            if (OnEndtoNothing != null) //we might move this inside each ending type
            {
                OnEndtoNothing(this);
            }
            //make change to all global parameters if needed, but this endingType meant for random NPC line that have no follow-up (like offended npcs)
            //ANNOUNCE TO ENABLE PLAYER INPUT ON PLAYER AND NPC HERE
        }

        //last thing to execute on EndConversation()


        //follow-up command here
        //OnEndDialogue event announce here

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
