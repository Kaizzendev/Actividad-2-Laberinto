using System;
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform patrolPoints;

    private NavMeshAgent agent;
    private int currentPatrolPosition = 0;
    private bool waiting;
    [SerializeField] private float waitTimeAtPoint = 1f;
    private Transform player;
    private enum State
    {
        Idle,
        Patrol,
        Chase,
        Attack,
        Dead
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
        CheckPlayer();
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Patrol:
                  PatrolBehaviour();
                  if (player != null)
                  {
                      currentState = State.Chase;
                  }
                break;
            case State.Chase:
                ChaseBehaviour();
                if (Vector3.Distance(player.position, transform.position) > 10f)
                {
                    currentState = State.Patrol;
                }
                // else if (Vector3.Distance(player.position, transform.position) < 10f)
                // {
                //     currentState = State.Attack;
                // }
                break;
            case State.Attack:
                break;
        }
        Debug.Log(currentState);
    }
    

    private void CheckPlayer()
    {
        
        Collider[] colliders = Physics.OverlapSphere(transform.position, 5f, LayerMask.GetMask("Player"));

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
                        Debug.Log(hit.collider.name);
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
