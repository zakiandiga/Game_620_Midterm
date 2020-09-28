using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System; //Zak added this line!

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;

    public Animator animator; 

    private Queue<string> sentences;

    //Zak added this line!
    public static event Action<DialogueManager> OnEndDialogue; //Announce if dialogue ends at EndDialogue()
    public static event Action<DialogueManager> OnStartDialogue; //Announce if dialogue start at StartDialogue()

    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //Debug.Log("Starting conversation with " + dialogue.name);

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name; 

        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();

        //Zak added this line
        Debug.Log(gameObject.name + " Guys! Dialogue is starting, everyone please be quiet!");
        if (OnStartDialogue != null)
        {
            
            OnStartDialogue(this);

        }
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    void EndDialogue()
    {
        animator.SetBool("IsOpen", false);

        //Zak added this line
        Debug.Log(gameObject.name + " notify that dialogue ends, anyone need that cue?");
        if (OnEndDialogue != null)
        {
            sentences.Clear(); //Jocelyn added this line
            OnEndDialogue(this);
            
        }
        //Debug.Log("End of conversation.");
    }
}
