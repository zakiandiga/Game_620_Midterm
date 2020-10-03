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

    void Start()
    {
        startButton = start.GetComponent<Button>();
        input = GetComponent<PlayerInput>();
    }
    
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    //Zak add these lines


    public void ExitGame()
    {
        Debug.Log("The game is quitting now.");
        Application.Quit();
    }
}
