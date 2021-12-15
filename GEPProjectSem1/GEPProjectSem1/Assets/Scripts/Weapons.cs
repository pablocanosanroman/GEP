using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private WeaponSO m_WeaponType;
    [SerializeField] private Char_Phys m_Player;
    

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damageInterface = other.GetComponent<IDamagable>();
        if (damageInterface != null)
        {
            if(m_Player.m_SpecialAttack == true)
            {
                damageInterface.Damage(m_WeaponType.m_SpecialAttack);
                Debug.Log("SpecialAttack dealt");
            }
            else
            {
                Debug.Log("NormalAttack dealt");
                damageInterface.Damage(m_WeaponType.m_NormalAttack);
            }

            if (m_Player.m_BulletActive == true)
            {
                Debug.Log("true");
                Debug.Log("NormalAttack dealt");
                damageInterface.Damage(m_WeaponType.m_NormalAttack);
            }

        }
    }
}
