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

    void OnEnable()
    {
        DialogueDisplay.OnEndLevelUp += LevelUp;
        DialogueDisplay.OnQuestStart += QuestStart;
        DialogueDisplay.OnOffendedCheck += CheckOffended;
        DialogueDisplay.OnGoodEnding += GoodEnding;
        DialogueDisplay.OnBadEnding += BadEnding;
        NpcBehavior.OnQuestCheck += QuestCheck;

    }


    void OnDisable()
    {
        DialogueDisplay.OnEndLevelUp -= LevelUp;
        DialogueDisplay.OnQuestStart -= QuestStart;
        DialogueDisplay.OnOffendedCheck -= CheckOffended;
        DialogueDisplay.OnGoodEnding -= GoodEnding;
        DialogueDisplay.OnBadEnding -= BadEnding;
        NpcBehavior.OnQuestCheck -= QuestCheck;
    }

    private void GoodEnding(DialogueDisplay d)
    {
        anim.SetTrigger("ending");
        Invoke("LoadGoodEnding", 1f);
    }

    private void LoadGoodEnding()
    {
        SceneManager.LoadScene(2, LoadSceneMode.Single);
    }

    private void BadEnding(DialogueDisplay d)
    {
        anim.SetTrigger("ending");
        Invoke("LoadBadEnding", 1f);
    }

    private void LoadBadEnding()
    {
        SceneManager.LoadScene(3, LoadSceneMode.Single); //CHECK SCENE NUMBER!
    }


    private void LevelUp(DialogueDisplay d)
    {
        roomLevel += 1;
        //we can add more parameter changes here

        Debug.Log("Room Level = " + roomLevel);
        OnRoomLevelUp?.Invoke(this);
    }

    private void QuestStart(DialogueDisplay d)
    {
        //Ideal path:
        //Get the quest name from the conversation
        //Set the quest requirements
        //Announce

        isQuesting = true;
        OnQuesting?.Invoke(this);        
    }

    private void CheckOffended (DialogueDisplay d)
    {
        //questReq -= 1;
        questPoint -= 1;
        if (questReq <= 0)
        {
            roomLevel += 1;
            //we can add more parameter changes here

            Debug.Log("Room Level = " + roomLevel);
            OnRoomLevelUp?.Invoke(this);            
        }
    }

    private void QuestCheck(NpcBehavior npc)
    {
        questReq -= 1;

        if(questReq <= 0)
        {
            isQuesting = false;

            if(questPoint <= 1)
            {
                roomLevel = 5;
                OnQuestFail?.Invoke(this);
            }

            if (questPoint >= 2)
            {
                roomLevel += 1;
                Debug.Log("Room Level = " + roomLevel);

                OnRoomLevelUp?.Invoke(this);

                OnQuestSuccess?.Invoke(this);
            }            
            //we can add more parameter changes here
            
        }
    }    
}
