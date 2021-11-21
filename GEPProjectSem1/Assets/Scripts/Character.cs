using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public float m_Health;
    private Char_Phys m_PlayerPhys;

    private void Update()
    {
        if(m_Health <= 0)
        {

            Death();

        }
        
    }

    private void Death()
    {
        m_PlayerPhys.m_PlayerState = PlayerState.DEATH;
    }
}
