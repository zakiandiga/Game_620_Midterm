using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomState : MonoBehaviour
{
    public int roomLevel = 0;

    public int questReq = 3;
    public bool isQuesting = false;

    public static event Action<RoomState> OnRoomLevelUp;
    public static event Action<RoomState> OnQuesting;

    void Start()
    {
        DialogueDisplay.OnEndLevelUp += LevelUp;
        DialogueDisplay.OnQuestStart += QuestStart;
        NpcBehavior.OnQuestCheck += QuestCheck;
    }

    private void LevelUp(DialogueDisplay d)
    {
        roomLevel += 1;
        //we can add more parameter changes here

        Debug.Log("Room Level = " + roomLevel);
        if(OnRoomLevelUp != null)
        {
            OnRoomLevelUp(this);
        }
    }

    private void QuestStart(DialogueDisplay d)
    {
        //Ideal path:
        //Get the quest name from the conversation
        //Set the quest requirements
        //Announce

        isQuesting = true;
        if(OnQuesting != null)
        {
            OnQuesting(this);
        }

    }

    private void QuestCheck(NpcBehavior npc)
    {
        questReq -= 1;

        if(questReq <= 0)
        {
            roomLevel += 1;
            //we can add more parameter changes here

            Debug.Log("Room Level = " + roomLevel);
            if (OnRoomLevelUp != null)
            {
                OnRoomLevelUp(this);
            }
        }
    }
}
