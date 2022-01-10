using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private WeaponSO m_WeaponType;
    private AudioManager m_SoundManager;

    private void Awake()
    {
        m_SoundManager = GameObject.FindObjectOfType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        IDamagable damageInterface = other.GetComponent<IDamagable>();
        if (damageInterface != null && other.transform.root.CompareTag("Player"))
        {
            m_SoundManager.Play("MeleeHitSound");
            damageInterface.Damage(m_WeaponType.m_NormalAttack);

        }
    }
}
