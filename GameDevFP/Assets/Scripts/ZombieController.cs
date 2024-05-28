using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{

    CharacterController controller;
    Animator animator;
    Transform player;
    public float moveSpeed = 10f;
    public float dps = 5;
    public float sightDist = 100;
    public float gravity = 9.81f;
    bool playerSpotted = false;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 playerVelocity = player.GetComponent<CharacterController>().velocity;
        if (playerVelocity.magnitude > 0.001) {
            Vector3 distance = player.position - transform.position;
            playerSpotted = distance.magnitude < sightDist;
            if (playerSpotted) {
                animator.SetTrigger("PlayerSpotted");
                Vector3 horzPos = player.position;
                horzPos.y = transform.position.y;
                transform.LookAt(horzPos);

                Vector3 moveVector = distance;
                moveVector.y = 0;
                moveVector = moveVector.normalized * moveSpeed;
                controller.Move(moveVector * Time.deltaTime);
            } else {
                animator.SetTrigger("PlayerUnspotted");
            }
        }
        Vector3 velocity = Vector3.zero;
        // Apply gravity
        if (!controller.isGrounded)
        {
            velocity.y -= gravity * Time.deltaTime;
        }
        else
        {
            velocity.y = 0; // Reset the fall velocity when grounded
        }

        controller.Move(velocity * Time.deltaTime);
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("Player")) {
            var playerHealth = hit.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(dps);
        } 
    }
}
