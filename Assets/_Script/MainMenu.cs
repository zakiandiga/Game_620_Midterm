using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    //Zak add these lines
    public GameObject start;
    Button startButton;

    PlayerInput input;

    public Animator anim;

    void Start()
    {
        startButton = start.GetComponent<Button>();
        input = GetComponent<PlayerInput>();
    }
    
    public void StartGame()
    {
        anim.SetTrigger("toStart");
        Invoke("TransToStart", 1f);
    }

    private void TransToStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Zak add these lines
    public void BackToMain()
    {
        anim.SetTrigger("ending");
        Invoke("TransToMain", 1f);
        
    }

    private void TransToMain()
    {
        SceneManager.LoadScene(0);
    }


    public void ExitGame()
    {
        anim.SetTrigger("ending");
        Invoke("TransToExit", 1f);
    }

    private void TransToExit()
    {
        Debug.Log("The game is quitting now.");
        Application.Quit();
    }
}
