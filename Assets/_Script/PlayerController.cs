using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector2 inputVector = Vector2.zero;
    private Rigidbody2D rb;

    [SerializeField] private float speed = 20;
    
    PlayerInput input;
    Animator anim;
    
    private PlayerMode playerMode = PlayerMode.walking;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        input = GetComponent<PlayerInput>();
        anim = GetComponent<Animator>();
        NpcBehavior.OnTalkStart += DisableMovement; //Observe if an NPC start a talk, disable movement
        DialogueDisplay.OnEndtoNothing += EnableMovement;
    }
    
    public void MoveInput(InputAction.CallbackContext con)
    {
        if (playerMode == PlayerMode.walking) //Drew replaced "if (!isTalking)" with the PlayerMode state machine
        {
            inputVector = con.ReadValue<Vector2>();            
            if (inputVector != Vector2.zero)
            {
                anim.SetFloat("Horizontal", inputVector.x);
                anim.SetFloat("Vertical", inputVector.y);
            }
            anim.SetFloat("Speed", inputVector.sqrMagnitude);
        }
    }

    private void DisableMovement(NpcBehavior npc)
    {
        playerMode = PlayerMode.talking;
        //Debug.Log(gameObject.name + " Read notif from the NPC, CONTROL DISABLED");
        input.enabled = false;
    }

    private void EnableMovement(DialogueDisplay d)
    {
        playerMode = PlayerMode.walking;
        Debug.Log(gameObject.name + "Read notif from the dialogue display, CONTROL ENABLED");
        input.enabled = true;
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + inputVector * speed * Time.deltaTime);
    }

    void Update()
    {
        // Non-physic movement mode
        //    Vector3 currentPos = transform.position;
        //    currentPos += inputVector * speed * Time.deltaTime;
        //    transform.position = currentPos;
    }

    public enum PlayerMode
    {
        walking,
        idle,
        talking
    }
}
