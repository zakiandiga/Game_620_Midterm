using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestionManager : MonoBehaviour
{
    public GameObject dialogue;
    public GameObject questionArea; //Send things to UI 
    DialogueDisplay dialogueDisplay; //Need this reference to keep track of DialogueDisplay block number

    private Question currentQuestion; //Question that player will answer

    [SerializeField] private Text factText; //reference to the text field of the question text
    //Anwser choice, we can add more if needed
    [SerializeField] private Text correctAnswer;
    [SerializeField] private Text wrongAnswer;

    public Question questions; //current assigned question

    void Start()
    {
        DialogueDisplay.OnEndtoQuestion += SetCurrentQuestion;
        dialogueDisplay = dialogue.GetComponent<DialogueDisplay>();
    }

    void SetCurrentQuestion(DialogueDisplay d) //change function to get specific question
    {
        Debug.Log("recieved msg from DialogueDisplay");
        int questionBlock = dialogueDisplay.blockNumber;
        currentQuestion = questions; 
        questionArea.SetActive(true);        

        factText.text = currentQuestion.fact; //show the question to the UI 
        correctAnswer.text = currentQuestion.correctAnswer;
        wrongAnswer.text = currentQuestion.wrongAnswer;
    }

    public void UserSelectTrue() //This will lead to branch A
    {
        Debug.Log("To convo0011");
        dialogueDisplay.conversaiton = questions.correctDestinationBlock;
        dialogueDisplay.StartConversation();
        questionArea.SetActive(false);
    }

    public void UserSelectFalse() //This will lead to branch B
    {
        Debug.Log("To convo0012");
        dialogueDisplay.conversaiton = questions.wrongDestinationBlock;
        dialogueDisplay.StartConversation();
        questionArea.SetActive(false);
    }
}
