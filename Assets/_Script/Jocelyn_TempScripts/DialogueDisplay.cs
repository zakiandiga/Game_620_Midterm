using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; //Input System

public class DialogueDisplay : MonoBehaviour
{
    PlayerInput input; //Input System
    public Conversation conversaiton;

    public GameObject speakerLeft;
    public GameObject speakerRight;

    private SpeakerUI speakerUILeft;
    private SpeakerUI speakerUIRight;

    private int activeLineIndex = 0;

    void Start()
    {
        input = GetComponent<PlayerInput>(); //Input System

        

        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        speakerUIRight = speakerRight.GetComponent<SpeakerUI>();

        speakerUILeft.Speaker = conversaiton.speakerLeft;
        speakerUIRight.Speaker = conversaiton.speakerRight;
        Debug.Log(conversaiton.lines[0]);
    }
    
    public void NextSentence(InputAction.CallbackContext con)
    {

        if(con.performed)
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
        input.enabled = true;
        //OnStartDialogue event announce here
        activeLineIndex = 0;
        DisplayLine();
        activeLineIndex += 1;
        Debug.Log("On start convo, ALI is " + activeLineIndex);
    }

    public void AdvanceConversation()
    {
        if (activeLineIndex < conversaiton.lines.Length)
        {
            DisplayLine();
            activeLineIndex += 1;
            Debug.Log("Current ALI is " + activeLineIndex);
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
