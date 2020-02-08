using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterControllerTMP controller;
    private float moveSpeed = 40f;

    private float horizontalMove = 0f;
    private bool jump = false;
    private bool crouch = false;


    // Start is called before the first frame update
    void Start()
    {
        moveSpeed= this.GetComponent<PlayerStats>()._Move_Speed;
    }

    // Update is called once per frame
    void Update()
    {
        horizontalMove= Input.GetAxisRaw("Horizontal") *moveSpeed;

        if (Input.GetButtonDown("Jump"))
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            crouch = true;
        }
        else if(Input.GetKeyUp(KeyCode.S))
        {
            crouch = false;
        }

    }

    private void FixedUpdate()
    {
        // move our character
        controller.Move(horizontalMove *Time.fixedDeltaTime, crouch, jump);
        jump = false;
    }
}
