using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    [SerializeField] private WeaponSO m_WeaponType;
    [SerializeField] private Char_Phys m_Player;

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damageInterface = other.GetComponent<IDamagable>();

        if(damageInterface != null)
        {
            
            
        }
    }
}
