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

    public GameObject controlMenu;
    public GameObject mainMenu;
    public GameObject backButton;
    public GameObject controlButton;

    public Animator anim;

    void Start()
    {
        startButton = start.GetComponent<Button>();
        input = GetComponent<PlayerInput>();
    }
    
    public void ControlMenu()
    {
        controlMenu.SetActive(true);
        mainMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(backButton);

    }

    public void BackButton()
    {
        mainMenu.SetActive(true);
        controlMenu.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlButton);
    }

    public void StartGame()
    {
        anim.SetTrigger("toStart");
        Invoke("TransToStart", 1f);
    }

    private void TransToStart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1, LoadSceneMode.Single);
    }

    //Zak add these lines
    public void BackToMain()
    {
        anim.SetTrigger("ending");
        Invoke("TransToMain", 1f);
        
    }

    private void TransToMain()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
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
