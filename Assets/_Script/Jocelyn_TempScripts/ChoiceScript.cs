using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ChoiceScript : MonoBehaviour
{
    public GameObject TextBox;
    public GameObject Choice1;
    public GameObject Choice2;
    public GameObject Choice3;
    public int ChoiceMade;

    public void ChoiceOption1()
    {
        TextBox.GetComponent<Text>().text = "Nice job, choice 1 rules.";
        ChoiceMade = 1;
    }

    public void ChoiceOption2()
    {
        TextBox.GetComponent<Text>().text = "Choice 2, my dude.";
        ChoiceMade = 2;
    }

    public void ChoiceOption3()
    {
        TextBox.GetComponent<Text>().text = "Wow, choice 3. A bold choice.";
        ChoiceMade = 3;
    }

    void Start()
    {
        
    }


    void Update()
    {
        if (ChoiceMade >= 1)
        {
            Choice1.SetActive(false);
            Choice2.SetActive(false);
            Choice3.SetActive(false);
        }
    }
}
