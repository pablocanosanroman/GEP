using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : MonoBehaviour
{
    [SerializeField] private Transform m_Target;
    [SerializeField] private Char_Phys m_Player;
    private NavMeshAgent m_NavMeshAgent;
    private AIState m_State;
    private float m_SightRange = 3f;
    private bool m_PlayerInSight;
    [SerializeField] private LayerMask m_PlayerLayerMask;

    private void Awake()
    {
        m_NavMeshAgent = GetComponent<NavMeshAgent>();
        m_State = AIState.WANDER;
    }

    private void Update()
    {
       switch(m_State)
        {
            case AIState.WANDER:
                if(FindTarget())
                {
                    m_State = AIState.CHASE;
                }
                break;
            case AIState.PICK_WEAPON:
                break;
            case AIState.CHASE:
                Chase();
                break;
            case AIState.ATTACK:
                break;

        }
    }

    private bool FindTarget()
    {
        m_PlayerInSight = Physics.CheckSphere(transform.position, m_SightRange, m_PlayerLayerMask);
        return m_PlayerInSight;
    }

    private void Chase()
    {
        m_NavMeshAgent.destination = m_Target.position;
    }

    private Vector3 NewWanderPoint()
    {
        return new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-25f, 25f));
    }

}

public enum AIState
{
    WANDER,
    PICK_WEAPON,
    CHASE,
    ATTACK
}
