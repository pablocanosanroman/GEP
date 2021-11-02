using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Weapon_Controller : MonoBehaviour
{
    WeaponType m_EquipedWeapon;
    public Transform[] m_Weapons;
    public GameObject[] m_PickupPrefabs;

    private void Start()
    {
        m_EquipedWeapon = WeaponType.NONE;
    }

    public bool EquipWeapon(WeaponType weaponType)
    {

        if (m_EquipedWeapon != weaponType)
        {
            if (m_EquipedWeapon != WeaponType.NONE)
            {

                StartCoroutine(DropEquipedWeapon((new Vector3(transform.position.x, (transform.position.y + 1), transform.position.z)) - (transform.forward * 2f), m_EquipedWeapon));
                m_Weapons[(int)m_EquipedWeapon].gameObject.SetActive(false);


            }
            m_EquipedWeapon = weaponType;
            m_Weapons[(int)m_EquipedWeapon].gameObject.SetActive(true);
            return true;

        }
        
        
        return false;
    }
    
    private IEnumerator DropEquipedWeapon(Vector3 location, WeaponType weapon)
    {

        yield return new WaitForSeconds(0.5f);
        Instantiate<GameObject>(m_PickupPrefabs[(int)weapon], location, Quaternion.identity);


    }
}
