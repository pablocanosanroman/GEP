using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour, IDamagable<float>
{
    private float m_CurrentHealth;
    private float m_MaxHealth = 10;

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
        }
    }
}
