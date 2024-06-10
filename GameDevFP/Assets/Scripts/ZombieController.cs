using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour, IEnemy
{
    Animator animator;
    Transform player;
    NavMeshAgent agent;
    public float moveSpeed = 10f;
    public float dps = 5;
    public float sightDist = 100;
    public float gravity = 9.81f;
    public AudioClip zombieHurt;
    public AudioClip zombieIdle;
    bool playerSpotted = false;

    private float health = 100;

    public static int numEnemies = 0;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
        numEnemies++;
        InvokeRepeating("ZombieIdleSound", 2.0f, 10.0f);
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

                // Vector3 horzPos = player.position;
                // horzPos.y = transform.position.y;
                // transform.LookAt(horzPos);

                // Vector3 moveVector = distance;
                // moveVector.y = 0;
                // moveVector = moveVector.normalized * moveSpeed;
                // controller.Move(moveVector * Time.deltaTime);
                agent.SetDestination(player.position);
            } else {
                animator.SetTrigger("PlayerUnspotted");
                agent.ResetPath();
            }
        }
        else {
            agent.ResetPath();
        }
    }

    private void OnControllerColliderHit(ControllerColliderHit hit) {
        if (hit.gameObject.CompareTag("Player")) {
            var playerHealth = hit.gameObject.GetComponent<PlayerHealth>();
            playerHealth.TakeDamage(dps);
        } 
    }

    public void Damage(float damage)
    {
        health -= damage;
        AudioSource.PlayClipAtPoint(zombieHurt, transform.position);
        if (health <= 0)
        {
            numEnemies--;
            Debug.Log(numEnemies);
            if(numEnemies == 0) {
                GameObject.FindObjectOfType<LevelManager>().LevelBeat();
            }
            Destroy(gameObject);
        }
    }

    private void ZombieIdleSound() {
        AudioSource.PlayClipAtPoint(zombieIdle, transform.position);
    }
}
