using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 inputVector = Vector2.zero;
    private Rigidbody2D rb;
    [SerializeField] private float speed = 20;
    [SerializeField] private bool isTalking = false;
    PlayerInput input;
    Animator anim;

    //OBSERVE the talk event to change isTalking!!!!!
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        NpcBehavior.OnTalkStart += DisableMovement;
        DialogueManager.OnEndDialogue += EnableMovement;
        
    }

    private void DisableMovement(NpcBehavior npc)
    {
        Debug.Log(gameObject.name + " Read notif from the NPC, CONTROL DISABLED");
        input.enabled = false;
    }

    private void EnableMovement(DialogueManager d)
    {
        Debug.Log(gameObject.name + "Read notif from the dialogue manager, CONTROL ENABLED");
        input.enabled = true;
    }

    public void MoveInput(InputAction.CallbackContext con)
    {
        if (!isTalking)
        {
            inputVector = con.ReadValue<Vector2>();            
            //anim.SetBool("isMoving", true);
            //
            if (inputVector != Vector2.zero)
            {
                anim.SetFloat("Horizontal", inputVector.x);
                anim.SetFloat("Vertical", inputVector.y);
                //anim.SetBool("isMoving", false);
            }
            anim.SetFloat("Speed", inputVector.sqrMagnitude);
        }

    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputVector * speed * Time.deltaTime);
    }

    void Update()
    {
        

        

        //    Vector3 currentPos = transform.position;
        //    currentPos += inputVector * speed * Time.deltaTime;
        //    transform.position = currentPos;
    }
}
