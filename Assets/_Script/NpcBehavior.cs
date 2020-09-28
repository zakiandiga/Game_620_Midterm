using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class NpcBehavior : MonoBehaviour
{
    GameObject sign;
    PlayerInput input;
    GameObject dialogue;
    DialogueDisplay dialogueDisplay;
    DialogueHolder dialogueHolder;

    private NPCMode npcMode = NPCMode.notTalking;

    [SerializeField] int myLevel = 0;
    int dialogueHolderIndex = 0;

    public static event Action<NpcBehavior> OnTalkStart; //Announce if the NPC innitiate a talk at Talk()

    void Start()
    {
        dialogueHolderIndex = myLevel;

        sign = transform.Find("Sign").gameObject;

        dialogue = GameObject.Find("Dialogue");
        dialogueDisplay = dialogue.GetComponent<DialogueDisplay>();
        dialogueHolder = GetComponent<DialogueHolder>();
        dialogueHolder = GetComponent<DialogueHolder>();
        input = GetComponent<PlayerInput>();

        DialogueDisplay.OnStartConversation += DisableInput; //Observe if the dialogue ends, enable input
        DialogueDisplay.OnEndtoNothing += EnableInput; //Observe if the dialogue starts, disable input
        RoomState.OnRoomLevelUp += NpcLevelup; //Observe if the room level up
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.gameObject.tag == "Player" && npcMode == NPCMode.notTalking)
        {
            npcMode = NPCMode.readyToTalk;
            sign.SetActive(true);            
            //Debug.Log(gameObject.name + " ready to talk!");            
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.tag == "Player" && npcMode == NPCMode.readyToTalk)
        {
            //Debug.Log(gameObject.name + " player too far to talk");
            npcMode = NPCMode.notTalking;
            sign.SetActive(false);            
        }
    }

    private void Talk()
    {
        //Debug.Log(gameObject.name + " if anybody need my cue, I'm talking now!");
        if (OnTalkStart != null)
        {
            OnTalkStart(this);           
        }

        Debug.Log(dialogueHolderIndex);
        dialogueDisplay.conversaiton = dialogueHolder.conversation[dialogueHolderIndex];
        dialogueDisplay.StartConversation();
        Debug.Log(dialogueHolderIndex);
        input.enabled = false;        
    }


    private void DisableInput(DialogueDisplay d)
    {
        //Debug.Log(gameObject.name + " Alright DialogueManager, we disable our Player Input!");
        input.enabled = false;
    }

    private void EnableInput(DialogueDisplay d)
    {
        //Debug.Log(gameObject.name + " read notif from dialogue manager, we can initiate talk again");
        input.enabled = true;
    }

    public void InteractInput(InputAction.CallbackContext con)
    {
        if (con.started && npcMode == NPCMode.readyToTalk)
        {
            Talk();            
        }
    }

    private void NpcLevelup(RoomState r)
    {
        myLevel += 1;
    }

    public enum NPCMode
    {
        readyToTalk,
        talking,
        notTalking, 
        offended
    }

}
