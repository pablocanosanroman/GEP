using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Weapon_Controller : MonoBehaviour
{
    public WeaponType m_EquipedWeapon;
    public Transform[] m_Weapons;
    public GameObject[] m_PickupPrefabs;
    public Pickup m_InteractObject;



    private void Start()
    {
        m_EquipedWeapon = WeaponType.NONE;
        
    }



    private void Update()
    {

        if (Input.GetKeyDown(KeyCode.E))
        {
            if(m_InteractObject != null)
            {
                if (EquipWeapon(m_InteractObject.m_WeaponType))
                {
                    Destroy(m_InteractObject.gameObject);
                    m_InteractObject = null;

                }
            }

        }
    }

    public bool EquipWeapon(WeaponType weaponType)
    {
        
        if (m_EquipedWeapon != weaponType)
        {
            if (m_EquipedWeapon != WeaponType.NONE)
            {
                StartCoroutine(DropEquipedWeapon((new Vector3(transform.position.x, (transform.position.y + 1), transform.position.z)), m_EquipedWeapon));
                m_Weapons[(int)m_EquipedWeapon].gameObject.SetActive(false);
            }

            m_EquipedWeapon = weaponType;
            m_Weapons[(int)m_EquipedWeapon].gameObject.SetActive(true);

            return true;
        }
        
        
        return false;
        
        

    }

    public void UpdateInteractObject(Pickup obj)
    {
        m_InteractObject = obj;

        if (m_InteractObject == null)
        {
            m_InteractObject = obj;
        }

    }



    private IEnumerator DropEquipedWeapon(Vector3 location, WeaponType weapon)
    {

        yield return new WaitForSeconds(0.5f);
        Instantiate<GameObject>(m_PickupPrefabs[(int)weapon], location, Quaternion.identity);


    }

    
}
