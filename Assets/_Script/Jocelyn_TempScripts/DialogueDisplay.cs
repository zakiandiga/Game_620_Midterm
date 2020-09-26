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
    private SpeakerUI SpeakerUIRight;

    private int activeLineIndex = 0;

    void Start()
    {
        input = GetComponent<PlayerInput>(); //Input System

        speakerUILeft = speakerLeft.GetComponent<SpeakerUI>();
        SpeakerUIRight = speakerRight.GetComponent<SpeakerUI>();

        speakerUILeft.Speaker = conversaiton.speakerLeft;
        SpeakerUIRight.Speaker = conversaiton.speakerRight;
    }
    
    public void NextSentence(InputAction.CallbackContext con)
    {
        float submitInput;
        submitInput = con.ReadValue<float>();
        if(submitInput == 1 )
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

    void AdvanceConversation()
    {
        if (activeLineIndex < conversaiton.lines.Length)
        {
            DisplayLine();
            activeLineIndex += 1;
        }
        else
        {
            speakerUILeft.Hide();
            SpeakerUIRight.Hide();
            activeLineIndex = 0;
        }
    }

    void DisplayLine()
    {
        Line line = conversaiton.lines[activeLineIndex];
        Character character = line.character;

        if (speakerUILeft.SpeakerIs(character))
        {
            SetDialogue(speakerUILeft, SpeakerUIRight, line.text);
        }
        else
        {
            SetDialogue(SpeakerUIRight, speakerUILeft, line.text);
        }
    }

    void SetDialogue(
        SpeakerUI activeSpeakerUI,
        SpeakerUI inactiveSpeakerUI,
        string text)
    {
        activeSpeakerUI.Dialogue = text;
        activeSpeakerUI.Show();
        inactiveSpeakerUI.Hide();

    }

}
