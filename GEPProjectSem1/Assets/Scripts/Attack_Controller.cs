using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack_Controller : MonoBehaviour
{
    private Attack m_InteractObject;
    private Char_Phys m_PlayerPhysics;
    private const float m_NormalAttackDamage = 1f;
    private Enemy m_Enemy;

    private void Awake()
    {
        m_InteractObject = GetComponent<Attack>();
        m_PlayerPhysics = GetComponent<Char_Phys>();
        m_Enemy = GetComponent<Enemy>();
    }

    private void Update()
    {        
        if(m_InteractObject != null)
        {
            Debug.Log(m_InteractObject);
            if (m_PlayerPhysics.m_PlayerState == PlayerState.NORMALATTACK)
            {
                DoDamage();
            }
        }
    }

    public void DoDamage()
    {
        m_Enemy.m_Health = m_Enemy.m_Health - m_NormalAttackDamage;
    }

    public void UpdateInteractObject(Attack obj)
    {
        
         m_InteractObject = obj;

         if (m_InteractObject == null)
         {
             m_InteractObject = obj;
         }
        
    }
}
