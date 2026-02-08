using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Player;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform patrolPoints;
    private PlayerController player_controller;

    
    private NavMeshAgent agent;
    private int currentPatrolPosition = 0;
    private bool waiting;
    [SerializeField] private float waitTimeAtPoint = 1f;
    [SerializeField] float detectionRange = 5f;
    
    [Header("Contact Damage")]
    [SerializeField] int contactDamage = 10;
    [SerializeField] float damageCooldown = 1f;
    float lastDamageTime = 0;
    
    private Transform player;
    
    private enum State
    {
        Idle,
        Patrol,
        Chase
    }
    
    private State currentState = State.Idle;
    
    private void Awake()
    {
        agent =  GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        if (patrolPoints != null && patrolPoints.childCount > 0)
        {
            currentState = State.Patrol;
            GoToNextPatrolPoint();
        }
        else
        {
            currentState = State.Idle;
        }
    }

    void Update()
    {
        if (player_controller != null)
        {
            if (player_controller.enemigo_muerto)
            {
                Destroy(this.gameObject);
                Destroy(this);
                Debug.Log("enemigo muerto");
            }
        }
        UpdateState();
        UpdateBehaviour();
    }

    private void UpdateBehaviour()
    {
        switch (currentState)
        {
            case State.Patrol:
                PatrolBehaviour();
                break;
            case State.Chase:
                ChaseBehaviour();
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        
        if (Time.time < lastDamageTime + damageCooldown)
            return;
        
        Debug.Log("Ataco jugador");

        player_controller = other.gameObject.GetComponent<PlayerController>();

        player_controller.vida -= 25f;
        player_controller.enemigo_a_tiro = true;
    }

    private void ChangeState(State newState)
    {
        if (newState == currentState) return;
        currentState = newState;
        Debug.Log(currentState);
    }

    private void UpdateState()
    {

        if (currentState == State.Patrol || currentState == State.Chase)
            CheckPlayer();

        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Patrol:
                if (player != null)
                {
                    ChangeState(State.Chase);
                }
                break;
            
            case State.Chase:
                if (player == null)
                {
                    ChangeState(State.Patrol);
                }
                break;
        }
    }


    private void CheckPlayer()
    {
        player = null;
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRange, LayerMask.GetMask("Player"));

        foreach (Collider c in colliders)
        {
            if (c.CompareTag("Player"))
            {
                Vector3 direction = c.transform.position - transform.position;
                if(Physics.Raycast(transform.position, direction, out RaycastHit hit))
                {
                    if (hit.collider == c)
                    {
                        player = hit.transform;
                        Debug.Log("Jugador encontrado!");
                    }
                }
            }
        }
    }

    private void ChaseBehaviour()
    {
        agent.SetDestination(player.position);
    }

    void PatrolBehaviour()
    {
        if (waiting || patrolPoints == null || patrolPoints.childCount == 0)
            return;
        
        if (!agent.pathPending && agent.remainingDistance < 0.5f)
            StartCoroutine(WaitAndGoNextPatrol());
    }
    
    IEnumerator WaitAndGoNextPatrol()
    {
        waiting = true;
        agent.isStopped = true;
        yield return new WaitForSeconds(waitTimeAtPoint);
        waiting = false;
        GoToNextPatrolPoint();
    }

    void GoToNextPatrolPoint()
    {
        agent.isStopped = false;
        agent.SetDestination(patrolPoints.GetChild(currentPatrolPosition).position);
        currentPatrolPosition = (currentPatrolPosition + 1) % patrolPoints.childCount; // recorrer en bucle
    }
}
