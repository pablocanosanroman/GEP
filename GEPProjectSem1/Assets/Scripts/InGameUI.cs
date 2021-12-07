using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InGameUI : MonoBehaviour
{
    //Slider
    public Slider m_HealthBar;
    [SerializeField] private Character m_Player;
    //Timer
    public float m_Time;
    public Text m_TimeText;
    private string m_Minutes;
    private string m_Seconds;
    private string m_Miliseconds;
    //ImageSwitch
    [SerializeField] private Char_Weapon_Controller m_CharWeaponController;
    public Image m_WeaponImage;
    private Color m_White;
    public Sprite[] m_WeaponSprites;
    //Points
    [SerializeField] private Enemy[] m_Enemies;
    public int m_Points;
    public Text m_PointsText;
    private float m_EnemiesCount = 10;
    //Canvas
    [SerializeField] private GameObject m_InGameCanvas;
    [SerializeField] private GameObject m_WinCanvas;

    private void Start()
    {
        m_White = new Color(255, 255, 255, 255);
    }

    private void Update()
    {
        ImageSwitcher();
        SliderChange();
        Timer();
        GetPoints();
        
    }

    public void SliderChange()
    {
        m_HealthBar.value = m_Player.m_CurrentHealth;
    }

    private void Timer()
    {
        if (m_Time > 0)
        {
            Time.timeScale = 1;
            m_Time -= Time.deltaTime;
        }
        else
        {
            m_Time = 0;
            Time.timeScale = 0;
            m_InGameCanvas.SetActive(false);
            m_WinCanvas.SetActive(true);
        }

        m_Minutes = (Mathf.FloorToInt(m_Time / 60)).ToString();
        m_Seconds = Mathf.FloorToInt(m_Time % 60).ToString("00");
        m_Miliseconds = ((m_Time * 100) % 100).ToString("00");

        m_TimeText.text = m_Minutes + ":" + m_Seconds + ":" + m_Miliseconds;
    }

    private void ImageSwitcher()
    {
        if (m_CharWeaponController.m_EquipedWeapon == PickUpWeaponType.AXE)
        {
            ImageSwitch(0);
            m_WeaponImage.color = m_White;
        }
        else if (m_CharWeaponController.m_EquipedWeapon == PickUpWeaponType.HAMMER)
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

    private void GetPoints()
    {
        for(int i = 0; i < m_EnemiesCount; i++)
        {
            if(m_Enemies[i].m_CurrentHealth <= 0)
            {
                if(m_Enemies[i].m_IsDead)
                {
                    m_Points += 100;
                    m_Enemies[i].m_IsDead = false;
                }
                
            }
        }

        m_PointsText.text = m_Points.ToString();

    }


}
