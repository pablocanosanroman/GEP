using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapons : MonoBehaviour
{
    [SerializeField] private WeaponSO m_WeaponType;
    [SerializeField] private Char_Phys m_Player;
    private AudioManager m_SoundManager;
    private void Awake()
    {
        m_SoundManager = GameObject.FindObjectOfType<AudioManager>();
    }
    private void OnTriggerEnter(Collider other)
    {
        IDamagable damageInterface = other.GetComponent<IDamagable>();
        if (damageInterface != null)
        {
            if(m_Player.m_SpecialAttack == true)
            {
                damageInterface.Damage(m_WeaponType.m_SpecialAttack);
                m_SoundManager.Play("MeleeHitSound");
            }
            else
            {
                m_SoundManager.Play("MeleeHitSound");
                damageInterface.Damage(m_WeaponType.m_NormalAttack);
            }

            if (m_Player.m_BulletActive == true)
            {
                damageInterface.Damage(m_WeaponType.m_NormalAttack);
            }

        }
    }
}
