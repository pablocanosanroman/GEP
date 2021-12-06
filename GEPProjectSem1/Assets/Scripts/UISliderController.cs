using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderController : MonoBehaviour
{
    public Slider m_HealthBar { get; private set; }
    [SerializeField] private Character m_Player;

    private void Awake()
    {
        m_HealthBar = GetComponent<Slider>();
    }

    private void Update()
    {
        SliderChange();
    }

    public void SliderChange()
    {
        m_HealthBar.value = m_Player.m_CurrentHealth;
    }
}
