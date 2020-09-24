using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class NpcBehavior : MonoBehaviour
{
    GameObject sign;
    //public Dialogue dialogue;
    PlayerInput input;
    DialogueTrigger dialogueTrigger;
    private NPCMode npcMode = NPCMode.notTalking;

    public static event Action<NpcBehavior> OnTalkStart; //Announce if the NPC innitiate a talk at Talk()

    void Start()
    {
        sign = transform.Find("Sign").gameObject;
        input = GetComponent<PlayerInput>();
        DialogueManager.OnEndDialogue += EnableInput; //Observe if the dialogue ends, enable input
        DialogueManager.OnStartDialogue += DisableInput; //Observe if the dialogue starts, disable input
        dialogueTrigger = GetComponent<DialogueTrigger>(); //this GameObject's dialogue trigger
        
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.gameObject.tag == "Player" && npcMode == NPCMode.notTalking)
        {
            npcMode = NPCMode.readyToTalk;
            sign.SetActive(true);
            Debug.Log(gameObject.name + " ready to talk!");            
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.tag == "Player" && npcMode == NPCMode.readyToTalk)
        {
            Debug.Log(gameObject.name + " player too far to talk");
            npcMode = NPCMode.notTalking;
            sign.SetActive(false);            
        }
    }

    private void Talk()
    {
        Debug.Log(gameObject.name + " if anybody need my cue, I'm talking now!");
        //Notify UI that talk happen
        if (OnTalkStart != null)
        {
            OnTalkStart(this);           
        }

        dialogueTrigger.TriggerDialogue(this);
        input.enabled = false;        
    }

    private void DisableInput(DialogueManager d)
    {
        Debug.Log(gameObject.name + " Alright DialogueManager, we disable our Player Input!");
        input.enabled = false;
    }

    private void EnableInput(DialogueManager d)
    {
        Debug.Log(gameObject.name + " read notif from dialogue manager, we can initiate talk again");
        input.enabled = true;
    }

    public void InteractInput(InputAction.CallbackContext con)
    {
        if (con.started && npcMode == NPCMode.readyToTalk)
        {
            Talk();
        }
    }

    public enum NPCMode
    {
        readyToTalk, 
        notTalking, 
        offended
    }

}
