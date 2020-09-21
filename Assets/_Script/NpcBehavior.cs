using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class NpcBehavior : MonoBehaviour
{
    [SerializeField] bool canTalk = false;
    GameObject sign;
    //public Dialogue dialogue;
    PlayerInput input;
    DialogueTrigger dialogueTrigger;

    public static event Action<NpcBehavior> OnTalkStart;

    void Start()
    {
        sign = transform.Find("Sign").gameObject;
        input = GetComponent<PlayerInput>();
        DialogueManager.OnEndDialogue += EnableInput;
        DialogueManager.OnStartDialogue += DisableInput;
        dialogueTrigger = GetComponent<DialogueTrigger>();
        
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log(gameObject.name + " ready to talk!");
            sign.SetActive(true);
            canTalk = true;
            //SHOW THE TALK BUTTON... somewhere?
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log(gameObject.name + " player too far to talk");
            sign.SetActive(false);
            canTalk = false;
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
        //FindObjectOfType<DialogueManager>().StartDialogue(dialogue);

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
        if (con.started)
        {
            if (canTalk)
            {
                Talk();
                //Debug.Log(gameObject.name + " calling Talk() function");

            }
        }
    }

}
