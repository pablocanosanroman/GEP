using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickup : MonoBehaviour
{
    public PickUpWeaponType m_WeaponType;
   

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            other.transform.root.GetComponent<Char_Weapon_Controller>().UpdateInteractObject(this);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform.root.CompareTag("Player"))
        {
            other.transform.root.GetComponent<Char_Weapon_Controller>().UpdateInteractObject(null);
        }
    }
}

public enum PickUpWeaponType
{
    NONE = -1,
    AXE = 0,
    MACE = 1,
    SPEAR = 2,
    STAFF = 3,
    HAMMER = 4
}