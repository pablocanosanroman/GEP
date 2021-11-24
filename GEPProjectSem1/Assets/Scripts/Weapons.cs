using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    private Char_Phys m_PlayerPhys;
    private Axe m_Axe;

    private void Awake()
    {
        m_PlayerPhys = GetComponent<Char_Phys>();
        m_Axe = GetComponent<Axe>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(m_PlayerPhys.m_IsAttacking == true && other.transform.root.CompareTag("Enemy"))
        {
            if(m_PlayerPhys.m_PlayerState == PlayerState.NORMAL_ATTACK)
            {
                other.GetComponent<IDamagable<float>>().Damage(m_Axe.m_AxePowerNormalAttack);
                Debug.Log("Damage Deal");
            }
            
        }
    }
}
