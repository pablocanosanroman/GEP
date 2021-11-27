using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private WeaponSO m_WeaponType;

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damageInterface = other.GetComponent<IDamagable>();
        if (damageInterface != null)
        {

            damageInterface.Damage(m_WeaponType.m_NormalAttack);
            
        }
    }
}
