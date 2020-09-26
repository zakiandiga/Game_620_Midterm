using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using UnityEngine.SceneManagement;


public class QuestionManager : MonoBehaviour
{
    public Question[] questions;
    private static List<Question> unansweredQuestions; //find alternative of list?

    private Question currentQuestion; //Question that player will answer

    [SerializeField]
    private Text factText;

    [SerializeField]
    private float timeBetweenQuestion = 1f; //we might not need this

    void Start()
    {
        //change the system of pulling questions to unansweredQuestion
        //Maybe pull the question based on the latest convo blockNumber (get convo.blockNumber, set currentQuestion blockNo)
        if (unansweredQuestions == null || unansweredQuestions.Count == 0) //Check if we should load the question to unansweredQuestions
        {
            unansweredQuestions = questions.ToList<Question>(); // load the question to the unansweredQuestions list
        }

        SetCurrentQuestion(); //later it's not random
        //Debug.Log(currentQuestion.fact + " is " + currentQuestion.isTrue);
    }



    void SetCurrentQuestion() //change function to get specific question
    {
        int randomQuestionIndex = Random.Range(0, unansweredQuestions.Count); //Change from random to the last convo block
        currentQuestion = unansweredQuestions[randomQuestionIndex];

        factText.text = currentQuestion.fact; //show the question to the UI
        
    }

    IEnumerator TransitionToNextQuestion()
    {
        unansweredQuestions.Remove(currentQuestion); //remove answered question from the list, placed here to make sure it's removed after we answer the question

        yield return new WaitForSeconds (timeBetweenQuestion);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); //instead of this, we will change the game state
    }

    public void UserSelectTrue()
    {
        if (currentQuestion.isTrue)
        {
            Debug.Log("CORRECT!"); //later, change this to the follow up (next dialogue block, get quest, npc state changes)

        }
        else
        {
            Debug.Log("WRONG!"); //later, change this to the follow up (next dialogue block, get quest, npc state changes)
        }

        StartCoroutine(TransitionToNextQuestion());
    }

    public void UserSelectFalse() //WE DONT NEED THIS SINCE WE ONLY HAVE 1 CORRECT ANSWER
    {
        {
            if (!currentQuestion.isTrue)
            {
                Debug.Log("Correct!");

            }
            else
            {
                Debug.Log("wrong"); //later, change
            }
        }
        StartCoroutine(TransitionToNextQuestion());
    }


}
