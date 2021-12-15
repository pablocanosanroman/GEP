using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroUI : MonoBehaviour
{
    [SerializeField] private GameObject m_IntroCanvas;
    [SerializeField] private GameObject m_OptionsCanvas;
    [SerializeField] private GameObject m_ControlsCanvas;
    public void StartButton()
    {
        SceneManager.LoadScene(1);
    }

    public void GetOptions()
    {
        m_IntroCanvas.SetActive(false);
        m_OptionsCanvas.SetActive(true);
        m_ControlsCanvas.SetActive(false);
    }

    public void GetControls()
    {
        m_IntroCanvas.SetActive(false);
        m_OptionsCanvas.SetActive(false);
        m_ControlsCanvas.SetActive(true);
    }

    public void GetIntro()
    {
        m_IntroCanvas.SetActive(true);
        m_OptionsCanvas.SetActive(false);
        m_ControlsCanvas.SetActive(false);
    }

    public void Exit()
    {
        Application.Quit();
    }
}
