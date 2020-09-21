using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Observer
{
    //Notification type TalkHappen -- from the notifyingNPC
    //This will tell UI and/or DialogueManager which dialogue should be shown
    //This will tell Player to disable movement


    //Notification type TalkFinished -- from dialogueManager?
    //This will tell Player to enable movement

    //void OnNotify(object obj, NotificationType notif);

}

public enum NotificationType
{
    //DialogueMode,
    //ControlMode,
    //DialogueEnd,

}
