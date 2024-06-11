using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ZombieController : MonoBehaviour, IEnemy
{
    Animator animator;
    GameObject player;
    NavMeshAgent agent;
    public float moveSpeed = 10f;
    public float dps = 5;
    public float sightDist = 25;
    public float attackDist = 1.0f;
    public float gravity = 9.81f;
    public AudioClip zombieHurt;
    public AudioClip zombieIdle;
    bool playerSpotted = false;

    private float health = 100;

    public static int numEnemies = 0;

    public enum FSMStates {
        Idle, Wander, Chase, Attack, Dead
    }
    public FSMStates currState;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        agent = GetComponent<NavMeshAgent>();
        if (player == null) {
            player = GameObject.FindGameObjectWithTag("Player");
        }
        numEnemies++;
        currState = FSMStates.Idle;
        InvokeRepeating("ZombieIdleSound", 2.0f, 10.0f);
    }

    void Update() {
        switch (currState) {
            case FSMStates.Idle:
                UpdateIdleState();
                break;
            case FSMStates.Wander:
                UpdateIdleState();
                break;
            case FSMStates.Chase:
                UpdateChaseState();
                break;
            case FSMStates.Attack:
                UpdateAttackState();
                break;
            case FSMStates.Dead:
                UpdateDeadState();
                break;
        }
        if (health <= 0) {
            currState = FSMStates.Dead;
        }
    }

    void UpdateIdleState() {
        agent.ResetPath();
        animator.SetTrigger("PlayerUnspotted");
        Vector3 playerVelocity = player.GetComponent<CharacterController>().velocity;
        if (playerVelocity.magnitude > 0.001) {
            Vector3 distance = player.transform.position - transform.position;
            playerSpotted = distance.magnitude < sightDist;
            if (playerSpotted) {
                currState = FSMStates.Chase;
            }
        }
    }

    void UpdateWanderState() {
        
    }

    void UpdateChaseState() {
        agent.ResetPath();
        agent.stoppingDistance = attackDist;
        animator.SetTrigger("PlayerSpotted");
        Vector3 playerVelocity = player.GetComponent<CharacterController>().velocity;
        if (playerVelocity.magnitude > 0.001) {
            Vector3 distance = player.transform.position - transform.position;
            if (distance.magnitude < attackDist) {
                currState = FSMStates.Attack;
            } else if (distance.magnitude > sightDist) {
                currState = FSMStates.Idle;
            } else {
                agent.SetDestination(player.transform.position);
            }
        }
    }

    void UpdateAttackState() {
        agent.ResetPath();
        Vector3 distance = player.transform.position - transform.position;
        if (distance.magnitude > sightDist) {
            currState = FSMStates.Idle;
        } else if (distance.magnitude > attackDist) {
            currState = FSMStates.Chase;
        } else {
            Attack();
        }
    }

    void UpdateDeadState() {
        
    }

    void Attack() {
        var playerHealth = player.GetComponent<PlayerHealth>();
        playerHealth.TakeDamage(dps);
    }

    public void Damage(float damage)
    {
        health -= damage;
        AudioSource.PlayClipAtPoint(zombieHurt, transform.position);
        if (health <= 0)
        {
            currState = FSMStates.Dead;
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
