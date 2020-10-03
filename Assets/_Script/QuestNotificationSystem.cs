using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class QuestNotificationSystem : MonoBehaviour
{

    public GameObject questText;

    void Start()
    {
        questText.SetActive(false);
        DialogueDisplay.QuestStatus += QuestUpdateAlert;
    }

    private void QuestUpdateAlert(bool StatusOfQuest)
    {
        DisplayQuestText();
        Invoke("RemoveQuestText", 5f);
        Debug.Log("I've recieved the change in the quest.");
    }

    private void DisplayQuestText()
    {
        questText.SetActive(true);
    }

    private void RemoveQuestText()
    {
        questText.SetActive(false);
    }
}
