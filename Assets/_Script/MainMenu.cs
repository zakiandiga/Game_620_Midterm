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
    public GameObject creditsMenu;
    public GameObject mainMenu;
    public GameObject controlBackButton; 
    public GameObject creditsBackButton;
    public GameObject controlButton;
    public GameObject creditsButton;
    
    public Animator anim;

    private MenuState menuState;
    public enum MenuState
    {
        Main,
        Control,
        Credits
    }

    void Start()
    {
        startButton = start.GetComponent<Button>();
        input = GetComponent<PlayerInput>();
        controlMenu.SetActive(false);
        creditsMenu.SetActive(false);
        menuState = MenuState.Main;
        EventSystem.current.SetSelectedGameObject(start);
    }
    
    public void ControlMenu()
    {
        controlMenu.SetActive(true);
        mainMenu.SetActive(false);
        menuState = MenuState.Control;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(controlBackButton);
    }

    public void CreditsMenu()
    {
        creditsMenu.SetActive(true);
        mainMenu.SetActive(false);
        menuState = MenuState.Credits;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(creditsBackButton);
    }

    public void BackButton()
    {
        mainMenu.SetActive(true);

        EventSystem.current.SetSelectedGameObject(null);
        if(menuState == MenuState.Control)
        {
            controlMenu.SetActive(false);
            EventSystem.current.SetSelectedGameObject(controlButton);
        }
        else if(menuState == MenuState.Credits)
        {
            creditsMenu.SetActive(false);
            EventSystem.current.SetSelectedGameObject(creditsButton);
        }

        menuState = MenuState.Main;

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
