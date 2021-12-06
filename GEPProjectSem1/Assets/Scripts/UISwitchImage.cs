using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISwitchImage : MonoBehaviour
{
    [SerializeField] private Char_Weapon_Controller m_CharWeaponController;
    public Image m_WeaponImage;
    private Color m_White;
    public Sprite[] m_WeaponSprites;

    private void Start()
    {
        m_White = new Color(255, 255, 255, 255);
    }

    private void Update()
    {
        if(m_CharWeaponController.m_EquipedWeapon == PickUpWeaponType.AXE)
        {
            ImageSwitch(0);
            m_WeaponImage.color = m_White;
        }
        else if(m_CharWeaponController.m_EquipedWeapon == PickUpWeaponType.HAMMER)
        {
            ImageSwitch(1);
            m_WeaponImage.color = m_White;
        }
        else if (m_CharWeaponController.m_EquipedWeapon == PickUpWeaponType.MACE)
        {
            ImageSwitch(2);
            m_WeaponImage.color = m_White;
        }
        else if (m_CharWeaponController.m_EquipedWeapon == PickUpWeaponType.SPEAR)
        {
            ImageSwitch(3);
            m_WeaponImage.color = m_White;
        }
        else if (m_CharWeaponController.m_EquipedWeapon == PickUpWeaponType.STAFF)
        {
            ImageSwitch(4);
            m_WeaponImage.color = m_White;
        }
    }
    private void ImageSwitch(int weaponSprite)
    {
        m_WeaponImage.sprite = m_WeaponSprites[weaponSprite];
    }
}
