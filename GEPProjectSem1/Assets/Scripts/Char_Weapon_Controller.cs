using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Char_Weapon_Controller : MonoBehaviour
{
    public PickUpWeaponType m_EquipedWeapon;
    public Transform[] m_Weapons;
    public GameObject[] m_PickupPrefabs;
    public Pickup m_InteractObject;
    private GameObject m_DamageCollider;


    private void Start()
    {
        m_EquipedWeapon = PickUpWeaponType.NONE;
        
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

    public bool EquipWeapon(PickUpWeaponType weaponType)
    {
        
        if (m_EquipedWeapon != weaponType)
        {
            if (m_EquipedWeapon != PickUpWeaponType.NONE)
            {
                StartCoroutine(DropEquipedWeapon((new Vector3(transform.position.x, (transform.position.y + 1), transform.position.z)), m_EquipedWeapon));
                m_Weapons[(int)m_EquipedWeapon].gameObject.SetActive(false);
            }

            m_EquipedWeapon = weaponType;
            m_Weapons[(int)m_EquipedWeapon].gameObject.SetActive(true);
            m_DamageCollider = m_Weapons[(int)m_EquipedWeapon].GetComponentInChildren<Collider>(true).gameObject;
            m_DamageCollider.SetActive(false);

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

    private IEnumerator DropEquipedWeapon(Vector3 location, PickUpWeaponType weapon)
    {

        yield return new WaitForSeconds(0.5f);
        Instantiate<GameObject>(m_PickupPrefabs[(int)weapon], location, Quaternion.identity);


    }

    public void SetDamageTriggerActive(int active)
    {
        m_DamageCollider.gameObject.SetActive((active == 1)? true : false);
    } 
}
