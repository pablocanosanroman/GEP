using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float m_CurrentHealth;
    private float m_MaxHealth = 30;
    private Char_Phys m_PlayerPhys;
    
    private void Awake()
    {
        m_PlayerPhys = gameObject.GetComponent<Char_Phys>();
    }

    private void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }

    private void Update()
    {
        Death();
        
    }

    private void Death()
    {
        if (m_CurrentHealth <= 0)
        {

            m_PlayerPhys.m_PlayerState = PlayerState.DEATH;
            

        }
        
    }
}
