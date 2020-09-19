using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class NpcBehavior : MonoBehaviour
{
    [SerializeField] bool canTalk = false;
    GameObject sign;

    public static event Action<NpcBehavior> OnTalkStart;

    void Start()
    {
        sign = transform.Find("Sign").gameObject;
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            Debug.Log(gameObject.name + " ready to talk!");
            sign.SetActive(true);
            canTalk = true;
            //SHOW THE TALK BUTTON... somewhere?
        }
    }

    void OnTriggerExit2D (Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Debug.Log(gameObject.name + " player too far to talk");
            sign.SetActive(false);
            canTalk = false;
        }
    }

    private void Talk()
    {
        Debug.Log(gameObject.name + " I'm talking to the player");
        Debug.Log(gameObject.name + " Calling my dialog script!!");
        //Notify UI that talk happen
        if(OnTalkStart != null)
        {
            OnTalkStart(this);            
        }
        
    }

    public void InteractInput(InputAction.CallbackContext con)
    {
        if (con.started)
        {
            if (canTalk)
            {
                Talk();
                //Debug.Log(gameObject.name + " calling Talk() function");

            }
        }
    }

}
