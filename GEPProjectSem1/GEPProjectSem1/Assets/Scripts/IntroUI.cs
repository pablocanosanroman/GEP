using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;
using UnityEngine.UI;

public class IntroUI : MonoBehaviour
{
    [SerializeField] private GameObject m_IntroCanvas;
    [SerializeField] private GameObject m_OptionsCanvas;
    [SerializeField] private GameObject m_ControlsCanvas;
    [SerializeField] private GameObject m_CreditsCanvas;
    [SerializeField] private AudioMixer m_AudioMixer;
    [SerializeField] private Slider m_MusicSlider;
    [SerializeField] private Slider m_SFXSlider;
    private float m_MusicVolume;
    private float m_SFXVolume;

    private void Start()
    {
        m_MusicVolume = PlayerPrefs.GetFloat("MusicVolumeValue");
        m_SFXVolume = PlayerPrefs.GetFloat("SFXVolumeValue");
        m_MusicSlider.value = m_MusicVolume;
        m_SFXSlider.value = m_SFXVolume;
    }

    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void GetOptions()
    {
        m_IntroCanvas.SetActive(false);
        m_OptionsCanvas.SetActive(true);
        m_ControlsCanvas.SetActive(false);
        m_CreditsCanvas.SetActive(false);
    }

    public void GetControls()
    {
        m_IntroCanvas.SetActive(false);
        m_OptionsCanvas.SetActive(false);
        m_ControlsCanvas.SetActive(true);
        m_CreditsCanvas.SetActive(false);

    }

    public void GetIntro()
    {
        m_IntroCanvas.SetActive(true);
        m_OptionsCanvas.SetActive(false);
        m_ControlsCanvas.SetActive(false);
        m_CreditsCanvas.SetActive(false);

    }

    public void GetCredits()
    {
        m_IntroCanvas.SetActive(false);
        m_OptionsCanvas.SetActive(false);
        m_ControlsCanvas.SetActive(false);
        m_CreditsCanvas.SetActive(true);

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

    public void Exit()
    {
        Application.Quit();
    }
}
