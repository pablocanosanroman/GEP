using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AIEnemy : Char_Phys
{
    private Char_Phys m_Target;
    private NavMeshPath m_CurrentPath;
    private Rigidbody m_EnemyRB;
    private Animator m_EnemyAnimator;
    private AIState m_State;
    private float m_SightRange = 5f;
    private float m_RangeOfAttack = 1.5f;
    private float m_EnemySpeed = 0.15f;
    private float m_EnemySpeedOnChase = 1f;
    private bool m_AttackDone = false;
    private float m_MaxEnemySpeed = 0.8f;
    [SerializeField] private GameObject m_DamageCollider;


    private void Awake()
    {
        m_EnemyAnimator = GetComponent<Animator>();
        m_State = AIState.WANDER;
        m_EnemyRB = GetComponent<Rigidbody>();
        m_CurrentPath = new NavMeshPath();
        m_RB = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
         

    }

    protected override void Update()
    {

    }

    protected override void FixedUpdate()
    {
       
        switch (m_State)
        {
            case AIState.WANDER:
                Wander();
                if (FindTarget())
                {
                    m_State = AIState.CHASE;
                }
                break;
            case AIState.CHASE:
                if(!Chase() || m_Target.transform.position.y > 1.7f)
                {
                    m_State = AIState.WANDER;
                }

                if(ShouldAttack())
                {
                    m_State = AIState.ATTACK;
                }
                break;
            case AIState.ATTACK:
                Attack();
                if((m_Target.transform.position - transform.position).magnitude > m_RangeOfAttack)
                {
                    m_State = AIState.CHASE;
                }
                break;

        }

        if (m_EnemyRB.velocity.magnitude > m_MaxEnemySpeed)
        {
            m_EnemyRB.velocity = m_EnemyRB.velocity.normalized * m_MaxEnemySpeed;
        }

        m_EnemyAnimator.SetFloat("RunSpeed", m_EnemyRB.velocity.magnitude / m_MaxEnemySpeed);
    }

    private void Wander()
    {
        if (m_CurrentPath.corners.Length < 2)
        {
           NavMesh.CalculatePath(transform.position, NewWanderPoint(), NavMesh.AllAreas, m_CurrentPath);
        }

        Vector3 currentDestination = m_CurrentPath.corners[m_CurrentPath.corners.Length - 1];
        

        if ((currentDestination - transform.position).magnitude < 0.5f)
        {
            currentDestination = NewWanderPoint();
            m_EnemyAnimator.SetBool("IsRunning", false);
        }
        NavMesh.CalculatePath(transform.position, currentDestination, NavMesh.AllAreas, m_CurrentPath);
        

        if (m_CurrentPath.corners.Length < 2)
        {
            NavMesh.FindClosestEdge(currentDestination, out NavMeshHit _hit, NavMesh.AllAreas);
            currentDestination = _hit.position;
            NavMesh.CalculatePath(transform.position, currentDestination, NavMesh.AllAreas, m_CurrentPath);
        }
        Vector3 toNextPoint = m_CurrentPath.corners[1] - transform.position;
        m_EnemyRB.AddForce(toNextPoint * m_EnemySpeed, ForceMode.Impulse);
        transform.rotation = Quaternion.LookRotation(toNextPoint);
        m_EnemyAnimator.SetBool("IsRunning", true);

    }

    private bool FindTarget()
    {
        GameObject possibleTarget = GameObject.FindGameObjectWithTag("Player");
        Char_Phys possibleTargetPlayer = possibleTarget.GetComponent<Char_Phys>();

        if(possibleTargetPlayer != null)
        {
            float tempDist = (possibleTarget.transform.position - transform.position).magnitude;
            if(tempDist < m_SightRange)
            {
                m_SightRange = tempDist;
                m_Target = possibleTargetPlayer;
                
            }
           
        }
        return (m_Target != null);
    }

    private bool Chase()
    {
        if(m_Target != null)
        {
            Vector3 toTarget = m_Target.transform.position - transform.position;
            Vector3 PathToFollow = new Vector3(toTarget.x, 0, toTarget.z);
            if (toTarget.magnitude - m_SightRange <= m_RB.velocity.magnitude)
            {
                m_EnemyRB.AddForce(PathToFollow * m_EnemySpeedOnChase, ForceMode.Impulse);
                transform.rotation = Quaternion.LookRotation(PathToFollow);
                m_PlayerState = AnimationState.RUN;
                m_EnemyAnimator.SetBool("IsRunning", true);
                return true;
            }
            else
            {
                return false;
            }
        }
        return false;
    }

    private void Attack()
    {
        m_EnemyAnimator.SetTrigger("Attack");
        
    }

    private bool ShouldAttack()
    {
        if(m_Target)
        {
            Vector3 targetDirection = m_Target.transform.position - transform.position;
            return (targetDirection.magnitude < m_RangeOfAttack);
        }
        return false;
    }

    private Vector3 NewWanderPoint()
    {
        NavMesh.SamplePosition(new Vector3(Random.Range(-25f, 25f), 0f, Random.Range(-25f, 25f)), out NavMeshHit _hit, 10f, 1);
        return _hit.position;
    }

    public void SetDamageTriggerActive(int active)
    {
        m_DamageCollider.gameObject.SetActive((active == 1) ? true : false);
    }

    IEnumerator AttackDone()
    {
        yield return new WaitForSeconds(1.12f);
        m_AttackDone = true;
    }
}

public enum AIState
{
    WANDER,
    CHASE,
    ATTACK
}
