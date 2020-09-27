using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class QuestionManager : MonoBehaviour
{
    //private static List<Question> unansweredQuestions; //Not sure if we still use this

    public GameObject dialogue;
    public GameObject questionArea; //Send things to UI 
    DialogueDisplay dialogueDisplay; //Need this reference to keep track of DialogueDisplay block number

    private Question currentQuestion; //Question that player will answer

    [SerializeField]
    private Text factText; //reference to the text field of the question text

    public Question[] questions; //List of the question blocks

    void Start()
    {
        DialogueDisplay.OnEndtoQuestion += SetCurrentQuestion;
        dialogueDisplay = dialogue.GetComponent<DialogueDisplay>();     
    }



    void SetCurrentQuestion(DialogueDisplay d) //change function to get specific question
    {
        Debug.Log("recieved msg from DialogueDisplay");
        int questionBlock = dialogueDisplay.blockNumber;
        currentQuestion = questions[questionBlock];
        questionArea.SetActive(true);
        Debug.Log(questionBlock);

        factText.text = currentQuestion.fact; //show the question to the UI        
    }

    /*
    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion); //remove answered question from the list, placed here to make sure it's removed after we answer the question

        yield return new WaitForSeconds (timeBetweenQuestion);

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //instead of this, we will change the game state
    }
    */

    public void UserSelectTrue() //This will lead to branch A
    {
        Debug.Log("THE NPC LIKES YOU");
        //Define what's next!!!!!!!!!!!!!!!!!!!!!!!
        //pass the destination dialogue to the dialogue
    }

    public void UserSelectFalse() //This will lead to branch B
    {
        Debug.Log("YOU MAKE THE NPC ANGRY");
        //Define what's next!!!!!!!!!!!!!!!!!!!!!!!!!!!
        //pass the destination dialogue to the dialogue
    }


}
