using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    CharacterController controller;

    [SerializeField] float speed = 10;

    private float verticalSpeed = 0;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    void Update()
    {
        Movement();
        GroundCheck();
    }

    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if(hit.gameObject.CompareTag("Pickup"))
        {
            hit.gameObject.GetComponent<Pickup>().Collect();
        }

        if(hit.gameObject.CompareTag("Win"))
        {
            GameManager.instance.WinGame();
        }
    }

    void GroundCheck()
    {
        RaycastHit hit;
        bool isHit = Physics.Raycast(
            transform.position, 
            Vector3.down, 
            out hit, 
            1.1f, 
            LayerMask.GetMask("Ground")
         );

        if(isHit)
        {
            isGrounded = true;

            switch(hit.collider.tag)
            {
                case "GroundFast":
                    speed = 20;
                    break;

                case "GroundSlow":
                    speed = 5;
                    break;

                default:
                    speed = 10;
                    break;
            }
        }
        else
        {
            isGrounded = false;
        }
    }

    void Movement()
    {
        float x = Input.GetAxis("Horizontal"); // -1 left, 1 right
        float z = Input.GetAxis("Vertical"); // -1 down, 1 up, 0 - none

        Vector3 moveVector = transform.forward * z + transform.right * x;
        controller.Move(moveVector * Time.deltaTime * speed);

        //new
        if (isGrounded)
        {
            verticalSpeed = 0;
        }
        else
        {
            verticalSpeed -= 10 * Time.deltaTime; 
            if (verticalSpeed < -30) verticalSpeed = -30;
        }
        controller.Move(new Vector3(0, verticalSpeed * Time.deltaTime, 0));
    }
}
