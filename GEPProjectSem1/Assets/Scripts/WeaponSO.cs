using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "WeaponData", menuName = "ScriptableObjects/WeaponData", order = 1)]
public class WeaponSO : ScriptableObject
{
    public WeaponType m_Weapon;
    public float m_NormalAttack;
    public float m_SpecialAttack;
}

public enum WeaponType
{
    NONE,
    AXE,
    MACE,
    SPEAR,
    STAFF,
    HAMMER
}
