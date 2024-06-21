using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed = 4f;
    public float jumpHeight = 3f;
    public float gravity = 9.81f;
    public float airControl = 10f;
    public AudioClip walkingSFX;

    CharacterController controller;
    Vector3 input, moveDirection;
    private bool isMoving;
    public AudioSource walkingSource;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        isMoving = false;
        walkingSource = GetComponent<AudioSource>();
        walkingSource.volume = 0;
    }

    // Update is called once per frame
    void Update()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        input = transform.right * moveHorizontal + transform.forward * moveVertical;
        input.Normalize();



        if((Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
        Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A)))
        {
            if(!isMoving){
                isMoving = true;
                walkingSource.volume = 1;
            }
        }
        else
        {
            isMoving = false;
            walkingSource.volume = 0;
        }
        

        if (controller.isGrounded)
        {
            moveDirection = input;
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(jumpHeight * gravity);
            }
            else
            {
                moveDirection.y = 0f;
            }
        }
        else
        {
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.fixedDeltaTime);
        }

        moveDirection.y -= gravity * Time.unscaledDeltaTime;
        controller.Move(moveDirection * speed * Time.unscaledDeltaTime);
    }
}
