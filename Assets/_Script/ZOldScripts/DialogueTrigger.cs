using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DialogueTrigger : MonoBehaviour
{
    public Dialogue dialogue;
    //[SerializeField]GameObject thisNPC;

    void Start()
    {
        //Zak added this line
        //NpcBehavior.OnTalkStart += TriggerDialogue;
        //thisNPC = this.GameObject;
    }

    public void TriggerDialogue(NpcBehavior npc)//Zak added this parameter! check your trigger button!
    {

        //Debug.Log("Notified from NPC" + thisNPC.name); //Zak also added this
        FindObjectOfType<DialogueManager>().StartDialogue(dialogue);
        
    }
}
