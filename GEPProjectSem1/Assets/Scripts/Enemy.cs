using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable
{
    public float m_CurrentHealth;
    private float m_MaxHealth = 30;
    public bool m_IsDead = false;

    private void Start()
    {
        m_CurrentHealth = m_MaxHealth;
    }


    public void Damage(float damageTaken)
    {
        m_CurrentHealth -= damageTaken;
        if(m_CurrentHealth <= 0)
        {
            Destroy(gameObject);
            m_IsDead = true;
        }
    }
}
