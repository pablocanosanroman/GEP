using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : Char_Phys
{
    private NavMeshPath m_CurrentPath;
    private Rigidbody m_RB;
    private AIState m_State;
    private float m_SightRange = 3f;
    private float m_EnemySpeed;
    private bool m_PlayerInSight;
    private bool m_WeaponPicked;
    [SerializeField] private LayerMask m_PlayerLayerMask;

    private void Awake()
    {
        m_State = AIState.WANDER;
        m_RB = GetComponent<Rigidbody>();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override void FixedUpdate()
    {
        switch (m_State)
        {
            case AIState.WANDER:
                Wander();
                if (FindTarget())
                {
                    m_State = AIState.PICK_WEAPON;
                }
                break;
            case AIState.PICK_WEAPON:
                if (m_WeaponPicked)
                {
                    m_State = AIState.CHASE;
                }
                break;
            case AIState.CHASE:
                Chase();

                break;
            case AIState.ATTACK:
                break;

        }
        base.FixedUpdate();
    }

    private void Wander()
    {
        if(m_CurrentPath != null)
        {
            if (m_CurrentPath.corners.Length < 2)
            {
                NavMesh.CalculatePath(transform.position, NewWanderPoint(), NavMesh.AllAreas, m_CurrentPath);
            }

            Vector3 currentDestination = m_CurrentPath.corners[m_CurrentPath.corners.Length - 1];

            if ((currentDestination - transform.position).magnitude < 0.5f)
            {
                currentDestination = NewWanderPoint();
            }
            NavMesh.CalculatePath(transform.position, currentDestination, NavMesh.AllAreas, m_CurrentPath);

            Vector3 toNextPoint = m_CurrentPath.corners[1] - transform.position;
            
        }
        
    }

    private bool FindTarget()
    {
        m_PlayerInSight = Physics.CheckSphere(transform.position, m_SightRange, m_PlayerLayerMask);
        return m_PlayerInSight;
    }

    private void Chase()
    {
    }

    private void PickWeapon()
    {

    }

    private void Attack()
    {

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
