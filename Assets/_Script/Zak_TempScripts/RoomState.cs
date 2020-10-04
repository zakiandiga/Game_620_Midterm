using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

public class RoomState : MonoBehaviour
{
    public int roomLevel = 0;

    public int questReq = 3;
    public int questPoint = 3;
    public bool isQuesting = false;

    public Animator anim;

    public static event Action<RoomState> OnRoomLevelUp;
    public static event Action<RoomState> OnQuesting;
    public static event Action<RoomState> OnQuestFail;
    public static event Action<RoomState> OnQuestSuccess;

    void Start()
    {
        DialogueDisplay.OnEndLevelUp += LevelUp;
        DialogueDisplay.OnQuestStart += QuestStart;
        DialogueDisplay.OnOffendedCheck += CheckOffended;
        DialogueDisplay.OnGoodEnding += GoodEnding;
        DialogueDisplay.OnBadEnding += BadEnding;
        NpcBehavior.OnQuestCheck += QuestCheck;

    }

    private void GoodEnding(DialogueDisplay d)
    {
        anim.SetTrigger("ending");
        Invoke("LoadGoodEnding", 1f);
    }

    private void LoadGoodEnding()
    {
        SceneManager.LoadScene(2);
    }

    private void BadEnding(DialogueDisplay d)
    {
        anim.SetTrigger("ending");
        Invoke("LoadBadEnding", 1f);
    }

    private void LoadBadEnding()
    {
        SceneManager.LoadScene(3);
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

    private void CheckOffended (DialogueDisplay d)
    {
        questReq -= 1;
        questPoint -= 1;
        if (questReq <= 0)
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

    private void QuestCheck(NpcBehavior npc)
    {
        questReq -= 1;

        if(questReq <= 0)
        {
            isQuesting = false;

            if( questPoint <= 1)
            {
                roomLevel = 5;
                if (OnQuestFail != null) //cheat
                {
                    OnQuestFail(this);
                }
            }

            if (questPoint >= 2)
            {
                roomLevel += 1;
                Debug.Log("Room Level = " + roomLevel);
                if (OnRoomLevelUp != null)
                {
                    OnRoomLevelUp(this);
                }

                if(OnQuestSuccess != null)
                {
                    OnQuestSuccess(this);
                }
            }
            
            //we can add more parameter changes here

            
        }
    }

    void OnDestroy()
    {
        DialogueDisplay.OnEndLevelUp -= LevelUp;
        DialogueDisplay.OnQuestStart -= QuestStart;
        DialogueDisplay.OnOffendedCheck -= CheckOffended;
        DialogueDisplay.OnGoodEnding -= GoodEnding;
        DialogueDisplay.OnBadEnding -= BadEnding;
        NpcBehavior.OnQuestCheck -= QuestCheck;
    }
}
