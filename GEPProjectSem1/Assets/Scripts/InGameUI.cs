using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


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
    private int m_TotalEnemies = 10;
    //Canvas
    [SerializeField] private GameObject m_InGameCanvas;
    [SerializeField] private GameObject m_WinCanvas;
    [SerializeField] private GameObject m_PauseCanvas;
    //EnemyCount
    public Text m_EnemyCountText;
    private int m_CurrentEnemyCount;
    //MusicSliders
    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider m_MusicSlider;
    [SerializeField] private Slider m_SFXSlider;
    private float m_MusicVolume;
    private float m_SFXVolume;
    //SensitivitySlider
    [SerializeField] private Slider m_SensitivitySlider;
    [SerializeField] private Camera_Chase m_CameraSensitivity;



    private void Start()
    {
        m_White = new Color(255, 255, 255, 255);
        m_MusicVolume = PlayerPrefs.GetFloat("MusicVolumeValue");
        m_SFXVolume = PlayerPrefs.GetFloat("SFXVolumeValue");
        m_CameraSensitivity.m_CamSpeed = PlayerPrefs.GetFloat("SensitivityValue");
        m_MusicSlider.value = m_MusicVolume;
        m_SFXSlider.value = m_SFXVolume;
        m_SensitivitySlider.value = m_CameraSensitivity.m_CamSpeed;
        Time.timeScale = 1;
        
    }

    private void Update()
    {
        PauseGame();
        ImageSwitcher();
        SliderChange();
        Timer();
        GetPoints();
        EnemyCount();
        
        
    }

    public void SliderChange()
    {
        m_HealthBar.value = m_Player.m_CurrentHealth;
    }

    private void Timer()
    {
        if (m_Time > 0)
        {
            m_Time -= Time.deltaTime;
        }
        else
        {
            m_Time = 0;
            Time.timeScale = 0;
            m_InGameCanvas.SetActive(false);
            m_PauseCanvas.SetActive(false);
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

    private void EnemyCount()
    {
        if(m_CurrentEnemyCount >= m_TotalEnemies)
        {
            m_CurrentEnemyCount = m_TotalEnemies;
        }

        m_EnemyCountText.text = m_CurrentEnemyCount.ToString() + " / " + m_TotalEnemies.ToString();

    }

    public void PauseGame()
    {
        if (m_Time > 0)
        {
            if (Input.GetKey(KeyCode.P))
            {
                Time.timeScale = 0;
                m_PauseCanvas.SetActive(true);
                m_InGameCanvas.SetActive(false);

            }
        }
        
    }

    public void BackToGame()
    {
        m_PauseCanvas.SetActive(false);
        m_InGameCanvas.SetActive(true);
        Time.timeScale = 1;
    }

    public void SetMusicVolume(float sliderValue)
    {
        m_AudioMixer.SetFloat("MusicVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("MusicVolumeValue", sliderValue);
    }

    public void SetSFXVolume(float sliderValue)
    {
        m_AudioMixer.SetFloat("SFXVolume", Mathf.Log10(sliderValue) * 20);
        PlayerPrefs.SetFloat("SFXVolumeValue", sliderValue);
    }

    public void SetSensitivity()
    {

        m_CameraSensitivity.m_CamSpeed = m_SensitivitySlider.value;
        PlayerPrefs.SetFloat("SensitivityValue", m_CameraSensitivity.m_CamSpeed);
    }

    private void GetPoints()
    {
        for(int i = 0; i < m_TotalEnemies; i++)
        {
            if(m_Enemies[i].m_CurrentHealth <= 0)
            {
                if(m_Enemies[i].m_IsDead)
                {
                    m_Points += 100;
                    m_CurrentEnemyCount++;

                    m_Enemies[i].m_IsDead = false;
                }
                
            }
        }

        m_PointsText.text = m_Points.ToString();

    }


}
