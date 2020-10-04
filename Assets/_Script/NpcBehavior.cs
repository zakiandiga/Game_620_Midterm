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
    QuestHolder questHolder;

    private NPCMode npcMode = NPCMode.notTalking;

    [SerializeField] int myLevel = 0;
    [SerializeField] int myQuestLevel = 0;
    int dialogueHolderIndex = 0;
    int questHolderIndex = 0;
    bool onQuest = false;  //cheat
    bool isOffended = false;
    bool isQuestFail = false; //Exclusive for Briar

    public static event Action<NpcBehavior> OnTalkStart; //Announce if the NPC innitiate a talk at Talk()
    public static event Action<NpcBehavior> OnQuestCheck;

    void Start()
    {
        dialogueHolderIndex = myLevel;

        sign = transform.Find("Sign").gameObject;

        dialogue = GameObject.Find("Dialogue");
        dialogueDisplay = dialogue.GetComponent<DialogueDisplay>();
        dialogueHolder = GetComponent<DialogueHolder>();
        questHolder = GetComponent<QuestHolder>();
        input = GetComponent<PlayerInput>();

        DialogueDisplay.OnStartConversation += DisableInput; //Observe if the dialogue ends, enable input
        DialogueDisplay.OnEndtoNothing += EnableInput; //Observe if the dialogue starts, disable input
        RoomState.OnRoomLevelUp += NpcLevelup; //Observe if the room level up
        RoomState.OnQuesting += QuestSetup;
        RoomState.OnQuestFail += QuestFailState;
        RoomState.OnQuestSuccess += QuestSuccess;
        DialogueDisplay.OnQuestCheck += CheckQuest;
        DialogueDisplay.OnOffendedCheck += CheckOffended;
    }

    private void QuestFailState (RoomState r)
    {
        myLevel = 5;
        dialogueHolderIndex = 5;
        isQuestFail = true;
        onQuest = false;
    }

    private void QuestSuccess(RoomState r)
    {
        onQuest = false;
    }

    private void QuestSetup(RoomState r)
    {
        questHolderIndex = 0;
        onQuest = true;
    }

    private void CheckQuest(DialogueDisplay d) //Cheat
    {
        Debug.Log("Observed OnQuestCheck");
        string whoTalk = dialogueDisplay.conversation.convoOwner;
        Debug.Log(whoTalk);
        if (this.gameObject.name == whoTalk)
        {
            this.myQuestLevel += 1;
            Debug.Log(this.gameObject.name + " Level Up Quest");
            this.questHolderIndex += 1;
            if(OnQuestCheck != null)
            {
                OnQuestCheck(this);
            }
        }
    }

    private void CheckOffended (DialogueDisplay d) //cheat
    {
        string whoTalk = dialogueDisplay.conversation.convoOwner;
        if (this.gameObject.name == whoTalk)
        {
            this.isOffended = true;
            this.questHolderIndex = 4; //cheat
            this.dialogueHolderIndex = 4;

        }
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

        if (isQuestFail && this.gameObject.name == " Briar_Bartender")
        {
            dialogueDisplay.conversation = dialogueHolder.conversation[5]; //cheat
            dialogueDisplay.StartConversation();
        }
        
        if (isOffended)
        {
            dialogueDisplay.conversation = dialogueHolder.conversation[5]; //cheat
            dialogueDisplay.StartConversation();
        }

        if (!onQuest && !isOffended)
        {
            dialogueDisplay.conversation = dialogueHolder.conversation[dialogueHolderIndex];
            dialogueDisplay.StartConversation();
        }

        if (onQuest && !isOffended)
        {
            dialogueDisplay.conversation = questHolder.questConversation[questHolderIndex];
            dialogueDisplay.StartConversation();
        }
        
        //Debug.Log(dialogueHolderIndex);
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
        Invoke("EnablingInput", 1); //cheat the input enable
        
    }

    private void EnablingInput()
    {
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
        OnMyLevelup();
    }

    private void OnMyLevelup()
    {
        //For now, just add the dialogueHolderIndex
        dialogueHolderIndex += 1;

    }

    public enum NPCMode
    {
        readyToTalk,
        talking,
        notTalking, 
        offended
    }

}
