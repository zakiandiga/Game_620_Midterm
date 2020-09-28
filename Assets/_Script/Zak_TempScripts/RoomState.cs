using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class RoomState : MonoBehaviour
{
    public int roomLevel = 0;

    public static event Action<RoomState> OnRoomLevelUp;

    void Start()
    {
        DialogueDisplay.OnEndLevelUp += LevelUp;
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
}
