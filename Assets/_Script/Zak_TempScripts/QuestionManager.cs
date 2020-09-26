using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;


public class QuestionManager : MonoBehaviour
{
    

    private static List<Question> unansweredQuestions; //find alternative of list?

    public GameObject dialogue;
    public GameObject questionArea;
    DialogueDisplay dialogueDisplay;

    private Question currentQuestion; //Question that player will answer

    [SerializeField]
    private Text factText;

    [SerializeField]
    private float timeBetweenQuestion = 1f; //we might not need this

    public Question[] questions;

    void Start()
    {
        DialogueDisplay.OnEndConvo += SetCurrentQuestion;
        dialogueDisplay = dialogue.GetComponent<DialogueDisplay>();      
        
    }



    void SetCurrentQuestion(DialogueDisplay d) //change function to get specific question
    {
        Debug.Log("recieved msg from DialogueDisplay");
        int questionBlock = dialogueDisplay.blockNumber;
        currentQuestion = questions[questionBlock];
        questionArea.SetActive(true);

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

    public void UserSelectTrue()
    {
        Debug.Log("THE NPC LIKES YOU");
        //Define what's next!!!!!!!!!!!!!!!!!!!!!!!
    }

    public void UserSelectFalse() //WE DONT NEED THIS SINCE WE ONLY HAVE 1 CORRECT ANSWER
    {
        Debug.Log("YOU MAKE THE NPC ANGRY");
        //Define what's next!!!!!!!!!!!!!!!!!!!!!!!!!!!
        
    }


}
