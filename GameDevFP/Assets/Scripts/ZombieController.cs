using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieController : MonoBehaviour
{

    CharacterController controller;
    Animator animator;
    Transform player;
    public float moveSpeed = 0.3f;
    public float damageAmount = 5;
    public float sightDist = 10;
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
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("Player")) {
            
        } 
    }
}
