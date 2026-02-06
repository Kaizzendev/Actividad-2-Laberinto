using System;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    [SerializeField] private Transform patrolPoints;

    private NavMeshAgent agent;
    private int currentPatrolPosition = 0;
    
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
        switch (currentState)
        {
            case State.Idle:
                break;
            case State.Patrol:
                  PatrolBehaviour();
                //CONDICION DE VER JUGADOR
                break;
            case State.Chase:
                ChaseBehaviour();
                //CONDICION DE ATACAR
                //CONDICION DE PATRULLAR
                break;
            case State.Attack:
                break;
        }
    }

    private void ChaseBehaviour()
    {
        throw new NotImplementedException();
    }

    void PatrolBehaviour()
    {
        throw new NotImplementedException();
    }

    void GoToNextPatrolPoint()
    {
        agent.SetDestination(patrolPoints.GetChild(currentPatrolPosition).position);
        currentPatrolPosition = (currentPatrolPosition + 1) % patrolPoints.childCount; // recorrer en bucle
    }
}
