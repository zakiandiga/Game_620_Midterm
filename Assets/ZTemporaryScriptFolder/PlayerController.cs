using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    private Vector3 inputVector = Vector3.zero;
    //private Rigidbody2D rb;
    [SerializeField] private float speed = 20;
    [SerializeField] private bool isTalking = false;
    PlayerInput input;

    //OBSERVE the talk event to change isTalking!!!!!
    void Start()
    {
        input = GetComponent<PlayerInput>();
        NpcBehavior.OnTalkStart += DisableMovement;
        
    }

    private void DisableMovement(NpcBehavior npc)
    {
        Debug.Log("CONTROL DISABLED");
        input.enabled = false;
    }

    public void MoveInput(InputAction.CallbackContext con)
    {
        if (!isTalking)
        {
            inputVector = con.ReadValue<Vector2>();
            //Debug.Log("I'm MOVING, Value = " + inputVector);
        }

    }    

    void Update()
    {
        Vector3 currentPos = transform.position;
        currentPos += inputVector * speed * Time.deltaTime;
        transform.position = currentPos;

    }
}
